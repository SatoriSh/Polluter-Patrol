using Godot;
using System;

public partial class Titres : Node2D
{
    [Export]
    private Level _level;

    [Export]
    private Node2D _pauseNode;

    [Export]
    private AnimationPlayer _anim;

    [Export]
    private PlayerCamera _camera;

    private bool _animPlaying = false;

    public override void _Ready()
    {
        _level.Win += OnWin;
        _anim.AnimationFinished += OnAnimFinished;
    }

    private void OnWin()
    {
        _pauseNode.QueueFree();
        _anim.Play("show");
    }

    private void OnAnimFinished(StringName anim)
    {
        if (anim == "show")
        {
            _anim.Play("move_up");
        }
    }

    public void ChangeSceenToLevelsMenu() => GetTree().ChangeSceneToFile("res://scenes/menu/levels_menu.tscn");
    public void SetCameraAudioFalse() => _camera.CanPlayAudio = false;
}
