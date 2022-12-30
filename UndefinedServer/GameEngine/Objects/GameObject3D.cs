using System;
using System.Collections.Generic;
using Networking;
using UndefinedNetworking.GameEngine.Objects;
using UndefinedNetworking.GameEngine.Objects.Components;

namespace UndefinedServer.GameEngine.Objects;

public class GameObject3D : IGameObject3D
{
    private readonly GameObjectAccess3D _access;
    private readonly object _accessLock = new();

    public GameObject3D()
    {
        _access = new GameObjectAccess3D(this);
    }

    public void InitializeAccess(Action<GameObjectAccess3D> action)
    {
        lock (_accessLock)
        {
            action.Invoke(_access);
            UpdateObject();
        }
    }

    private void UpdateObject()
    {
        //TODO: update and send to client
    }

    public Identifier Identifier { get; }
    public void Destroy()
    {
        throw new NotImplementedException();
    }

    public IEnumerable<GameObject3DComponent> Components { get; }
    public GameObject3DComponent AddComponent(Type type)
    {
        throw new NotImplementedException();
    }

    public T1 AddComponent<T1>() where T1 : GameObject3DComponent, new()
    {
        throw new NotImplementedException();
    }

    public GameObject3DComponent[] AddComponents(params Type[] types)
    {
        throw new NotImplementedException();
    }

    public T1? GetComponent<T1>() where T1 : GameObject3DComponent
    {
        throw new NotImplementedException();
    }

    public GameObject3DComponent? GetComponent(Type type)
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