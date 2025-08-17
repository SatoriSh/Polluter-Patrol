using Godot;
using System;

public partial class Pause : Node2D
{
    private bool _worldInPause = true;

    [Export]
    private AnimationPlayer _anim;

    private bool _animFinished = true;

    public override void _Ready()
    {
        _anim.AnimationFinished += OnAnimationFinished;

        UpdateWorldPause();
    }

    public override void _Process(double delta)
    {
        if (Input.IsActionJustPressed("pause") && _animFinished)
        {
            UpdateWorldPause();
        }
    }

    private void UpdateWorldPause()
    {
        _animFinished = false;
        _worldInPause = !_worldInPause;
        GetTree().Paused = _worldInPause;
        _anim.Play(_worldInPause ? "show" : "hide");
    }

    private void OnAnimationFinished(StringName anim) => _animFinished = true;
}
