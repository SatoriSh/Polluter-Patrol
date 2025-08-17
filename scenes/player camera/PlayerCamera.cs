using Godot;
using System;
using System.Collections.Generic;

public partial class PlayerCamera : Node2D
{
    [Export]
    private AnimationPlayer _anim;

    private List<Character> _charactersInZoom = new List<Character>();
    private List<Garbage> _garbageInZoom = new List<Garbage>();
    
    public override void _Process(double delta)
    {
        if (GetTree().Paused) return;

        this.GlobalPosition = GetGlobalMousePosition();

        if (Input.IsActionJustPressed("take_picture"))
        {
            _anim.Play("flash");
            CheckCharactersInZoom();
        }
    }

    private void CheckCharactersInZoom()
    {
        if (_charactersInZoom.Count < 1 || _charactersInZoom == null) return;

        foreach (Character character in _charactersInZoom)
        {
            if (character == null)
            {
                _charactersInZoom.Remove(character);
                continue;
            }

            if (character.CanTakePicture && !character.AlreadyBeenPhotographed && _garbageInZoom.Count > 0)
            {
                character.AlreadyBeenPhotographed = true;
                (GetParent() as Level).UpdateLabelCountToWinText();

                continue;
            }
        }
    }

    private void _on_area_2d_area_entered(Area2D area)
    {
        if (area.GetParent() as Character != null && area.GetParent() is Character character)
        {
            _charactersInZoom.Add(character);
        }
        else if (area.GetParent() as Garbage != null && area.GetParent() is Garbage garbage)
        {
            _garbageInZoom.Add(garbage);
        }
    }

    private void _on_area_2d_area_exited(Area2D area)
    {
        if (area.GetParent() as Character != null && area.GetParent() is Character character)
        {
            _charactersInZoom.Remove(character);
        }
        else if (area.GetParent() as Garbage != null && area.GetParent() is Garbage garbage)
        {
            _garbageInZoom.Remove(garbage);
        }
    }

}
