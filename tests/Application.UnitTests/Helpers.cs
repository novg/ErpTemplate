using AutoMapper;

namespace Application.UnitTests;

public static class Helpers
{
    public static IMapper CreateMapper()
    {
        var mapperConfiguration = new MapperConfiguration(configuration =>
        {
            configuration.AddProfile(new MappingModelsProfile());
        });

        return mapperConfiguration.CreateMapper();
    }
}