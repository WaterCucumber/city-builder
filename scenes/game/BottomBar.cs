using System.Linq;
using System.Threading.Tasks;
using Godot;
using Godot.Collections;

namespace CityBuilder.Scenes.UI;


public partial class BottomBar : VBoxContainer
{
    [Export] private float _subBarsHideY;
    [Export] private float _mainBarHideY;
    [Export] private float _tweenTime;
    [Export] private CustomTextureButton _toolbarToggler;
    [Export] private Control _mainToolbar;
    [Export] private Dictionary<NodePath, NodePath> _subToolbars;
    private Control _shownSubBar;
    private Tween _tween;


    public override void _Ready()
    {
        _toolbarToggler.Button.Pressed += ToggleAll;
        foreach (var keyValuePair in _subToolbars)
        {
            var button = GetNode<CustomTextureButton>(keyValuePair.Key);
            var bar = GetNode<Control>(keyValuePair.Value);
            button.Button.Pressed += () => ToggleBar(bar, _subBarsHideY, _tweenTime);
            if(bar.Visible) bar.Hide();
        }
        if(_mainToolbar.Visible) _mainToolbar.Hide();
    }

    private async void ToggleAll()
    {
        if(_mainToolbar.Visible) await HideBar(_shownSubBar, _subBarsHideY, _tweenTime);
        else await ShowBar(_shownSubBar, _subBarsHideY, _tweenTime);
        ToggleBar(_mainToolbar, _subBarsHideY, _tweenTime);
    }

    private async void ToggleBar(Control obj, float hideY, float tweenTime)
    {
        if(obj.Visible) await HideBar(obj, hideY, tweenTime);
        else await ShowBar(obj, hideY, tweenTime);
    }

    private async Task HideBar(Control obj, float hideY, float tweenTime)
    {
        if(obj == null) return;
        if(obj == _shownSubBar) _shownSubBar = null;
        obj.OffsetTransformPosition = new(0, 0);

        _tween?.Kill();
        _tween = CreateTween().SetTrans(Tween.TransitionType.Circ).SetEase(Tween.EaseType.In);
        _tween.TweenProperty(obj, "offset_transform_position", new Vector2(0, hideY), tweenTime);
        _tween.TweenCallback(new(obj, CanvasItem.MethodName.Hide));
        await ToSignal(_tween, Tween.SignalName.Finished);
    }

    private async Task ShowBar(Control obj, float hideY, float tweenTime)
    {
        if(obj == null) return;
        if(obj != _mainToolbar) 
        {
            await HideBar(_shownSubBar, hideY, tweenTime);
            _shownSubBar = obj;
        }
        obj.OffsetTransformPosition = new(0, hideY);

        obj.Show();
        _tween?.Kill();
        _tween = CreateTween().SetTrans(Tween.TransitionType.Back).SetEase(Tween.EaseType.Out);
        _tween.TweenProperty(obj, "offset_transform_position", new Vector2(0, 0), tweenTime);
        await ToSignal(_tween, Tween.SignalName.Finished);
    }
}   
