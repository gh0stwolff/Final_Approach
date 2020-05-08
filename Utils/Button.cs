using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using GXPEngine;
using GXPEngine.Managers;

public class Button : AnimSprite
{
    public bool Pressed = false;

    protected Vec2 _position;

    protected bool _hover = false;

    protected float _sizeOnHover = 1.1f;

    public Button(string fileName, Vec2 position, int cols = 1, int rows = 1) : base(fileName, cols, rows)
    {
        _position = position;
        SetOrigin(width / 2, height / 2);
        x = position.x;
        y = position.y;
    }

    public void Update()
    {
        hover();
        pressed();
    }

    protected void hover()
    {
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

    protected void pressed()
    {
        
        if (_hover && Input.GetMouseButtonDown(0))
        { Pressed = true; }
        else
        { Pressed = false; }
    }
}