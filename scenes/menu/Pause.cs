using Godot;
using System;
using System.Collections.Generic;

public partial class Pause : Node2D
{
    private bool _worldInPause = false;

    [Export]
    private AnimationPlayer _anim;

    [Export]
    private PackedScene _menuScene;
    [Export]
    private PackedScene _levelsScene;

    [Export]
    private TextureButton[] buttons;

    private bool _animFinished = true;

    private bool _animPlaying = false;

    Timer timer = new Timer();
    private bool _canOpenPauseMenu = true;

    [Export]
    private Level _level;

    private Dictionary<int, string> _levelsPaths = new()
    {
        {0, "res://scenes/levels/level_0.tscn"},
        {1, "res://scenes/levels/level_1.tscn"},
        {2, "res://scenes/levels/level_2.tscn"},
        {3, "res://scenes/levels/level_3.tscn"},
        {4, "res://scenes/levels/level_4.tscn"},
        {5, "res://scenes/levels/level_5.tscn"},
        {6, "res://scenes/levels/level_6.tscn"},
        {7, "res://scenes/levels/level_7.tscn"},
        {8, "res://scenes/levels/level_8.tscn"},
        {9, "res://scenes/levels/level_9.tscn"},
    };

    public override void _Ready()
    {
        _anim.AnimationFinished += OnAnimationFinished;

        timer.WaitTime = 0.5f;
        timer.Timeout += _on_timer_timeout;
        AddChild(timer);

        SetButtonsDisabledStatus(true);
    }

    public override void _Process(double delta)
    {
        if (!_canOpenPauseMenu) return;
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
        Input.MouseMode = _worldInPause ? Input.MouseModeEnum.Visible : Input.MouseModeEnum.Hidden;

        _canOpenPauseMenu = false;
        timer.Start();
    }

    private void _on_button_pause_off_button_up()
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
    private void _on_restart_button_button_down()
    {
        if (!_animPlaying)
        {
            _anim.Play("restart_level");
            _animPlaying = true;
            GetTree().Paused = false;
        }
    }

    public void SetButtonsDisabledStatus(bool status)
    {
        foreach (TextureButton button in buttons)
        {
            button.Disabled = status;
        }
    }

    public void RestartLevel() => GetTree().ChangeSceneToFile(_levelsPaths[_level.ThisLevelNum]);
    public void ChangeSceneToMenu() => GetTree().ChangeSceneToPacked(_menuScene);
    public void ChangeSceneToLevels() => GetTree().ChangeSceneToPacked(_levelsScene);

    private void OnAnimationFinished(StringName anim) => _animFinished = true;

    private void _on_timer_timeout() => _canOpenPauseMenu = true;
}
