using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using GXPEngine;
using GXPEngine.Managers;

public class Diggingsite : AnimSprite
{
    private SoundChannel _channel;

    private float _animationTimer = 0;

    private int numberOfFrames = 23;
    private int startFrame = 0;

    public bool _playAnimation = true;

    public Diggingsite(string fileName, int cols = 1, int rows = 1) : base(fileName, cols, rows)
    {
        _channel = new SoundChannel(1);
        if (cols != 1 || rows != 1)
        {
            Sound effect = new Sound("Brush.wav");
            _channel = effect.Play();
        }
    }

    public void Update()
    {

        if (_playAnimation)
        {
            animation();
            if (currentFrame == 22)
            {
                _playAnimation = false;
                _channel.Stop();
            }
        }
    }

    protected void animation()
    {
        //IF THERE IS TIME, TIMER
        float _frameIntervalAF = 70f;

        _animationTimer += Time.deltaTime;
        int currentFrame = (int)(_animationTimer / _frameIntervalAF) % numberOfFrames + startFrame;
        SetFrame(currentFrame);
    }
}
