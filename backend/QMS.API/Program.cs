var builder = WebApplicationBuilder.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddSignalR();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader()
              .AllowCredentials();
    });
});

// Add Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add database
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") 
    ?? "Server=(local)\\SQLEXPRESS;Database=QMS;Trusted_Connection=true;Encrypt=false;";

builder.Services.AddDbContext<QMS.Data.Context.QmsDbContext>(options =>
    options.UseSqlServer(connectionString));

// Add Authentication
var secretKey = builder.Configuration["Jwt:SecretKey"] ?? "your-secret-key-change-in-production-with-minimum-32-characters";
builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", options =>
    {
        options.Authority = null;
        options.RequireHttpsMetadata = false;
        options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(
                System.Text.Encoding.UTF8.GetBytes(secretKey)),
            ValidateIssuer = true,
            ValidIssuer = "QMS",
            ValidateAudience = true,
            ValidAudience = "QMSUsers",
            ValidateLifetime = true
        };
    });

builder.Services.AddAuthorization();

// Register services
builder.Services.AddScoped<QMS.Data.Repositories.IQueueRepository, QMS.Data.Repositories.QueueRepository>();
builder.Services.AddScoped<QMS.Data.Repositories.ITicketRepository, QMS.Data.Repositories.TicketRepository>();
builder.Services.AddScoped<QMS.Data.Repositories.ICounterRepository, QMS.Data.Repositories.CounterRepository>();
builder.Services.AddScoped<QMS.Data.Repositories.IUserRepository, QMS.Data.Repositories.UserRepository>();
builder.Services.AddScoped<QMS.Data.Repositories.IServiceRepository, QMS.Data.Repositories.ServiceRepository>();

builder.Services.AddScoped<QMS.Services.Authentication.IAuthenticationService>(provider =>
    new QMS.Services.Authentication.AuthenticationService(secretKey, 1440));
builder.Services.AddScoped<QMS.Services.Users.IUserService, QMS.Services.Users.UserService>();
builder.Services.AddScoped<QMS.Services.Queues.IQueueService, QMS.Services.Queues.QueueService>();
builder.Services.AddScoped<QMS.Services.Tickets.ITicketService, QMS.Services.Tickets.TicketService>();
builder.Services.AddScoped<QMS.Services.Counters.ICounterService, QMS.Services.Counters.CounterService>();

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowAll");
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapHub<QMS.API.Hubs.QueueHub>("/hubs/queue");

app.Run();
