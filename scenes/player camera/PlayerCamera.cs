using Godot;
using System;
using System.Collections.Generic;

public partial class PlayerCamera : Node2D
{
    [Export]
    private AnimationPlayer _anim;

    private List<Character> _charactersInZoom = new List<Character>();

    //public override void _Ready() => this.Visible = true;

    public override void _Process(double delta)
    {
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

            if (character.CanTakePicture && !character.AlreadyBeenPhotographed)
            {
                GD.Print($"ПОПАЛСЯ: {character.Name}");
                character.AlreadyBeenPhotographed = true;
                (GetParent() as Level).UpdateLabelCountToWinText();

                continue;
            }
        }
    }

    private void _on_area_2d_area_entered(Area2D area)
    {
        if (area.GetParent() as Character != null)
        {
            _charactersInZoom.Add(area.GetParent() as Character);
        }
    }

    private void _on_area_2d_area_exited(Area2D area)
    {
        if (area.GetParent() as Character != null)
        {
            _charactersInZoom.Remove(area.GetParent() as Character);
        }
    }

}
