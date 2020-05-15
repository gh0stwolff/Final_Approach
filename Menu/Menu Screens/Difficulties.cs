using System;
using GXPEngine;
using System.Drawing;
using System.Collections.Generic;


class Difficulties : Sprite
{
    Button _buttonEasy, _buttonMedium, _buttonHard;

    public bool EasyIsSelected = false;
    public bool MediumIsSelected = false;
    public bool HardIsSelected = false;

    public Difficulties() : base("MenuBackground.png")
    {
        CreateButtons();
    }

    public void Update()
    {
        checkButtonPressed();
    }

    public void CreateButtons()
    {

        _buttonEasy = new Button("easybutton.png", new Vec2(501, 226), 5, 1);
        AddChild(_buttonEasy);

        _buttonMedium = new Button("mediumbutton.png", new Vec2(501, 398), 5, 1);
        AddChild(_buttonMedium);

        _buttonHard = new Button("hardbutton.png", new Vec2(501, 570), 5, 1);
        AddChild(_buttonHard);

    }

    public void checkButtonPressed()
    {
        if (_buttonEasy.Pressed)
        {
            EasyIsSelected = true;
        }
        if (_buttonMedium.Pressed)
        {
            MediumIsSelected = true;
        }
        if (_buttonHard.Pressed)
        {
            HardIsSelected = true;
        }



    }
}

