using Godot;
using System;

public partial class Character : CharacterBody2D
{
    [Export]
    private float _speed = 150f;
    [Export]
    private Marker2D[] _pathPositions;

    private float minDistanceToNextPosition = 25f;

    private Vector2 _direction;

    private int _pathPositionIndex = 0;

    [Export]
    private AnimatedSprite2D anim;

    public override void _PhysicsProcess(double delta)
    {
        _direction = (_pathPositions[_pathPositionIndex].GlobalPosition - this.GlobalPosition).Normalized();

        this.Velocity = _direction * _speed;

        CheckNextPosition();
        SetAnim();

        MoveAndSlide();
    }

    private void CheckNextPosition()
    {
        if (PlayerIsOnPosition())
        {
            if (_pathPositionIndex < _pathPositions.Length - 1)
            {
                _pathPositionIndex++;
            }
            else if (_pathPositionIndex == _pathPositions.Length - 1 && PlayerIsOnPosition())
            {
                this.QueueFree();
            }
        }
    }

    private bool PlayerIsOnPosition()
    {
        return this.GlobalPosition.DistanceTo(_pathPositions[_pathPositionIndex].GlobalPosition) <= minDistanceToNextPosition;
    }

    private void SetAnim()
    {
        if (Math.Abs(_direction.X) > Math.Abs(_direction.Y))
            anim.Play(_direction.X > 0 ? "move_right" : "move_left");
        else
            anim.Play(_direction.Y > 0 ? "move_down" : "move_up");
    }
}
