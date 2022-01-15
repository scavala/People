using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace People.Models
{
    public class Person
    {

		[JsonProperty(PropertyName = "id")]
		public string Id { get; set; }

		[Required]
		[JsonProperty(PropertyName = "firstname")]
		public string FirstName { get; set; }


		[Required]
		[JsonProperty(PropertyName = "lastname")]
		public string LastName { get; set; }

		[Required]
		[DataType(DataType.EmailAddress)]
		[EmailAddress]
		[JsonProperty(PropertyName = "email")]
		public string Email { get; set; }

	

	}
}