using System;
using GXPEngine;
using System.Drawing;
using System.Collections.Generic;

public class MyGame : Game
{

	public MyGame () : base(1024, 768, false,false)
	{
        ScreenHandler screenHandler = new ScreenHandler();
        AddChild(screenHandler);

        //Jigsaw jigsaw = new Jigsaw();
        //AddChild(jigsaw);

        //BoneSlide slide = new BoneSlide(width, height);
        //AddChild(slide);

        //MixAndMatch mix = new MixAndMatch(width, height);
        //AddChild(mix);
    }

    static void Main()
    {
        new MyGame().Start();
    }
}