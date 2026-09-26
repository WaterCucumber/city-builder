using CityBuilder.Data;
using Godot;

namespace CityBuilder.CustomNodes;


public class PlayerInventory
{
    private static PlayerInventory _instance;
    public Inventory Inventory { get; private set; }


    private PlayerInventory() 
    { 
        Inventory = new(CalculateLimit); 
    }


    public static PlayerInventory GetInstance() => _instance ?? new();

    private int CalculateLimit(string resourceId)
    {

        return 100;
    }
}
