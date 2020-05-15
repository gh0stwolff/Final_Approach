using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;

public class Jigsaw : GameObject
{
    private int _amountOfPieces;

    private int _startPointX = 154;
    private int _startPointY = 204;

    //private int _widthPiece = ;
    //private int _heightPiece = ;

    private int _amountPiecesX;
    private int _amountPiecesY;

    private int _pWidth;
    private int _pHeight;

    int deltaTime = 2000;
    int targetTime = 0;

    static private int _amountTargets = 3;

    private bool _isFinished;
    private bool _showBetweenText = false;
    public bool _IsGameFinished = false;
    private bool _doOnce = true;
    private bool _doOnce2 = true;

    private Sprite _background;

    Vec2[] points;
    Vec2[] targetPoints;
    Pieces[] piece;

    Bones _bone1, _bone2, _bone3;

    Button _buttonJigsawDone;


    Information _info;


    public Jigsaw(int difficulty) : base()
    {
        createBackground();

        
        setupList(difficulty);
    }

    private void setupList(int difficulty)
    {
        if (difficulty == 1) { load1(); }
        else if (difficulty == 2) { load2(); }
        else if (difficulty == 3) { load3(); }
    }

    //easy
    private void load1()
    {
        _amountOfPieces = 8;
        _amountPiecesX = 4;
        _amountPiecesY = 2;

        Pieces _piece = new Pieces("jigsaweasy.png", new Vec2(), 1,  _amountPiecesX, _amountPiecesY);

        _piece.scale = 1f;
        _pWidth = _piece.width;
        _pHeight = _piece.height;

        targetPoints = new Vec2[_amountOfPieces];
        points = new Vec2[_amountOfPieces];
        piece = new Pieces[_amountOfPieces];


        createInfo("INFOeasyJigsaw.png");

        createPiece("jigsaweasy.png", _amountPiecesX, _amountPiecesY, _piece.width/2 +25, _piece.height + 15, 1, 8);
        createPoints(_amountPiecesX, _amountPiecesY, _pWidth, _pHeight);
        createTargetPoints();

        createBones();
    }

    //medium
    private void load2()
    {
        _amountOfPieces = 12;

        _amountPiecesX = 3;
        _amountPiecesY = 4;

        Pieces _piece = new Pieces("jigsaweasy.png", new Vec2(), 1, _amountPiecesX, _amountPiecesY);

        _piece.scale = 1f;
        _pWidth = _piece.width;
        _pHeight = _piece.height;


        targetPoints = new Vec2[_amountOfPieces];
        points = new Vec2[_amountOfPieces];
        piece = new Pieces[_amountOfPieces];

        createInfo("INFOmediumJigsaw.png");

        createPiece("jigsawmedium.png", _amountPiecesX, _amountPiecesY, _piece.width/2 +25, _piece.height/ 2 +15, 2, 6);
        createPoints(_amountPiecesX, _amountPiecesY, _pWidth, _pHeight, 15);
        createTargetPoints();

        createBones();
    }

    //hard
    private void load3()
    {
        _amountOfPieces = 16;

        _amountPiecesX = 4;
        _amountPiecesY = 4;

        Pieces _piece = new Pieces("jigsaweasy.png", new Vec2(), 1, _amountPiecesX, _amountPiecesY);

        _piece.scale = 1f;
        _pWidth = _piece.width;
        _pHeight = _piece.height;

        targetPoints = new Vec2[_amountOfPieces];
        points = new Vec2[_amountOfPieces];
        piece = new Pieces[_amountOfPieces];

        createInfo("INFOhardJigsaw.png");

        createPiece("jigsawhard.png", _amountPiecesX, _amountPiecesY, _piece.width/2 + 15, _piece.height/2 + 15, 2, 8);
        createPoints(_amountPiecesX, _amountPiecesY, _pWidth, _pHeight);
        createTargetPoints();

        createBones();
    }

    public void Update()
    {
        checkClickingPoints();
        noOverlapPieces();
        checkPuzzleSucceeded();
        showCorrectInfo();
        
        if (_doOnce && _isFinished && _buttonJigsawDone.Pressed)
        {
            _buttonJigsawDone.LateDestroy();
            _IsGameFinished = true;
            _doOnce = false;
        }



    }

    private void createBackground()
    {
        _background = new Sprite("backgroundjigsaw.png");
        AddChild(_background);

    }

    private void createPiece(string filename, int amountPiecesX, int amountPiecesY, int _distanceX, int _distanceY, int spawncountX, int spawncountY)
    {
        // deze draaien bij puzzle easy //original 120
        int _offset = 160;
        //original 110
        // = 140;

        int l = 0;

        Vec2[] _spawnPoints = new Vec2[_amountOfPieces];


        for (int i = 0; i < spawncountX; i++)
        {
            for (int j = 0; j < spawncountY; j++)
            {
                {
                    _spawnPoints[l] = new Vec2((_offset + _distanceX * j), (_distanceY * i + 600));
                    l++;
                }
            }
        }

        Randomizer.Randomize(_spawnPoints);

        for (int i = 0; i < _spawnPoints.Length; i++)
        {
            Pieces _piece = new Pieces(filename, _spawnPoints[i], i, amountPiecesX, amountPiecesY);
            AddChild(_piece);
            piece[i] = _piece;
        }

    }

    private void createInfo(string filename)
    {
        _info = new Information(filename, new Vec2(650, 105), 3, 1);
        AddChild(_info);
    }

    private void createBones()
    {
        _bone1 = new Bones("collectBone.png", points[0], targetPoints[0]);
        AddChild(_bone1);

        _bone2 = new Bones("collectBone.png", points[2], targetPoints[1]);
        AddChild(_bone2);

        _bone3 = new Bones("collectBone.png", points[7], targetPoints[2]);
        AddChild(_bone3);
    }

    private void createPoints(int columns, int rows, int pieceWidth, int pieceHeight, int offSet = 0)
    {
        int i = 0;        

        for (int y = _startPointY; y < _startPointY + pieceHeight * rows; y += pieceHeight)
        {
            for (int x = _startPointX; x < _startPointX + pieceWidth * columns; x += pieceWidth)
            {
                points[i] = new Vec2(x + offSet, y);
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


    private void jigsawDoneButton()
    {
        _buttonJigsawDone = new Button("arrowR_spritesheet_2.png", new Vec2(900, 700), 7, 1);
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
                        piece[i].scale = 0.6f;
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
            if (_doOnce2)
            {
                Sound effect = new Sound("Minigame_end.wav");
                effect.Play();
                _doOnce2 = false;
            }
            _showBetweenText = true;
            ((MyGame)game).GoodJob();
            jigsawDoneButton();
            targetTime = Time.time + deltaTime;
        }
    }


    private void checkClickingPoints()
    {
        Vec2 mousePos = new Vec2(Input.mouseX, Input.mouseY);

        for (int i = 0; i < points.Length; i++)
        {
            Vec2 Delta = points[i] - mousePos;
            if(Input.GetMouseButtonDown(0) &&  Delta.Length() <= (piece[1].height + piece[1].width)/4 )
            {
               for (int j = 0; j < piece.Length; j++)
                {
                    if(piece[j]._isSelected)
                    {
                        piece[j]._position = points[i];
                        piece[j].updatePos();

                        Sound effect = new Sound("Puzzle_zap.wav");
                        effect.Play();

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