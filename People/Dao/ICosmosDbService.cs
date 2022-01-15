using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using People.Models;

namespace People.Dao
{
    public interface ICosmosDbService
    {
        Task<IEnumerable<Person>> GetItemsAsync(string queryString);
        Task<Person> GetItemAsync(string id);
        Task AddItemAsync(Person person);
        Task UpdateItemAsync(Person person);
        Task DeleteItemAsync(Person person);
    }
}
