using Godot;
using System;

public partial class Character : CharacterBody2D
{
    [Export]
    private float _speed = 80f;
    [Export]
    private Marker2D[] _pathPositions;

    public bool CanTakePicture = false;
    public bool AlreadyBeenPhotographed = false;

    private float _minDistanceToNextPosition = 10f;

    private Vector2 _direction;

    private int _pathPositionIndex = 0;

    [Export]
    private AnimatedSprite2D _anim;

    [Export]
    private PackedScene _garbageScene;

    private Timer _timerToTakePicture = new Timer();

    [Export]
    private float _timeToTakePicture;

    public override void _Ready()
    {
        _timerToTakePicture.Autostart = false;
        _timerToTakePicture.WaitTime = _timeToTakePicture;
        _timerToTakePicture.Timeout += OnTimerToTakePictureTimeOut;

        AddChild(_timerToTakePicture);
    }

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
        return this.GlobalPosition.DistanceTo(_pathPositions[_pathPositionIndex].GlobalPosition) <= _minDistanceToNextPosition;
    }

    private void _on_area_2d_area_entered(Area2D area)
    {
        if (area.IsInGroup("triggers"))
        {
            ThrowOutGarbage();
        }
    }

    private void ThrowOutGarbage()
    {
        var tempScene = _garbageScene.Instantiate() as Node2D;
        tempScene.GlobalPosition = this.GlobalPosition;
        GetParent().AddChild(tempScene);

        _timerToTakePicture.Start();
        CanTakePicture = true;
    }

    private void OnTimerToTakePictureTimeOut()
    {
        CanTakePicture = false;
        AlreadyBeenPhotographed = false;
    }

    private void SetAnim()
    {
        if (Math.Abs(_direction.X) > Math.Abs(_direction.Y))
            _anim.Play(_direction.X > 0 ? "move_right" : "move_left");
        else
            _anim.Play(_direction.Y > 0 ? "move_down" : "move_up");
    }
}
