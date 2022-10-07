// Link to API: https://zoo-animal-api.herokuapp.com/
// This API randomly generates animal data
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace IntroToAPI_Activity_5
{
    class Animal
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("latin_name")]
        public string Latin { get; set; }
        [JsonProperty("animal_type")]
        public string Type { get; set; }
        [JsonProperty("lifespan")]
        public string Lifespan { get; set; }
        [JsonProperty("diet")]
        public string Diet { get; set; }
        [JsonProperty("habitat")]
        public string Habitat { get; set; }
    }


    class Program
    {
        private static readonly HttpClient client = new HttpClient();
        static async Task Main(string[] args)
        {
            await ProcessRepositories();
        }
        private static async Task ProcessRepositories()
        {
            while (true)
            {
                try
                {
                    Console.WriteLine("Enter Number of Animals to generate randomly. Press Enter Without writing a number to quit the program.");

                    var num = Console.ReadLine();

                    if (string.IsNullOrEmpty(num))
                    {
                        break;
                    }

                    var result = await client.GetAsync("https://zoo-animal-api.herokuapp.com/animals/rand/" + num);
                    var resultRead = await result.Content.ReadAsStringAsync();
                    // Since the API is in a list 
                    // We have to DeserializeObject to list of objects
                    var animal = JsonConvert.DeserializeObject<IEnumerable<Animal>>(resultRead).ToList();

                    for (int i = 0; i < Convert.ToInt32(num); i++)
                    {
                        Console.WriteLine("Animal #" + animal[i].Id);
                        Console.WriteLine("Name: " + animal[i].Name);
                        Console.WriteLine("Latin Name: " + animal[i].Latin);
                        Console.WriteLine("Type: " + animal[i].Type);
                        Console.WriteLine("Lifespan: " + animal[i].Lifespan);
                        Console.WriteLine("Diet: " + animal[i].Diet);
                        Console.WriteLine("Habitat: " + animal[i].Habitat);
                        Console.WriteLine("---");
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine("ERROR. Please enter a valid number!");
                }
            }
        }
    }
}
