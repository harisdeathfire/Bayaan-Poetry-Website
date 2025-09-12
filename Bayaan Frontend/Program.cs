using Bayaan_Frontend.Components;
using DAL;
using Frontend.Authentication;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Repo.Iservices;
using Repo.IServices;
using Repo.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddDbContext<AppDbContext>(option =>
    option.UseSqlServer("Data Source=ACER-NITRO5\\SQLEXPRESS;Initial Catalog=UrduDB;Integrated Security=True;Trust Server Certificate=True"));

builder.Services.AddCascadingAuthenticationState();
builder.Services.AddScoped<AuthenticationStateProvider, ServerAuthenticationStateProvider>();

builder.Services.AddAuthentication(AppConstants.AuthScheme)
    .AddCookie(AppConstants.AuthScheme, cookieOptions =>
    {
        cookieOptions.Cookie.Name = AppConstants.AuthScheme;
    })
    .AddGoogle(GoogleDefaults.AuthenticationScheme, googleOptions =>
    {
        googleOptions.ClientId = "Your Client ID";
        googleOptions.ClientSecret = "Your Client Secret";
        googleOptions.AccessDeniedPath = "/access-denied";
        googleOptions.SignInScheme = AppConstants.AuthScheme;

        googleOptions.Events.OnRedirectToAuthorizationEndpoint = context =>
        {
            context.Response.Redirect(context.RedirectUri + "&prompt=select_account");
            return Task.CompletedTask;
        };
    });

builder.Services.AddHttpContextAccessor();


builder.Services.AddScoped<PoetDAL>();
builder.Services.AddScoped<IPoetService, PoetService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<UserDAL>();
builder.Services.AddScoped<IGoogleUserLoginService, GoogleUserLoginService>();
builder.Services.AddScoped<GoogleUserLoginDAL>();
builder.Services.AddScoped<IGhazalService, GhazalService>();
builder.Services.AddScoped<GhazalDAL>();
builder.Services.AddScoped<ITestimonialService, TestimonialService>();
builder.Services.AddScoped<TestimonialDAL>();




var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseAuthentication();
app.UseAuthorization();
app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
