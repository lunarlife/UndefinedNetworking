namespace UndefinedServer.GameEngine;

public abstract class ObjectCore
{
    public string Name { get; }
    public bool IsDestroyed { get; private set; }

    public void Destroy()
    {
        IsDestroyed = true;
        DoDestroy();
    }
    protected abstract void DoDestroy();
}