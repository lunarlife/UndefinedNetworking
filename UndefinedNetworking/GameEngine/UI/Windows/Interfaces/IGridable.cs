using Utils.Dots;

namespace UndefinedNetworking.GameEngine.UI.Windows.Interfaces;

public interface IGridable
{
    public Axis StartAxis { get; set; }
    public Corner StartCorner { get; set; }
    public Constraint Constraint { get; set; }
    public Alignment ChildAlignment { get; set; }
    public int ConstraintCount { get; set; }
    public Dot2 CellSize { get; set; }
    public Dot2 Spacing { get; set; }
}