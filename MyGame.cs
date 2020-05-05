using System;
using GXPEngine;
using System.Drawing;
using System.Collections.Generic;

public class MyGame : Game
{

	public MyGame () : base(1024, 768, false,false)
	{
        ScreenHandler screenhandler = new ScreenHandler();
        AddChild(screenhandler);
	}

    static void Main()
    {
        new MyGame().Start();
    }

}