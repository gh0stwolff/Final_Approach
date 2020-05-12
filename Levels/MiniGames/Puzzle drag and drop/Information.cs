using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;

public class Information : AnimSprite
{
    public Vec2 _position;

    //public Vec2 _originalPosition;

    public bool _isCollected;


    public int _whichInfo;



    public Information(string fileName, Vec2 position, int cols, int rows) : base(fileName, cols, rows)
    {
        _position = position;

       // _originalPosition = _position;

        x = position.x;
        y = position.y;
    }

    public void Update()
    {
        updatePos();
    }

    public void updatePos()
    {
        
            x = _position.x;
            y = _position.y;

        boneIsCollected();
        

        SetFrame(_whichInfo);
    }

    public void boneIsCollected()
    {
        if (_isCollected == false)
        {
            alpha = 0f;
        } else
        {
            alpha = 1f;
        }

    }


  
}