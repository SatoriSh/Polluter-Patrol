using Godot;
using System;

public partial class Music : Node2D
{
    private bool _musicAlreadyPlaying = false;

    private AudioStreamPlayer2D _audioPlayer;

    private AnimationPlayer _anim;

    public override void _Ready()
    {
        _audioPlayer = GetNode<AudioStreamPlayer2D>("AudioStreamPlayer2D");
        _anim = GetNode<AnimationPlayer>("AnimationPlayer");
    }

    public void PlayMusic()
    {
        if (!_musicAlreadyPlaying)
        {
            _audioPlayer.Play();
            _anim.Play("PlusVolume");
            _musicAlreadyPlaying = true;
        }
    }
    public void StopMusic() => _anim.Play("MinusVolume");
}
