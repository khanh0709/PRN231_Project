using AutoMapper;

namespace WebAPI.Business.Mapping
{
    public class MapperConfig
    {
        public static Mapper InitializeAutomapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });
            return new Mapper(config);
        }
    }
}
