using System;
using GXPEngine;
using System.Drawing;
using System.Collections.Generic;

public class MyGame : Game
{
    Mika _mika;

	public MyGame () : base(1024, 768, false,false)
	{
        //ScreenHandler screenHandler = new ScreenHandler();
        //AddChild(screenHandler);

        //Jigsaw jigsaw = new Jigsaw();
        //AddChild(jigsaw);

        //BoneSlide slide = new BoneSlide(width, height);
        //AddChild(slide);

        //MixAndMatch mix = new MixAndMatch(width, height);
        //AddChild(mix);

        BoneSlide slide = new BoneSlide(width, height);
        AddChild(slide);

        _mika = new Mika();
        AddChild(_mika);
    }

    public void Update()
    {
        if (Input.GetKeyDown(Key.ONE)) { _mika.GoodJob();
            Console.WriteLine("thumbs");
        }
        if (Input.GetKeyDown(Key.TWO)) { _mika.Talking();
            Console.WriteLine("talking...");
        }
        if (Input.GetKeyDown(Key.THREE)) { _mika.Down();
            Console.WriteLine("*disapears");
        }
    }

    static void Main()
    {
        new MyGame().Start();
    }
}