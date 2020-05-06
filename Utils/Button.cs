using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using GXPEngine;
using GXPEngine.Managers;

public class Button : Sprite
{
    protected bool Pressed = false;

    protected Vec2 _position;

    protected bool _hover = false;

    private float _sizeOnHover = 1.1f;

    public Button(string fileName, Vec2 position) : base(fileName)
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

    private void hover()
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

    private void pressed()
    {
        if (_hover && Input.GetMouseButtonDown(0))
        { Pressed = true; }
        else
        { Pressed = false; }
    }
}