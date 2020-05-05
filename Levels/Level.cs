using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;

class Level : GameObject
{
    private Sprite _background;

    public Level() : base()
    {
        _background = new Sprite("backgroundLevels.jpg");
        AddChild(_background);
    }
}
