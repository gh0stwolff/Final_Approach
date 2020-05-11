using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using GXPEngine;
using GXPEngine.Managers;

class ClickablePiece : Button
{
    private int _id;

    public int ID
    { get {
            return _id;
        }
    }

    private int _selectionStrokeWidth = 5;
    private int _red = 255;
    private int _green = 0;
    private int _blue = 0;

    private bool _active = false;
    private Color _rainbow = Color.FromArgb(255, 0, 0);

    private EasyDraw _selection;

    public ClickablePiece(string fileName, Vec2 position, int ID) : base(fileName, position, 9, 1)
    {
        _id = ID;

        _selection = new EasyDraw(width + _selectionStrokeWidth * 2, height + _selectionStrokeWidth * 2);
        AddChild(_selection);
        _selection.SetOrigin(width / 2, height / 2);
        _selection.SetXY(-_selectionStrokeWidth / 2, -_selectionStrokeWidth / 2);
    }

    override public void Update()
    {
        hover();
        selected();
        updateColor();
        if (Input.GetKeyDown(Key.D))
        {
            clearSelection();
        }
    }

    public void CheckPressed()
    {
        pressed();
    }

    private void updateColor()
    {
        if (_red == 255 && _green < 255 && _blue == 0)
        {
            _green+=3;
        }
        else if (_red > 0 && _green == 255 && _blue == 0)
        {
            _red-=3;
        }
        else if (_red == 0 && _green == 255 && _blue < 255)
        {
            _blue+=3;
        }
        else if (_red == 0 && _green > 0 && _blue == 255)
        {
            _green-=3;
        }
        else if (_red < 255 && _green == 0 && _blue == 255)
        {
            _red+=3;
        }
        else if (_red == 255 && _green == 0 && _blue > 0)
        {
            _blue-=3;
        }
        _rainbow = Color.FromArgb(_red, _green, _blue);
    }

    private void selected()
    {
        if (Pressed)
        { _active = true; }

        if (_active)
        { 
            selection();
            SetFrame(_id + 1);
        } else
        {
            SetFrame(0);
        }
    }

    public void SelfDestroy()
    {
        Console.WriteLine("selfDestroy");
        _hover = false;
        LateDestroy();
    }

    public void clearSelection()
    {
        _active = false;
        _selection.Clear(Color.Transparent);
    }

    private void selection()
    {
        _selection.Clear(Color.Transparent);
        _selection.NoFill();
        _selection.Stroke(_rainbow);
        _selection.StrokeWeight(5);
        _selection.ShapeAlign(CenterMode.Min, CenterMode.Min);
        _selection.Rect(0, 0, width + _selectionStrokeWidth/2, height + _selectionStrokeWidth/2);

        if (_hover)
        {
            float scaleDelta = _sizeOnHover - 1;
            float newScale = 1 - scaleDelta;

            _selection.scale = newScale;
            _selection.SetXY(-_selectionStrokeWidth, -_selectionStrokeWidth);
        }
        else 
        {
            _selection.scale = 1.0f;
            _selection.SetXY(-_selectionStrokeWidth / 2, -_selectionStrokeWidth / 2);
        }
    }

    override protected void pressed()
    {
        if(_hover && Input.GetMouseButtonDown(0)) 
        { Pressed = true; }
        else if (Input.GetMouseButtonDown(1))
        { Pressed = false; }
        else
        {
            Pressed = false;
        }

        //if (Pressed) Console.WriteLine("PRESSED:"+ID);
        //if (_hover) Console.WriteLine("HOVER:"+ID+ "/ "+ parent);
        
    }

}
