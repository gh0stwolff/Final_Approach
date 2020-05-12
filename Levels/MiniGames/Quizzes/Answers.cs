using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using GXPEngine;
using GXPEngine.Managers;

class Answers : Button
{
    protected Vec2 _positionA;

    public Answers(string fileName, Vec2 position, int cols = 1, int rows = 1) : base(fileName, position, cols, rows)
    {
        _positionA = position;

        x = position.x;
        y = position.y;
    }

    override public void Update()
    {
        hover();
        pressed();
        startAnimation();
        animation();
    }


}
