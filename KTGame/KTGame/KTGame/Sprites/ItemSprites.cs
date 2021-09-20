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

        class CoinSprite : Sprite
        {
            public CoinSprite(Texture2D block, int[] FramesX, int[] FramesY, Point frameSize, IEntity entity, int Ms)
            : base(block, FramesX, FramesY, frameSize, entity, Ms)
        {

            }
        }

        class MushroomSprite : Sprite
        {
            public MushroomSprite(Texture2D block, int[] FramesX, int[] FramesY, Point frameSize, IEntity entity)
            : base(block, FramesX, FramesY, frameSize, entity)
        {

            }
        }

        class OneUpSprite : Sprite
        {
            public OneUpSprite(Texture2D block, int[] FramesX, int[] FramesY, Point frameSize, IEntity entity)
            : base(block, FramesX, FramesY, frameSize, entity)
        {

            }
        }

        class FlowerSprite : Sprite
        {
            public FlowerSprite(Texture2D block, int[] FramesX, int[] FramesY, Point frameSize, IEntity entity)
            : base(block, FramesX, FramesY, frameSize, entity)
        {

            }
        }

        class StarSprite : Sprite
        {
            public StarSprite(Texture2D block, int[] FramesX, int[] FramesY, Point frameSize, IEntity entity)
            : base(block, FramesX, FramesY, frameSize, entity)
        {

            }
        }

}



