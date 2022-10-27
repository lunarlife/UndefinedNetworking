
namespace UndefinedNetworking.GameEngine.UI.Elements.Interfaces;

public interface IUIElement : IObjectCore
{
   public IRectTransform Transform { get; }
   public string Name { get; }
}