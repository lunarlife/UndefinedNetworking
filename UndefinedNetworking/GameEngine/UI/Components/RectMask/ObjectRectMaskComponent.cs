using Networking.DataConvert;
using Utils;

namespace UndefinedNetworking.GameEngine.UI.Components.RectMask;

public sealed record ObjectRectMaskComponent : RectMaskComponent
{
    [ExcludeData]
    private Rect _viewRect;

    [ClientData]
    public override Rect ViewRect
    {
        get => _viewRect;
        set
        {
            _viewRect = value;
            Update();
        }
    }
}