using System.Threading.Tasks;
using RestSharp;

namespace CretaceousClient.Models
{
    class ApiHelper
    {
        public static async Task<string> GetAll()
        {
            RestClient client = new RestClient("http://localhost:5000/api");
            RestRequest request = new RestRequest($"animals", Method.GET);
            var response = await client.ExecuteTaskAsync(request);
            return response.Content;
        }
    }
}