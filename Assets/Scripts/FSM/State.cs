using System;
using UnityEngine;

public abstract class State
{
    protected GameObject characterGameObject;
    public virtual void Enter(GameObject characterGameObject)
    {
        this.characterGameObject = characterGameObject;
    }
    public virtual void Update()
    {

    }
    public virtual void FixedUpdate()
    {

    }
    public virtual void LateUpdate()
    {

    }
    public virtual void Exit()
    {

    }
}
