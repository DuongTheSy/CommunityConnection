using Azure;
using CommunityConnection.Common;
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
            var text = $"Mục tiêu học \"{goal}\" đã  đúng theo format 'Tôi sẽ hoàn thành cái gì trong bao nhiêu ngày' chưa\r\n" +
                   $"Nếu chưa đạt hãy nói\r\n\"status\" : \"false\",\r\n\"result\" : đưa ra đề xuất hoàn thành cái gì trong bao nhiêu ngày cụ thể và không cần giải thích\r\nN" +
                   $"Nếu đạt rồi hãy nói\r\n\"status\" : \"true\",\r\n\"result\" : \"null\"\r\n" +
                   $"cấm trả lời thừa";
            var (result, errorMessage) = await GetGeminiResponse(text);
            if (errorMessage != null)
            {
                return new ApiResponse<CheckGoalResponse>
                {
                    status = "false",
                    message = errorMessage
                };
            }

            try
            {
                // Gemini trả về JSON dạng text, cần parse thủ công
                var parsed = JObject.Parse(result);

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

        public async Task<ApiResponse<RoadmapResponse>> RoadmapService(string goal)
        {
            var text = $@"Dưới đây là một mục tiêu học tập hoặc phát triển cá nhân:

                        ""{goal}""

                        Hãy phân tích mục tiêu này và tạo thành một kế hoạch hành động chi tiết chia theo từng giai đoạn.

                        Yêu cầu:
                        - Trả về kết quả dưới dạng JSON chuẩn với các trường như sau:

                        {{
                          ""goal"": ""mục tiêu tổng thể"",
                          ""subGoals"": [
                            {{
                              ""title"": ""tiêu đề giai đoạn"",
                              ""expectedDays"": số ngày dự kiến hoàn thành (số nguyên),
                              ""description"": ""mô tả ngắn về giai đoạn"",
                              ""activities"": [
                                ""các hoạt động cụ thể""
                              ]
                            }}
                          ],
                          ""notes"": [
                            ""các ghi chú quan trọng hoặc mẹo hỗ trợ""
                          ]
                        }}

                        - Chỉ trả về JSON. Không thêm lời giải thích, markdown hoặc văn bản khác.
                        ";
            var (result, errorMessage) = await GetGeminiResponse(text);
            if (errorMessage != null)
            {
                return new ApiResponse<RoadmapResponse>
                {
                    status = "false",
                    message = errorMessage
                };
            }

            try
            {
                // Gemini trả về JSON dạng text, cần parse thủ công
                var parsed = JObject.Parse(result);

                return new ApiResponse<RoadmapResponse>
                {
                    status = "true",
                    message = "Thành công",
                    data = new RoadmapResponse
                    {
                        goal = parsed["goal"]?.ToString() ?? string.Empty,
                        subGoals = parsed["subGoals"]?.Select(subGoal => new SubGoal
                        {
                            title = subGoal["title"]?.ToString() ?? string.Empty,
                            expectedDays = subGoal["expectedDays"]?.Value<int>() ?? 0,
                            description = subGoal["description"]?.ToString() ?? string.Empty,
                            activities = subGoal["activities"]?.Select(a => a.ToString()).ToList() ?? new List<string>()
                        }).ToList() ?? new List<SubGoal>(),
                        Notes = parsed["notes"]?.Select(note => note.ToString()).ToList() ?? new List<string>()
                    }
                };
            }
            catch (Exception)
            {
                return new ApiResponse<RoadmapResponse>
                {
                    status = "false",
                    message = "Phản hồi không hợp lệ từ Gemini."
                };
            }
        }
        private async Task<(string? result, string? errorMessage)> GetGeminiResponse(string prompt)
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
                                text = prompt
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
            catch(Exception ex)
            {
                return (null,"Lỗi kết nối: " + ex.Message);
            }

            if (!response.IsSuccessStatusCode)
            {
                JObject error = JObject.Parse(await response.Content.ReadAsStringAsync());
                return (null, $"Lỗi: {error["error"]?["message"]}");
     
            }

            var responseString = await response.Content.ReadAsStringAsync();
            var responseJson = JObject.Parse(responseString);
            var resultText = responseJson["candidates"]?[0]?["content"]?["parts"]?[0]?["text"]?.ToString();
            return (resultText?.Replace("```json", "").Replace("```", "").Trim(), null);

        }
    }
}
