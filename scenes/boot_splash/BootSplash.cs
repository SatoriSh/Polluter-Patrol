using Godot;
using System;

public partial class BootSplash : Control
{
    [Export]
    private PackedScene _mainMenuScene;

    public void ChangeScene() => GetTree().ChangeSceneToPacked(_mainMenuScene);
}
