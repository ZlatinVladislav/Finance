using System;
using System.Text;
using Finance.Application.CQRS.Commands.TransactionCommands;
using Finance.Application.CQRS.Querries.Transaction;
using Finance.Application.Interfaces;
using Finance.Application.Services.Photos;
using Finance.Application.Services.Security;
using Finance.Application.Services.Security.Base;
using Finance.Domain.Models;
using Finance.Infrastructure.Data.Context;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace Finance.Config
{
    public static class ServicesExtensions
    {
        public static IServiceCollection AddApplicationLayer(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContextPool<FinanceDBContext>(options =>
                options.UseSqlServer(config.GetConnectionString("Finance")));
            services.AddMvc(option => option.EnableEndpointRouting = false);
            services.AddIoCService();
            services.AddSwaggerGen(c => { c.SwaggerDoc("v2", new OpenApiInfo {Title = "My API", Version = "v2"}); });
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            
            services.AddControllers(options =>
                {
                    var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
                    options.Filters.Add(new AuthorizeFilter(policy));
                })
                .AddNewtonsoftJson()
                .AddFluentValidation(config =>
                {
                    config.RegisterValidatorsFromAssemblyContaining<TransactionCreate>();
                });
            services.AddCors(opt =>
            {
                opt.AddPolicy("CorsPolicy",
                    policy => { policy.AllowAnyMethod().AllowAnyHeader().WithOrigins("http://localhost:3000"); });
            });
            services.AddMediatR(typeof(TransactionList.Handler).Assembly);

            // Setting Identity
            services.AddIdentityCore<AppUser>(opt => { opt.Password.RequireNonAlphanumeric = false; })
                .AddEntityFrameworkStores<FinanceDBContext>()
                .AddSignInManager<SignInManager<AppUser>>();

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["TokenKey"]));

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(opt =>
                {
                    opt.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = key,
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });

            services.AddAuthorization(opt =>
            {
                opt.AddPolicy("TransactionByIdRequirement",
                    policy => { policy.Requirements.Add(new TransactionByIdRequirement()); });
            });
            services.AddTransient<IAuthorizationHandler, IsUserRequirementHandler>();

            services.Configure<CloudinarySettings>(config.GetSection("Cloudinary"));

            return services;
        }
    }
}