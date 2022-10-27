using Utils;
using Utils.Dots;

namespace UndefinedNetworking.GameEngine.UI.Windows.Interfaces;

public interface IHorizontalScrollable : IScrollable
{
    public float HorizontalScrollSpeed { get; }
    public bool CanHorizontalScrollNow { get; }

}