using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;

class collectedBS : Canvas
{
    private int _distbetweenPoints = 50;
    private int _offsetX = 400;
    private int _offsetY = 400;

    private Vec2[] points = new Vec2[3];

    public collectedBS(int width, int height) : base(width, height)
    {

    }

}