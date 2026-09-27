using CityBuilder.CustomNodes.FSM;
using CityBuilder.CustomNodes.FSM.Buildings;
using CityBuilder.Data;
using Godot;

namespace CityBuilder.CustomNodes;


public partial class BuildManager : Node
{
    private static BuildManager _instance;
    [Export] private Vector2I _gridSize;
    [Export] public TileMapLayer TileMapData { get; private set; }
    [Export] private BuildingData building;

    public GridVisualiser GridVisualiser { get; private set; }
    public GridData GridData { get; private set; }
    public FiniteStateMachine FiniteStateMachine { get; private set; }


    public override void _Ready()
    {
        _instance = this;
        
        GridData = new(_gridSize);
        GridVisualiser = new(GridData);

        FiniteStateMachine = new();
        FiniteStateMachine.TransitTo(new Idle());
    }

    public override void _Process(double delta) => FiniteStateMachine.Process(delta);
    public override void _PhysicsProcess(double delta) => FiniteStateMachine.PhysicsProcess(delta);
    public override void _UnhandledInput(InputEvent @event) => FiniteStateMachine.UnhandledInput(@event);
    public override void _Input(InputEvent @event) => FiniteStateMachine.Input(@event);

    public static BuildManager GetInstance() => _instance;
}
