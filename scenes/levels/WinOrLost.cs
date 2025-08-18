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
    private AnimationPlayer _anim;
    [Export]
    private Label _label;

    [Export]
    private Level _level;

    public override void _Ready()
    {
        _levelsScene = GD.Load<PackedScene>(_pathToLevelsScene);

        _level.Win += OnWin;
        _level.Lost += OnLost;
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
    }

    private void _on_change_level_button_button_down()
    {
        GD.Print("ЗАПУСК change_to_levels");
        _anim.Play("change_to_levels");
    }

    public void ChangeToLevels() => GetTree().ChangeSceneToPacked(_levelsScene);
}
