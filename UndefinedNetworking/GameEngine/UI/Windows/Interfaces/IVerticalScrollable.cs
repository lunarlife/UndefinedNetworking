using Utils;
using Utils.Dots;

namespace UndefinedNetworking.GameEngine.UI.Windows.Interfaces;

public interface IVerticalScrollable : IScrollable
{
    public float VerticalScrollSpeed { get; }
    public bool CanVertialScrollNow { get; }
}