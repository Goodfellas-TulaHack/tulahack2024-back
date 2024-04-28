using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using TulaHack.API.Controllers;
using TulaHack.Application.Authentification;
using TulaHack.Application.Services;
using TulaHack.DataAccess;
using TulaHack.DataAccess.Repositories;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
        policy =>
        {
            policy.AllowAnyMethod().AllowAnyHeader().SetIsOriginAllowed(origin => true).AllowCredentials();
        });
});

builder.Services.ConfigureApplicationCookie(options => {
    options.Cookie.SameSite = SameSiteMode.None;
});


builder.Services.AddDbContext<TulaHackDbContext>(
    options =>
    {
        options.UseNpgsql("ConnectionString");
    });
builder.Services.AddScoped<UsersRepository>();
builder.Services.AddScoped<UsersService>();
builder.Services.AddScoped<RestaurantsRepository>();
builder.Services.AddScoped<RestaurantsService>();
builder.Services.AddScoped<BookingRepository>();
builder.Services.AddScoped<BookingsService>();
builder.Services.AddScoped<KitchensService>();
builder.Services.AddScoped<KitchensRepository>();
builder.Services.AddScoped<TablesRepository>();
builder.Services.AddScoped<MenusRepository>();
builder.Services.AddScoped<MenusService>();
builder.Services.AddScoped<NotificationsService>();
builder.Services.AddScoped<NotificationsRepository>();
builder.Services.AddScoped<SchemesRepository>();
builder.Services.AddScoped<SchemesService>();
builder.Services.AddScoped<TablesService>();
builder.Services.AddScoped<PasswordHasher>();
builder.Services.AddScoped<FileUploadController>();
builder.Services.AddScoped<JwtProvider>();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
});

var issur = configuration["JwtOptions:Issuer"];
var audience = configuration["JwtOptions:Audience"];

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = false,
            ValidateIssuer = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration["JwtOptions:SecretKey"]))
        };
    });


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseAuthentication();

app.MapControllers();

app.UseCors(MyAllowSpecificOrigins);

app.Run();
