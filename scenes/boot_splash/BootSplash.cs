using Godot;
using System;

public partial class BootSplash : Control
{
    [Export]
    private PackedScene _mainMenuScene;

    public override void _Ready() => Input.MouseMode = Input.MouseModeEnum.Hidden;
    public void ChangeScene() => GetTree().ChangeSceneToPacked(_mainMenuScene);
}
