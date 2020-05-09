using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;

public class Pieces : AnimSprite
{
    public Vec2 _position;

    public bool _isSelected;

    private bool _isMouseOnPiece;

    public bool PieceInRightPlace = false;

    public Pieces(string fileName, Vec2 position, int puzzleID) : base(fileName, 2, 2)
    {
        _position = position;

        SetFrame(puzzleID);

        SetOrigin(width / 2, height / 2);

        x = position.x;
        y = position.y;
    }

    public void Update()
    {
        mouseOnPiece();
        selectPiece();
        updatePos();
    }

    public void updatePos()
    {
        if (PieceInRightPlace == false)
        {
            x = _position.x;
            y = _position.y;

        }
    }

    private void mouseOnPiece()
    {
        if (Mathf.Abs(x - Input.mouseX) <= width / 2 &&
            Mathf.Abs(y - Input.mouseY) <= height / 2)
        {
            _isMouseOnPiece = true;
        }
        else
        {
            _isMouseOnPiece = false;
        }
    }
    private void selectPiece()
    {

        if (_isSelected == false && _isMouseOnPiece && Input.GetMouseButtonDown(0) && PieceInRightPlace == false)
        {
            _isSelected = true;
            scale = 1.2f;
        } 
        else if (Input.GetMouseButtonDown(0))
        {
            _isSelected = false;
            scale = 1f;
        }
        
    }
}