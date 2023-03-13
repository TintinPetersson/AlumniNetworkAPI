using AlumniNetworkAPI.Models.Domain;
using AlumniNetworkAPI.Services.EventServices;
using AlumniNetworkAPI.Services.GroupServices;
using AlumniNetworkAPI.Services.PostServices;
using AlumniNetworkAPI.Services.TopicServices;
using AlumniNetworkAPI.Services.UserServices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "AllowAll",
        policy =>
        {
            policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
        });
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(opt =>
    {
        opt.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidIssuer = "https://lemur-3.cloud-iam.com/auth/realms/alumni-network",
            ValidAudience = "account",
            IssuerSigningKeyResolver = (token, securityToken, kid, parameters) =>
            {
                var client = new HttpClient();
                var keyuri = "https://lemur-3.cloud-iam.com/auth/realms/alumni-network/protocol/openid-connect/certs";
                var response = client.GetAsync(keyuri).Result;
                var responseString = response.Content.ReadAsStringAsync().Result;
                var keys = new JsonWebKeySet(responseString);
                return keys.Keys;
            }
        };
    });

builder.Services.AddAutoMapper(typeof(Program).Assembly);
builder.Services.AddScoped(typeof(ITopicService), typeof(TopicService));
builder.Services.AddScoped(typeof(IUserService), typeof(UserService));
//builder.Services.AddScoped(typeof(IGroupService), typeof(GroupService));
builder.Services.AddScoped(typeof(IPostService), typeof(PostService));
//builder.Services.AddScoped(typeof(IEventService), typeof(EventService));

builder.Services.AddDbContext<AlumniNetworkDbContext>(
    opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Alumni Network API",
        Description = "Get information about users, posts, groups and events",
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowAll"); // Cors stay open, but needs to be authorized by keycloaktoken to access endpoints

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
