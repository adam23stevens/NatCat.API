using System.Text;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using NatCat.API.Service;
using NatCat.Application.Mapping;
using NatCat.Application.QueryHandlers.BookClubs;
using NatCat.DAL;
using NatCat.DAL.Contracts;
using NatCat.DAL.Entity;
using NatCat.DAL.Repository;
using NatCat.DAL.Seed;
using NatCat.Model.Dto.BookClub;
using NatCat.Model.Dto.Genre;
using NatCat.Model.Dto.KeyWord;
using NatCat.Model.Dto.RhymingPattern;
using NatCat.Model.Dto.Story;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var connectionString = builder.Configuration.GetConnectionString("NatCatDbConnectionString");
builder.Services.AddDbContext<NatCatDbContext>(options => {
    options.UseLazyLoadingProxies();
    options.UseSqlServer(connectionString);
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c => {
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "NatCat API",
        Version = "v1"
    });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 1safsfsdfdfd\"",
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement {
        {
            new OpenApiSecurityScheme {
                Reference = new OpenApiReference {
                    Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

builder.Services.AddCors((options) => options.AddPolicy("AllowAll",
    o => o.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod()
    .WithOrigins("http://localhost:3000", "https://natcat-e5a1b.web.app", "https://natcat-e5a1b.firebaseapp.com")));

builder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddDefaultTokenProviders()
      .AddEntityFrameworkStores<NatCatDbContext>();

var apiSettingsSection = builder.Configuration.GetSection("APISettings");
builder.Services.Configure<APISettings>(apiSettingsSection);

var apiSettings = apiSettingsSection.Get<APISettings>();
var key = Encoding.UTF8.GetBytes(apiSettings.SecretKey);

builder.Services.AddAuthentication(opt =>
{
    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = true;
    x.SaveToken = true;
    x.TokenValidationParameters = new()
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidAudience = apiSettings.ValidAudience,
        ValidIssuer = apiSettings.ValidIssuer
    };
});

builder.Services.AddAuthorization();

builder.Services.AddAutoMapper(typeof(GenreMap));
builder.Services.AddAutoMapper(typeof(KeyWordMap));

builder.Services.AddScoped<IDbInitialiser, DbInitialiser>();
builder.Services.AddScoped<IRepository<Genre, GenreDetailDto, GenreListDto>, Repository<Genre, GenreDetailDto, GenreListDto>>();
builder.Services.AddScoped<IRepository<KeyWord, KeyWordDetailDto, KeyWordDetailDto>, Repository<KeyWord, KeyWordDetailDto, KeyWordDetailDto>>();
builder.Services.AddScoped<IRepository<Story, StoryDetailDto, StoryListDto>, Repository<Story, StoryDetailDto, StoryListDto>>();
builder.Services.AddScoped<IRepository<BookClub, BookClubDetailDto, BookClubListDto>, Repository<BookClub, BookClubDetailDto, BookClubListDto>>();
builder.Services.AddScoped<IRepository<StoryType, StoryTypeDetailDto, StoryTypeListDto>, Repository<StoryType, StoryTypeDetailDto, StoryTypeListDto>>();
builder.Services.AddScoped<IRepository<StoryPart, StoryPartDetailDto, StoryPartListDto>, Repository<StoryPart, StoryPartDetailDto, StoryPartListDto>>();
builder.Services.AddScoped<IRepository<BookClubJoinRequest, BookClubJoinRequestDetailDto, BookClubJoinRequestListDto>, Repository<BookClubJoinRequest, BookClubJoinRequestDetailDto, BookClubJoinRequestListDto>>();
builder.Services.AddScoped<IRepository<StoryJoinRequest, StoryJoinRequestDetailDto, StoryJoinRequestListDto>, Repository<StoryJoinRequest, StoryJoinRequestDetailDto, StoryJoinRequestListDto>>();
builder.Services.AddScoped<IRepository<RhymingPattern, RhymingPatternDto, RhymingPatternDto>, Repository<RhymingPattern, RhymingPatternDto, RhymingPatternDto>>();

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.AddMediatR(typeof(BookClubsPagedHandler));

builder.Host.UseSerilog((ctx, lc) => lc.WriteTo.Console().ReadFrom.Configuration(ctx.Configuration));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAll");

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

SeedDatabase();

void SeedDatabase()
{
    using (var scope = app.Services.CreateScope())
    {
        var dbInitialiser = scope.ServiceProvider.GetService<IDbInitialiser>();
        if (dbInitialiser != null)
        {
            dbInitialiser.SeedDatabase();
        }
    }
}

app.Run();
