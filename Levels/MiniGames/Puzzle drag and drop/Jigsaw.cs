using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;

public class Jigsaw : GameObject
{
    static private int _amountOfPieces = 4;

    Vec2 _oldPosition1;


    Pieces _piece1, _piece2, _piece3, _piece4;


    public Jigsaw() : base()
    {
        Pieces[] pieces = new Pieces[_amountOfPieces];
        int[] places = new int[_amountOfPieces];

        //Pieces[] places = new Pieces[4];
        //int[] places = new int[];


        //createPiece();
        _oldPosition1.x = 120;
        _oldPosition1.y = 150;
        createPiece2();

    }

    public void Update()
    {
        movePieces();
        if (Input.GetMouseButtonDown(0))
        {
            Console.WriteLine(Input.mouseX);
            Console.WriteLine(Input.mouseY);
        }
    }

    private void createPiece2()
    {

        _piece1 = new Pieces("puzzle.png", new Vec2(120,150), 0);
        AddChild(_piece1);
        _piece2 = new Pieces("puzzle.png", new Vec2(120, 600), 1);
        AddChild(_piece2);
        _piece3 = new Pieces("puzzle.png", new Vec2(350, 150), 2);
        AddChild(_piece3);
        _piece4 = new Pieces("puzzle.png", new Vec2(350, 600), 3);
        AddChild(_piece4);
    }

    private void movePieces()
    {
        //if(Input.GetMouseButtonDown(0) && Input.mouseX >= 470)
        //{
        //    _piece1.x = Input.mouseX;
        //    _piece1.y = Input.mouseY;
        //}

        if (Input.GetMouseButtonDown(0) && _piece1._isSelected == true)
        {
            _piece1.x = Input.mouseX;
            _piece1.y = Input.mouseY;
        }
        if (Input.GetMouseButton(0) && _piece2._isSelected == true)
        {
            _piece2.x = Input.mouseX;
            _piece2.y = Input.mouseY;
        }
        if (Input.GetMouseButton(0) && _piece3._isSelected == true)
        {
            _piece3.x = Input.mouseX;
            _piece3.y = Input.mouseY;
        }
        if (Input.GetMouseButton(0) && _piece4._isSelected == true)
        {
            _piece4.x = Input.mouseX;
            _piece4.y = Input.mouseY;
        }

    }

    private void createPiece()
    {
        int place = 0;
        int i = 0;


        for (int x = 150; x < 550; x += 200)
        {
            for (int y = 150; y < 768; y += 400)
            {
                Pieces _piece = new Pieces("puzzle.png", new Vec2(x, y), place);
                AddChild(_piece);

                //pieces[i] = _piece;

                place++;
                Console.WriteLine(place);
                Console.WriteLine(_piece);
            }
        }
    }
}