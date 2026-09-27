using Godot;

namespace CityBuilder.Scenes.UI;


[Tool]
public partial class CustomTextureButton : PanelContainer
{
    [Export] public Button Button { get; private set; }
    [Export] private TextureRect _textureRect;
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
}
    
