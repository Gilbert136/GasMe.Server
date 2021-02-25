using System.Linq;
using Microsoft.AspNetCore.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Net.Http;

namespace GasMe.Service.Extentions
{
    public static class HttpContentExtensions
{
    public static async Task<T> ReadAsAsync<T>(this HttpContent content) =>
        await JsonSerializer.DeserializeAsync<T>(await content.ReadAsStreamAsync());
    }   
}