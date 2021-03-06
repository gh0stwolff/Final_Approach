﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using GXPEngine;
using GXPEngine.Core;
using GXPEngine.Managers;

public class Button : AnimSprite
{

    private float _animationTimer = 0;

    public bool Pressed = false;
    public bool StartAnimation = false;

    protected Vec2 _position;
    private Vec2 _orgSize;

    public bool _hover = false;

    protected float _sizeOnHover = 1.1f;

    private int lastFrame;
    private int numberOfFrames;
    private int startFrame = 0;

    public Button(string fileName, Vec2 position, int cols = 1, int rows = 1) : base(fileName, cols, rows)
    {
        _position = position;
        _orgSize = new Vec2(width, height);
        SetOrigin(width / 2, height / 2);
        x = position.x;
        y = position.y;

        lastFrame = cols * rows - 1;
        numberOfFrames = cols * rows;
    }

    virtual public void Update()
    {
        hover();
        pressed();
        startAnimation();
        animation();
    }

    protected void hover()
    {
        Vector2 checkPoint = InverseTransformPoint(Input.mouseX, Input.mouseY);

        if (Mathf.Abs(_position.x - Input.mouseX) <= width/2 &&
            Mathf.Abs(_position.y - Input.mouseY) <= height/2)
        {
            scale = _sizeOnHover;
            _hover = true;
        }
        else
        {
            scale = 1.0f;
            _hover = false;
        }
    }

    //plaats pressed niet in update, laat het updaten door moederklas // show alles

    protected void animation()
    {
        if(StartAnimation)
        { 
        float _frameIntervalAF = 80f;

        _animationTimer += Time.deltaTime;
        int currentFrame = (int)(_animationTimer / _frameIntervalAF) % numberOfFrames + startFrame;
        SetFrame(currentFrame);
        }
    }

    protected void startAnimation()
    {
        if (_hover && Input.GetMouseButtonDown(0))
        { 
            StartAnimation = true;
            Sound effect = new Sound("Button_click.wav");
            effect.Play();
        }
        else if (currentFrame == lastFrame)
        { StartAnimation = false; }
    }


    virtual protected void pressed()
    {
        if (currentFrame == lastFrame)
        { Pressed = true; }
        else
        { Pressed = false; }
    }
}