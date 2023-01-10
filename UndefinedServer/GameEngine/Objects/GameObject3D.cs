using System;
using UndefinedNetworking.GameEngine.Components;
using UndefinedNetworking.GameEngine.Scenes.Objects;
using UndefinedNetworking.GameEngine.Scenes.Objects.Components;

namespace UndefinedServer.GameEngine.Objects;

public class GameObject3D : IGameObject3D
{
    public uint Identifier { get; }
    public void Destroy()
    {
        throw new NotImplementedException();
    }

    public IComponent<GameObject3DComponent>[] Components { get; }
    public IComponent<GameObject3DComponent> AddComponent(Type type)
    {
        throw new NotImplementedException();
    }

    public IComponent<T1> AddComponent<T1>() where T1 : GameObject3DComponent, new()
    {
        throw new NotImplementedException();
    }

    public IComponent<GameObject3DComponent>[] AddComponents(params Type[] types)
    {
        throw new NotImplementedException();
    }

    public IComponent<T1> GetComponent<T1>() where T1 : GameObject3DComponent
    {
        throw new NotImplementedException();
    }

    public bool TryGetComponent<T1>(out IComponent<T1> component) where T1 : GameObject3DComponent
    {
        throw new NotImplementedException();
    }

    public IComponent<GameObject3DComponent> GetComponent(Type type)
    {
        throw new NotImplementedException();
    }

    public bool ContainsComponent<T1>() where T1 : GameObject3DComponent
    {
        throw new NotImplementedException();
    }

    public bool ContainsComponent(Type type)
    {
        throw new NotImplementedException();
    }
}