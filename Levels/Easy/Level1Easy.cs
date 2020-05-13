using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;

class Level1Easy : GameObject
{
    Diggingsite _ds0, _ds1;

    Button _buttonStartBS;

    BoneSlide _boneslide;



    enum Scene
    {
        INTRO,
        BONESLIDE,
        DIGGING1,
    }

    private Scene _scene;


    public Level1Easy() : base()
    {
        _ds0 = new Diggingsite("diggingsite0easy.png", 1, 1);
        AddChild(_ds0);

    }

    public void Update()
    {
        handleScene();
    }

    public void handleScene()
    {
        switch (_scene)
        {
            case Scene.INTRO:
                handleIntroScene();
                break;

            case Scene.BONESLIDE:
                handleBoneSlideScene();
                break;

            case Scene.DIGGING1:
                handleDigging1Scene();
                break;

        }
    }

    private void setScene(Scene newScene)
    {
        if (_scene != newScene)
        {
            _scene = newScene;
        }
    }


    private void handleIntroScene()
    {

        //here mica pops up says something. You click it away and then Boneslide starts
        if (_buttonStartBS == null)
        {
            _buttonStartBS = new Button("playbutton.png", new Vec2 (50, 50), 7, 1);
            AddChild(_buttonStartBS);
        }


        if (_buttonStartBS.Pressed)
        {
            Console.WriteLine("pressed bs button");
            {
                setScene(Scene.BONESLIDE);
                if (_buttonStartBS != null)
                {
                    _buttonStartBS.LateDestroy();
                    _buttonStartBS = null;
                }
            }
        }
    }

    private void handleBoneSlideScene()
    {

        if (_boneslide == null)
        {
            _boneslide = new BoneSlide(((MyGame)game).width, ((MyGame)game).height);
            AddChild(_boneslide);
        }


        if (_boneslide._IsGameFinished)
        {
            {
                setScene(Scene.DIGGING1);
                if (_boneslide != null)
                {
                    _boneslide.LateDestroy();
                    _boneslide= null;
                }
            }
        }
    }

    private void handleDigging1Scene()
    {

        if (_ds1 == null)
        {
            _ds1 = new Diggingsite("diggingsite1easy.png", 23, 1);
            AddChild(_ds1);
        }


        //if (_boneslide._IsGameFinished)
        //{
        //    {
        //        setScene(Scene.BONESLIDE);
        //        if (_buttonStartBS != null)
        //        {
        //            _buttonStartBS.LateDestroy();
        //            _buttonStartBS = null;
        //        }
        //    }
        //}
    }

}