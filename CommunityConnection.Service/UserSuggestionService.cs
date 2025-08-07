using CommunityConnection.Entities.DTO;
using CommunityConnection.Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CommunityConnection.Service
{
    public class UserSuggestionService : IUserSuggestionService
    {
        private readonly IGoalRepository _repository;
        private readonly HttpClient _httpClient;
        private readonly ICommunityRepository _communityRepository;

        public UserSuggestionService(IGoalRepository repository, HttpClient httpClient, ICommunityRepository communityRepository)
        {
            _repository = repository;
            _httpClient = httpClient;
            _communityRepository = communityRepository;
        }
        public async Task<List<SuggestedUserDto>> GetSuggestedUsersAsync(long userId)
        {
            var goals = await _repository.GetAllGoalsAsync();

            var all_goals = goals.Select(g => new GoalDto
            {
                id = g.Id,
                user_id = g.UserId,
                goal_name = g.GoalName
            }).ToList();

            SuggestionRequestDto suggestionRequestDto = new SuggestionRequestDto
            {
                current_user_id = userId,
                all_goals = all_goals
            };
            var json = JsonSerializer.Serialize(suggestionRequestDto, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("http://localhost:8000/suggest-users", content);
            var responseString = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Python API failed: {response.StatusCode} - {responseString}");
            }

            var result = JsonSerializer.Deserialize<SuggestionResponseDto>(responseString);
            return result?.SuggestedUsers ?? new List<SuggestedUserDto>();

        }
        public async Task<List<SuggestedUserDto>> GetSuggestedMentorsAsync(long userId, string goal)
        {
            var goals = await _repository.GetAllGoalsAsync();

            var member = await _communityRepository.GetCommunitiesWithRole2MembersAsync();

            var all_goals = member.Select(m => new GoalDto
            {
                id = m.CommunityId,
                user_id = m.MemberUserId,
                goal_name = m.CommunityName,
            }).ToList();

            all_goals.Add(new GoalDto
            {
                id = 0,
                user_id = userId,
                goal_name = goal
            });

            SuggestionRequestDto suggestionRequestDto = new SuggestionRequestDto
            {
                current_user_id = userId,
                all_goals = all_goals
            };
            var json = JsonSerializer.Serialize(suggestionRequestDto, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("http://localhost:8000/suggest-users", content);
            var responseString = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Python API failed: {response.StatusCode} - {responseString}");
            }

            var result = JsonSerializer.Deserialize<SuggestionResponseDto>(responseString);
            return result?.SuggestedUsers ?? new List<SuggestedUserDto>();

        }
    }
}
