using CommunityConnection.Common.Helpers;
using CommunityConnection.Service;
using CommunityConnection.WebApi.Hubs;
using CommunityConnection.WebApi.Middleware;
using Microsoft.EntityFrameworkCore;

namespace CommunityConnection.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<ThesisContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("AZURE_SQL_CONNECTIONSTRING")));
 
            builder.Services.AddSignalR();
            // connect to other devices
            //builder.Services.AddCors(options =>
            //{
            //    options.AddPolicy("AllowAll", policy =>
            //    {
            //        policy.AllowAnyOrigin()
            //              .AllowAnyMethod()
            //              .AllowAnyHeader();
            //    });
            //});


            builder.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(policy =>
                    policy.AllowAnyHeader()
                          .AllowAnyMethod()
                          .SetIsOriginAllowed(_ => true)
                          .AllowCredentials());
            });


            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<UserService>();
            builder.Services.AddScoped<CallGeminiService>();
            builder.Services.AddScoped<JwtHelper>();
            builder.Services.AddScoped<IMessageChannelRepository, MessageChannelRepository>();
            builder.Services.AddScoped<IMessageService, MessageService>();
            builder.Services.AddScoped<ICommunityService, CommunityService>();
            builder.Services.AddScoped<ICommunityRepository, CommunityRepository>();
            builder.Services.AddScoped<IGoalRepository, GoalRepository>();
            builder.Services.AddScoped<IGoalService, GoalService>();
            builder.Services.AddScoped<ISubGoalRepository, SubGoalRepository>();
            builder.Services.AddScoped<ISubGoalService, SubGoalService>();
            builder.Services.AddScoped<IChannelRepository, ChannelRepository>();
            builder.Services.AddScoped<IChannelService, ChannelService>();
            var app = builder.Build();
            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseCors();
            app.UseCors("AllowWebClient");
            app.MapHub<ChatHub>("/chatHub");

            //app.UseCors("AllowAll");
            //// Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseMiddleware<JwtAuthMiddleware>();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
