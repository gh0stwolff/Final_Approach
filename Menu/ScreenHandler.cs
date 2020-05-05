using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;

class ScreenHandler : GameObject
{
    Menu _menu;
    Collections _collect;

    enum State
    {
        MENU,
        COLLECTIONS,
    }

    private State _state;

    private bool _isPlayerInMenu; 

    public ScreenHandler() : base()
    {
       

    }

    public void Update()
    {
        handleState();
    }

    public void handleState()
    {
        switch (_state)
        {
            case State.MENU:
                handleMenuScene();
                break;

            case State.COLLECTIONS:
               handleCollectionsScene();
                break;
        }
    }

    private void setState(State newState)
    {
        if (_state != newState)
        {
            _state = newState;
        }
    }


    private void handleMenuScene()
    {
        Console.WriteLine("does something");
        if (_menu == null)
        {
            Console.WriteLine("WORKS");
            _menu = new Menu();
            AddChild(_menu);
        }

        if (Input.GetKeyDown(Key.C))
        {
            setState(State.COLLECTIONS);
            if (_menu != null)
            {
                _menu.LateDestroy();
                _menu = null;
            }
        }
    }

    private void handleCollectionsScene()
    {
        Console.WriteLine("PPPPPPPPPP");
        if (_collect == null)
        {
            _collect = new Collections();
            AddChild(_collect);
        }
      
        if (Input.GetKeyDown(Key.M))
        {
            setState(State.MENU);
            if (_collect != null)
            {
                _collect.LateDestroy();
                _collect = null;
            }
        }
    }
}
