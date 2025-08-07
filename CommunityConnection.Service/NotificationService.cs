using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FirebaseAdmin;
using FirebaseAdmin.Messaging;
using Google.Apis.Auth.OAuth2;
using System.Collections.Concurrent;
using CommunityConnection.Entities;

namespace CommunityConnection.Service
{
    public class NotificationService : INotificationService
    {
        private static bool _firebaseInitialized = false;
        private static readonly object _lock = new object();
        private static readonly ConcurrentQueue<Task> _scheduledTasks = new();

        public NotificationService()
        {
            if (!_firebaseInitialized)
            {
                lock (_lock)
                {
                    if (!_firebaseInitialized)
                    {
                        FirebaseApp.Create(new AppOptions()
                        {
                            Credential = GoogleCredential.FromFile("service-account-key.json")
                        });
                        _firebaseInitialized = true;
                    }
                }
            }
        }

        public async Task<string> SendNotificationAsync(string deviceToken, string title, string body)
        {
            var message = new Message
            {
                Token = deviceToken,
                Notification = new Notification
                {
                    Title = title,
                    Body = body
                }
            };

            return await FirebaseMessaging.DefaultInstance.SendAsync(message);
        }

        public Task ScheduleNotificationAsync(ScheduledNotificationData data)
        {
            if (!DateTime.TryParse(data.ScheduledTimeIsoString, out DateTime scheduledTime))
                throw new ArgumentException("Invalid ScheduledTimeIsoString format. Please use ISO 8601 format.");

            TimeSpan delay = scheduledTime.ToUniversalTime() - DateTime.UtcNow;

            if (delay.TotalMilliseconds < 0)
                throw new ArgumentException("Scheduled time must be in the future.");

            var task = Task.Run(async () =>
            {
                Console.WriteLine($"Notification scheduled for {scheduledTime.ToLocalTime()}. Waiting for {delay.TotalSeconds} seconds...");
                await Task.Delay(delay);

                var message = new Message
                {
                    Token = data.DeviceToken,
                    Notification = new Notification
                    {
                        Title = data.Title,
                        Body = data.Body
                    },
                    Data = new Dictionary<string, string>
                {
                        { "source", "scheduled_task" },
                        { "scheduled_time", data.ScheduledTimeIsoString }
                }
                };

                string response = await FirebaseMessaging.DefaultInstance.SendAsync(message);
                Console.WriteLine($"Notification sent successfully! Response: {response}");
            });

            _scheduledTasks.Enqueue(task);
            return Task.CompletedTask;
        }
    }

}
