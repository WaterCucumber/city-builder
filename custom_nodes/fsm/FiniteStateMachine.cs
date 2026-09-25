using Godot;

namespace CityBuilder.CustomNodes.FSM;


public class FiniteStateMachine
{
    private State _currentState;
    public void TransitTo(State state)
    {
        if(_currentState == state) return;
        if(_currentState != null) 
        {
            _currentState.Transition -= TransitTo;
            _currentState?.Exit();
        }
        
        _currentState = state;
        _currentState.Transition += TransitTo;
        state.Enter();
    }

    public void PhysicsProcess(double delta) => _currentState?.PhysicsProcess(delta);
    public void Process(double delta) => _currentState?.Process(delta);
}
