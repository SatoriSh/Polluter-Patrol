using Godot;
using System;

public partial class LevelsMenu : Node2D
{
    private PackedScene _menuScene;

    [Export]
    private PackedScene _level0Scene;
    [Export]
    private PackedScene _level1Scene;
    [Export]
    private PackedScene _level2Scene;
    [Export]
    private PackedScene _level3Scene;
    [Export]
    private PackedScene _level4Scene;
    [Export]
    private PackedScene _level5Scene;
    [Export]
    private PackedScene _level6Scene;
    [Export]
    private PackedScene _level7Scene;
    [Export]
    private PackedScene _level8Scene;
    [Export]
    private PackedScene _level9Scene;

    [Export]
    private AnimationPlayer _anim;

    private bool _animPlaying = false;

    public override void _Ready()
    {
        _menuScene = GD.Load<PackedScene>("res://scenes/menu/menu.tscn");
    }

    private void _on_button_to_menu_button_down()
    {
        if (!_animPlaying) _anim.Play("change_to_menu");
        _animPlaying = true;
    }

    public void ChangeSceneToMenu() => GetTree().ChangeSceneToPacked(_menuScene); 
}
