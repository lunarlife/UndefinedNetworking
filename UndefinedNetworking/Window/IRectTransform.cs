using System.Collections.Generic;
using Networking.Window.UI;
using Utils;

namespace Networking.Window;

public interface IRectTransform
{
    public Identifier NetIdentifier { get; }
    public Rect Rect { get; set; }
    public UIBind Bind { get; set; }
    public bool IsActive { get; set; }
    public IRectTransform Parent { get; }
    public IReadOnlyList<IRectTransform> Childs { get; }
}