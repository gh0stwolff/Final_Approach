using GXPEngine;
using System;

public class Timer : GameObject
{

    private int _time;
    Action _onTimeOut;

    public Timer(int time, Action ontimeout)
    {
        _time = time;
        _onTimeOut = ontimeout;
    }

    public void Update()
    {
        _time -= Time.deltaTime;
        if (_time <= 0)
        {
            _onTimeOut();
            Destroy();
        }

    }

}