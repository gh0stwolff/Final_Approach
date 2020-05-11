using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;

public class Level1Easy : Level
{

    
    public Level1Easy(int width, int height) : base()
    {
        MixAndMatch mix = new MixAndMatch(width, height);
        AddChild(mix);
    }

}