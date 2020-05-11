using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;

class BoneSlide : Canvas
{
    private int _amountOfBlocks = 5;

    private List<BlockBS> blocks = new List<BlockBS>();
    private List<Vec2> positions = new List<Vec2>();

    public BoneSlide(int width, int height) : base(width, height)
    {
        //BlockBS block = new BlockBS("colors.png", new Vec2(200, 200), true);
        //AddChild(block);
        //BlockBS block2 = new BlockBS("checkers.png", new Vec2(400, 400), false);
        //AddChild(block2);
        setupList();
    }

    private void setupList()
    {
        positions.Add(new Vec2(100, 100));
        positions.Add(new Vec2(300, 300));
        positions.Add(new Vec2(600, 200));

        for (int i = 0; i < positions.Count; i++)
        {
            BlockBS block = new BlockBS("checkers.png", positions[i], false);
            AddChild(block);
            blocks.Add(block);
        }
        BlockBS goal = new BlockBS("colors.png", new Vec2(200, 200), true);
        AddChild(goal);
        blocks.Add(goal);
    }

    public void Update()
    {
        checkCollisions();
    }

    private void checkCollisions()
    {
        for (int i = 0; i < blocks.Count; i++)
        {
            for (int j = 0; j < blocks.Count; j++)
            {
                BlockBS thisBlock = blocks[i];
                BlockBS otherBlock = blocks[j];

                if (thisBlock != otherBlock)
                {
                    if (Mathf.Abs(otherBlock.Position.x - thisBlock.Position.x) <= thisBlock.radiusX + otherBlock.radiusX &&
                        Mathf.Abs(otherBlock.Position.y - thisBlock.Position.y) <= thisBlock.radiusY + otherBlock.radiusY)
                    {
                        handleCollisions(thisBlock, otherBlock);
                    }
                }
            }
        }
    }

    private void handleCollisions(BlockBS thisBlock, BlockBS otherBlock)
    {
        if (thisBlock.Velocity.Length() == 0) { return; }

        CollisionInfo info = timeOfImpact(thisBlock._oldPosition, thisBlock.radiusX, thisBlock.radiusY, thisBlock.Velocity, otherBlock.Position, otherBlock.radiusX, otherBlock.radiusY);

        //thisBlock._position = thisBlock.Position + thisBlock.Velocity * info.timeOfImpact;

        Vec2 point;

        if(info.horizontal)
        {
            float delta1Y = (otherBlock.Position.y + otherBlock.radiusY + thisBlock.radiusY) - thisBlock.Position.y;
            float delta2Y = (otherBlock.Position.y - otherBlock.radiusY - thisBlock.radiusY) - thisBlock.Position.y;

            if (Mathf.Abs(delta1Y) < Mathf.Abs(delta2Y)) { point = new Vec2(0, otherBlock.Position.y + otherBlock.radiusY + thisBlock.radiusY); }
            else { point = new Vec2(0, otherBlock.Position.y - otherBlock.radiusY - thisBlock.radiusY); }

            Vec2 newPos = Vec2.PointOfImpact(thisBlock._oldPosition, thisBlock.Velocity, point);
            thisBlock._position.y = newPos.y;
        }
        else
        {
            float delta1X = (otherBlock.Position.x + otherBlock.radiusX + thisBlock.radiusX) - thisBlock.Position.x;
            float delta2X = (otherBlock.Position.x - otherBlock.radiusX - thisBlock.radiusX) - thisBlock.Position.x;

            if(Mathf.Abs(delta1X) < Mathf.Abs(delta2X)) { point = new Vec2(otherBlock.Position.x + otherBlock.radiusX + thisBlock.radiusX, 0); }
            else { point = new Vec2(otherBlock.Position.x - otherBlock.radiusX - thisBlock.radiusX, 0); }

            Vec2 newPos = Vec2.PointOfImpact(thisBlock._oldPosition, thisBlock.Velocity, point);
            thisBlock._position.x = newPos.x;
        }

        
    }

    private static CollisionInfo timeOfImpact(Vec2 position, float radiusX, float radiusY, Vec2 velocity, Vec2 otherPosition, float otherRadiusX, float otherRadiusY)
    {
        float totalRadiusX = otherRadiusX + radiusX;
        float totalRadiusY = otherRadiusY + radiusY;
        float topY = otherPosition.y - totalRadiusY;
        float bottomY = otherPosition.y + totalRadiusY;
        float leftX = otherPosition.x - totalRadiusX;
        float rightX = otherPosition.x + totalRadiusX;

        float deltaTopY = topY - position.y;
        float deltaBottomY = bottomY - position.y;
        float deltaLeftX = leftX - position.x;
        float deltaRightX = rightX - position.x;

        float time1Y = deltaTopY / velocity.y;
        float time2Y = deltaBottomY / velocity.y;
        float time1X = deltaLeftX / velocity.x;
        float time2X = deltaRightX / velocity.x;

        float minY = Mathf.Min(time1Y, time2Y);
        float maxY = Mathf.Max(time1Y, time2Y);
        float minX = Mathf.Min(time1X, time2X);
        float maxX = Mathf.Max(time1X, time2X);

        if (maxY > minX && minY < maxX)
        {
            return new CollisionInfo(minX < minY, null, Mathf.Max(minX, minY));
        }
        else
        {
            return new CollisionInfo(false, null, 0);
        }
    }

}
