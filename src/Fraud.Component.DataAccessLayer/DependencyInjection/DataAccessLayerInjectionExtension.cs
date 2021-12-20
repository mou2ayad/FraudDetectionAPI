using AutoMapper;
using Fraud.Component.DataAccessLayer.Config;
using Fraud.Component.DataAccessLayer.Contracts;
using Fraud.Component.DataAccessLayer.Profiles;
using Fraud.Component.DataAccessLayer.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

namespace Fraud.Component.DataAccessLayer.DependencyInjection
{
    public static class DataAccessLayerInjectionExtension
    {
        public static IServiceCollection AddDataAccessLayerServices(this IServiceCollection container,
            IConfiguration config)
        {
            container.Configure<StorageConfig>(config.GetSection("StorageConfig"));
            AddDatabase(container, config);
            container.AddSingleton<IPersonsRepository, PersonsRepository>();
            if(config.GetValue<bool>("StorageConfig:EnableCache"))
                container.Decorate<IPersonsRepository, CachePersonsRepositoryDecorator>();
            return container;
        }

        

        public static void AddDataAccessLayerProfiles(this IMapperConfigurationExpression mc)
            => mc.AddProfile(new PersonProfile());
        private static void AddDatabase(IServiceCollection container, IConfiguration config)
        {
            string storageType = config.GetValue<string>("StorageConfig:StorageType");
            switch (storageType)
            {
                case "MongoDb":
                {
                    // inject MongoDb
                    // container.AddSingleton<IMongoClient>(new MongoClient(configuration["ConnectionStrings:FraudDb"]));
                    //container.AddSingleton<IFraudStorage, FraudMongoDbStorage>();
                    break;
                }
                default:
                    container.AddSingleton<IFraudStorage, RunTimeStorage>();
                    break;
            }
        }

    }
}
