using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Primitives;

namespace AnonMessagesWeb;

internal class MyFileProvider : IFileProvider
{
    private readonly IFileProvider _htmlFp;
    private readonly IFileProvider _stylesFp;
    
    public MyFileProvider(IFileProvider htmlFp, IFileProvider stylesFp)
    {
        _htmlFp = htmlFp;
        _stylesFp = stylesFp;
    }
    
    public IDirectoryContents GetDirectoryContents(string subpath)
    {
        Console.WriteLine(subpath);
        return GetFileExtension(subpath) switch
        {
            "html" => _htmlFp.GetDirectoryContents("room.html"),
            "css" => _stylesFp.GetDirectoryContents("styles.css"),
            _ => _stylesFp.GetDirectoryContents(subpath)
        };
    }

    public IFileInfo GetFileInfo(string subpath)
    {
        Console.WriteLine(subpath);
        switch (GetFileExtension(subpath))
        {
            case "html":
                if (subpath.Contains("/index.html"))
                    return _htmlFp.GetFileInfo("index.html");
                if (_htmlFp.GetDirectoryContents(subpath).Exists)
                    return _htmlFp.GetFileInfo(subpath);
                return _htmlFp.GetFileInfo("room.html");
            case "css":
                return _stylesFp.GetFileInfo("styles.css");
            default:
                return _stylesFp.GetFileInfo(subpath);
        }
    }

    public IChangeToken Watch(string filter)
    {
        Console.WriteLine(filter);
        return GetFileExtension(filter) switch
        {
            "html" => _htmlFp.Watch("room.html"),
            "css" => _stylesFp.Watch("styles.css"),
            _ => _stylesFp.Watch(filter)
        };
    }

    private static string GetFileExtension(string filename)
    {
        return filename.Split(".").Last();
    }
}