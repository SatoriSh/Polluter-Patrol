using Godot;
using System;
using System.ComponentModel;

public partial class Garbage : Node2D
{
    [Export]
    private Sprite2D[] _garbageSprites;

    private Random _random = new Random();

    public override void _Ready()
    {
        foreach (Sprite2D garbageSprite in _garbageSprites) garbageSprite.Visible = false;

        _garbageSprites[_random.Next(0, _garbageSprites.Length)].Visible = true;
    }

}
