using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CommunityConnection.Entities.DTO
{
    public class GoalDto
    {
        public long id { get; set; }
        public long user_id { get; set; }
        public string goal_name { get; set; } = string.Empty;
    }

    public class SuggestionRequestDto
    {
        public long current_user_id { get; set; }
        public List<GoalDto> all_goals { get; set; } = new();
    }
    public class SuggestedUserDto
    {
        [JsonPropertyName("user_id")]
        public long userId { get; set; }

        [JsonPropertyName("score")]
        public double score { get; set; }
    }

    public class SuggestionResponseDto
    {
        [JsonPropertyName("suggested_users")]
        public List<SuggestedUserDto> SuggestedUsers { get; set; } = new();
    }
}
