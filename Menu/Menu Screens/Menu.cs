using System;
using GXPEngine;
using System.Drawing;
using System.Collections.Generic;


class Menu : Sprite
{
    Button _buttonPlay;
    private AnimationSprite _animationLogo;

    private float _animationTimer = 0;

    private int numberOfFrames = 23;
    private int startFrame = 0;

    public bool _buttonPressed = false;

    public bool _playAnimation = true;

    public Menu() : base("MenuBackground.png")
    {

        CreatePlayButton();
        CreateAnimLogo();
        
    }

    public void Update()
    {
        checkButtonPressed();
        if (_playAnimation)
        { 
            animation();
            if (_animationLogo.currentFrame == 22)
            {
                _playAnimation = false;
            }
        }
    }

    public void CreatePlayButton()
    {
        Vec2 myVec = new Vec2(501, 530);

        _buttonPlay = new Button("playbutton.png", myVec, 7, 1);
        AddChild(_buttonPlay);

    }

    public void CreateAnimLogo()
    {
        _animationLogo = new AnimationSprite("AnimationLogo.png", 23, 1);
        AddChild(_animationLogo);
        _animationLogo.SetXY(0,-50);
    }

    public void checkButtonPressed()
    {
        if (_buttonPlay.Pressed)
        {
            _buttonPressed = true;
        }
        if (Input.GetKey(Key.R))
        {
            _playAnimation = true;
        }
    }

    protected void animation()
    {
        //IF THERE IS TIME, TIMER
            float _frameIntervalAF = 70f;

            _animationTimer += Time.deltaTime;
            int currentFrame = (int)(_animationTimer / _frameIntervalAF) % numberOfFrames + startFrame;
            _animationLogo.SetFrame(currentFrame);
    }
}

