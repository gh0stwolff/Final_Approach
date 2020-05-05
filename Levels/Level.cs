using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;

public class Level : GameObject
{
    private Sprite _background;
    private LevelHUD _HUD;

    public Level() : base()
    {
        _background = new Sprite("backgroundLevels.jpg");
        AddChild(_background);
        _HUD = new LevelHUD(((MyGame)game).width, ((MyGame)game).height);
        AddChild(_HUD);
    }

    public void Update()
    {

    }
}
