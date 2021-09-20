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

    class IdleSprite : Sprite
    {
        public IdleSprite(Texture2D block, int[] FramesX, int[] FramesY, Point frameSize, IEntity entity)
            : base(block, FramesX, FramesY, frameSize, entity)
        {

        }
    }

    class CrouchingSprite : Sprite
    {
        public CrouchingSprite(Texture2D block, int[] FramesX, int[] FramesY, Point frameSize, IEntity entity)
            : base(block, FramesX, FramesY, frameSize, entity)
        {

        }
    }

    class WalkingSprite : Sprite
    {
        public WalkingSprite(Texture2D block, int[] FramesX, int[] FramesY, Point frameSize, IEntity entity)
            : base(block, FramesX, FramesY, frameSize, entity, 120)
        {

        }
    }
    class RunningSprite : Sprite
    {
        public RunningSprite(Texture2D block, int[] FramesX, int[] FramesY, Point frameSize, IEntity entity)
            : base(block, FramesX, FramesY, frameSize, entity, 60)
        {

        }
    }
    class JumpingSprite : Sprite
    {
        public JumpingSprite(Texture2D block, int[] FramesX, int[] FramesY, Point frameSize, IEntity entity)
            : base(block, FramesX, FramesY, frameSize, entity)
        {

        }
    }
    class FallingSprite : Sprite
    {
        public FallingSprite(Texture2D block, int[] FramesX, int[] FramesY, Point frameSize, IEntity entity)
            : base(block, FramesX, FramesY, frameSize, entity)
        {

        }
    }
    class DyingSprite : Sprite
    {
        public DyingSprite(Texture2D block, int[] FramesX, int[] FramesY, Point frameSize, IEntity entity)
            : base(block, FramesX, FramesY, frameSize, entity)
        {

        }
    }

    class FlagSprite : Sprite
    {
        public FlagSprite(Texture2D block, int[] FramesX, int[] FramesY, Point frameSize, IEntity entity)
            : base(block, FramesX, FramesY, frameSize, entity)
        {

        }
    }
}
