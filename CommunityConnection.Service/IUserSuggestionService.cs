using CommunityConnection.Entities.DTO;
using CommunityConnection.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunityConnection.Service
{
    public interface IUserSuggestionService
    {
        Task<List<User>> GetSuggestedUsersAsync(long currentUserId);
        Task<List<User>> GetSuggestedMentorsAsync(long userId, string goal);
    }
}
