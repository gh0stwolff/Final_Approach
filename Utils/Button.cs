using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using GXPEngine;
using GXPEngine.Managers;

public class Button : Sprite
{
    public bool Pressed = false;

    private Vec2 _location;

    private bool _hover = false;

    private float _sizeOnHover = 1.1f;

    public Button(string fileName, Vec2 location) : base(fileName)
    {
        _location = location;
        SetOrigin(width / 2, height / 2);
        x = location.x;
        y = location.y;
    }

    public void Update()
    {
        hover();
        pressed();
    }

    private void hover()
    {
        if (Mathf.Abs(_location.x - Input.mouseX) <= width/2 &&
            Mathf.Abs(_location.y - Input.mouseY) <= height/2)
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