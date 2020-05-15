using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;

class Level1Easy : GameObject
{

    private bool _startHelloText = true;
    private bool _startIntroText = true;
    private bool _startHeyText = true;

    private bool _startClearText = true;

    private bool _startLetsText = true;

    private bool _startItLooksText = true;

    private bool _startPutText = true;

    private bool _startAmazingText = true;
    private bool _startWellText = true;
    private bool _startItWasNiceText = true;

    int deltaTime = 1000;
    int targetTime = 0;

    Diggingsite _ds0, _ds1, _ds2, _ds3;

    Button _buttonStartBS, _buttonStartMAM, _buttonStartJig;

    BoneSlide _boneslide;
    MixAndMatch _mixandmatch;
    Jigsaw _jigsaw;
    Quiz _quiz;



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
            ((MyGame)game).Play("Hello.wav");
            _startHelloText = false;
            Console.WriteLine("1");
        } else
        //click and then
        //maybe do use the timer to time talking part?
        //When clicked start intro
        if (Input.GetMouseButtonDown(0) && _startHelloText != true && _startIntroText)
        {
            ((MyGame)game).StopSound();
            ((MyGame)game).Play("Intro.wav");
            _startIntroText = false;
            Console.WriteLine("2");
        } else

        //When clicked again hey.wav starts
        if (Input.GetMouseButtonDown(0) && _startIntroText != true && _startHeyText)
        {
            ((MyGame)game).StopSound();
            ((MyGame)game).Play("Hey.wav");
            _startHeyText = false;
            Console.WriteLine("3");

        } else if(_startHeyText != true && Input.GetMouseButtonDown(0))
        {
            targetTime = Time.time + deltaTime;
            ((MyGame)game).StopSound();
            ((MyGame)game).Down();
        }


        if (targetTime < Time.time && targetTime != 0)
        {
            {
                targetTime = 0;
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
            ((MyGame)game).Play("Clear.wav");
            ((MyGame)game).Textbaloon("textBoneSlide.png");

            _startClearText = false;
        }
        else if(_startClearText != true && Input.GetMouseButtonDown(0))
        {
            ((MyGame)game).Down();
        }


        if (_boneslide == null)
        {
            _boneslide = new BoneSlide(((MyGame)game).width, ((MyGame)game).height, 1);
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

        if (Input.GetMouseButtonDown(0))
        {
            setScene(Scene.MIXANDMATCH);
        }
    }

    private void handleMixAndMatchScene()
    {

        if (_mixandmatch == null)
        {
            _mixandmatch = new MixAndMatch(((MyGame)game).width, ((MyGame)game).height, 8);
            AddChild(_mixandmatch);
        }


        if (_startLetsText)
        {
            ((MyGame)game).Talk();
            ((MyGame)game).Play("Lets.wav");
            ((MyGame)game).Textbaloon("textMemory.png");

            _startLetsText = false;
        }
        else if (_startLetsText != true && Input.GetMouseButtonDown(0))
        {
            ((MyGame)game).Down();
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
            _ds2 = new Diggingsite("diggingsite2easy.png", 23, 1);
            AddChild(_ds2);

            
        }

        if (_startItLooksText)
        {
            ((MyGame)game).StopSound();
            ((MyGame)game).Talk();
            ((MyGame)game).Play("It_looks.wav");
            _startItLooksText = false;
        }
        else if (_startItLooksText != true && Input.GetMouseButtonDown(0))
        {
            targetTime = Time.time + deltaTime;
            ((MyGame)game).StopSound();
            ((MyGame)game).Down();
        }


        if (targetTime < Time.time && targetTime != 0)
        {
            {
                targetTime = 0;
                setScene(Scene.JIGSAW);
            }
        }

    }

    private void handleJigsawScene()
    {

        if (_jigsaw == null)
        {
            _jigsaw = new Jigsaw(1);
            AddChild(_jigsaw);
           
        }

        if (_startPutText)
        {
            ((MyGame)game).Talk();
            ((MyGame)game).Play("Put.wav");
            ((MyGame)game).Textbaloon("textJigsaw.png");

            _startPutText = false;
        }
        else if (_startPutText != true && Input.GetMouseButtonDown(0))
        {
            ((MyGame)game).Down();
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
            _quiz = new Quiz("quizquesttest1.png", new Vec2(0,0), 1);
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
            _ds3 = new Diggingsite("diggingsite3easy.png", 23, 1);
            AddChild(_ds3);
        }


        if (_startAmazingText)
        {
            ((MyGame)game).Talk();
            ((MyGame)game).Play("Amazing.wav");
            _startAmazingText = false;
        }
        else if (Input.GetMouseButtonDown(0) && _startAmazingText != true && _startWellText)
        {
            ((MyGame)game).StopSound();
            ((MyGame)game).Play("Well.wav");
            _startWellText = false;
        }
        else

        //When clicked again hey.wav starts
        if (Input.GetMouseButtonDown(0) && _startWellText != true && _startItWasNiceText)
        {
            ((MyGame)game).StopSound();
            ((MyGame)game).Play("It_was_nice.wav");
            _startItWasNiceText = false;

        }
        else if (_startItWasNiceText != true && Input.GetMouseButtonDown(0))
        {
            targetTime = Time.time + deltaTime;
            ((MyGame)game).StopSound();
            ((MyGame)game).Down();
        }


        if (targetTime < Time.time && targetTime != 0)
        {
            {
                targetTime = 0;
                //setScene(Scene.BONESLIDE);
            }
        }
    }



}