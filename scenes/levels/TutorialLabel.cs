using Godot;
using System;

public partial class TutorialLabel : Label
{
	private string _tutorialText = "Photograph people standing near litter to document their violation\n(left click, enter or space)";
	private string _winTutorialText = "Good job!";

	private char[] _letters;

	private Timer _timer = new Timer();

	private int _letterIndex = -1;

	[Export]
	private Level _level;

	private bool _beginWrite2Text = false;

	public override void _Ready()
	{
		_timer.WaitTime = 0.035f;
		_timer.Timeout += OnTimerTimeOut;
		_timer.Autostart = true;
		AddChild(_timer);

		_letters = _tutorialText.ToCharArray();

		_level.Win += OnWin;
	}

	private void OnTimerTimeOut()
	{
		if (_letterIndex == _letters.Length - 1)
		{
			_timer.Autostart = false;
			_timer.Stop();
			return;
		}

		_letterIndex++;
		this.Text += _letters[_letterIndex];
	}

	private void OnWin()
	{
		this.Text = "";

		_beginWrite2Text = true;
		_letterIndex = -1;
		_letters = _winTutorialText.ToCharArray();

		_timer.Autostart = true;
		_timer.WaitTime = 0.065;
		_timer.Start();
	}
}
