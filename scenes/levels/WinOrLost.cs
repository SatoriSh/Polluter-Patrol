using Godot;
using System;
using System.Collections.Generic;
using System.IO;

public partial class WinOrLost : Node2D
{
    [Export]
    private Node2D _pauseNode;

    [Export]
    private string _pathToLevelsScene;
    private PackedScene _levelsScene;

    [Export]
    private bool _showOnlyLost = false;

    [Export]
    private AnimationPlayer _anim;
    [Export]
    private Label _label;

    [Export]
    private Level _level;

    private PlayerCamera _camera;

    [Export]
    private TextureButton[] _buttons; // 0 - levels menu, 1 - restart level, 2 - next level

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
        {8, ""},
        {9, ""},
    };

    private int _levelToChange;

    public override void _Ready()
    {
        _camera = GetNode<PlayerCamera>("../../PlayerCamera");

        _levelsScene = GD.Load<PackedScene>(_pathToLevelsScene);

        if (!_showOnlyLost)
        {
            _level.Win += OnWin;
        }

        _level.Lost += OnLost;

        foreach (var button in _buttons)
        {
            button.Disabled = true;
        }
    }

    private void OnWin()
    {
        _label.Text = "Good job! You've got everyone!";
        ShowScene();

        if (_level.ThisLevelNum < 9) _buttons[2].Disabled = false;
        _buttons[0].Disabled = false;
        _buttons[1].Disabled = false;
    }
    private void OnLost()
    {
        _label.Text = "You couldn't catch all of them...";
        ShowScene();

        _buttons[0].Disabled = false;
        _buttons[1].Disabled = false;
        _buttons[2].Visible = false;
    }

    private void ShowScene()
    {
        _anim.Play("show");
        _pauseNode.QueueFree();
        Input.MouseMode = Input.MouseModeEnum.Visible;
    }

    private void _on_change_level_button_button_down()
    {
        _anim.Play("change_to_levels");
    }
    private void OnRestartButtonDown()
    {
        _levelToChange = _level.ThisLevelNum;
        _anim.Play("change_scene");
    }
    private void OnNextLevelButtonDown()
    {
        if (_level.ThisLevelNum < 9)
            _levelToChange = ++_level.ThisLevelNum;

        _anim.Play("change_scene");
    }

    public void ChangeScene() => GetTree().ChangeSceneToFile(_levelsPaths[_levelToChange]);
    public void ChangeToLevels() => GetTree().ChangeSceneToPacked(_levelsScene);
    public void SetCameraAudioFalse() => _camera.CanPlayAudio = false;
}
