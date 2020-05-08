using GXPEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class ItemMM : AnimSprite
{
    private float _speed = 6f;
    private float _dist;
    private float _deltaScale = 0.5f;

    private int _id;

    private bool _continue = false;
    public bool pressed = false;

    private Vec2 _position;
    private Vec2 _target;
    private Vec2 _velocity;

    public int ID
    { get { return _id;
        }
    }

    public ItemMM(Vec2 position, Vec2 target, int ID) : base("memoryTiles.png", 9, 1) 
    {
        _position = position;
        _target = target;
        _velocity = _target - _position;
        _dist = _velocity.Length();
        _velocity.Normalize();
        _velocity *= _speed;
        _id = ID;
        SetFrame(ID + 1);
        updatePosition();
    }

    public ItemMM(Vec2 position, int ID) : base("memoryTiles.png", 9, 1)
    {
        _position = position;
        _target = position;
        _id = ID;
        SetFrame(ID + 1);
        scale -= _deltaScale;
    }

    private void updatePosition()
    {
        x = _position.x;
        y = _position.y;
    }

    public void Update()
    {
        handleButtonPresses();
        updatePosition();
        handleMovementState();
    }

    private void handleButtonPresses()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!_continue)
            {
                _continue = true;
            }

            if (Mathf.Abs(_position.x - Input.mouseX) <= width/2 &&
                Mathf.Abs(_position.y - Input.mouseY) <= height/2)
            {
                pressed = true;
            } else
            {
                pressed = false;
            }
        }
    }

    private void handleMovementState()
    {
        if (_continue)
        {
            if (Mathf.Abs(_position.x - _target.x) > Mathf.Abs(_velocity.x) &&
                Mathf.Abs(_position.y - _target.y) > Mathf.Abs(_velocity.y))
            {
                Movement();
            }
            else
            {
                _position = _target;
            }
        }
    }

    private void Movement()
    {
        float frames2Target = _dist / _speed;
        float scaleReductionPerFrame = _deltaScale / frames2Target;
        _position += _velocity;
        scale -= scaleReductionPerFrame;
    }
}