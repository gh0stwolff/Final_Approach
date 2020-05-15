using System;
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
    private AnimSprite _infoBoard;
    private BlockBS _goal;
    private Button _buttonBoneSDone;

    private Bones bone1, bone2, bone3;

    private int _distbetweenPoints = 100;
    private int _offsetX = 720;
    private int _offsetY = 600;


    Quiz quiz;
    private bool _showDoneText = false;
    private bool _createQuizonce = true;
    public bool _IsGameFinished = false;

    public bool _doOnce = true;

    private List<BlockBS> blocks = new List<BlockBS>();
    private List<Bones> bones = new List<Bones>();
    private Vec2[] targetPoints = new Vec2[3];

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

        createTargetPoints();
        setupList(difficulty);
    }

    private void setupList(int difficulty)
    {
        if (difficulty == 1) { load1(); }
        else if (difficulty == 2) { load2(); }
        else if (difficulty == 3) { load3(); }
    }

    private void createTargetPoints()
    {
        for (int i = 0; i < targetPoints.Length; i++)
        {
            targetPoints[i] = new Vec2(_offsetX + _distbetweenPoints * i, _offsetY);
        }
    }

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

        bone1 = new Bones("collectBone.png", new Vec2(143, 153), targetPoints[0]);
        AddChild(bone1);
        bone1.SetOrigin(bone1.width / 2, bone1.height / 2);
        bones.Add(bone1);
        bone2 = new Bones("collectBone.png", new Vec2(531, 347), targetPoints[1]);
        AddChild(bone2);
        bone2.SetOrigin(bone2.width / 2, bone2.height / 2);
        bones.Add(bone2);
        bone3 = new Bones("collectBone.png", new Vec2(628, 444), targetPoints[2]);
        AddChild(bone3);
        bone3.SetOrigin(bone3.width / 2, bone3.height / 2);
        bones.Add(bone3);

        _infoBoard = new AnimSprite("testinfojigsaw.png", 3, 1);
        AddChild(_infoBoard);
        _infoBoard.alpha = 0.0f;
        _infoBoard.SetXY(700, 100);
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

        bone1 = new Bones("collectBone.png", new Vec2(240, 153), targetPoints[0]);
        AddChild(bone1);
        bone1.SetOrigin(bone1.width / 2, bone1.height / 2);
        bones.Add(bone1);
        bone2 = new Bones("collectBone.png", new Vec2(531, 347), targetPoints[1]);
        AddChild(bone2);
        bone2.SetOrigin(bone2.width / 2, bone2.height / 2);
        bones.Add(bone2);
        bone3 = new Bones("collectBone.png", new Vec2(143, 638), targetPoints[2]);
        AddChild(bone3);
        bone3.SetOrigin(bone3.width / 2, bone3.height / 2);
        bones.Add(bone3);

        _infoBoard = new AnimSprite("testinfojigsaw.png", 3, 1);
        AddChild(_infoBoard);
        _infoBoard.alpha = 0.0f;
        _infoBoard.SetXY(700, 100);
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

        bone1 = new Bones("collectBone.png", new Vec2(133, 143), targetPoints[0]);
        AddChild(bone1);
        bone1.SetOrigin(bone1.width / 2, bone1.height / 2);
        bones.Add(bone1);
        bone2 = new Bones("collectBone.png", new Vec2(531, 347), targetPoints[1]);
        AddChild(bone2);
        bone2.SetOrigin(bone2.width / 2, bone2.height / 2);
        bones.Add(bone2);
        bone3 = new Bones("collectBone.png", new Vec2(337, 541), targetPoints[2]);
        AddChild(bone3);
        bone3.SetOrigin(bone3.width / 2, bone3.height / 2);
        bones.Add(bone3);

        _infoBoard = new AnimSprite("testinfojigsaw.png", 3, 1);
        AddChild(_infoBoard);
        _infoBoard.alpha = 0.0f;
        _infoBoard.SetXY(700, 100);
    }

    public void Update()
    {
        checkIfDone();
        checkCollisions();
        showCorrectInfo();
        if (_showDoneText && _buttonBoneSDone.Pressed)
        {
            _buttonBoneSDone.LateDestroy();
            _IsGameFinished = true;
            _showDoneText = false;
        }

    }

    private void checkIfDone()
    {
        if (Mathf.Abs(_goal.x - _exit.x) <= _goal.radiusX &&
            _goal.y - _exit.y < _goal.radiusY * 2)
        {
            if (_doOnce)
            {
                Sound effect = new Sound("Minigame_end.wav");
                effect.Play();
                _doOnce = false;
            }
            if (!_showDoneText)
            {
                ((MyGame)game).GoodJob();
                
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
        _buttonBoneSDone = new Button("arrowR_spritesheet_2.png", new Vec2(900, 700), 7, 1);
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

            foreach (Bones bone in bones)
            {
                BlockBS block = blocks[i];

                if (Mathf.Abs(block.Position.x - bone.x) <= block.radiusX + bone.width/2 &&
                    Mathf.Abs(block.Position.y - bone.y) <= block.radiusY + bone.height/2)
                {
                    bone._isBoneMoving = true;

                    if (_infoBoard.alpha < 0.5f)
                    {
                        _infoBoard.alpha = 1.0f;
                    }

                    if (bone == bone1) { _infoBoard.SetFrame(0); }
                    else if (bone == bone2) { _infoBoard.SetFrame(1); }
                    else if (bone == bone3) { _infoBoard.SetFrame(2); }
                }
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

    private void showCorrectInfo()
    {
        if (bones[0]._isPressed && bones[0]._position == targetPoints[0])
        {
            Console.WriteLine("1");
            _infoBoard.SetFrame(0);
            if (_infoBoard.alpha < 0.5f) { _infoBoard.alpha = 1.0f; }
        }
        if (bones[1]._isPressed && bones[1]._position == targetPoints[1])
        {
            Console.WriteLine("2");
            _infoBoard.SetFrame(1);
            if (_infoBoard.alpha < 0.5f) { _infoBoard.alpha = 1.0f; }
        }
        if (bones[2]._isPressed && bones[2]._position == targetPoints[2])
        {
            Console.WriteLine("3");
            _infoBoard.SetFrame(2);
            if (_infoBoard.alpha < 0.5f) { _infoBoard.alpha = 1.0f; }
        }
    }

}
