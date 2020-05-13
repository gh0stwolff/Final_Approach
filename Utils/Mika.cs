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

    private int _thumbStartFrame = 6;
    private int _thumbEndFrame = 12;

    private int _downStartFrame = 13;
    private int _downEndFrame = 18;

    private int _startFrame = 0;
    private int _endFrame = 5;
    private int frameInterval = 100;
    private int _animationTimer;

    private AnimSprite _text;

    private bool _doOnce = true;
    private bool _doOnce2 = true;

    private enum actions { Talking, ThumbsUp, Gone, Down, Up}
    actions state = actions.Gone;

    public Mika() : base("mikaSpriteSheet.png", 19, 3)
    {
        SetXY(-350, 190);
    }

    public void Update()
    {
        handleAnimation();
        playAnimtion();
        Console.WriteLine(currentFrame);
    }

    private void handleAnimation()
    {
        switch(state)
        {
            case actions.Talking:
                if (_doOnce)
                {
                    PopsUp();
                    _doOnce = false;
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

                if (currentFrame == _endFrame) 
                {
                    state = actions.Gone;
                }
                break;
            case actions.Gone:
                alpha = 0.0f;
                _doOnce = true;
                _doOnce2 = true;
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

    public void TextBalloon(int number)
    {
        string fileName = "textBalloon" + number + ".png";
        _text = new AnimSprite(fileName, 1, 1);
        AddChild(_text);
    }

}
