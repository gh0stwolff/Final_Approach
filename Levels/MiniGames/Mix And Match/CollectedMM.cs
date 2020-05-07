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

    Vec2[] collectionPoints;

    public CollectedMM(Vec2 position, int width, int height, int Matches) : base(width, height)
    {
        x = position.x;
        y = position.y;
        _spawnPoint = new Vec2(width / 2, height / 2);
        pointsY = height - _offset;
        collectionPoints = new Vec2[Matches];
        setCollectionPoints();
        silhouette();
    }

    private void setCollectionPoints()
    {
        for (int i = 0; i < collectionPoints.Length; i++)
        {
            collectionPoints[i] = new Vec2((height / collectionPoints.Length * i) + _offset, pointsY);
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

    public void Update()
    {

    }

    public void Collected(int ID)
    {
        ItemMM item = new ItemMM(_spawnPoint, collectionPoints[ID], ID);
        AddChild(item);
    }

}
