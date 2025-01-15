using Microsoft.Extensions.FileProviders;

namespace AnonMessagesWeb;

public static class ApplicationExtensions
{
    public static IApplicationBuilder SetFilesProvider(this IApplicationBuilder app)
    {
        var options = new DefaultFilesOptions();
        //options.DefaultFileNames.Clear(); // удаляем имена файлов по умолчанию
        
        options.DefaultFileNames.Add("index.html");
        app.UseDefaultFiles(options);
        
        
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
        return app;
    }
}