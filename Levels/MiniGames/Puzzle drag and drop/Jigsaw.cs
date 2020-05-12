using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;

public class Jigsaw : GameObject
{
    static private int _amountOfPieces = 8;

    private int _startPointX = 105;
    private int _startPointY = 145;

    private int _amountPiecesX = 4;
    private int _amountPiecesY = 2;

    static private int _amountTargets = 3;

    private bool _isFinished;
    private bool _doOnce = true;
    private bool _showBetweenText = false;

    private Sprite _background;

    private Sprite _jigsawDoneText;

    Vec2[] points = new Vec2[_amountOfPieces];
    Vec2[] targetPoints = new Vec2[_amountTargets];
    Pieces[] piece = new Pieces[_amountOfPieces];

    Bones _bone1, _bone2, _bone3;

    Button _buttonJigsawDone;

    Information _info;


    public Jigsaw() : base()
    {
        createBackground();

        createInfo();

        
        createPiece();
        createPoints(_amountPiecesX, _amountPiecesY);
        createTargetPoints();
        createBones();
        
    }

    public void Update()
    {
        checkClickingPoints();
        checkPuzzleSucceeded();
        noOverlapPieces();
        showCorrectInfo();



        if (_doOnce && _isFinished == true && Input.GetMouseButtonDown(0) && _showBetweenText == true && _buttonJigsawDone._hover)
        {
            _buttonJigsawDone.LateDestroy();
            Console.WriteLine("DOEDOE");
            createQuiz();
            _doOnce = false;            
        }          
            
    }

    private void createBackground()
    {
        _background = new Sprite("testbackground.png");
        AddChild(_background);
        _background.alpha = 0.2f;
    }

    private void createPiece()
    {
        int _offset = 90;
        int _distance = 120;
        
        for (int i = 0; i < piece.Length; i++)
        {
            Pieces _piece = new Pieces("puzzleeasy.png", new Vec2((_offset + _distance * i), 650), i, _amountPiecesX, _amountPiecesY);
            AddChild(_piece);
            piece[i] = _piece;
        }        
    }

    private void createInfo()
    {
        _info = new Information("testinfojigsaw.png", new Vec2(675, 35), 3, 1);
        AddChild(_info);
    }

    private void createBones()
    {
        _bone1 = new Bones("testbone.png", points[0], targetPoints[0]);
        AddChild(_bone1);

        _bone2 = new Bones("testbone.png", points[2], targetPoints[1]);
        AddChild(_bone2);

        _bone3 = new Bones("testbone.png", points[7], targetPoints[2]);
        AddChild(_bone3);
    }

    private void createPoints(int columns, int rows)
    {
        int i = 0;

        //int pieceWidth = (piece[i].width );
        //int pieceHeight = (piece[i].height);
        int pieceWidth = 160;
        int pieceHeight = 240;

        for (int y = _startPointY; y < _startPointY + pieceHeight * rows; y += pieceHeight)
        {
            for (int x = _startPointX; x < _startPointX + pieceWidth * columns; x += pieceWidth)
            {
                points[i] = new Vec2(x, y);
                i++;
            }
        }
    }

    private void createTargetPoints()
    {
        int i = 0;

        int pieceWidth = 65;

        
            for (int x = 725; x < 725 + pieceWidth * 3; x += pieceWidth)
            {
                targetPoints[i] = new Vec2(x, 457);
                i++;
            }   
    }

    private void createQuiz()
    {
            Quiz quiz = new Quiz("quizquesttest1.png", new Vec2(25, 25));
            AddChild(quiz);
    }

    private void quizDoneText()
    {
        _buttonJigsawDone = new Button("jigsawdone.png", new Vec2(500,350), 1,1);
        AddChild(_buttonJigsawDone);
    }

    private void noOverlapPieces()
    {
        for (int i = 0; i < points.Length; i++)
        {
            for (int j = 0; j < piece.Length; j++)
            {
                if (i != j)
                {
                    //bug, sometimes a piece can overlap?
                    if (piece[i]._isSelected && piece[j]._position == piece[i]._position)
                    {
                        Console.WriteLine("pos taken");

                        piece[i].SetOriginalPosition();

                    }
                }
            }
        }
    }

    private void checkPuzzleSucceeded()
    {

        for (int i = 0; i < piece.Length; i++)
        {
            if (points[i] == piece[i]._position)
            {
                piece[i].PieceInRightPlace = true;
                
            }
        }

        for (int j = 0; j < piece.Length; j++)
        {
            if (points[j] == piece[j]._position)
            {
                _isFinished = true;
            }
            else
            {
                _isFinished = false;
                return;
            }
        }

        if (_isFinished == true && _showBetweenText == false)
        {
            quizDoneText();
            _showBetweenText = true;
        }
    }


    private void checkClickingPoints()
    {
        Vec2 mousePos = new Vec2(Input.mouseX, Input.mouseY);

        for (int i = 0; i < points.Length; i++)
        {
            Vec2 Delta = points[i] - mousePos;
            if(Input.GetMouseButtonDown(0) &&  Delta.Length() <= 100 )
            {
               for (int j = 0; j < piece.Length; j++)
                {
                    if(piece[j]._isSelected)
                    {
                        piece[j]._position = points[i];
                        piece[j].updatePos();

                        piece[j].scale = 1f;
                        Console.WriteLine(points[i]);
                    }
                }
            }
        }
        collectBones();
    }

    private void collectBones()
    {
        if (_bone1._position == piece[0]._position)
        {
            _info._isCollected = true;

            _bone1._isBoneMoving = true;

            _info._whichInfo = 0;


        }

        if (_bone2._position == piece[2]._position)
        {
            _info._isCollected = true;

            _bone2._isBoneMoving = true;

            _info._whichInfo = 1;
        }

        if (_bone3._position == piece[7]._position)
        {
            _info._isCollected = true;

            _bone3._isBoneMoving = true;

            _info._whichInfo = 2;
        }
    }

    private void showCorrectInfo()
    {
        //alleen als zze op de juiste plek staan
        if (_bone1._isPressed && _bone1._position == targetPoints[0])
        {
            _info._whichInfo = 0;
        }
        if (_bone2._isPressed && _bone2._position == targetPoints[1])
        {
            _info._whichInfo = 1;
        }
        if (_bone3._isPressed && _bone3._position == targetPoints[2])
        {
            _info._whichInfo = 2;
        }
    }
}