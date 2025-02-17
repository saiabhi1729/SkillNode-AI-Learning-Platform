using DotNetEnv;
using Microsoft.EntityFrameworkCore;
using SkillNode_Backend.Data;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using SkillNode_Backend.Models;
using SkillNode_Backend.Services;



var builder = WebApplication.CreateBuilder(args);

// ✅ This binds JwtSettings from appsettings.json
builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));

// ✅ Optionally bind manually for debugging
var jwtSettings = new JwtSettings();
builder.Configuration.GetSection("JwtSettings").Bind(jwtSettings);

Env.Load();
var jwtSecret = Env.GetString("JWT_SECRET");
var key = Encoding.ASCII.GetBytes(jwtSecret);

//  Retrieve JWT settings from appsettings.json



builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Authority = jwtSettings.Authority; // Auth Microservice URL
        options.Audience = jwtSettings.Audience;   // API that should accept the token
        options.RequireHttpsMetadata = false;      // Set to true in production
       
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });
// ✅ Use environment variable for database connection
var connectionString = Env.GetString("DATABASE_URL");

builder.Services.AddDbContext<SkillNodeDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IUserService, UserService>();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();




app.UseAuthorization();
app.MapControllers();
app.Run();

