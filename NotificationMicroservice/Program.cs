using Microsoft.EntityFrameworkCore;
using NotificationEngine.Process.AllHubs;
using NotificationEngine.Process.Data;
using NotificationEngine.Process.DBservices;
using NotificationEngine.Process.Models;
using NotificationMicroservice.Models;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

try
{


    var connectionString = builder.Configuration.GetConnectionString("PolicyContext"); // or your dynamic logic to get the connection string

    builder.Services.AddSignalR(); // Add SignalR


    // Add Hosted Service    
    //builder.Services.AddHostedService<PolicyWorker>();   
    //builder.Services.AddHostedService<PolicyBackgroundService>();
    //builder.Services.AddHostedService<PolicyBackgroundService>();


    // Configure CORS
    builder.Services.AddCors(options => options.AddPolicy("CorsPolicy",
            builder =>
            {
                builder.AllowAnyHeader()
                       .AllowAnyMethod()
                       .SetIsOriginAllowed((host) => true)
                       .AllowCredentials();
            }));

    builder.Services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();



    builder.Services.AddDbContext<NotificationDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("PolicyContext") ?? throw new InvalidOperationException("Connection string 'PolicyContext' not found.")));

    //two new 
    builder.Services.AddScoped<INotificationClient, NotificationClient>();
    builder.Services.AddScoped<IPolicyExpiryService, PolicyExpiryService>();


    var app = builder.Build();

    // Map the SignalR hub
    app.MapHub<PolicyHub>("/hubs/policy");

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseCors("CorsPolicy");
    app.UseRouting();

    app.UseAuthorization();


    app.UseHttpsRedirection();



    //app.MapPost("/sendClientNotification", async (string clientId, IHubContext<PolicyHub> context) =>
    //{
    //    var policies = await PolicyBackgroundService.GetExpiringPoliciesAsync(clientId); // Your database call
    //    foreach (var policy in policies)
    //    {
    //        //await context.Clients.All.SendAsync("ReceiveClientUpdates", policy);
    //        await context.Clients.Group(policy.ClientId)
    //                      .SendAsync("ReceiveClientUpdates", $"Policy '{policy.PolicyName}' is expiring soon!");
    //    }
    //});


    app.MapControllers();

    app.Run();
}

catch (Exception ex)
{
    throw ex;
}
