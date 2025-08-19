using Godot;
using System;
using System.Collections.Generic;

public partial class Level : Node2D
{
    [Export]
    private Label _labelCountToWin;
    [Export]
    public int ThisLevelNum;

    private int _photographedCharactersCount = 0;

    [Export]
    private int _maxPhotographedCharactersCount;

    [Export]
    private CharacterBody2D[] _charactersInLevelArray;
    private List<Character> _charactersInLevel = new List<Character>();

    [Export]
    private AnimationPlayer _anim;

    [Signal]
    public delegate void WinEventHandler();
    [Signal]
    public delegate void LostEventHandler();

    private bool _alreadyWinOrLost = false;

    public void WinEventHandlerEmit()
    {
        EmitSignal(SignalName.Win);

        SaveManager.SaveLevel(ThisLevelNum + 1, false);

        if (ThisLevelNum == 0) _anim.Play("change_to_levels");
    }

    public void LostEventHandlerEmit()
    {
        EmitSignal(SignalName.Lost);
    }

    public override void _Ready()
    {
        Input.MouseMode = Input.MouseModeEnum.Hidden;

        _labelCountToWin.Text = $"Caught: {_photographedCharactersCount}/{_maxPhotographedCharactersCount}";

        foreach (CharacterBody2D ch in _charactersInLevelArray)
        {
            if (ch is Character character) _charactersInLevel.Add(character);
        }
    }

    public override void _Process(double delta)
    {
        if (_alreadyWinOrLost) return;

        if (_charactersInLevel.Count > 0)
        {
            foreach (Character ch in _charactersInLevel)
            {
                if (!IsInstanceValid(ch))
                {
                    _charactersInLevel.Remove(ch);
                }
            }   
        }

        if (_charactersInLevel.Count < 1)
        {
            GameOver();
        }
    }

    public void UpdateLabelCountToWinText()
    {
        _photographedCharactersCount++;
        _labelCountToWin.Text = $"Caught: {_photographedCharactersCount}/{_maxPhotographedCharactersCount}";

        if (_alreadyWinOrLost) return;

        if (_photographedCharactersCount >= _maxPhotographedCharactersCount)
        {
            WinEventHandlerEmit();
            _alreadyWinOrLost = true;
        }
    }

    private void GameOver()
    {
        _alreadyWinOrLost = true;
        LostEventHandlerEmit();
    }

    public void ChangeToLevelsScene() => GetTree().ChangeSceneToFile("res://scenes/menu/levels_menu.tscn");
}
