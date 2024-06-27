using BuberDiner.Application;
using BuberDiner.Infrastructure;
using BuberDiner.WebApi;

var builder = WebApplication.CreateBuilder(args);

{
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    builder.Services.AddControllers();


    builder.Services
        .AddInfrastructure(builder.Configuration)
        .AddApplication()
        .AddWebApi();
}

var app = builder.Build();

{
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseCors(c =>
    {
        c.AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });


    app.UseHttpsRedirection();
    app.MapControllers();

    app.Run();
}