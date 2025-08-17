using Godot;
using System;

public partial class Menu : Node2D
{
    private PackedScene _levelsScene;

    [Export]
    private AnimationPlayer anim;

    private bool _animPlaying = false;

    public override void _Ready()
    {
        GetTree().Paused = false;
        
        _levelsScene = GD.Load<PackedScene>("res://scenes/menu/levels_menu.tscn");
    }

    private void _on_play_button_button_down()
    {
        if (!_animPlaying)
        {
            _animPlaying = true;
            anim.Play("show");
        }
    }

    private void _on_exit_button_button_down()
    {
        if (!_animPlaying)
        {
            _animPlaying = true;
            anim.Play("exit");
        }
    }

    public void ChengeSceneToLevels() => GetTree().ChangeSceneToPacked(_levelsScene);

    public void ExitGame() => GetTree().Quit();
}
