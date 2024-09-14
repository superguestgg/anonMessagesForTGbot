using AnonMessagesWeb;
using Microsoft.AspNetCore.StaticFiles.Infrastructure;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Primitives;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<IAnonMessageService, AnonMessageService>();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

var app = builder.Build();
DefaultFilesOptions options = new DefaultFilesOptions();
//options.DefaultFileNames.Clear(); // удаляем имена файлов по умолчанию
options.DefaultFileNames.Add("index.html"); // добавляем новое имя файла
app.UseDefaultFiles(options); // установка параметров
var mfp = new MyFileProvider(
    new PhysicalFileProvider(
    Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot")),
    new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"static"))
    );
app.UseStaticFiles(new StaticFileOptions()
{
    FileProvider = mfp,
    RequestPath = new PathString("")
});

app.UseRouting();

app.MapControllers();
app.UseSwagger()
    .UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    });
var h = new ExceptionHandlerOptions();

//app.UseExceptionHandler();
app.Run();
