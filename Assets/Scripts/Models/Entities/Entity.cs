using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    public EntityType EntityType => _entityType;
    protected EntityType _entityType;

    protected abstract void SetEntityType();

    private void Awake()
    {
        SetEntityType();
        Subscribe();
    }

    protected virtual void Subscribe() { }

    private void OnDestroy()
    {
        Unsubscribe();
    }

    protected virtual void Unsubscribe() { }

    public abstract void KillEntity();
}
