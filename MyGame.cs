using System;
using GXPEngine;
using System.Drawing;
using System.Collections.Generic;

public class MyGame : Game
{

	public MyGame () : base(800, 600, false,false)
	{
        Level level = new Level();
        AddChild(level);
	}

    static void Main()
    {
        new MyGame().Start();
    }

}