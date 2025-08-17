using Godot;
using System;

public partial class LevelsMenu : Node2D
{
    private PackedScene _menuScene;

    [Export]
    private string[] _pathToScenes = new string[9];
    private PackedScene[] _scenes = new PackedScene[9];

    [Export]
    private AnimationPlayer _anim;

    private bool _animPlaying = false;

    private PackedScene _currentChoiceScene;

    public override void _Ready()
    {
        _menuScene = GD.Load<PackedScene>("res://scenes/menu/menu.tscn");

        for (int i = 0; i < _scenes.Length; i++)
        {
            if (_pathToScenes[i] != null)
            {
                _scenes[i] = GD.Load<PackedScene>(_pathToScenes[i]);
            }
        }
    }

    private void _on_button_to_menu_button_down()
    {
        if (!_animPlaying)
        {
            _animPlaying = true;
            _anim.Play("ChangeScene");
            _currentChoiceScene = _menuScene;
        }
    }

    private void _on_levelbutton_3_button_down()
    {
        if (!_animPlaying)
        {
            SetAnimAndCurrentScene(3);
        }
    }

    private void SetAnimAndCurrentScene(int sceneIndex)
    {
        _animPlaying = true;
        _anim.Play("ChangeScene");
        _currentChoiceScene = _scenes[sceneIndex];
    }

    public void ChangeScene()
    {
        GetTree().ChangeSceneToPacked(_currentChoiceScene);
    }
}
