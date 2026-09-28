using Godot;
using Godot.Collections;

namespace CityBuilder.Scenes.UI;


public partial class CustomTextureButton : PanelContainer
{
    [Export] public Button Button { get; private set; }
    [Export] private TextureRect _textureRect;
    [Export] private TooltipInteract _tooltipInteract;
    [Export] public Array<string> Description
    {
        get => _description;
        set
        {
            _description = value;
            if(IsInstanceValid(_tooltipInteract))
                _tooltipInteract.DisplayText = Description;
        }
    }
    [Export] public Texture2D Texture
    {
        get => _texture;
        set
        {
            _texture = value;
            if(IsInstanceValid(_textureRect))
                _textureRect.Texture = Texture;
        }

    }

    private Texture2D _texture;
    private Array<string> _description = [];
}
    
