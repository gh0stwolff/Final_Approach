using System;
using GXPEngine;
using System.Drawing;
using System.Collections.Generic;

public class MyGame : Game
{
    Mika _mika;
    //Sound _backgroundMusic;
    //SoundChannel _backgroundMusicChannel;
    

	public MyGame () : base(1024, 768, false,false)
	{
        //ScreenHandler screenHandler = new ScreenHandler();
        //AddChild(screenHandler);

        //puzzle minigame easy \/
        //Jigsaw jigsaw = new Jigsaw(2);
        //AddChild(jigsaw);

        Quiz _quiz = new Quiz("quizquesttest1easy.png", new Vec2(0, 0), 3);
        AddChild(_quiz);

        ////boneslide minigame \/                     \/ change this number to 1 - easy 2 - meduim 3 - hard
        //BoneSlide slide = new BoneSlide(width, height, 1);
        //AddChild(slide);

        //memory minigame \/
        //MixAndMatch mix = new MixAndMatch(width, height, 16);
        //AddChild(mix);

        _mika = new Mika();
        AddChild(_mika);

        setupSound();
    }

    private void setupSound()
    {
        Sound _Music = new Sound("Background_music.mp3", true);
        SoundChannel _Channel = new SoundChannel(20);
        _Channel = _Music.Play();
        _Channel.Volume = 0.3f;
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