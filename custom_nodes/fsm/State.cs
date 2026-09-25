using System;
using Godot;

namespace CityBuilder.CustomNodes.FSM;


public abstract class State
{
    public event Action<State> Transition;
    
    public virtual void Enter() {}
    public virtual void Exit() {}
    public virtual void PhysicsProcess(double delta) {}
    public virtual void Process(double delta) {}

    protected void TransitTo(State state) => Transition?.Invoke(state);
}
