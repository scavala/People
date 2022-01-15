using Microsoft.Azure.Cosmos;
using System.Threading.Tasks;

namespace People.Dao
{
    public static class CosmosDbServiceProvider
    {
        private const string DatabaseName = "People";
        private const string ContainerName = "People";
        private const string Account = "https://people1.documents.azure.com:443/";
        private const string Key = "hGcPt0NSXmo9HOMYPriYSfpmeB3mqpTdyPwQ0RdliYqM5klC4wJPkhQ0dxTruEwUbkt1DOB6a9l28vtovtd1WQ==";
        private static ICosmosDbService cosmosDbService;

        public static ICosmosDbService CosmosDbService { get => cosmosDbService; }

        public async static Task Init()
        {
            CosmosClient client = new CosmosClient(Account, Key);
            cosmosDbService = new CosmosDbService(client, DatabaseName, ContainerName);
            DatabaseResponse database = await client.CreateDatabaseIfNotExistsAsync(DatabaseName);
            await database.Database.CreateContainerIfNotExistsAsync(ContainerName, "/id");
        }
    }
}