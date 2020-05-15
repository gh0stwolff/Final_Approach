using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;
using System.Drawing;

//puzzle id geeft aan difficulty, 0 = easy, 1= med, 2= hard

public class Quiz : AnimSprite
{
    public Vec2 _position;

    private int _puzzleID = 1;

    public int _whichquestion = 0;
    public int nextframe = 0;

    public bool _doOnceCreateContinueButt1 = true;
    public bool _doOnceCreateContinueButt2 = true;
    public bool _doOnceCreateContinueButt3 = true;
    //public bool _doOnceCreateContinueButt4 = true;

    public bool _doOnceCreateAnswers2 = true;
    public bool _doOnceCreateAnswers3 = true;
    //public bool _doOnceCreateAnswers4 = true;

    public bool _isQ1Answered = false;
    public bool _isQ2Answered = false;
    public bool _isQ3Answered = false;
    //public bool _isQ4Answered = false;

    public bool _doOnce = true;

    public bool _isGameFinished = false;

    public bool _isQuizDone = false;

    Answers _Q1A1, _Q1A2, _Q1A3, _Q1A4;
    Answers _Q2A1, _Q2A2, _Q2A3, _Q2A4;
    Answers _Q3A1, _Q3A2, _Q3A3, _Q3A4;
   // Answers _Q4A1, _Q4A2, _Q4A3, _Q4A4;

    Button _continue;
    Button _lastDiggingbutton;

    Information _infoQ1, _infoQ2, _infoQ3;

    Answers[] answersQ1 = new Answers[4];
    Answers[] answersQ2 = new Answers[4];
    Answers[] answersQ3 = new Answers[4];
    //Answers[] answersQ4 = new Answers[4];


    public Quiz(string fileName, Vec2 position, int puzzleID) : base(fileName, 3, 1)
    {
        _position = position;
        _puzzleID = puzzleID;

        x = position.x;
        y = position.y;


        

        createAnswersQ1();
        createInfoQ1();
        createInfoQ2();
        createInfoQ3();
        createContinueButton();

    }

    public void Update()
    {
        SetFrame(nextframe);

        updatePos();
        
        nextQuestion();
        if (nextframe == 0)
        {
            checkAnswerQ1();
        }
        if (nextframe == 1)
        {
            checkAnswerQ2();
        }
        if (nextframe == 2)
        {
            checkAnswerQ3();
        }

        //if (_i && _isFinished && _buttonJigsawDone.Pressed)
        //{
        //    _buttonJigsawDone.LateDestroy();
        //    _IsGameFinished = true;
        //    _doOnce = false;
        //}

        if (_lastDiggingbutton != null)
        { 
        if (_lastDiggingbutton.Pressed && _doOnce)
            {
                Console.WriteLine("done");
                _lastDiggingbutton.LateDestroy();
                _isGameFinished = true;
                _doOnce = false;
            }
        }

    }

    public void updatePos()
    {
        x = _position.x;
        y = _position.y;
    }

    //------------------------------
    //// everything from QEUSTION 1
    //------------------------------
    private void createAnswersQ1()
    {
        _Q1A1 = new Answers("Q1AnswA"+_puzzleID+".png", new Vec2(225, 390), 2, 1);
        AddChild(_Q1A1);
        answersQ1[0] = _Q1A1;

        _Q1A2 = new Answers("Q1AnswB"+_puzzleID+".png", new Vec2(490, 390), 2, 1);
        AddChild(_Q1A2);
        answersQ1[1] = _Q1A2;

        _Q1A3 = new Answers("Q1AnswC"+_puzzleID+".png", new Vec2(225, 590), 2, 1);
        AddChild(_Q1A3);
        answersQ1[2] = _Q1A3;

        _Q1A4 = new Answers("Q1AnswD"+_puzzleID+".png", new Vec2(490, 590), 2, 1);
        AddChild(_Q1A4);
        answersQ1[3] = _Q1A4;
    }

