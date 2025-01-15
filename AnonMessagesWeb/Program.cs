namespace AnonMessagesWeb;

public static class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.RegisterServices();
        
        builder.Configuration.AddInMemoryCollection();
        var app = builder.Build();
        
        app.SetFilesProvider();

        app.UseRouting();

        app.MapControllers();
        app.UseSwagger()
            .UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1"); });
        var h = new ExceptionHandlerOptions();

        //app.UseExceptionHandler();
        app.Run();
    }

    private static WebApplicationBuilder RegisterServices(this WebApplicationBuilder builder)
    {
        builder.Services.Configure<AnonMessagesConfiguration>(builder.Configuration.GetSection("AnonMessagesConfiguration"));
        builder.Services.AddScoped<IAnonMessageService, AnonMessageService>();
        builder.Services.AddSwaggerGen();
        builder.Services.AddControllers();
        builder.Services.AddHttpClient();
        return builder;
    }
}
