using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using UndefinedNetworking.GameEngine.Resources;
using UndefinedServer.Exceptions;
using Utils;

namespace UndefinedServer.GameEngine.Resources;

internal sealed class ResourcesManager : IResourcesManager
{
    private readonly List<string> _resourcesFiles = new();
    private Dictionary<string, IResource> _resources = new();
    public static IEnumerable<string> AvailableFileExtensions => new[]
    {
        ".png"
    };
    public IEnumerable<string> ResourcesFiles => _resourcesFiles.Copy();
    public bool IsLoaded { get; private set; }

    public int ResourcesCount { get; private set; }
    public string ResourcesFolder => Paths.ResourcesFolder;

    public void LoadAll()
    {
        if (IsLoaded) throw new ResourcesLoadException("resources already loaded");
        CheckDirectory(Paths.ResourcesFolder);
        Undefined.Logger.Info($"Loaded {ResourcesCount} resources");
        IsLoaded = true;
    }


    public ISprite GetSprite(string pathInResources) => _resources.ContainsKey(pathInResources) ? _resources[pathInResources] as Sprite ?? throw new ResourceNotFoundException("resource is not sprite") : throw new ResourceNotFoundException();
    public bool TryGetSprite(string pathInResources, out ISprite? sprite) => (sprite = _resources.ContainsKey(pathInResources) ? _resources[pathInResources] as ISprite : null) is not null;

    public IResource GetResource(int id) =>
        id < 0 || id >= _resources.Count
            ? throw new ResourceNotFoundException("id less then 0 or greater then resources count")
            : _resources.ElementAt(id).Value;

    public IResource GetResource(string path) => !_resources.ContainsKey(path) ? throw new ResourceNotFoundException("path not found") : _resources[path];
    public bool TryGetResource(int id, out IResource? resource) => (resource = id >= 0 && id < _resources.Count ? _resources.ElementAt(id).Value : null) is not null;

    public bool TryGetResource(string pathInResources, out IResource? resource) => _resources.TryGetValue(pathInResources, out resource);

    private void CheckDirectory(string dir)
    {
        var files = Directory.GetFiles(dir);
        foreach (var file in files) ValidateAndAddFile(file);
        ResourcesCount += files.Length;
        foreach (var directory in Directory.GetDirectories(dir)) CheckDirectory(directory);
    }

    private void ValidateAndAddFile(string file)
    {
        var fileInRes = file.Replace(Paths.ResourcesFolder + "\\", null);
        _resourcesFiles.Add(fileInRes);
        var extension = Path.GetExtension(file);
        IResource res;
        switch (extension)
        {
            case ".png":
            {
                using var stream = File.OpenRead(file);
                var image = Image.Load<Rgba32>(stream);
                res = new Sprite(fileInRes, _resources.Count, image.Width, image.Height);
                break;
            }
            default:
                throw new ResourcesLoadException($"file extension {extension} does not supported");
        }
        _resources.Add(fileInRes, res);
    }
}