using AutoMapper;
using Prosperitopia.Application.Configuration;
using Prosperitopia.Domain;
using Prosperitopia.Web.Api.Configuration;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Host.ConfigureServices((context, services) =>
{
    //var configuration = context.Configuration;
    services.AddRouting(options => options.LowercaseUrls = true);

    services.AddDbContext<ProsperitopiaDbContext>();
    //commented out. we will use in-memory for now.
    //services.AddDbContext<ProsperitopiaDbContext>(opt =>
    //{
    //    opt.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
    //});

    services.RegisterRepositories();
    services.RegisterServices();

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
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
