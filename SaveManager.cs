using Godot;
using System;

public partial class SaveManager : Node
{
    public static SaveManager Instance { get; private set; }

    public override void _Ready()
    {
        Instance = this;
    }

    public static void SaveLevel(int levelNum)
    {
        GD.Print("там было " + LoadMaxLevel());
        if (LoadMaxLevel() < levelNum)
        {
            using var file = Godot.FileAccess.Open("user://max_level.save", Godot.FileAccess.ModeFlags.Write); // перезаписываем нахуй весь файл
            file.StoreLine(levelNum.ToString());
        }
    }

    public static int LoadMaxLevel()
    {
        if (!Godot.FileAccess.FileExists("user://max_level.save"))
            return 0; // tutorial

        using var file = Godot.FileAccess.Open("user://max_level.save", Godot.FileAccess.ModeFlags.Read);
        string line = file.GetLine();
        if (int.TryParse(line, out int value))
        {
            return value;
        }

        return 0;
    }
}
