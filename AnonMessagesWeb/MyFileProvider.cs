using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Primitives;

namespace AnonMessagesWeb;

class MyFileProvider : IFileProvider
{
    private readonly IFileProvider _hfp;
    private readonly IFileProvider _sfp;
    
    public MyFileProvider(IFileProvider hfp, IFileProvider sfp)
    {
        _hfp = hfp;
        _sfp = sfp;
    }
    
    public IDirectoryContents GetDirectoryContents(string subpath)
    {
        Console.WriteLine(subpath);

        if (subpath.Split(".").Last() == "html")
        {
            return _hfp.GetDirectoryContents("room.html");
        }
        if (subpath.Split(".").Last() == "css")
            return _sfp.GetDirectoryContents("styles.css");
        return _sfp.GetDirectoryContents(subpath);
    }

    public IFileInfo GetFileInfo(string subpath)
    {
        Console.WriteLine(subpath);
        if (subpath.Split(".").Last() == "html")
        {
            if (subpath.Contains("/index.html"))
                return _hfp.GetFileInfo("index.html");
            if (_hfp.GetDirectoryContents(subpath).Exists)
                return _hfp.GetFileInfo(subpath);
            return _hfp.GetFileInfo("room.html");
        }
        if (subpath.Split(".").Last() == "css")
            return _sfp.GetFileInfo("styles.css");
        return _sfp.GetFileInfo(subpath);
    }

    public IChangeToken Watch(string filter)
    {
        Console.WriteLine(filter);

        if (filter.Split(".").Last() == "html")
            return _hfp.Watch("room.html");
        if (filter.Split(".").Last() == "css")
            return _sfp.Watch("styles.css");
        return _sfp.Watch(filter);
    }
}