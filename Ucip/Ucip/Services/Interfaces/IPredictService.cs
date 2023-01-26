using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Ucip.Models;

namespace Ucip.Services.Interfaces
{
    public interface IPredictService
    {
        Task<ResultPrediction> GetPrediction(int group, IFormFile file);
    }
}
