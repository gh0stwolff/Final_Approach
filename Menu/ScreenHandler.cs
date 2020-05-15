using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;

class ScreenHandler : GameObject
{
    
    Menu _menu;
    Difficulties _difficulties;
    Level1Easy _level1easy;
    Level1Medium _level1medium;
    Level1Hard _level1hard;
    Collections _collect;

    private Sound _backgroundMusic;
    private SoundChannel _backgroundMusicChannel;

    enum Scene
    {
        MENU,
        COLLECTIONS,
        DIFFICULTIES,
        LEVEL1EASY,
        LEVEL1MEDIUM,
        LEVEL1HARD,
    }

    private Scene _scene;

    //private bool _isPlayerInMenu;

    public ScreenHandler() : base()
    {
        setupSound();
    }

    private void setupSound()
    {
        _backgroundMusic = new Sound("Background_music.mp3", true, true);
        _backgroundMusicChannel = new SoundChannel(1);
        _backgroundMusicChannel = _backgroundMusic.Play();
        _backgroundMusicChannel.Volume = 0.5f;
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

            case Scene.DIFFICULTIES:
                handleDifficultiesScene();
                break;

            case Scene.LEVEL1EASY:
                handleLevel1EasyScene();
                break;

            case Scene.LEVEL1MEDIUM:
                handleLevel1MediumScene();
                break;

            case Scene.LEVEL1HARD:
                handleLevel1HardScene();
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


    private void handleMenuScene()
    {
        if (_menu == null)
        {
            _menu = new Menu();
            AddChild(_menu);
            
        }

        if (_menu._buttonPressed)
        {
            { 
                setScene(Scene.DIFFICULTIES);
                if (_menu != null)
                {
                    _menu.LateDestroy();
                    _menu = null;
                }
            }
        }
    }

    private void handleDifficultiesScene()
    {
        if (_difficulties == null)
        {
            _difficulties = new Difficulties();
            AddChild(_difficulties);

        }

        if (_difficulties.EasyIsSelected)
        {
            {
                setScene(Scene.LEVEL1EASY);
                if (_difficulties != null)
                {
                    _difficulties.LateDestroy();
                    _difficulties = null;
                }
            }
        }

        else if (_difficulties.MediumIsSelected)
        {
            {
                setScene(Scene.LEVEL1MEDIUM);
                if (_difficulties != null)
                {
                    _difficulties.LateDestroy();
                    _difficulties = null;
                }
            }
        }

        else if (_difficulties.HardIsSelected)
        {
            {
                setScene(Scene.LEVEL1HARD);
                if (_difficulties != null)
                {
                    _difficulties.LateDestroy();
                    _difficulties = null;
                }
            }
        }
    }

    private void handleLevel1EasyScene()
    {
        if (_level1easy == null)
        {
            _level1easy = new Level1Easy();
            AddChild(_level1easy);

        }

        //if (_difficulties.EasyIsSelected)
        //{
        //    {
        //        setScene(Scene.LEVEL1EASY);
        //        if (_difficulties != null)
        //        {
        //            _difficulties.LateDestroy();
        //            _difficulties = null;
        //        }
        //    }
        //}
    }

    private void handleLevel1MediumScene()
    {
        if (_level1medium == null)
        {
            _level1medium = new Level1Medium();
            AddChild(_level1medium);

        }
    }

    private void handleLevel1HardScene()
    {
        if (_level1hard == null)
        {
            _level1hard = new Level1Hard();
            AddChild(_level1hard);

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
