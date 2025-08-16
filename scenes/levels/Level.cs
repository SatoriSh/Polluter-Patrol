using Godot;
using System;

public partial class Level : Node2D
{
    [Export]
    private Label _labelCountToWin;

    private int _photographedCharactersCount = 0;

    [Export]
    private int _maxPhotographedCharactersCount;

    public override void _Ready()
    {
        _labelCountToWin.Text = $"Count {_photographedCharactersCount} / {_maxPhotographedCharactersCount}";
    }

    public void UpdateLabelCountToWinText()
    {
        _photographedCharactersCount++;
        _labelCountToWin.Text = $"Count {_photographedCharactersCount} / {_maxPhotographedCharactersCount}";
    }

}
