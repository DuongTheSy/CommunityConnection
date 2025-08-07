using Microsoft.AspNetCore.SignalR;
    using System.Collections.Concurrent;
namespace CommunityConnection.WebApi.Hubs
{

    public record User(int user_id, string Name, string Room);
    public record Message(int userId, string User, string Text);

    public class ChatHub : Hub
    {
        private readonly IMessageService _service;

        public ChatHub(IMessageService service)
        {
            _service = service;
        }
        private static ConcurrentDictionary<string, User> _users = new();

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            if (_users.TryRemove(Context.ConnectionId, out var user))
            {
                await Groups.RemoveFromGroupAsync(Context.ConnectionId, user.Room);
                await Clients.Group(user.Room).SendAsync("UserLeft", user.Name);

                // Gửi lại số người online
                var count = _users.Values.Count(u => u.Room == user.Room);
                await Clients.Group(user.Room).SendAsync("UpdateUserCount", count);
            }
        }

        public async Task JoinRoom(string userName, string roomName, int user_id)
        {
            _users[Context.ConnectionId] = new User(user_id, userName, roomName);
            await Groups.AddToGroupAsync(Context.ConnectionId, roomName);
            await Clients.Group(roomName).SendAsync("UserJoined", userName);

            // Gửi số người online
            var count = _users.Values.Count(u => u.Room == roomName);
            await Clients.Group(roomName).SendAsync("UpdateUserCount", count);
        }

        public async Task SendMessageToRoom(string roomName, string content)
        {
            
            var message = new Message(_users[Context.ConnectionId].user_id ,_users[Context.ConnectionId].Name, content);
            await Clients.Group(roomName).SendAsync("ReceiveMessage", message);

            // Lưu vào csdl
            await _service.SendMessageAsync(long.Parse(roomName), _users[Context.ConnectionId].user_id, content);
        }

    }

}
