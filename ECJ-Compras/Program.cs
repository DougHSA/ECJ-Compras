using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Dominio.Context;
using ECJ_Compras.Services.Interfaces;
using ECJ_Compras.Services;

namespace ECJ_Compras
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorPages();
            ConfigurarServices(builder.Services,builder.Configuration);

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseAuthentication();
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Auth}/{action=Index}");
            });


            app.MapRazorPages();

            app.Run();
        }

        private static void ConfigurarServices(IServiceCollection services, ConfigurationManager config)
        {
            services.AddScoped<IEmailService,EmailService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<ITransacaoService, TransacaoService>();
            services.AddScoped<IDoacaoService, DoacaoService>();
            services.AddScoped<IConsultaService, ConsultaService>();
            services.AddScoped<IVendaService, VendaService>();


            services.AddControllersWithViews();
            services.AddAuthentication(x => { 
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme; 
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme; 
            })
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JwtSettings:Key"]))
                };
                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        if (context.Request.Cookies.TryGetValue("jwtToken", out var jwtToken))
                        {
                            context.Token = jwtToken;
                        }
                        return Task.CompletedTask;
                    }
                };
            });

            services.AddDbContext<EjcContext>(options =>
                options.UseSqlServer(config["ConnectionStrings:Local"]));
        }
    }
}