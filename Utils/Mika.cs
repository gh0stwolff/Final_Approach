using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;

class Mika : AnimSprite
{
    private int _upStartFrame = 0;
    private int _upEndFrame = 5;

    private int _talkStartFrame = 19;
    private int _talkEndFrame = 28;

    private int _idleStartFrame = 41;
    private int _idleEndFrame = 50;

    private int _thumbStartFrame = 6;
    private int _thumbEndFrame = 12;

    private int _downStartFrame = 13;
    private int _downEndFrame = 18;

    private int _startFrame = 0;
    private int _endFrame = 5;
    private int frameInterval = 100;
    private int _animationTimer;

    private int _targetTime = 0;

    private uint _channelID = 0;

    private bool _doOnce = true;
    private bool _doOnce2 = true;
    private bool _doOnceS = true;

    private Sound _effect;

    private List<Sprite> textBalloon = new List<Sprite>();
    private List<SoundChannel> channels = new List<SoundChannel>();
    private string[] goodjob = new string[4] { "Good.wav", "Great.wav", "Nice.wav", "Well.wav"};
    
    private enum actions { Talking, Idle, ThumbsUp, Gone, Down, Up}
    actions state = actions.Gone;

    public Mika() : base("mikaSpriteSheet.png", 19, 3)
    {
        SetXY(-350, 190);
    }

    public void Update()
    {
        handleAnimation();
        playAnimtion();
    }

    private void handleAnimation()
    {
        switch(state)
        {
            case actions.Talking:
                if (_doOnce)
                {
                    PopsUp();
                    _effect = new Sound("Mika_up.wav");
                    _effect.Play();
                    Console.WriteLine("*up");
                    _doOnce = false;
                }
                if (_targetTime < Time.time && _targetTime != 0)
                {
                    Console.WriteLine("done");
                    _targetTime = 0;
                    Idle();
                }
                if (currentFrame == _endFrame) 
                {
                    Talking(); 
                }
                break;
            case actions.ThumbsUp:
                if (_doOnce)
                {
                    PopsUp();
                    _effect = new Sound("Mika_up.wav");
                    _effect.Play();
                    Console.WriteLine("*up");
                    playRandom();
                    _doOnce = false;
                }
                if (currentFrame == _endFrame && _doOnce2)
                {
                    _doOnce2 = false;
                    GoodJob();
                }
                if (currentFrame == _thumbEndFrame)
                {
                    Down();
                }
                break;
            case actions.Down:
                if (_doOnceS)
                {
                    _effect = new Sound("Mika_down1.wav");
                    _effect.Play();
                    Console.WriteLine("*down");
                    _doOnceS = false;
                }
                if (textBalloon.Count != 0)
                {
                    foreach (Sprite text in textBalloon)
                    {
                        text.LateDestroy();
                    }
                    textBalloon.Clear();
                }
                StopSounds();
                if (currentFrame == _endFrame) 
                {
                    state = actions.Gone;
                }
                break;
            case actions.Gone:
                alpha = 0.0f;
                _doOnce = true;
                _doOnce2 = true;
                _doOnceS = true;
                break;
        }
    }

    private void playAnimtion()
    {
        int numberOfFrames = _endFrame - _startFrame + 1;

        _animationTimer += Time.deltaTime;
        int currentFrame = (_animationTimer / frameInterval) % numberOfFrames + _startFrame;
        SetFrame(currentFrame);
    }

    private void PopsUp()
    {
        _startFrame = _upStartFrame;
        _endFrame = _upEndFrame;
        _animationTimer = 0;
        alpha = 1.0f;
    }

    public void Down()
    {
        _startFrame = _downStartFrame;
        _endFrame = _downEndFrame;
        _animationTimer = 0;
        state = actions.Down;
    }

    public void GoodJob()
    {
        _startFrame = _thumbStartFrame;
        _endFrame = _thumbEndFrame;
        _animationTimer = 0;
        state = actions.ThumbsUp;
    }

    public void Talking()
    {
        _startFrame = _talkStartFrame;
        _endFrame = _talkEndFrame;
        _animationTimer = 0;
        state = actions.Talking;
    }

    public void Idle()
    {
        _startFrame = _idleStartFrame;
        _endFrame = _idleEndFrame;
        _animationTimer = 0;
        state = actions.Idle;
    }

    public void TextBalloon(string fileName)
    {
        Sprite _text = new Sprite(fileName);
        AddChild(_text);
        _text.SetXY(420, 420);
        textBalloon.Add(_text);
    }

    public void Play(string fileName, int time)
    {
        Sound sound = new Sound(fileName, false, true);
        SoundChannel channel = new SoundChannel(_channelID);
        channel = sound.Play();
        _targetTime = time + Time.time;
        state = actions.Talking;
        channels.Add(channel);
        _channelID++;
    }

    public void StopSounds()
    {
        if (channels.Count != 0)
        {
            foreach (SoundChannel sound in channels)
            {
                sound.Stop();
            }
            channels.Clear();
        }
    }

    private void playRandom()
    {
        Sound sound = new Sound(goodjob[Utils.Random(0, 4)], false, true);
        sound.Play();
    }

}
