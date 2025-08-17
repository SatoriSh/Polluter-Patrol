using Godot;
using System;

public partial class Pause : Node2D
{
    private bool _worldInPause = false;

    [Export]
    private AnimationPlayer _anim;

    [Export]
    private PackedScene _menuScene;
    [Export]
    private PackedScene _levelsScene;

    private bool _animFinished = true;

    private bool _animPlaying = false;

    public override void _Ready()
    {
        _anim.AnimationFinished += OnAnimationFinished;
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

    private void _on_button_pause_off_button_down()
    {
        if (!_animPlaying)
        {
            UpdateWorldPause();
        }
    }

    private void _on_button_to_menu_button_down()
    {
        if (!_animPlaying)
        {
            _anim.Play("change_to_menu");
            _animPlaying = true;
        }
    }
    private void _on_change_level_button_button_down()
    {
        if (!_animPlaying)
        {
            _anim.Play("change_to_levels");
            _animPlaying = true;
        }
    }
    public void ChangeSceneToMenu() => GetTree().ChangeSceneToPacked(_menuScene);
    public void ChangeSceneToLevels() => GetTree().ChangeSceneToPacked(_levelsScene);

    private void OnAnimationFinished(StringName anim) => _animFinished = true;
}
