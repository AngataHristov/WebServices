namespace SourceControlSystem.Api
{
    using System.Reflection;

    using Infrastructure.Mapping;

    public static class AutoMappingConfig
    {
        public static void Config()
        {
            var autoMapperConfig = new AutoMapperConfig();
            autoMapperConfig.Execute(Assembly.GetExecutingAssembly());
        }
    }
}

