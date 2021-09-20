using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using KTGame.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTGame.Sprites.Items
{

        class FireballSprite : Sprite
        {
            public FireballSprite(Texture2D block, int[] FramesX, int[] FramesY, Point frameSize, IEntity entity)
            : base(block, FramesX, FramesY, frameSize, entity)
        {

            }
        }

        class HammerSprite : Sprite
        {
            public HammerSprite(Texture2D block, int[] FramesX, int[] FramesY, Point frameSize, IEntity entity)
            : base(block, FramesX, FramesY, frameSize, entity)
        {

            }
        }

        class BowserFireaballSprite : Sprite
        {
            public BowserFireaballSprite(Texture2D block, int[] FramesX, int[] FramesY, Point frameSize, IEntity entity)
            : base(block, FramesX, FramesY, frameSize, entity)
        {

            }
        }

        class SpikeballSprite : Sprite
        {
            public SpikeballSprite(Texture2D block, int[] FramesX, int[] FramesY, Point frameSize, IEntity entity)
            : base(block, FramesX, FramesY, frameSize, entity)
        {

            }
        }
}
