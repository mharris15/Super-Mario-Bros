using KTGame.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTGame.Sprites
{
    class QuestionBlockSprites : Sprite
    {
        public QuestionBlockSprites(Texture2D block, int[] FramesX, int[] FramesY, Point frameSize, IEntity entity)
            : base(block, FramesX, FramesY, frameSize, entity,170)
        {

        }
    }

    class BrickBlockSprites : Sprite
    {
        public BrickBlockSprites(Texture2D block, int[] FramesX, int[] FramesY, Point frameSize, IEntity entity)
            : base(block, FramesX, FramesY, frameSize, entity)
        {

        }
    }
    class FloorBlockSprites : Sprite
    {
        public FloorBlockSprites(Texture2D block, int[] FramesX, int[] FramesY, Point frameSize, IEntity entity)
            : base(block, FramesX, FramesY, frameSize, entity)
        {

        }
    }
    class StairBlockSprites : Sprite
    {
        public StairBlockSprites(Texture2D block, int[] FramesX, int[] FramesY, Point frameSize, IEntity entity)
            : base(block, FramesX, FramesY, frameSize, entity)
        {

        }
    }
    class UsedBlockSprites : Sprite
    {
        public UsedBlockSprites(Texture2D block, int[] FramesX, int[] FramesY, Point frameSize, IEntity entity)
            : base(block, FramesX, FramesY, frameSize, entity)
        {

        }
    }
    class HiddenBlockSprites : Sprite
    {
        public HiddenBlockSprites(Texture2D block, int[] FramesX, int[] FramesY, Point frameSize, IEntity entity)
            : base(block, FramesX, FramesY, frameSize, entity)
        {

        }
    }
    class PipeBlockSprites : Sprite
    {
        public PipeBlockSprites(Texture2D block, int[] FramesX, int[] FramesY, Point frameSize, IEntity entity)
            : base(block, FramesX, FramesY, frameSize, entity)
        {

        }
    }
    class UnderGroundFBSprite : Sprite
    {
        public UnderGroundFBSprite(Texture2D block, int[] FramesX, int[] FramesY, Point frameSize, IEntity entity)
            : base(block, FramesX, FramesY, frameSize, entity)
        {

        }
    }
    class UndergroundBBSprite : Sprite
    {
        public UndergroundBBSprite(Texture2D block, int[] FramesX, int[] FramesY, Point frameSize, IEntity entity)
            : base(block, FramesX, FramesY, frameSize, entity)
        {

        }
    }

    class BossBrickSprite: Sprite
    {
        public BossBrickSprite(Texture2D block, int[] FramesX, int[] FramesY, Point frameSize, IEntity entity)
            : base(block, FramesX, FramesY, frameSize, entity)
        {

        }
    }

    class BridgeSprite : Sprite
    {
        public BridgeSprite(Texture2D block, int[] FramesX, int[] FramesY, Point frameSize, IEntity entity)
            : base(block, FramesX, FramesY, frameSize, entity)
        {

        }
    }
    class FlameSprite : Sprite
    {
        public FlameSprite(Texture2D block, int[] FramesX, int[] FramesY, Point frameSize, IEntity entity)
            : base(block, FramesX, FramesY, frameSize, entity)
        {

        }
    }

}