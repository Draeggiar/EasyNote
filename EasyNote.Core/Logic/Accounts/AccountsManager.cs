using AutoMapper;
using EasyNote.Core.Model.Accounts;
using EasyNote.Core.Model.DbEntities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Linq;
using System.Security.Authentication;
using System.Security.Claims;
using System.Threading.Tasks;

namespace EasyNote.Core.Logic.Accounts
{
    public class AccountsManager : IAccountsManager
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly UserManager<UserEntity> _usersManager;
        private readonly IJwtFactory _jwtFactory;
        private readonly JwtIssuerOptions _jwtOptions;

        public AccountsManager(IDbContext dbContext, IMapper mapper, UserManager<UserEntity> usersManager, IJwtFactory jwtFactory, IOptions<JwtIssuerOptions> jwtOptions)
        {
            _dbContext = dbContext as ApplicationDbContext;
            _mapper = mapper;
            _usersManager = usersManager;
            _jwtFactory = jwtFactory;
            _jwtOptions = jwtOptions.Value;
        }

        public async Task CreateUserAsync(CredentialsParams credentialsParams)
        {
            var userIdentity = _mapper.Map<UserEntity>(credentialsParams);

            var result = await _usersManager.CreateAsync(userIdentity, credentialsParams.Password);
            if (!result.Succeeded)
                throw new System.Exception("Cannot add user");

            await _dbContext.Users.AddAsync(userIdentity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<string> GetUserTokenAsync(CredentialsParams credentials)
        {
            var identity = await GetClaimsIdentity(credentials);
            if (identity == null)
                throw new AuthenticationException("Invalid username or password");

            return await GenerateJwt(identity, _jwtFactory, credentials.UserName, _jwtOptions);
        }

        private async Task<ClaimsIdentity> GetClaimsIdentity(CredentialsParams credentials)
        {
            if (string.IsNullOrEmpty(credentials.Email) || string.IsNullOrEmpty(credentials.Password))
                return await Task.FromResult<ClaimsIdentity>(null);

            // get the user to verify
            var userToVerify = _usersManager.Users.FirstOrDefault(u => u.Email == credentials.Email);

            if (userToVerify == null) return await Task.FromResult<ClaimsIdentity>(null);

            // check the credentials
            if (await _usersManager.CheckPasswordAsync(userToVerify, credentials.Password))
            {
                credentials.UserName = userToVerify.UserName;
                return await Task.FromResult(_jwtFactory.GenerateClaimsIdentity(userToVerify.Email, userToVerify.Id));
            }

            // Credentials are invalid, or account doesn't exist
            return await Task.FromResult<ClaimsIdentity>(null);
        }

        private async Task<string> GenerateJwt(ClaimsIdentity identity, IJwtFactory jwtFactory, string userName, JwtIssuerOptions jwtOptions)
        {
            var response = new
            {
                id = identity.Claims.Single(c => c.Type == "id").Value,
                authToken = await jwtFactory.GenerateEncodedToken(userName, identity),
                expiresIn = (int)jwtOptions.ValidFor.TotalSeconds,
                userName
            };

            return JsonConvert.SerializeObject(response, Formatting.None);
        }
    }
}
