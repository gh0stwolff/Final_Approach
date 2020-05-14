﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using GXPEngine;

class BoneSlide : Canvas
{
    private Sprite _backgroundHitBox;
    private Sprite _background;
    private Sprite _exit;
    private BlockBS _goal;
    private Button _buttonBoneSDone;

    Quiz quiz;
    private bool _showDoneText = false;
    private bool _createQuizonce = true;
    public bool _IsGameFinished = false;

    private List<BlockBS> blocks = new List<BlockBS>();

    public BoneSlide(int width, int height, int difficulty) : base(width, height)
    {
        Sprite Frame = new Sprite("boneSliderFrame.png");
        AddChild(Frame);

        _backgroundHitBox = new Sprite("backgroundBS.png");
        AddChild(_backgroundHitBox);
        _backgroundHitBox.SetXY(95, 105);
        _backgroundHitBox.alpha = 0.0f;

        _background = new Sprite("bBackgroundBS.png");
        AddChild(_background);
        _background.SetXY(75, 85);

        _exit = new Sprite("exit.png");
        AddChild(_exit);
        _exit.SetXY(_background.x + _background.width - _exit.width, _background.y + _background.height/6 * 2);
        
        setupList(difficulty);
    }

    private void setupList(int difficulty)
    {
        if (difficulty == 1) { load1(); }
        else if (difficulty == 2) { load2(); }
        else if (difficulty == 3) { load3(); }
    }
    ///////////////////////reset alle blokken zodat ze weer goed staan
    private void load1()
    {
        BlockBS block1 = new BlockBS("stone3V.png", new Vec2(434, 347), false);
        AddChild(block1);
        blocks.Add(block1);
        BlockBS block2 = new BlockBS("stone2.png", new Vec2(191, 444), true);
        AddChild(block2);
        blocks.Add(block2);
        BlockBS block3 = new BlockBS("stone2V.png", new Vec2(143, 589), false);
        AddChild(block3);
        blocks.Add(block3);
        BlockBS block4 = new BlockBS("stone3.png", new Vec2(337, 638), true);
        AddChild(block4);
        blocks.Add(block4);

        _goal = new BlockBS("bigBone.png", new Vec2(240, 347), true);
        AddChild(_goal);
        blocks.Add(_goal);
    }

    private void load2()
    {
        BlockBS block1 = new BlockBS("stone3.png", new Vec2(531, 153), true);
        AddChild(block1);
        blocks.Add(block1);
        BlockBS block2 = new BlockBS("stone2V.png", new Vec2(434, 298), false);
        AddChild(block2);
        blocks.Add(block2);
        BlockBS block3 = new BlockBS("stone3V.png", new Vec2(628, 444), false);
        AddChild(block3);
        blocks.Add(block3);
        BlockBS block4 = new BlockBS("stone2.png", new Vec2(191, 444), true);
        AddChild(block4);
        blocks.Add(block4);
        BlockBS block5 = new BlockBS("stone2V.png", new Vec2(240, 590), false);
        AddChild(block5);
        blocks.Add(block5);
        BlockBS block6 = new BlockBS("stone4.png", new Vec2(482, 638), true);
        AddChild(block6);
        blocks.Add(block6);

        _goal = new BlockBS("bigBone.png", new Vec2(240, 347), true);
        AddChild(_goal);
        blocks.Add(_goal);
    }

    private void load3()
    {
        BlockBS block1 = new BlockBS("stone3.png", new Vec2(531, 153), true);
        AddChild(block1);
        blocks.Add(block1);
        BlockBS block2 = new BlockBS("stone3V.png", new Vec2(337, 250), false);
        AddChild(block2);
        blocks.Add(block2);
        BlockBS block3 = new BlockBS("stone2.png", new Vec2(482, 250), true);
        AddChild(block3);
        blocks.Add(block3);
        BlockBS block4 = new BlockBS("stone2V.png", new Vec2(628, 298), false);
        AddChild(block4);
        blocks.Add(block4);
        BlockBS block5 = new BlockBS("stone3.png", new Vec2(240, 444), true);
        AddChild(block5);
        blocks.Add(block5);
        BlockBS block6 = new BlockBS("stone2V.png", new Vec2(531, 493), false);
        AddChild(block6);
        blocks.Add(block6);
        BlockBS block7 = new BlockBS("stone2v.png", new Vec2(143, 590), false);
        AddChild(block7);
        blocks.Add(block7);
        BlockBS block8 = new BlockBS("stone2V.png", new Vec2(434, 580), false);
        AddChild(block8);
        blocks.Add(block8);
        BlockBS block9 = new BlockBS("stone2.png", new Vec2(580, 638), true);
        AddChild(block9);
        blocks.Add(block9);

        _goal = new BlockBS("bigBone.png", new Vec2(191, 347), true);
        AddChild(_goal);
        blocks.Add(_goal);
    }

