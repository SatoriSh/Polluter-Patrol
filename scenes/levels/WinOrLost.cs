using Godot;
using System;

public partial class WinOrLost : Node2D
{
    [Export]
    private Node2D _pauseNode;

    [Export]
    private AnimationPlayer _anim;
    [Export]
    private Label label;

    [Export]
    private Level _level;

    public override void _Ready()
    {
        _pauseNode.QueueFree();

        _level.Win += OnWin;
    }

    private void OnWin()
    {

    }

}
