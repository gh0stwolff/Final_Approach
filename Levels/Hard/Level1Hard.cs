using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;

class Level1Hard : GameObject
{
    Diggingsite _ds0, _ds1, _ds2, _ds3;

    Button _buttonStartBS, _buttonStartMAM, _buttonStartJig;

    BoneSlide _boneslide;
    MixAndMatch _mixandmatch;
    Jigsaw _jigsaw;



    enum Scene
    {
        INTRO,
        BONESLIDE,
        DIGGING1,
        MIXANDMATCH,
        DIGGING2,
        JIGSAW,
        DIGGING3,
    }

    private Scene _scene;


    public Level1Hard() : base()
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

            case Scene.MIXANDMATCH:
                handleMixAndMatchScene();
                break;

            case Scene.DIGGING2:
                handleDigging2Scene();
                break;

            case Scene.JIGSAW:
                handleJigsawScene();
                break;

            case Scene.DIGGING3:
                handleDigging3Scene();
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
            _buttonStartBS = new Button("playbutton.png", new Vec2(50, 50), 7, 1);
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
            _boneslide = new BoneSlide(((MyGame)game).width, ((MyGame)game).height, 3);
            AddChild(_boneslide);
        }


        if (_boneslide._IsGameFinished)
        {
            {
                setScene(Scene.DIGGING1);
                if (_boneslide != null)
                {
                    _boneslide.LateDestroy();
                    _boneslide = null;
                }
            }
        }
    }

    private void handleDigging1Scene()
    {

        if (_ds1 == null)
        {
            _ds1 = new Diggingsite("diggingsite1hard.png", 23, 1);
            AddChild(_ds1);
        }

        //here mica pops up says something there is site uncovered. You click it away and then MAM starts
        if (_buttonStartMAM == null)
        {
            _buttonStartMAM = new Button("playbutton.png", new Vec2(50, 50), 7, 1);
            AddChild(_buttonStartMAM);
        }


        if (_buttonStartMAM.Pressed)
        {
            {
                setScene(Scene.MIXANDMATCH);
                if (_buttonStartMAM != null)
                {
                    _buttonStartMAM.LateDestroy();
                    _buttonStartMAM = null;
                }
            }
        }
    }

    private void handleMixAndMatchScene()
    {

        if (_mixandmatch == null)
        {
            _mixandmatch = new MixAndMatch(((MyGame)game).width, ((MyGame)game).height, 16);
            AddChild(_mixandmatch);
        }


        if (_mixandmatch._IsGameFinished)
        {
            {
                setScene(Scene.DIGGING2);
                if (_mixandmatch != null)
                {
                    _mixandmatch.LateDestroy();
                    _mixandmatch = null;
                }
            }
        }
    }

    private void handleDigging2Scene()
    {

        if (_ds2 == null)
        {
            _ds2 = new Diggingsite("diggingsite2hard.png", 23, 1);
            AddChild(_ds2);
        }

        //here mica pops up says something there is site uncovered. You click it away and then MAM starts
        if (_buttonStartJig == null)
        {
            _buttonStartJig = new Button("playbutton.png", new Vec2(50, 50), 7, 1);
            AddChild(_buttonStartJig);
        }


        if (_buttonStartJig.Pressed)
        {
            {
                setScene(Scene.JIGSAW);
                if (_buttonStartJig != null)
                {
                    _buttonStartJig.LateDestroy();
                    _buttonStartJig = null;
                }
            }
        }
    }

    private void handleJigsawScene()
    {

        if (_jigsaw == null)
        {
            _jigsaw = new Jigsaw(3);
            AddChild(_jigsaw);

        }


        if (_jigsaw._IsGameFinished)
        {
            {
                setScene(Scene.DIGGING3);
                if (_jigsaw != null)
                {
                    _jigsaw.LateDestroy();
                    _jigsaw = null;
                }
            }
        }
    }

    private void handleDigging3Scene()
    {

        if (_ds3 == null)
        {
            _ds3 = new Diggingsite("diggingsite3hard.png", 23, 1);
            AddChild(_ds3);
        }

        ////here mica pops up says something there is site uncovered. You click it away and then MAM starts
        //if (_buttonStartJig == null)
        //{
        //    _buttonStartJig = new Button("playbutton.png", new Vec2(50, 50), 7, 1);
        //    AddChild(_buttonStartJig);
        //}


        //if (_buttonStartJig.Pressed)
        //{
        //    {
        //        setScene(Scene.JIGSAW);
        //        if (_buttonStartJig != null)
        //        {
        //            _buttonStartJig.LateDestroy();
        //            _buttonStartJig = null;
        //        }
        //    }
        //}
    }

}