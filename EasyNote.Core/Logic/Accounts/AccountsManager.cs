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
            if (result.Succeeded)
                throw new System.Exception("Cannot add user");

            await _dbContext.Users.AddAsync(userIdentity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<string> GetUserTokenAsync(CredentialsParams credentials)
        {
            var identity = await GetClaimsIdentity(credentials.Email, credentials.Password);
            if (identity == null)
                throw new AuthenticationException("Invalid username or password");

            return await GenerateJwt(identity, _jwtFactory, credentials.Email, _jwtOptions, new JsonSerializerSettings { Formatting = Formatting.Indented });
        }

        private async Task<ClaimsIdentity> GetClaimsIdentity(string userName, string password)
        {
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
                return await Task.FromResult<ClaimsIdentity>(null);

            // get the user to verify
            var userToVerify = _usersManager.Users.FirstOrDefault(u => u.Email == userName);

            if (userToVerify == null) return await Task.FromResult<ClaimsIdentity>(null);

            // check the credentials
            if (await _usersManager.CheckPasswordAsync(userToVerify, password))
            {
                return await Task.FromResult(_jwtFactory.GenerateClaimsIdentity(userName, userToVerify.Id));
            }

            // Credentials are invalid, or account doesn't exist
            return await Task.FromResult<ClaimsIdentity>(null);
        }

        private async Task<string> GenerateJwt(ClaimsIdentity identity, IJwtFactory jwtFactory, string userName, JwtIssuerOptions jwtOptions, JsonSerializerSettings serializerSettings)
        {
            var response = new
            {
                id = identity.Claims.Single(c => c.Type == "id").Value,
                auth_token = await jwtFactory.GenerateEncodedToken(userName, identity),
                expires_in = (int)jwtOptions.ValidFor.TotalSeconds
            };

            return JsonConvert.SerializeObject(response, serializerSettings);
        }
    }
}
