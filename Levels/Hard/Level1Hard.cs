using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;

class Level1Hard : GameObject
{
    private int _timer = 0;
    private int _targetTime1 = 0;
    private int _targetTime2 = 0;
    private int _targetTime3 = 0;
    private int _targetTime4 = 0;

    private bool _startHelloText = true;
    private bool _startIntroText = true;
    private bool _startHeyText = true;

    private bool _startClearText = true;

    private bool _startLetsText = true;

    private bool _startItLooksText = true;

    private bool _startPutText = true;

    private bool _isDS2Mikadown = false;

    private bool _doOnce1 = true;
    private bool _doOnce2 = true;
    private bool _doOnce3 = true;

    private bool _textOnce1 = true;
    private bool _textOnce2 = true;
    private bool _textOnce3 = true;
    private bool _textOnce4 = true;

    private bool _startAmazingText = true;
    private bool _startWellText = true;
    private bool _startItWasNiceText = true;
    public bool _isGoBackMenuPressed = false;
    public bool _makeLastButtonOnce = true;
    int deltaTime = 1000;
    int targetTime = 0;

    Diggingsite _ds0, _ds1, _ds2, _ds3;

    Button _goBackMenu;

    BoneSlide _boneslide;
    MixAndMatch _mixandmatch;
    Jigsaw _jigsaw;
    Quiz _quiz;

    private Sprite _taptocontinue;

    enum Scene
    {
        INTRO,
        BONESLIDE,
        DIGGING1,
        MIXANDMATCH,
        DIGGING2,
        JIGSAW,
        DIGGING3,
        QUIZ,
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

            case Scene.QUIZ:
                handleQuizScene();
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
        //when intro first speak hello.
        if (_startHelloText)
        {
            ((MyGame)game).Talk();
            ((MyGame)game).Play("Hello.wav", 7000);
            ((MyGame)game).Textbaloon("introtext1.png");
            _startHelloText = false;
            Console.WriteLine("1");
            _targetTime1 = Time.time + 7500;
            _taptocontinue = new Sprite("taptocontinue.png");
            AddChild(_taptocontinue);
            _taptocontinue.SetXY(650, 690);
        }
        else
        {
            if ((_targetTime1 < Time.time && _textOnce1 && _targetTime1 != 0) || Input.GetMouseButtonDown(0) && _startIntroText)
            {
                ((MyGame)game).StopSound();
                ((MyGame)game).Play("Intro.wav", 45000);
                ((MyGame)game).Textbaloon("introtext2.png");
                _targetTime2 = Time.time + 15100;
                _targetTime3 = Time.time + 24900;
                _targetTime4 = Time.time + 37000;
                _textOnce1 = false;
                _startIntroText = false;
                Console.WriteLine("first");
            }

            if (_targetTime2 < Time.time && _textOnce2 && _targetTime2 != 0)
            {
                ((MyGame)game).Textbaloon("introtext3.png");
                _textOnce2 = false;
            }
            if (_targetTime3 < Time.time && _textOnce3 && _targetTime3 != 0)
            {
                ((MyGame)game).Textbaloon("introtext4.png");
                _textOnce3 = false;
            }
            if (_targetTime4 < Time.time && _textOnce4 && _targetTime4 != 0)
            {
                ((MyGame)game).Textbaloon("introtext5.png");
                _textOnce4 = false;
            }
        }

        //click and then
        //maybe do use the timer to time talking part?
        //When clicked start intro
        //if (Input.GetMouseButtonDown(0) && _startHelloText != true && _startIntroText)
        //{
        //    ((MyGame)game).StopSound();
        //    ((MyGame)game).Play("Intro.wav", 45000);
        //    _startIntroText = false;
        //    Console.WriteLine("2");
        //}
        //else

        //When clicked again hey.wav starts
        if (Input.GetMouseButtonDown(0) && _startIntroText != true && _startHeyText)
        {
            ((MyGame)game).StopSound();
            ((MyGame)game).Play("Hey.wav", 6000);
            _startHeyText = false;
            Console.WriteLine("3");

        }
        else if (_startHeyText != true && Input.GetMouseButtonDown(0))
        {
            targetTime = Time.time + deltaTime;
            ((MyGame)game).StopSound();
            ((MyGame)game).Down();
        }


        if (targetTime < Time.time && targetTime != 0)
        {
            {
                setScene(Scene.BONESLIDE);
            }
        }
    }

