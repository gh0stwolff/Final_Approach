using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;

class BlockBS : Sprite
{
    public int radiusX;
    public int radiusY;

    private bool _holding;
    private bool _doOnce = true;
    private bool _movingHorizontal;

    public Vec2 _position;
    public Vec2 _oldPosition;
    private Vec2 _velocity;
    private Vec2 _mouseGrabPoint;

    private SoundChannel _channel = new SoundChannel(1);

    public Vec2 Velocity
    {
        get { return _velocity; }
    }

    public Vec2 Position
    {
        get { return _position; }
    }

    public bool Holding
    {
        get { return _holding; }
    }

    public bool CanMoveHorizontal
    {
        get { return _movingHorizontal; }
    }

    public BlockBS(string filename, Vec2 position, bool movingHorizontal) : base(filename)
    {
        _position = position;
        _movingHorizontal = movingHorizontal;
        radiusX = width / 2;
        radiusY = height / 2;
        SetOrigin(radiusX, radiusY);
    }

    public void Update()
    {
        updatePosition();
        grabbed();
        movement();
    }

    private void updatePosition()
    {
        x = _position.x;
        y = _position.y;
    }

    private void grabbed()
    {
        float deltaX = _position.x - Input.mouseX;
        float deltaY = _position.y - Input.mouseY;

        if (Mathf.Abs(deltaX) <= width/2 &&
            Mathf.Abs(deltaY) <= height/2 && Input.GetMouseButtonDown(0))
        {
            _holding = true;
            Sound effect = new Sound("Slide.wav", true);
            _channel = effect.Play();
            if (_doOnce)
            {
                _mouseGrabPoint = new Vec2(deltaX, deltaY);
                _doOnce = false;
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            _doOnce = true;
            _holding = false;
            _channel.Stop();
        }
    }

    private void movement()
    {
        if (_holding)
        {
            _oldPosition = _position;
            Vec2 MousePos = new Vec2(Input.mouseX, Input.mouseY);
            if (_movingHorizontal)
            {
                _velocity = Vec2.Lerp(_position, new Vec2(MousePos.x + _mouseGrabPoint.x, _position.y), 0.1f);

                _position.x = _velocity.x;
            }
            else
            {
                _velocity = Vec2.Lerp(_position, new Vec2(_position.x, MousePos.y + _mouseGrabPoint.y), 0.1f);

                _position.y = _velocity.y;
            }
        }
        //_velocity = _position - _oldPosition;
    }

}
