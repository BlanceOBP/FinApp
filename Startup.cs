using FinApp.Token;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.IdentityModel.Tokens;

namespace FinApp
{
    public class Startup
    {
        public void Configure(IApplicationBuilder app)
        {

        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options =>
                    {
                        options.RequireHttpsMetadata = false;
                        options.TokenValidationParameters = new TokenValidationParameters
                        {

                            IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),

                            ValidateIssuerSigningKey = true,
                        };
                    });
        }
    }
} 
