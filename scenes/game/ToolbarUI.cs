using System;
using Godot;

namespace CityBuilder.Scenes.UI;


public partial class ToolbarUI : Control
{
    [Export] private float _tweenTime = 0.3f;
    [Export] private float _hideY = 32f;
    [Export] public CustomTextureButton ToggleButtons
    {
        get => _toggleButton;
        set
        {
            _toggleButton = value;
            _toggleButton.Button.Pressed += OnToggled;
        }
    }

    [Export] private Control[] _items = [];

    private CustomTextureButton _toggleButton;
    private bool _buttonsVisible = false;
    private Tween _tween;


    public override void _Ready()
    {
        foreach (var item in _items)
        {
            item.OffsetTransformEnabled = true;
            item.OffsetTransformPosition = new Vector2(0, _hideY);
        }
    }

    private void OnToggled()
    {
        _tween?.Kill();
        _buttonsVisible = !_buttonsVisible;

        if(_buttonsVisible) _tween = CreateTween().SetTrans(Tween.TransitionType.Back).SetEase(Tween.EaseType.Out).SetParallel();
        else _tween = CreateTween().SetTrans(Tween.TransitionType.Circ).SetEase(Tween.EaseType.In).SetParallel();

        float targetY = _buttonsVisible ? 0 : _hideY;

        foreach (var item in _items)
        {
            _tween.TweenProperty(item, "offset_transform_position", new Vector2(0, targetY), _tweenTime);
        }
    }
}
