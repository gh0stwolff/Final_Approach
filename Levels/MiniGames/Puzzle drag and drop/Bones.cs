using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;

public class Bones : AnimSprite
{
    public Vec2 _position;

    private Vec2 _target;

    private Vec2 _velocity;

    public bool _isBoneMoving = false;
    public bool _isPressed;

    private float _speed = 6f;

    public Bones(string fileName, Vec2 position, Vec2 target) : base(fileName, 1, 1)
    {
        _position = position;
        _target = target;

        x = position.x;
        y = position.y;

        _velocity = _target - _position;
        _velocity.Normalize();
        _velocity *= _speed;

        SetOrigin(width / 2, height / 2);
    }

    public void Update()
    {
        updatePos();
        //Movement();
        handleMovementState();
        mouseIsOnBone();
    }

    public void updatePos()
    {
            x = _position.x;
            y = _position.y;
    }

    private void handleMovementState()
    {

        if (_isBoneMoving)
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

    private void mouseIsOnBone()
    {
        if (Mathf.Abs(x - Input.mouseX) <= width / 2 &&
           Mathf.Abs(y - Input.mouseY) <= height / 2 && Input.GetMouseButtonDown(0))
        {
            _isPressed = true;
        }
        else
        {
            _isPressed = false;
        }
    }

    private void Movement()
    {
        _position += _velocity;
    }
}

