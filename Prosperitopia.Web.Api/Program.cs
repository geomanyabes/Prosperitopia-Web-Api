using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Prosperitopia.Application.Configuration;
using Prosperitopia.Application.Middleware;
using Prosperitopia.Domain;
using Prosperitopia.Web.Api.Configuration;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Host.ConfigureServices((context, services) =>
{
    services.AddRouting(options => options.LowercaseUrls = true);

    services.AddDbContext<ProsperitopiaDbContext>(options =>
    {
        //options.UseSqlServer(context.Configuration.GetConnectionString("DefaultConnection"));
        options.UseInMemoryDatabase("Prosperitopia");
    });

    services.RegisterRepositories();
    services.RegisterServices();
    services.AddCors(options =>
    {
        options.AddPolicy("AllowAllOrigins",
            builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            });
    });
    var mapperConfiguration = new MapperConfiguration(cfg =>
    {
        cfg.AddProfile<MappingProfile>();
    });

    var mapper = mapperConfiguration.CreateMapper();
    services.AddSingleton(mapper);
})
.UseSerilog((hostingContext, loggerConfiguration) =>
            loggerConfiguration.ReadFrom.Configuration(hostingContext.Configuration));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseCors("AllowAllOrigins");
}
else
{
    app.UseCors();
}

app.UseMiddleware<ApiKeyAuthMiddleware>();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
