using CommunityConnection.Infrastructure.Data;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CommunityConnection.Service
{
    public class CallGeminiService
    {
        private static readonly string apiKey = "AIzaSyClXZwYVsswlD0fTR1HhUEXY7C2Un9nnKA";
        private static readonly string endpoint = $"https://generativelanguage.googleapis.com/v1beta/models/gemini-2.0-flash:generateContent?key={apiKey}";

        public async Task<string> EvaluateGoals(string goal)
        {
            using var httpClient = new HttpClient();
            var requestBody = new
            {
                contents = new[]
            {
                new
                {
                    parts = new[]
                    {
                        new
                        {
                            text = $"Đặt lại mục tiêu học {goal} của tôi theo mẫu sau \"Tôi sẽ hoàn thành cái gì thật cụ thể trong bao nhiêu ngày\". " +
                                   $"Trả lời dưới dạng đoạn văn và ít hơn 20 từ"
                        }
                    }
                }
            }
            };

            var json = JsonConvert.SerializeObject(requestBody);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync(endpoint, content);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException($"Gemini API error: {response.StatusCode} - {error}");
            }

            var responseString = await response.Content.ReadAsStringAsync();
            var responseJson = JObject.Parse(responseString);
            var resultText = responseJson["candidates"]?[0]?["content"]?["parts"]?[0]?["text"]?.ToString();

            return resultText ?? "Không có phản hồi từ Gemini.";
        }
    }
}
