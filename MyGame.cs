using System;
using GXPEngine;
using System.Drawing;
using System.Collections.Generic;

public class MyGame : Game
{

	public MyGame () : base(1024, 768, false,false)
	{
        //ScreenHandler screenHandler = new ScreenHandler();
        //AddChild(screenHandler);
        MixAndMatch mix = new MixAndMatch(((MyGame)game).width, ((MyGame)game).height);
        AddChild(mix);
	}

    static void Main()
    {
        new MyGame().Start();
    }
}