    private void createContinueButton()
    {
        _continue = new Button("arrowR_spritesheet_2.png", new Vec2(900, 700), 7, 1);
        AddChild(_continue);
        _continue.alpha = 0.0f;
    }

    
    private void createInfoQ1()
    {
        _infoQ1 = new Information("infoQ1" + _puzzleID + ".png", new Vec2(655, 105), 2, 1);
        AddChild(_infoQ1);

    }

    
    private void checkAnswerQ1()
    {
        if (answersQ1[0]._hover && Input.GetMouseButtonDown(0) && _infoQ1._isCollected == false)
        {
            //onderstaande zin is voor alpha, van 0 naar 1
            _infoQ1._isCollected = true;

            _infoQ1._whichInfo = 0;

            _isQ1Answered = true;

            Sound effect = new Sound("Correct_sound.wav");
            effect.Play();

            Console.WriteLine("question1 right!");
        }
        else if ((answersQ1[1]._hover || answersQ1[2]._hover || answersQ1[3]._hover) &&
        Input.GetMouseButtonDown(0) && _infoQ1._isCollected == false)
        {

            _infoQ1._isCollected = true;

            _infoQ1._whichInfo = 0;

            _isQ1Answered = true;

            Sound effect = new Sound("False.wav");
            effect.Play();

            Console.WriteLine("question1 wrong");
        }

              _infoQ1._isCollected = true;

              _infoQ1._whichInfo = 1;

              _isQ1Answered = true;
            
              Console.WriteLine("question1 wrong");
             }

