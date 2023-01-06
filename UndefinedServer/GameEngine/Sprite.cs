using UndefinedNetworking.GameEngine.Resources;

namespace UndefinedServer.GameEngine;

internal sealed class Sprite : ISprite
{
    public string Path { get; }
    public string FullPath { get; }
    public int Id { get; }
    public int Width { get; }
    public int Height { get; }

    internal Sprite(string path, int id, int width, int height)
    {
         Path = path;
         Id = id;
         Width = width;
         Height = height;
         FullPath = System.IO.Path.Join(Paths.ResourcesFolder, path);
    }
}