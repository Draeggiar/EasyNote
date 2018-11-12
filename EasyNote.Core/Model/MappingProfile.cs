using AutoMapper;
using EasyNote.Core.Model.Accounts;
using EasyNote.Core.Model.DbEntities;
using EasyNote.Core.Model.Files;

namespace EasyNote.Core.Model
{
    class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<FileEntity, File>();
            CreateMap<CredentialsParams, UserEntity>();
        }
    }
}
