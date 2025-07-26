using Azure;
using CommunityConnection.Infrastructure.Data;
using CommunityConnection.WebApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CommunityConnection.Service
{
    public class CallGeminiService
    {
        private static readonly string apiKey = "AIzaSyClXZwYVsswlD0fTR1HhUEXY7C2Un9nnKA";
        private static readonly string endpoint = $"https://generativelanguage.googleapis.com/v1beta/models/gemini-2.0-flash:generateContent?key={apiKey}";

        public async Task<ApiResponse<CheckGoalResponse>> EvaluateGoals(string goal)
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
                                text = $"Mục tiêu học \"{goal}\" đã  đúng theo format 'Tôi sẽ hoàn thành cái gì trong bao nhiêu ngày' chưa\r\n" +
                                $"Nếu chưa đạt hãy nói\r\n\"status\" : \"false\",\r\n\"result\" : đưa ra đề xuất và không cần giải thích\r\nN" +
                                $"ếu đạt rồi hãy nói\r\n\"status\" : \"true\",\r\n\"result\" : \"null\"\r\n" +
                                $"cấm trả lời thừa"
                            }
                        }
                    }
                }
            };

            var json = JsonConvert.SerializeObject(requestBody);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = new HttpResponseMessage();
            try
            {
                response = await httpClient.PostAsync(endpoint, content);
            }
            catch
            {
                return new ApiResponse<CheckGoalResponse>
                {
                    status = "false",
                    message = "Mất kết nối mạng"
                };
            }     

            if (!response.IsSuccessStatusCode)
            {
                JObject error = JObject.Parse(await response.Content.ReadAsStringAsync());
                return new ApiResponse<CheckGoalResponse>
                {
                    status = "false",
                    message = $"Lỗi: {error["error"]?["message"]}"
                };
            }

            var responseString = await response.Content.ReadAsStringAsync();
            var responseJson = JObject.Parse(responseString);
            var resultText = responseJson["candidates"]?[0]?["content"]?["parts"]?[0]?["text"]?.ToString().Replace("```json", "").Replace("```", "").Trim();

            try
            {
                // Gemini trả về JSON dạng text, cần parse thủ công
                var parsed = JObject.Parse(resultText);

                return new ApiResponse<CheckGoalResponse>
                {
                    status = "true",
                    message = "Thành công",
                    data = new CheckGoalResponse
                    {
                        status = parsed["status"]?.ToString() ?? "false",
                        result = parsed["result"]?.ToString()
                    }
                };
            }
            catch (Exception)
            {
                return new ApiResponse<CheckGoalResponse>
                {
                    status = "false",
                    message = "Phản hồi không hợp lệ từ Gemini."
                };
            }
        }
    }
}