    private void handleBoneSlideScene()
    {
        //when intro first speak hello.
        if (_startClearText)
        {
            ((MyGame)game).Talk();
            ((MyGame)game).Play("Clear.wav", 5000);
            ((MyGame)game).Textbaloon("textBoneSlide.png");

            _startClearText = false;
        }
        else if (_startClearText != true && Input.GetMouseButtonDown(0) && _doOnce1)
        {
            ((MyGame)game).Down();
            _doOnce1 = false;
        }


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
            _taptocontinue = new Sprite("taptocontinue.png");
            AddChild(_taptocontinue);
            _taptocontinue.SetXY(650, 690);
        }

        if (Input.GetMouseButtonDown(0))
        {
            _taptocontinue.LateDestroy();
            setScene(Scene.MIXANDMATCH);
        }
    }

    private void handleMixAndMatchScene()
    {

        if (_mixandmatch == null)
        {
            _mixandmatch = new MixAndMatch(((MyGame)game).width, ((MyGame)game).height, 16);
            AddChild(_mixandmatch);
        }

        if (_startLetsText)
        {
            ((MyGame)game).Talk();
            ((MyGame)game).Play("Lets.wav", 12000);
            ((MyGame)game).Textbaloon("textMemory.png");

            _startLetsText = false;
        }
        else if (_startLetsText != true && Input.GetMouseButtonDown(0) && _doOnce2)
        {
            ((MyGame)game).Down();
            _doOnce2 = false;
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
            _taptocontinue = new Sprite("taptocontinue.png");
            AddChild(_taptocontinue);
            _taptocontinue.SetXY(650, 690);

        }

        if (_startItLooksText)
        {
            ((MyGame)game).StopSound();
            ((MyGame)game).Talk();
            ((MyGame)game).Play("It_looks.wav", 11000);
            _startItLooksText = false;
        }
        else if (_startItLooksText != true && Input.GetMouseButtonDown(0))
        {
            targetTime = Time.time + deltaTime;
            ((MyGame)game).StopSound();
            ((MyGame)game).Down();
            _isDS2Mikadown = true;
        }


        if (targetTime < Time.time && targetTime != 0 && _isDS2Mikadown == true)
        {
            {
                _taptocontinue.LateDestroy();
                targetTime = 0;
                setScene(Scene.JIGSAW);
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

        if (_startPutText)
        {
            ((MyGame)game).Talk();
            ((MyGame)game).Play("Put.wav", 6000);
            ((MyGame)game).Textbaloon("textJigsaw.png");

            _startPutText = false;
        }
        else if (_startPutText != true && Input.GetMouseButtonDown(0) && _doOnce3)
        {
            ((MyGame)game).Down();
            _doOnce3 = false;
        }


        if (_jigsaw._IsGameFinished)
        {
            {
                setScene(Scene.QUIZ);
                if (_jigsaw != null)
                {
                    _jigsaw.LateDestroy();
                    _jigsaw = null;
                }
            }
        }
    }

    private void handleQuizScene()
    {
        if (_quiz == null)
        {
            _quiz = new Quiz("quizquesttest3hard.png", "infoQhard", new Vec2(0, 0), 3);
            AddChild(_quiz);
        }


        if (_quiz._isGameFinished)
        {
            {
                setScene(Scene.DIGGING3);
                if (_quiz != null)
                {
                    _quiz.LateDestroy();
                    _quiz = null;
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
            _taptocontinue = new Sprite("taptocontinue.png");
            AddChild(_taptocontinue);
            _taptocontinue.SetXY(650, 690);
        }

        if (_startAmazingText)
        {
            ((MyGame)game).Talk();
            ((MyGame)game).Play("Look.wav", 5000);
            _startAmazingText = false;
        }
        else if (Input.GetMouseButtonDown(0) && _startAmazingText != true && _startWellText)
        {
            ((MyGame)game).StopSound();
            ((MyGame)game).Play("Well.wav", 1000);
            _startWellText = false;
        }
        else


        if (Input.GetMouseButtonDown(0) && _startWellText != true && _startItWasNiceText)
        {
            ((MyGame)game).StopSound();
            ((MyGame)game).Play("It_was_nice.wav", 10000);
            _startItWasNiceText = false;

        }
        else if (_startItWasNiceText != true && Input.GetMouseButtonDown(0) && _makeLastButtonOnce)
        {

            ((MyGame)game).StopSound();
            ((MyGame)game).Down();
            _goBackMenu = new Button("arrowL_spritesheet_2.png", new Vec2(900, 700), 7, 1);
            AddChild(_goBackMenu);
            _makeLastButtonOnce = false;
        }

        if (_goBackMenu != null)
        {
            if (_goBackMenu.Pressed && _isGoBackMenuPressed != true)
            {

                _isGoBackMenuPressed = true;
                if (_ds3 != null)
                {
                    _taptocontinue.LateDestroy();
                    _ds3.LateDestroy();
                    _ds3 = null;
                }
            }
        }

    }

}