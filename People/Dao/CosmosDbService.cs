using Microsoft.Azure.Cosmos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using People.Models;

namespace People.Dao
{
    public class CosmosDbService : ICosmosDbService
    {
        private readonly Container container;
        public CosmosDbService(CosmosClient cosmosClient, string databaseName, string containerName)
        {
            container = cosmosClient.GetContainer(databaseName, containerName);
        }
        // type inferred from the item!
        public async Task AddItemAsync(Person person) => await container.CreateItemAsync(person, new PartitionKey(person.Id));
        

        public async Task DeleteItemAsync(Person person) => await container.DeleteItemAsync<Person>(person.Id, new PartitionKey(person.Id));

        public async Task<Person> GetItemAsync(string id)
        {
            try
            {
                return await container.ReadItemAsync<Person>(id, new PartitionKey(id));
            }
            catch (CosmosException e) when (e.StatusCode == System.Net.HttpStatusCode.NotFound)
            {

                return null;
            }
        }

        public async Task<IEnumerable<Person>> GetItemsAsync(string queryString)
        {
            List<Person> people = new List<Person>();
            var query = container.GetItemQueryIterator<Person>(new QueryDefinition(queryString));
            while(query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();
                people.AddRange(response.ToList());
            }
            return people;
        }

        public async Task UpdateItemAsync(Person person) => await container.UpsertItemAsync(person, new PartitionKey(person.Id));
    }
}