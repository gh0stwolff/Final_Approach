using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Schema;
using GXPEngine;

public class MixAndMatch : Canvas
{
    static private int _amountOfPieces = 16;
    private int _horizontalPieces = 4;
    private int _verticalPieces = 4;

    private int _startXGrid = 100;
    private int _startYGrid = 100;
    private int _distanceBetweenPieces = 20;

    private int _firstPiece = -1;
    private int _secondPiece = -1;
    private int _piecesSelected = 0;

    private float timeDelta = 0;

    private bool _doOnce = true;

    ClickablePiece[] pieces = new ClickablePiece[_amountOfPieces];
    int[] IDs = new int[_amountOfPieces];

    ClickablePiece click1;
    CollectedMM _collection;

    public MixAndMatch(int width, int height) : base(width, height)
    {
        click1 = new ClickablePiece("memoryTiles.png", new Vec2(0, 0), 0);
        loadPieces(_horizontalPieces, _verticalPieces);
        _collection = new CollectedMM(new Vec2(0, 0), ((MyGame)game).width, ((MyGame)game).height, _amountOfPieces/2);
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

        Randomizer.Randomize(IDs);

        for (int x = _startXGrid; x < _startXGrid + pieceWidth * colmns; x += pieceWidth)
        {
            for (int y = _startYGrid; y < _startYGrid + pieceHeight * rows; y += pieceHeight)
            {
                ClickablePiece click = new ClickablePiece("memoryTiles.png", new Vec2(x, y), IDs[i]);
                AddChild(click);

                pieces[i] = click;
                Console.WriteLine("i = {0}, ID = {1}", i, IDs[i]);
                i++;
            }
        }
    }

    public void Update()
    {

        handlePresses();
        checkMatches();
        debug();
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
            for (int i = 0; i < pieces.Length; i++)
            {
                pieces[i].CheckPressed();
            }
        }
        if (_firstPiece >= 0) { pieces[_firstPiece].CheckPressed(); }
        if (_secondPiece >= 0) { pieces[_secondPiece].CheckPressed(); }
        
    }

    private void checkMatches()
    {
        for(int i = 0; i < pieces.Length; i++)
        {
            if (pieces[i] != null)
            {
                if (pieces[i].Pressed)
                {
                    if (_firstPiece == -1)
                    {
                        _firstPiece = i;
                        _piecesSelected++;
                        Console.WriteLine("index = {1}, ID = {0}", pieces[_firstPiece].ID, i);
                    }
                    else if (_secondPiece == -1)
                    {
                        _secondPiece = i;
                        _piecesSelected++;
                        Console.WriteLine("index = {1}, ID = {0}", pieces[_secondPiece].ID, i);
                    }
                }
            }
        }

        if (_firstPiece != -1 && _secondPiece != -1)
        {
            if (pieces[_firstPiece].ID == pieces[_secondPiece].ID)
            {
                pieces[_firstPiece].Pressed = false;
                pieces[_secondPiece].Pressed = false;
                pieces[_firstPiece].SelfDestroy();
                pieces[_secondPiece].SelfDestroy();
                if (_doOnce)
                {
                    _collection.Collected(pieces[_firstPiece].ID);
                    _doOnce = false;
                }
                resetChoices();
            }
            else
            {
                resetChoices();
            }
        }
    }

    private void resetChoices()
    {
        int time = 1000;
        timeDelta += Time.deltaTime;
        if (timeDelta > time)
        {
            timeDelta = 0;
            for (int i = 0; i < pieces.Length; i++)
            {
                //if (pieces[i] != null)
                //{
                    Console.WriteLine("index = {0} Cleared!", i);
                    pieces[i].clearSelection();
                //}
            }

            //pieces[_firstPiece] = null;
            //pieces[_secondPiece] = null;
            _firstPiece = -1;
            _secondPiece = -1;
            _piecesSelected = 0;
            _doOnce = true;
        }
    }
}