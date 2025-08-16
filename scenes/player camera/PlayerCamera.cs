using Godot;
using System;

public partial class PlayerCamera : Node2D
{
    [Export]
    private AnimationPlayer _anim;

    public override void _Ready() => this.Visible = true;

    public override void _Process(double delta)
    {
        this.GlobalPosition = GetGlobalMousePosition();

        if (Input.IsActionJustPressed("take_picture"))
        {
            _anim.Play("flash");
        }
    }
}
