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

        //puzzle minigame easy \/
        //Jigsaw jigsaw = new Jigsaw(1);
        //AddChild(jigsaw);

        //Quiz _quiz = new Quiz("quizquesttest1.png", new Vec2 (0,0), 0);
        //AddChild(_quiz);

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
            _mika.Play("Put.wav", 6000);
            Console.WriteLine("talking...");
        }
        if (Input.GetKeyDown(Key.THREE)) { _mika.Down();
            Console.WriteLine("*disapears");
        }
        if (Input.GetKeyDown(Key.FOUR))
        {
            _mika.Idle();
            Console.WriteLine("*idle");
        }
    }

    static void Main()
    {
        new MyGame().Start();
    }

    public void Play(string filename, int time)
    {
        _mika.Play(filename, time);
    }

    public void StopSound()
    {
        _mika.StopSounds();
    }

    public void Talk()
    {
        _mika.Talking();
    }

    public void Textbaloon(string filename)
    {
        _mika.TextBalloon(filename);
    }

    public void GoodJob()
    {
        _mika.GoodJob();
    }

    public void Down()
    {
        _mika.Down();
    }
}