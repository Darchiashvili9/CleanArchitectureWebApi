using CleanArchitecht.Api.Middleware;
using CleanArchitecht.Application;
using CleanArchitecht.Application.Services.Authentication;
using CleanArchitecht.Domain.Entities;
using CleanArchitecht.Infrastructure;
using CleanArchitecht.Infrastructure.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddDbContext<DataContext>(opts =>
    {
        opts.UseSqlServer(connectionString: "Server=(localdb)\\MSSQLLocalDB;Database=QuoteQuiz;MultipleActiveResultSets=True");
    });


    builder.Services.AddIdentity<User, IdentityRole>(o =>
    {
        // configure identity options
        o.User.RequireUniqueEmail = true;
        o.Password.RequireDigit = false;
        o.Password.RequireLowercase = false;
        o.Password.RequireUppercase = false;
        o.Password.RequireNonAlphanumeric = false;
        o.Password.RequiredLength = 1;
    })
        .AddEntityFrameworkStores<DataContext>()
    .AddDefaultTokenProviders();


    builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.SaveToken = true;
        options.RequireHttpsMetadata = false;
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidAudience = builder.Configuration["JWT:ValidAudience"],
            ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Secret"]))
        };
    });



    builder.Services.AddInfrastructure(builder.Configuration).AddApplication();
    builder.Services.AddControllers();

}

var app = builder.Build();
{
    app.UseMiddleware<ErrorHandlingMiddleware>();

    app.UseAuthentication();
    app.UseAuthorization();

    app.UseCors(x => x
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
    app.UseHttpsRedirection();



    app.MapControllers();
    app.Run();
}