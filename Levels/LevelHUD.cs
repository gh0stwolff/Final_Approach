using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;

public class LevelHUD : Canvas
{
    private int _width;
    private int _height;

    public Button _back;

    public LevelHUD(int width, int height) : base(width, height)
    {
        _width = width;
        _height = height;
        _back = new Button("colors.png", new Vec2(50, 50));
        AddChild(_back);
    }

    public void Update()
    {
        
    }
}
