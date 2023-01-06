using System.Collections.Generic;

namespace UndefinedNetworking.GameEngine.Resources;

public interface IResourcesManager
{
    public IEnumerable<string> ResourcesFiles { get; }
    public bool IsLoaded { get; }
    public int ResourcesCount { get; }
    public string ResourcesFolder { get; }
    public void LoadAll();
    public IResource GetResource(int id);
    public IResource GetResource(string pathInResources);
    public bool TryGetResource(int id, out IResource? resource);
    public bool TryGetResource(string pathInResources, out IResource? resource);
    public ISprite GetSprite(string pathInResources);
    public bool TryGetSprite(string pathInResources, out ISprite? sprite);
    
}