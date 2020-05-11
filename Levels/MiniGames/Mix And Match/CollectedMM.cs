using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;

class CollectedMM : Canvas
{
    Vec2 _spawnPoint;

    private int pointsY;

    private int _offset = 100;

    private int _active = -1;

    private AnimSprite _infoBoard;

    private Vec2[] collectionPoints;
    private List<ItemMM> items = new List<ItemMM>();

    public CollectedMM(Vec2 position, int width, int height, int Matches) : base(width, height)
    {
        x = position.x;
        y = position.y;
        _spawnPoint = new Vec2(width / 2, height / 2);
        pointsY = height - _offset;
        collectionPoints = new Vec2[Matches];
        setCollectionPoints();
        silhouette();
        setupInfoBoard();
    }

    private void setCollectionPoints()
    {
        for (int i = 0; i < collectionPoints.Length; i++)
        {
            collectionPoints[i] = new Vec2((width / collectionPoints.Length * i) + _offset, pointsY);
        }
    }

    private void silhouette()
    {
        for (int i = 0; i < collectionPoints.Length; i++)
        {
            ItemMM item = new ItemMM(collectionPoints[i], i);
            AddChild(item);
        }
    }
    private void setupInfoBoard()
    {
        _infoBoard = new AnimSprite("memoryTiles.png", 9, 1);
        AddChild(_infoBoard);
        _infoBoard.SetXY(800, 200);
        _infoBoard.alpha = 0.0f;
    }

    public void Update()
    {
        updateInfoBoard();
    }

    public void Collected(int ID)
    {
        ItemMM item = new ItemMM(_spawnPoint, collectionPoints[ID], ID);
        AddChild(item);
        items.Add(item);
    }

    private void updateInfoBoard()
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].pressed && i != _active)
            {
                _active = i;
                _infoBoard.SetFrame(items[i].ID + 1);

                if(_infoBoard.alpha != 1.0f)
                {
                    _infoBoard.alpha = 1.0f;
                }
            }
        }
    }

}
