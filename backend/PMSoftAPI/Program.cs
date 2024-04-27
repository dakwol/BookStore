using Microsoft.EntityFrameworkCore;
using DataAccess;
using DataAccess.Repositories;
using Domain.AutoMapper;
using Domain.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using PMSoftAPI;
using PMSoftAPI.AutoMapper;
using PMSoftAPI.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors();

builder.Services.AddAuthorization();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = AuthOptions.Issuer,
            ValidateAudience = true,
            ValidAudience = AuthOptions.Audience,
            ValidateLifetime = true,
            IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
            ValidateIssuerSigningKey = true,
        };
    });

var connection = builder.Configuration.GetConnectionString("DefaultConnection");

//EF
builder.Services.AddDbContext<ApplicationContext>(options => options.UseNpgsql(connection));

//Mapping
builder.Services.AddAutoMapper(typeof(AppMappingPresentationToDomain), typeof(AppMappingDomainToDataAccess));


//Services
builder.Services.AddScoped<AuthorService>();
builder.Services.AddScoped<BookService>();
builder.Services.AddScoped<CountryService>();
builder.Services.AddScoped<GenreService>();
builder.Services.AddScoped<PublisherService>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<TokenService>();
builder.Services.AddScoped<ImageService>();

//Repositories
builder.Services.AddScoped<AuthorRepository>();
builder.Services.AddScoped<BookRepository>();
builder.Services.AddScoped<CountryRepository>();
builder.Services.AddScoped<GenreRepository>();
builder.Services.AddScoped<PublisherRepository>();
builder.Services.AddScoped<UserRepository>();

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhost",
        corsPolicyBuilder => corsPolicyBuilder
            .AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod());
});

//Swagger
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.CustomSchemaIds(type => type.ToString());
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseCors("AllowLocalhost");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();