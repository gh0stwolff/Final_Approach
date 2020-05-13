using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;

public class Level1Easy : GameObject
{
    Diggingsite _ds1, ds2, ds3, ds4;
    
    public Level1Easy() : base()
    {
        //MixAndMatch mix = new MixAndMatch(((MyGame)game).width, ((MyGame)game).height);
        //AddChild(mix);

        createDiggingSites();
    }

    private void createDiggingSites()
    {
        _ds1 = new Diggingsite("diggingsite1.png", 1, 1);
        AddChild(_ds1);
    }

}