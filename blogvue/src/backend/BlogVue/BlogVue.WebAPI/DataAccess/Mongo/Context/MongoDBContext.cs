using MFramework.Services.DataAccess.Mongo.Context;
using Microsoft.Extensions.Configuration;

namespace BlogVue.WebAPI.DataAccess.Mongo.Context
{
    public class MongoDBContext : MongoDBDefaultContext
    {
        public MongoDBContext(IConfiguration configuration) : base(configuration)
        {
        }
    }
}