    public void Update()
    {
        checkIfDone();
        checkCollisions();
        // when text done pops up en you click on it, delete it and make quiz once
        if (_createQuizonce && _showDoneText == true && Input.GetMouseButtonDown(0) && _buttonBoneSDone._hover)
        {
            _buttonBoneSDone.LateDestroy();
            createQuiz();
            _createQuizonce = false;
        }
        if (quiz != null)
        {
            if (quiz._isQuizDone)
            {
                _IsGameFinished = true;
            }
        }
    }

    private void checkIfDone()
    {
        if (Mathf.Abs(_goal.x - _exit.x) <= _goal.radiusX &&
            _goal.y - _exit.y < _goal.radiusY * 2)
        {
            if (!_showDoneText)
            {
                BoneSlideDoneButton();
                _showDoneText = true;
            }
        }
    }

    private void createQuiz()
    {
        quiz = new Quiz("quizquesttest1.png", new Vec2(25, 25), 0);
        AddChild(quiz);
    }

    private void BoneSlideDoneButton()
    {
        _buttonBoneSDone = new Button("jigsawdone.png", new Vec2(500, 350), 1, 1);
        AddChild(_buttonBoneSDone);
    }

    private void checkCollisions()
    {
        for (int i = 0; i < blocks.Count; i++)
        {
            if (blocks[i].Holding)
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
                handleBoundaryCollisions(blocks[i]);
            }
        }
    }

    private void handleBoundaryCollisions(BlockBS block)
    {
        if(block.Position.x - block.radiusX < _backgroundHitBox.x)
        {
            Vec2 barrier = new Vec2(_backgroundHitBox.x + block.radiusX, 0);
            Vec2 newPos = Vec2.PointOfImpact(block._position, block.Velocity, barrier);
            block._position.x = newPos.x;
        }
        else if (block.Position.x + block.radiusX > _backgroundHitBox.x + _backgroundHitBox.width)
        {
            Vec2 barrier = new Vec2(_backgroundHitBox.x + _backgroundHitBox.width - block.radiusX, 0);
            Vec2 newPos = Vec2.PointOfImpact(block._position, block.Velocity, barrier);
            block._position.x = newPos.x;
        }
        else if (block.Position.y - block.radiusY < _backgroundHitBox.y)
        {
            Vec2 barrier = new Vec2(0, _backgroundHitBox.y + block.radiusY);
            Vec2 newPos = Vec2.PointOfImpact(block._position, block.Velocity, barrier);
            block._position.y = newPos.y;
        }
        else if (block.Position.y + block.radiusY > _backgroundHitBox.y + _backgroundHitBox.height)
        {
            Vec2 barrier = new Vec2(0, _backgroundHitBox.y + _backgroundHitBox.height - block.radiusY);
            Vec2 newPos = Vec2.PointOfImpact(block._position, block.Velocity, barrier);
            block._position.y = newPos.y;
        }
    }

    private void handleCollisions(BlockBS thisBlock, BlockBS otherBlock)
    {
        if (thisBlock.Velocity.Length() == 0) { return; }

        CollisionInfo info = timeOfImpact(thisBlock._oldPosition, thisBlock.radiusX, thisBlock.radiusY, thisBlock.Velocity, otherBlock.Position, otherBlock.radiusX, otherBlock.radiusY);

        Vec2 point;

        if(!thisBlock.CanMoveHorizontal)
        {
            float delta1Y = (otherBlock.Position.y + otherBlock.radiusY + thisBlock.radiusY) - thisBlock.Position.y;
            float delta2Y = (otherBlock.Position.y - otherBlock.radiusY - thisBlock.radiusY) - thisBlock.Position.y;

            if (Mathf.Abs(delta1Y) < Mathf.Abs(delta2Y)) { point = new Vec2(0, otherBlock.Position.y + otherBlock.radiusY + thisBlock.radiusY + 1); }
            else { point = new Vec2(0, otherBlock.Position.y - otherBlock.radiusY - thisBlock.radiusY - 1); }

            Vec2 newPos = Vec2.PointOfImpact(thisBlock._oldPosition, thisBlock.Velocity, point);
            thisBlock._position.y = newPos.y;
        }
        else
        {
            float delta1X = (otherBlock.Position.x + otherBlock.radiusX + thisBlock.radiusX) - thisBlock.Position.x;
            float delta2X = (otherBlock.Position.x - otherBlock.radiusX - thisBlock.radiusX) - thisBlock.Position.x;

            if(Mathf.Abs(delta1X) < Mathf.Abs(delta2X)) { point = new Vec2(otherBlock.Position.x + otherBlock.radiusX + thisBlock.radiusX + 1, 0); }
            else { point = new Vec2(otherBlock.Position.x - otherBlock.radiusX - thisBlock.radiusX - 1, 0); }

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
