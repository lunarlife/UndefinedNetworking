namespace UndefinedNetworking.GameEngine.UI.Elements.Structs
{
    public struct UIBind
    {
        public UIBind()
        {
        }

        public Side Side { get; set; } = Side.TopLeft;
        public bool IsExpandable { get; set; } = false;
    }
}