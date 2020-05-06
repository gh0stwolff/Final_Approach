using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;

public class Jigsaw : GameObject
{
    
    public Jigsaw() : base()
    {

        Pieces[] places = new Pieces[4];

        int[] places = new int[];


        createPiece();

    }

    public void Update()
    {
        
       
    }

    private void createPiece()
    {
        int place = 0;


        for (int x = 150; x < 550; x += 200)
        {
            for (int y = 150; y < 768; y += 400)
            {
                Pieces _piece = new Pieces("puzzle.png", new Vec2(x, y), place);
                AddChild(_piece);
                place++;
                Console.WriteLine(place);
                Console.WriteLine(_piece);
            }
        }


        //for (int x = 0; x < 800 ; x++)
        //{
        //    for (int y = 0; y < 800; y++)
        //    {
        //        _piece1 = new Pieces("puzzle.png", new Vec2(x, y), 0);
        //        AddChild(_piece1);
        //        place++;
        //    }
        //}
        //_piece1 = new Pieces("puzzle.png", new Vec2(90, 150), 0);
        //AddChild(_piece1);
        //_piece2 = new Pieces("puzzle.png", new Vec2(90, 600), 1);
        //AddChild(_piece2);
    }
}