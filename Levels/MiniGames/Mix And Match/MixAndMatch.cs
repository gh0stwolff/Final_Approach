using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Schema;
using GXPEngine;

public class MixAndMatch : Canvas
{
    private int _amountOfPieces = 16;
    static private int _horizontalPieces = 4;
    static private int _verticalPieces = 4;

    private int _startXGrid = 175;
    private int _startYGrid = 175;
    private int _distanceBetweenPieces = 5;

    private int _firstPiece = -1;
    private int _secondPiece = -1;
    private int _piecesSelected = 0;

    private float timeDelta = 0;

    private bool _reset = false;
    private bool _showDoneText = false;
    public bool _IsGameFinished = false;
    private bool _doOnce = true;

    Button _buttonMemmDone;

    List<ClickablePiece> pieces = new List<ClickablePiece>();
    int[] IDs;
    Vec2[] points = new Vec2[_horizontalPieces * _verticalPieces];

    ClickablePiece click1;
    CollectedMM _collection;

    public MixAndMatch(int width, int height, int pieceCount) : base(width, height)
    {
        Sprite background = new Sprite("mixMatchFrame.png");
        AddChild(background);
        _amountOfPieces = pieceCount;
        IDs = new int[_amountOfPieces];

        click1 = new ClickablePiece("memoryTiles.png", new Vec2(0, 0), 0);

        loadPieces(_horizontalPieces, _verticalPieces);

        string fileNameInfo = "memoryTiles.png";
        if (pieceCount <= 8) { fileNameInfo = "INFOeasyMixmatch.png"; }
        else if (pieceCount > 8 && pieceCount <= 12) { fileNameInfo = "INFOmediumMixmatch.png"; }
        else if (pieceCount > 12) { fileNameInfo = "INFOhardMixmatch.png"; }

        _collection = new CollectedMM(new Vec2(0, 0), ((MyGame)game).width, ((MyGame)game).height, _amountOfPieces / 2, fileNameInfo);
        AddChild(_collection);
    }

    private void loadPieces(int colmns, int rows)
    {
        int i = 0;
        int pieceWidth = click1.width + _distanceBetweenPieces;
        int pieceHeight = click1.height + _distanceBetweenPieces;

        for (int j = 0; j < IDs.Length; j++)
        {
            IDs[j] = j / 2;
        }

        for (int x = _startXGrid; x < _startXGrid + pieceWidth * colmns; x += pieceWidth)
        {
            for (int y = _startYGrid; y < _startYGrid + pieceHeight * rows; y += pieceHeight)
            {
                points[i] = new Vec2(x, y);
                i++;
            }
        }

        Randomizer.Randomize(points);
        Randomizer.Randomize(IDs);

        for (int l = 0; l < _amountOfPieces; l++)
        {
            ClickablePiece click = new ClickablePiece("memoryTiles.png", points[l], IDs[l]);
            AddChild(click);
            pieces.Add(click);
            Console.WriteLine("i = {0}, ID = {1}", l, IDs[l]);
        }
    }

    public void Update()
    {
        handlePresses();
        checkMatches();
        //debug();
        resetChoices();
        checkIfDone();


        if (_showDoneText && _buttonMemmDone.Pressed)
        {
            _buttonMemmDone.LateDestroy();
            _IsGameFinished = true;
            _showDoneText = false;
        }

    }

    private void checkIfDone()
    {
        if (pieces.Count == 0)
        {
            if ( _showDoneText == false)
            {
                ((MyGame)game).GoodJob();
                memmDoneButton();
                if (_doOnce)
                {
                    Sound effect = new Sound("Minigame_end.wav");
                    effect.Play();
                    _doOnce = false;
                }
                _showDoneText = true;
            }

        }
    }


    private void memmDoneButton()
    {
        _buttonMemmDone = new Button("arrowR_spritesheet_2.png", new Vec2(900, 700), 7, 1);
        AddChild(_buttonMemmDone);
    }

    private void debug()
    {
        if (Input.GetKeyDown(Key.ONE)) { _collection.Collected(0); }
        if (Input.GetKeyDown(Key.TWO)) { _collection.Collected(1); }
        if (Input.GetKeyDown(Key.THREE)) { _collection.Collected(2); }
        if (Input.GetKeyDown(Key.FOUR)) { _collection.Collected(3); }
        if (Input.GetKeyDown(Key.FIVE)) { _collection.Collected(4); }
        if (Input.GetKeyDown(Key.SIX)) { _collection.Collected(5); }
        if (Input.GetKeyDown(Key.SEVEN)) { _collection.Collected(6); }
        if (Input.GetKeyDown(Key.EIGHT)) { _collection.Collected(7); }
    }

    private void handlePresses()
    {
        if (_piecesSelected < 2)
        {
            for (int i = 0; i < pieces.Count; i++)
            {
                pieces[i].CheckPressed();
            }
        }
        if (_firstPiece >= 0) { pieces[_firstPiece].CheckPressed(); }
        if (_secondPiece >= 0) { pieces[_secondPiece].CheckPressed(); }

    }

    private void checkMatches()
    { 
        for(int i = 0; i < pieces.Count; i++)
        {
            if (pieces[i].Pressed)
            {
                if (_firstPiece == -1)
                {
                    _firstPiece = i;
                    _piecesSelected++;
                    Console.WriteLine("A index = {1}, ID = {0}", pieces[_firstPiece].ID, i);
                }
                else if (_secondPiece == -1 && i != _firstPiece)
                {
                    _secondPiece = i;
                    _piecesSelected++;
                    Console.WriteLine("B index = {1}, ID = {0}", pieces[_secondPiece].ID, i);
                }

                //Console.WriteLine("Pressed"+i);
            }
        }

        if (_firstPiece != -1 && _secondPiece != -1)
        {
            if (pieces[_firstPiece].ID == pieces[_secondPiece].ID)
            {
                ClickablePiece firstPiece = pieces[_firstPiece];
                ClickablePiece secondPiece = pieces[_secondPiece];

                _collection.Collected(firstPiece.ID);

                _firstPiece = -1;
                _secondPiece = -1;
                pieces.Remove(firstPiece);
                pieces.Remove(secondPiece);
                firstPiece.SelfDestroy();
                secondPiece.SelfDestroy();

                _reset = true;
            }
            else
            {
                _reset = true;
            }
        }
    }

    private void resetChoices()
    {
        if (_reset)
        {
            int time = 1000;
            timeDelta += Time.deltaTime;
            if (timeDelta > time)
            {
                timeDelta = 0;
                for (int i = 0; i < pieces.Count; i++)
                {
                    //Console.WriteLine("index = {0} Cleared!", i);
                    pieces[i].clearSelection();
                }

                _firstPiece = -1;
                _secondPiece = -1;
                _piecesSelected = 0;
                _reset = false;
            }
        }
    }
}