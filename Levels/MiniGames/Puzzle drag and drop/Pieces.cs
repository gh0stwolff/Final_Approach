using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;

public class Pieces : AnimSprite
{
    public Vec2 _position;

    public Vec2 _originalPosition;

    public bool _isSelected;

    private bool _isMouseOnPiece;

    public bool PieceInRightPlace = false;

    private bool _doOnce = true;

    public Pieces(string fileName, Vec2 position, int puzzleID, int cols, int rows) : base(fileName, cols, rows)
    {
        _position = position;

        _originalPosition = _position;
        
        SetFrame(puzzleID);

        SetOrigin(width / 2, height / 2);

        scale = 0.6f;

        x = position.x;
        y = position.y;
    }

    public void Update()
    {
        mouseOnPiece();
        selectPiece();
        updatePos();

        if (PieceInRightPlace && _doOnce)
        {
            Sound effect = new Sound("Correct_sound.wav");
            effect.Play();
            _doOnce = false;
        }
    }

    public void updatePos()
    {
        if (PieceInRightPlace == false)
        {
            x = _position.x;
            y = _position.y;

        }
    }

    public void SetOriginalPosition()
    {
        _position = _originalPosition;
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

        if (scale >= 1)
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
        } else
        {
            if (_isSelected == false && _isMouseOnPiece && Input.GetMouseButtonDown(0) && PieceInRightPlace == false)
            {
                _isSelected = true;
                scale = 0.7f;
            }
            else if (Input.GetMouseButtonDown(0))
            {
                _isSelected = false;
                scale = 0.6f;
            }
        }
    }
}