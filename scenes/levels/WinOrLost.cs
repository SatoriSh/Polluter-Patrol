using Godot;
using System;

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

    [Export]
    private TextureButton _button;

    private PlayerCamera _camera;

    public override void _Ready()
    {
        _camera = GetNode<PlayerCamera>("../../PlayerCamera");

        _levelsScene = GD.Load<PackedScene>(_pathToLevelsScene);

        if (!_showOnlyLost)
        {
            _level.Win += OnWin;
        }

        _level.Lost += OnLost;

        SetButtonDisabledStatus(true);
    }

    private void OnWin()
    {
        _label.Text = "Good job! You've got everyone!";
        ShowScene();
    }
    private void OnLost()
    {
        _label.Text = "You couldn't catch all of them...";
        ShowScene();
    }

    private void ShowScene()
    {
        _anim.Play("show");
        _pauseNode.QueueFree();
        Input.MouseMode = Input.MouseModeEnum.Visible;
        SetButtonDisabledStatus(false);
    }

    private void _on_change_level_button_button_down()
    {
        _anim.Play("change_to_levels");
    }

    public void SetCameraAudioFalse() => _camera.CanPlayAudio = false;

    public void SetButtonDisabledStatus(bool status)
    {
        _button.Disabled = status;
    }

    public void ChangeToLevels() => GetTree().ChangeSceneToPacked(_levelsScene);
}
