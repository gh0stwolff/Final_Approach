using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;
using System.Drawing;

public class Quiz : AnimSprite
{
    public Vec2 _position;

    public int _whichquestion = 0;

    public bool _doOnceCreateContinueButt = true;
    public bool _isQ1Answered = false;

    //buttons question 1
    Answers _Q1A1, _Q1A2, _Q1A3, _Q1A4;

    Button _continueQ1;

    Information _infoQ1;

    Answers[] answersQ1 = new Answers[4];


    public Quiz(string fileName, Vec2 position) : base(fileName, 4, 1)
    {
        _position = position;

        x = position.x;
        y = position.y;

        SetFrame(0);

        createAnswersQ1();
        createInfoQ1();
        
    }

    public void Update()
    {
        updatePos();
        checkAnswerQ1();
    }

    public void updatePos()
    {
        x = _position.x;
        y = _position.y;
    }

    private void createAnswersQ1()
    {
        _Q1A1 = new Answers("Q1AnswA.png", new Vec2(170, 330), 2, 1);
        AddChild(_Q1A1);
        answersQ1[0] = _Q1A1;
        _Q1A2 = new Answers("Q1AnswB.png", new Vec2(490, 330), 2, 1);
        AddChild(_Q1A2);
        answersQ1[1] = _Q1A2;
        _Q1A3 = new Answers("Q1AnswC.png", new Vec2(170, 530), 2, 1);
        AddChild(_Q1A3);
        answersQ1[2] = _Q1A3;
        _Q1A4 = new Answers("Q1AnswD.png", new Vec2(490, 530), 2, 1);
        AddChild(_Q1A4);
        answersQ1[3] = _Q1A4;
    }

    private void createContinueButton()
    {
        _continueQ1 = new Button("testbutton.png", new Vec2(790, 590), 1,1);
        AddChild(_continueQ1);
    }

    private void createInfoQ1()
    {
        
         
        _infoQ1 = new Information("infoQ1.png", new Vec2(675, 35), 2, 1);
        AddChild(_infoQ1);
        //right answer _whichinfo = 0
        //wrong answer _whichinfo = 1;
    }

    private void checkAnswerQ1()
    {
        if ( _Q1A1._hover && Input.GetMouseButtonDown(0))
        {
            //onderstaande zin is voor alpha, van 0 naar 1
            _infoQ1._isCollected = true;

            _isQ1Answered = true;

            Console.WriteLine("question right!");
        }
        else if((answersQ1[1]._hover || _Q1A3._hover || _Q1A4._hover) && Input.GetMouseButtonDown(0))
        {

            _infoQ1._isCollected = true;

            _infoQ1._whichInfo = 1;

            _isQ1Answered = true;

            Console.WriteLine("question wrong");
        }

        if (_doOnceCreateContinueButt && _isQ1Answered)
        { 
            createContinueButton();
            _doOnceCreateContinueButt = false;
        }
    }

}

