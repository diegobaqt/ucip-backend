using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using OfficeOpenXml;
using Ucip.Models;
using Ucip.Services.Interfaces;

namespace Ucip.Services
{
    public class PredictService : IPredictService
    {
        private readonly HttpClient _httpClient;

        public PredictService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<ResultPrediction> GetPrediction(int group, IFormFile file)
        {
            if (file is not { Length: > 0 }) throw new Exception("File is empty");

            if (!Path.GetExtension(file.FileName).Equals(".xlsx", StringComparison.OrdinalIgnoreCase))
                throw new Exception("File extension is not supported");

            using var stream = new MemoryStream();
            await file.CopyToAsync(stream);
            using var package = new ExcelPackage(stream);
            var worksheet = package.Workbook.Worksheets[0];
            var rowCount = worksheet.Dimension.End.Row;

            var dataToPredictList = new List<DataToPredict>();
            for (var row = 2; row <= rowCount; row++)
            {
                var fc = worksheet.Cells[row, 1].Value == null ? "0" : worksheet.Cells[row, 1].Value.ToString();
                var spO2 = worksheet.Cells[row, 2].Value == null ? "0" : worksheet.Cells[row, 2].Value.ToString();
                var fr = worksheet.Cells[row, 3].Value == null ? "0" : worksheet.Cells[row, 3].Value.ToString();
                var tam = worksheet.Cells[row, 4].Value == null ? "0" : worksheet.Cells[row, 4].Value.ToString();
                var condition = worksheet.Cells[row, 5].Value == null ? "0" : worksheet.Cells[row, 5].Value.ToString();

                dataToPredictList.Add(new DataToPredict
                {
                    Fc = float.Parse(fc!),
                    SpO2 = float.Parse(spO2!),
                    Fr = float.Parse(fr!),
                    Tam = float.Parse(tam!),
                    Condition = Convert.ToInt32(condition)
                });
            }

            var data = new PreparedData
            {
                Data = dataToPredictList
            };


            var jsonRequest = JsonConvert.SerializeObject(data);
            var httpContent = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync($"https://m-ucipv2.azurewebsites.net/predict?group={group}",
                httpContent);
            //var response = await _httpClient.PostAsync($"http://localhost:8000/predict?group={group}", httpContent);
            if (!response.IsSuccessStatusCode) throw new Exception();

            var json = response.Content.ReadAsStringAsync().Result;
            var result = JsonConvert.DeserializeObject<ResultPrediction>(json);

            return result;
        }
    }
}