        if ( _isQ1Answered && _doOnceCreateContinueButt1)
        {
            _continue.alpha = 1f;
            _doOnceCreateContinueButt1 = false;
            
            for (int i = 0; i <= 3; i++)
            {
                answersQ1[i].SetFrame(1);
            }
        }
    }

    private void nextQuestion()
    {
            if(_continue._hover && Input.GetMouseButtonDown(0) && _continue.alpha == 1f)
            {
                //next question
                nextframe += 1;

                //make continue dissapear
                
                _continue.alpha = 0.0f;

                //if the last question is answerd game is done //als er iets stuk gaat nextframe was eerst 3
                if (nextframe == 2 && _isQ3Answered == true)
                {
                    _isQuizDone = true;
                }
                
                //next answers, delete old answes
                // antwoord 1gedaan maak vraag 2
                if (_doOnceCreateAnswers2 && _isQ1Answered)
                {
                    _infoQ1.LateDestroy();
                    //delete old answers
                for (int i = 0; i <= 3; i++)
                    {
                        answersQ1[i].LateDestroy();
                        Console.WriteLine("Answers 1 gone");
                    }
                        Console.WriteLine("answers2 are now there");
                        createAnswersQ2();
                        _doOnceCreateAnswers2 = false;
                }
                // antwoord 2 gedaan maak vraag 3
                if (_doOnceCreateAnswers3 && _isQ2Answered)
                {
                    _infoQ2.LateDestroy();
                    for (int i = 0; i <= 3; i++)
                    {
                        answersQ2[i].LateDestroy();
                        Console.WriteLine("Answers 2 gone");
                    }

                    createAnswersQ3();
                    _doOnceCreateAnswers3 = false;
                    Console.WriteLine("answers3 are now there");
                }
                   //antwoord 3 gedaan maak vraag 4(laatste)
                //if (_doOnceCreateAnswers4 && _isQ3Answered)
                //{
                //    _infoQ3.LateDestroy();

                //    for (int i = 0; i <= 3; i++)
                //    {
                //        answersQ3[i].LateDestroy();
                //        Console.WriteLine("Answers 3 gone");
                //    }

                //    createAnswersQ4();
                //    _doOnceCreateAnswers4 = false;
                //    Console.WriteLine("answers4 are now there");
                //}

            }
        
    }


    //------------------------------
    //// everything from QEUSTION 2
    //------------------------------
    private void createAnswersQ2()
    {
        _Q2A1 = new Answers("Q2AnswA" + _puzzleID + ".png", new Vec2(225, 390), 2, 1);
        AddChild(_Q2A1);
        answersQ2[0] = _Q2A1;

        _Q2A2 = new Answers("Q2AnswB" + _puzzleID + ".png", new Vec2(490, 390), 2, 1);
        AddChild(_Q2A2);
        answersQ2[1] = _Q2A2;

        _Q2A3 = new Answers("Q2AnswC" + _puzzleID + ".png", new Vec2(225, 590), 2, 1);
        AddChild(_Q2A3);
        answersQ2[2] = _Q2A3;

        _Q2A4 = new Answers("Q2AnswD" + _puzzleID + ".png", new Vec2(490, 590), 2, 1);
        AddChild(_Q2A4);
        answersQ2[3] = _Q2A4;
    }

    private void createInfoQ2()
    {
        _infoQ2 = new Information("infoQ1"+ _puzzleID +".png", new Vec2(655, 105), 2, 1);
        AddChild(_infoQ2);
    }

    private void checkAnswerQ2()
    {
        if (answersQ2 != null && _doOnceCreateAnswers2 == false)
        {
            if (answersQ2[3]._hover && Input.GetMouseButtonDown(0) && _infoQ2._isCollected == false)
            {
                //onderstaande zin is voor alpha, van 0 naar 1
                _infoQ2._isCollected = true;

                _infoQ2._whichInfo = 0;

                _isQ2Answered = true;

                Sound effect = new Sound("Correct_sound");
                effect.Play();

                Console.WriteLine("question2 right!");
            }
            else if ((answersQ2[0]._hover || answersQ2[1]._hover || answersQ2[2]._hover) &&
            Input.GetMouseButtonDown(0) && _infoQ2._isCollected == false)
            {

                _infoQ2._isCollected = true;

                _infoQ2._whichInfo = 1;

                _isQ2Answered = true;

                Sound effect = new Sound("False.wav");
                effect.Play();

                Console.WriteLine("question2 wrong");
            }

            if (_isQ2Answered && _doOnceCreateContinueButt2)
            {
                _continue.alpha = 1f;
                _doOnceCreateContinueButt2 = false;

                for (int i = 0; i <= 3; i++)
                {
                    answersQ2[i].SetFrame(1);
                }
            }
        }
    }

    //------------------------------
    //// everything from QEUSTION 3
    //------------------------------
    private void createAnswersQ3()
    {
        _Q3A1 = new Answers("Q3AnswA" + _puzzleID + ".png", new Vec2(225, 390), 2, 1);
        AddChild(_Q3A1);
        answersQ3[0] = _Q3A1;

        _Q3A2 = new Answers("Q3AnswB" + _puzzleID + ".png", new Vec2(490, 390), 2, 1);
        AddChild(_Q3A2);
        answersQ3[1] = _Q3A2;

        _Q3A3 = new Answers("Q3AnswC" + _puzzleID + ".png", new Vec2(225, 590), 2, 1);
        AddChild(_Q3A3);
        answersQ3[2] = _Q3A3;

        _Q3A4 = new Answers("Q3AnswD" + _puzzleID + ".png", new Vec2(490, 590), 2, 1);
        AddChild(_Q3A4);
        answersQ3[3] = _Q3A4;
    }

    private void createInfoQ3()
    {
        _infoQ3 = new Information("infoQ1" + _puzzleID + ".png", new Vec2(655, 105), 2, 1);
        AddChild(_infoQ3);
    }

    private void checkAnswerQ3()
    {
        if (answersQ3 != null && _doOnceCreateAnswers3 == false)
        {
            if (answersQ3[1]._hover && Input.GetMouseButtonDown(0) && _infoQ3._isCollected == false)
            {
                //onderstaande zin is voor alpha, van 0 naar 1
                _infoQ3._isCollected = true;

                _infoQ3._whichInfo = 0;

                _isQ3Answered = true;

                Sound effect = new Sound("Correct_sound.wav");
                effect.Play();

                Console.WriteLine("question3 right!");
            }
            else if ((answersQ3[0]._hover || answersQ3[2]._hover || answersQ3[3]._hover) &&
                    Input.GetMouseButtonDown(0) && _infoQ3._isCollected == false)
            {

                _infoQ3._isCollected = true;

                _infoQ3._whichInfo = 0;

                _isQ3Answered = true;

                Sound effect = new Sound("False.wav");
                effect.Play();

                Console.WriteLine("question3 wrong");
            }

                    _infoQ3._whichInfo = 1;

                    _isQ3Answered = true;

                    Console.WriteLine("question3 wrong");
                 }

            if (_isQ3Answered && _doOnceCreateContinueButt3)
            {
                _continue.LateDestroy();
                _lastDiggingbutton = new Button("arrowR_spritesheet_2.png", new Vec2(900, 700), 7, 1);
                AddChild(_lastDiggingbutton);
                _doOnceCreateContinueButt3 = false;

                for (int i = 0; i <= 3; i++)
                {
                    answersQ3[i].SetFrame(1);
                }
            }
        }
    }

}

