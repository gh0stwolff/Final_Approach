using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using GXPEngine;
using GXPEngine.Managers;

class ClickablePiece : Button
{
    private int _selectionStrokeWidth = 5;
    private bool _active = false;
    private Color _rainbow = Color.FromArgb(255, 0, 0);

    private EasyDraw _selection;

    public ClickablePiece(string fileName, Vec2 position) : base(fileName, position)
    {
        _selection = new EasyDraw(width + _selectionStrokeWidth * 2, height + _selectionStrokeWidth * 2);
    }

    public void Update()
    {
        selected();
        updateColor();
    }

    private void updateColor()
    {

    }

    private void selected()
    {
        if (Pressed)
        {
            _active = true;
        }

        if (_active)
        {
            selection();
        }
    }

    private void selection()
    {
        _selection.Clear(Color.Transparent);
        _selection.NoFill();
        _selection.Stroke(_rainbow);
        _selection.StrokeWeight(_selectionStrokeWidth);
        _selection.Rect(_position.x - width / 2, _position.y - height / 2, width, height);
    }

}
