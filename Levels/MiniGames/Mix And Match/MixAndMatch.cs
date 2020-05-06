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

    private float timeDelta = 0;

    ClickablePiece[] pieces = new ClickablePiece[_amountOfPieces];
    int[] IDs = new int[_amountOfPieces];

    ClickablePiece click1;

    public MixAndMatch(int width, int height) : base(width, height)
    {
        click1 = new ClickablePiece("tilesColor.png", new Vec2(0, 0), 0);
        loadPieces(_horizontalPieces, _verticalPieces);
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
                ClickablePiece click = new ClickablePiece("tilesColor.png", new Vec2(x, y), IDs[i]);
                AddChild(click);
                pieces[i] = click;
                Console.WriteLine("i = {0}, ID = {1}", i, IDs[i]);
                i++;
            }
        }
    }

    public void Update()
    {
        checkMatches();
    }

    private void checkMatches()
    {
        for(int i = 0; i < pieces.Length; i++)
        {
            if (pieces[i].Pressed)
            {
                if (_firstPiece == -1)
                {
                    _firstPiece = i;
                    Console.WriteLine("ID = {0}", pieces[_firstPiece].ID);
                }
                else if (_secondPiece == -1)
                {
                    _secondPiece = i;
                    Console.WriteLine("ID = {0}", pieces[_secondPiece].ID);
                }
            }
        }

        if (_firstPiece != -1 && _secondPiece != -1)
        {
            if (pieces[_firstPiece].ID == pieces[_secondPiece].ID)
            {
                pieces[_firstPiece].LateDestroy();
                pieces[_secondPiece].LateDestroy();
                //Timer timer = new Timer(1000, resetChoices);
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
            pieces[_firstPiece].clearSelection();
            pieces[_secondPiece].clearSelection(); ;

            _firstPiece = -1;
            _secondPiece = -1;
        }
    }
}