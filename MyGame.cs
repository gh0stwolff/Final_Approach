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

        ////puzzle minigame \/
        //Jigsaw jigsaw = new Jigsaw();
        //AddChild(jigsaw);

        ////boneslide minigame \/                     \/ change this number to 1 - easy 2 - meduim 3 - hard
        BoneSlide slide = new BoneSlide(width, height, 1);
        AddChild(slide);

        //memory minigame \/
        //MixAndMatch mix = new MixAndMatch(width, height, 16);
        //AddChild(mix);

        _mika = new Mika();
        AddChild(_mika);
    }

    public void Update()
    {
        if (Input.GetKeyDown(Key.ONE)) { _mika.GoodJob();
            Console.WriteLine("thumbs");
        }
        if (Input.GetKeyDown(Key.TWO)) 
        { 
            _mika.Talking();
            _mika.TextBalloon("textJigsaw.png");
            _mika.Play("Put.wav");
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