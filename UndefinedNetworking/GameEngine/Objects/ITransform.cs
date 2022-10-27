using System.Collections.Generic;
using Utils.Dots;

namespace UndefinedNetworking.GameEngine.Objects;

public interface ITransform
{
    public Dot2 Position { get; set; }
    public Dot2 Scale { get; set; }
    public float Rotation { get; set; }
    public ITransform? Parent { get; set; }
    public IGameObject Object { get; }
    public bool IsActive { get; set; }
    public IReadOnlyList<ITransform> Childs { get; }
}