using Godot;
using System;

public partial class Level : Node2D
{
    [Export]
    private Label _labelCountToWin;

    private int _photographedCharactersCount = 0;

    [Export]
    private int _maxPhotographedCharactersCount;

    [Signal]
    public delegate void WinEventHandler();

    private bool _alreadyWinOrLost = false;

    [Export]
    private AnimationPlayer _anim;

    public void WinEventHandlerEmit()
    {
        EmitSignal(SignalName.Win);
        _anim.Play("change_to_levels");
    }

    public override void _Ready()
    {
        _labelCountToWin.Text = $"Caught: {_photographedCharactersCount}/{_maxPhotographedCharactersCount}";
    }

    public void UpdateLabelCountToWinText()
    {
        _photographedCharactersCount++;
        _labelCountToWin.Text = $"Caught: {_photographedCharactersCount}/{_maxPhotographedCharactersCount}";

        if (_alreadyWinOrLost) return;

        if (_photographedCharactersCount >= _maxPhotographedCharactersCount)
        {
            WinEventHandlerEmit();
            _alreadyWinOrLost = true;
        }
    }

    public void ChangeToLevelsScene() => GetTree().ChangeSceneToFile("res://scenes/menu/levels_menu.tscn");
}
