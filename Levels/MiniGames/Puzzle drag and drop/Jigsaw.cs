using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;

public class Jigsaw : GameObject
{
    static private int _amountOfPieces = 4;

    private int _startPointX = 654;
    private int _startPointY = 315;


    Vec2[] points = new Vec2[_amountOfPieces];
    Pieces[] piece = new Pieces[_amountOfPieces];


    public Jigsaw() : base()
    {
        
        createPiece();
        createPoints(2, 2);

    }

    public void Update()
    {
        checkClickingPoints();
        checkPuzzleSucceeded();
        noOverlapPieces();
    }

    private void createPiece()
    {
        int i = 0;

        for (int x = 150; x < 550; x += 200)
        {
            for (int y = 150; y < 768; y += 400)
            {
                Pieces _piece = new Pieces("puzzle.png", new Vec2(x, y), i);
                AddChild(_piece);
                piece[i] = _piece;
                i++;
            }
        }
    }

    private void noOverlapPieces()
    {
        if (piece[0]._isSelected && (piece[0]._position == piece[1]._position
            || piece[0]._position == piece[2]._position || piece[0]._position == piece[3]._position))
        {
            piece[0]._position.x = 150;
            piece[0]._position.y = 150;
            Console.WriteLine("overlappingPIECE1");
        }
        if (piece[1]._isSelected && (piece[1]._position == piece[2]._position
            || piece[1]._position == piece[3]._position || piece[1]._position == piece[0]._position))
        {
            piece[1]._position.x = 150;
            piece[1]._position.y = 550;
            Console.WriteLine("overlappingPIECE2");
        }
        if (piece[2]._isSelected && (piece[2]._position == piece[0]._position
            || piece[2]._position == piece[1]._position || piece[2]._position == piece[3]._position))
        {
            piece[2]._position.x = 350;
            piece[2]._position.y = 150;
            Console.WriteLine("overlappingPIECE3");
        }
        if (piece[3]._isSelected && (piece[3]._position == piece[0]._position
            || piece[3]._position == piece[1]._position || piece[3]._position == piece[2]._position))
        {
            piece[3]._position.x = 350;
            piece[3]._position.y = 550;
            Console.WriteLine("overlappingPIECE4");
        }
    }

    private void checkPuzzleSucceeded()
    {
        if (points[0] == piece[0]._position && points[2] == piece[1]._position
            && points[1] == piece[2]._position && points[3] == piece[3]._position)
        {
            Console.WriteLine("YEAHS1");
        }

        if (points[0] == piece[0]._position)
        {
            piece[0].PieceInRightPlace = true;            
        }
        if (points[2] == piece[1]._position)
        {
            piece[1].PieceInRightPlace = true;
        }
        if (points[1] == piece[2]._position)
        {
            piece[2].PieceInRightPlace = true;
        }
        if (points[3] == piece[3]._position)
        {
            piece[3].PieceInRightPlace = true;
        }
    }


    private void checkClickingPoints()
    {
        Vec2 mousePos = new Vec2(Input.mouseX, Input.mouseY);
      
        for(int i = 0; i < points.Length; i++)
        {
            Vec2 Delta = points[i] - mousePos;
            if(Input.GetMouseButtonDown(0) &&  Delta.Length() <= 100 )
            {
               if(piece[0]._isSelected)
                {
                    piece[0]._position = points[i];
                    piece[0].updatePos();
                    Console.WriteLine(points[i]);
                } 
                else if(piece[1]._isSelected)
                {
                    piece[1]._position = points[i];
                    piece[1].updatePos();
                } 
                else if (piece[2]._isSelected)
                {
                    piece[2]._position = points[i];
                    piece[2].updatePos();
                } 
                else if (piece[3]._isSelected)
                {
                    piece[3]._position = points[i];
                    piece[3].updatePos();
                }
            }
        }

        
    }

        private void createPoints(int columns, int rows)
    {
        int i = 0;
        int pieceWidth = piece[i].width;
        int pieceHeight = piece[i].height;

        for (int x = _startPointX; x < _startPointX + pieceWidth * columns; x += pieceWidth)
        {
            for (int y = _startPointY; y < _startPointY + pieceHeight * rows; y += pieceHeight )
            {
                points[i] = new Vec2(x,y);
                i++;
            }
        }
    }
}