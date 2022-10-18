using System.Collections.Generic;
using Networking;
using UndefinedNetworking.Window.UI;
using Utils;

namespace UndefinedNetworking.Window;

public interface IRectTransform
{
    public Identifier NetIdentifier { get; }
    public Rect Rect { get; set; }
    public UIBind Bind { get; set; }
    public bool IsActive { get; set; }
    public IRectTransform Parent { get; }
    public IReadOnlyList<IRectTransform> Childs { get; }
}