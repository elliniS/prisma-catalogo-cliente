using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using PrismaCatalogo.Validations;
using PrismaCatalogo.Front.Cliente.Handlers;
using PrismaCatalogo.Front.Cliente.Services;
using PrismaCatalogo.Front.Cliente.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();
builder.Services.AddValidatorsFromAssemblyContaining<TamanhoValidator>();

builder.Services.AddHttpContextAccessor();
builder.Services.AddTransient<AuthTokenHandler>();

builder.Services.AddHttpClient("Api", c => c.BaseAddress = new Uri(
    builder.Configuration["ServiceUri:Api"])
).AddHttpMessageHandler<AuthTokenHandler>();

builder.Services.AddScoped<ITamanhoService, TamanhoService>();
builder.Services.AddScoped<ICorService, CorService>();
builder.Services.AddScoped<ICategoriaService, CategoriaService>();
builder.Services.AddScoped<IProdutoService, ProdutoService>();
builder.Services.AddScoped<IProdutoFilhoService, ProdutoFilhoService>();
builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<IAvaliacaoService, AvaliacaoService>();

builder.Services
    .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie((options) =>
    {
        options.LoginPath = "/login";
        options.AccessDeniedPath = "/unauthorized";
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "Auth.Login",
    pattern: "login",
    defaults: new { controller = "Auth", action = "Login" });
app.MapControllerRoute(
    name: "Auth.MinhaConta",
    pattern: "minhaconta",
    defaults: new { controller = "Auth", action = "MinhaConta" });
app.MapControllerRoute(
    name: "Auth.Logout",
    pattern: "logout",
    defaults: new { controller = "Auth", action = "Logout" });
app.MapControllerRoute(
    name: "auth.Unauthorized",
    pattern: "unauthorized",
    defaults: new { controller = "Auth", action = "Unauthorized" });

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
);

app.Run();
