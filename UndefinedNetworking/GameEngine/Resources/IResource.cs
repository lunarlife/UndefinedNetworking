namespace UndefinedNetworking.GameEngine.Resources;

public interface IResource
{
    public string Path { get; }
    public string FullPath { get; }
    public int Id { get; }
}