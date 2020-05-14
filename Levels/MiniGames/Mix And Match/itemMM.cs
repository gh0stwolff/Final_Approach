using GXPEngine;
using GXPEngine.Core;
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

    private Vec2 _orgSize;

    public int ID
    { get { return _id;
        }
    }

    public ItemMM(Vec2 position, Vec2 target, int ID, string fileName) : this(position, ID, fileName)
    {
        _target = target;
        _velocity = _target - _position;
        _dist = _velocity.Length();
        _velocity.Normalize();
        _velocity *= _speed;
        scale = 1.0f;
    }

    public ItemMM(Vec2 position, int ID, string fileName) : base(fileName, 8, 1)
    {
        _position = position;
        _target = position;
        _id = ID;
        SetOrigin(width / 2, 0);
        SetFrame(ID);
        updatePosition();
        _orgSize = new Vec2(width, height);

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


            Vector2 checkPoint = InverseTransformPoint(Input.mouseX, Input.mouseY);

            if (checkPoint.x > -_orgSize.x/2 && checkPoint.x <= _orgSize.x/2 &&
                checkPoint.y > 0 && checkPoint.y <= _orgSize.y)
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