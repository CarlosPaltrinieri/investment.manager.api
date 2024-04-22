using investiment.manager.api.Interfaces;
using investiment.manager.api.Interfaces.Investment;
using investiment.manager.api.Interfaces.User;
using investiment.manager.api.Repositories;
using investiment.manager.api.Repositories.Investment;
using investiment.manager.api.Repositories.User;
using investiment.manager.api.Services.Investment;
using investiment.manager.api.Services.User;
using investiment.manager.api.Utils;
using investment.manager.api.Data.Interfaces.Wallet;
using investment.manager.api.Data.Repositories.Wallet;
using investment.manager.api.Data.Services.Wallet;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(options => options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<DbContext>(
    builder.Configuration.GetSection("DbContext"));
builder.Services.AddSingleton<DbContext>();

builder.Services.AddScoped(typeof(IRepositoryBase<>), typeof(RepositoryBase<>));
builder.Services.AddScoped<IInvestmentRepository, InvestmentRepository>();
builder.Services.AddScoped<IUserInvestorRepository, UserInvestorRepository>();
builder.Services.AddScoped<IWalletRepository, WalletRepository>();

builder.Services.AddScoped<InvestmentService>();
builder.Services.AddScoped<UserInvestorService>();
builder.Services.AddScoped<WalletService>();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Investment API",
        Description = "An API to manage your investments.",
        Contact = new OpenApiContact
        {
            Name = "Carlos Paltrinieri - LinkedIn",
            Url = new Uri("https://www.linkedin.com/in/paltrinieri-/")
        }
    });

    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger(options =>
    {
        options.SerializeAsV2 = true;
    });
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("./swagger/v1/swagger.json", "v1");
        options.RoutePrefix = string.Empty;
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();