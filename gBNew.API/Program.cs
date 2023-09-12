using gBNew.API.Context;
using gBNew.API.Repositories;
using gBNew.API.Services;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// builder.Services.AddSwaggerGen(c =>
// {
//   c.SwaggerDoc("v1", new OpenApiInfo { Title = "VShop.ProductApi", Version = "v1" });
//   c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
//   {
//     Description = @"'Bearer' [space] seu token",
//     Name = "Authorization",
//     In = ParameterLocation.Header,
//     Type = SecuritySchemeType.ApiKey,
//     Scheme = "Bearer"
//   });

//   c.AddSecurityRequirement(new OpenApiSecurityRequirement
//     {
//          {
//             new OpenApiSecurityScheme
//             {
//                Reference = new OpenApiReference
//                {
//                   Type = ReferenceType.SecurityScheme,
//                   Id = "Bearer"
//                },
//                Scheme = "oauth2",
//                Name = "Bearer",
//                In= ParameterLocation.Header
//             },
//             new List<string> ()
//          }
//     });
// });



var connectionString = builder.Configuration.GetConnectionString("Default");
builder.Services.AddDbContext<AppDbContext>(options => options.UseNpgsql(connectionString));

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IPostRepository, PostRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IPostService, PostService>();

// builder.Services.AddAuthentication("Bearer").AddJwtBearer("Bearer", options => {
//   options.Authority = builder.Configuration["gBNew.IdentityServer:ApplicationUrl"];

//   options.TokenValidationParameters = new TokenValidationParameters
//   {
//     ValidateAudience = false
//   };

// });

// builder.Services.AddAuthorization(options =>
// {
//   options.AddPolicy("ApiScope", policy =>
//   {
//     policy.RequireAuthenticatedUser();
//     policy.RequireClaim("scope", "gBNew");
//   });
// });

// builder.Services.AddCors(options =>
// {
//   options.AddPolicy("ApiScope",
//     builder => builder.AllowAnyOrigin());
// });


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseCors(opt => opt.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
