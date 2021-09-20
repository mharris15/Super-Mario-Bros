using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using KTGame.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTGame.Sprites.Enemies
{
 
        class GoombaSprite : Sprite
        {
            public GoombaSprite(Texture2D block, int[] FramesX, int[] FramesY, Point frameSize, IEntity entity,int Ms)
            : base(block, FramesX, FramesY, frameSize, entity, Ms)
        {

            }
        }

        class RKoopaSprite : Sprite
        {
            public RKoopaSprite(Texture2D block, int[] FramesX, int[] FramesY, Point frameSize, IEntity entity, int Ms)
            : base(block, FramesX, FramesY, frameSize, entity, Ms)
        {

            }
        }

        class GKoopaSprite : Sprite
        {
            public GKoopaSprite(Texture2D block, int[] FramesX, int[] FramesY, Point frameSize, IEntity entity, int Ms)
            : base(block, FramesX, FramesY, frameSize, entity, Ms)
        {

            }
        }


        class PlantSprite : Sprite
        {
            public PlantSprite(Texture2D block, int[] FramesX, int[] FramesY, Point frameSize, IEntity entity, int Ms)
            : base(block, FramesX, FramesY, frameSize, entity, Ms)
            {

            }
        }

    class HammerBroSprite : Sprite
    {
        public HammerBroSprite(Texture2D block, int[] FramesX, int[] FramesY, Point frameSize, IEntity entity)
            : base(block, FramesX, FramesY, frameSize, entity)
        {

        }
    }

    class LakituSprite : Sprite
    {
        public LakituSprite(Texture2D block, int[] FramesX, int[] FramesY, Point frameSize, IEntity entity, int Ms)
            : base(block, FramesX, FramesY, frameSize, entity, Ms)
        {

        }
    }
    class BowserSprite : Sprite
    {
        public BowserSprite(Texture2D block, int[] FramesX, int[] FramesY, Point frameSize, IEntity entity, int Ms)
            : base(block, FramesX, FramesY, frameSize, entity, Ms)
        {

        }
    }
}
