using UndefinedNetworking.GameEngine.UI.Components.RectMask;
using Utils;
using Utils.Dots;

namespace UndefinedNetworking.GameEngine.UI.Components;

public sealed record ScrollComponent : UINetworkComponent
{
    private Rect _viewRect;
    private float _verticalScrollSpeed;
    private float _horizontalScrollSpeed;
    private Dot2 _position;

    /// <summary>
    /// zero if only vertical
    /// </summary>
    [ClientData]
    public float HorizontalScrollSpeed
    {
        get => _horizontalScrollSpeed;
        set
        {
            _horizontalScrollSpeed = value;
            Update();
        }
    }

    /// <summary>
    /// zero if only horizontal
    /// </summary>
    [ClientData]
    public float VerticalScrollSpeed
    {
        get => _verticalScrollSpeed;
        set
        {
            _verticalScrollSpeed = value;
            Update();
        }
    }
    
    [ClientData]
    public Rect ViewRect
    {
        get => _viewRect;
        set
        {
            _viewRect = value;
            Update();
        }
    }

    [ServerData]
    public Dot2 Position
    {
        get => _position;
        set
        {
            _position = value;
            Update();
        }
    }

    protected override void Initialize()
    {
    }
}