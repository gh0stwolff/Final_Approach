using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;

class ScreenHandler : GameObject
{
    Button _buttonPlay;
    Menu _menu;
    Collections _collect;


    enum Scene
    {
        MENU,
        COLLECTIONS,
    }

    private Scene _scene;

    //private bool _isPlayerInMenu;

    public ScreenHandler() : base()
    {


    }

    public void Update()
    {
        handleScene();
    }

    public void handleScene()
    {
        switch (_scene)
        {
            case Scene.MENU:
                handleMenuScene();
                break;

            case Scene.COLLECTIONS:
                handleCollectionsScene();
                break;
        }
    }

    private void setScene(Scene newScene)
    {
        if (_scene != newScene)
        {
            _scene = newScene;
        }
    }

    private void handleButtons()
    {
        
    }


    private void handleMenuScene()
    {
        if (_menu == null)
        {
            _menu = new Menu();
            AddChild(_menu);

            Vec2 myVec = new Vec2(512, 384);

            _buttonPlay = new Button("testbutton.png", myVec);
            AddChild(_buttonPlay);
        }

        if (_buttonPlay.Pressed)
        {
            setScene(Scene.COLLECTIONS);
            if (_menu != null)
            {
                _menu.LateDestroy();
                _buttonPlay.LateDestroy();
                _menu = null;
            }
        }
    }

    private void handleCollectionsScene()
    {
        if (_collect == null)
        {
            _collect = new Collections();
            AddChild(_collect);
        }

        if (Input.GetKeyDown(Key.M))
        {
            setScene(Scene.MENU);
            if (_collect != null)
            {
                _collect.LateDestroy();
                _collect = null;
            }
        }
    }
}
