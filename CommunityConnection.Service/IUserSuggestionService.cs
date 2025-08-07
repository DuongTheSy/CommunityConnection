using CommunityConnection.Entities.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunityConnection.Service
{
    public interface IUserSuggestionService
    {
        Task<List<SuggestedUserDto>> GetSuggestedUsersAsync(long currentUserId);
    }
}
