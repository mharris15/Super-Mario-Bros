using KTGame.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using KTGame.Objects;
using KTGame.Sprites;
using KTGame.Objects.BlockObjects;

namespace KTGame.Factories
{
    public class BlockFactory : IFactory
    {


        private readonly Texture2D BlockTexture;
        private Point FrameSize = new Point(16,16);
        private Point PipeSize = new Point(32, 32);
        private Point FrameSize2 = new Point(4,4);
        private Point FlameSize = new Point(16, 32);

        public BlockFactory(Texture2D texture)
        {
              BlockTexture = texture;
        }

        public ISprite GetSprite(IEntity entity)
        {

            int[] FramesX, FramesY;

            string Name = ((Block)entity).EntityName;

            switch (Name)
            {
                case "Question":
                    FramesX = new int[] {368,384,400,368};
                    FramesY = new int[] {0};
                    return new QuestionBlockSprites(BlockTexture,FramesX,FramesY,FrameSize, entity);

                case "Brick":
                    FramesX = new int[] {16};
                    FramesY = new int[] {0};
                    return new BrickBlockSprites(BlockTexture, FramesX, FramesY, FrameSize, entity);

                case "Broken":
                    FramesX = new int[] { 16 };
                    FramesY = new int[] { 0 };
                    return new BrickBlockSprites(BlockTexture, FramesX, FramesY, FrameSize2, entity);

                case "Floor":
                    FramesX = new int[] {0};
                    FramesY = new int[] {0};
                    return new FloorBlockSprites(BlockTexture,FramesX,FramesY,FrameSize, entity);

                case "Stair":
                    FramesX = new int[] { 0 };
                    FramesY = new int[] { 16 };
                    return new StairBlockSprites(BlockTexture, FramesX, FramesY, FrameSize, entity);

                case "Used":
                    FramesX = new int[] { 416 };
                    FramesY = new int[] { 0 };
                    return new UsedBlockSprites(BlockTexture, FramesX, FramesY, FrameSize, entity);

                case "Hidden":
                    FramesX = new int[] { 448};
                    FramesY = new int[] { 0 };
                    return new HiddenBlockSprites(BlockTexture, FramesX, FramesY, FrameSize, entity);

                case "Pipe":
                    FramesX = new int[] { 0 };
                    FramesY = new int[] { 128 };
                    return new PipeBlockSprites(BlockTexture, FramesX, FramesY, PipeSize, entity);


                case "PipeLength":
                    FramesX = new int[] { 80 };
                    FramesY = new int[] { 128 };
                    return new PipeBlockSprites(BlockTexture, FramesX, FramesY, PipeSize, entity);

                case "Flag":
                    FramesX = new int[] { 16 * 7 };
                    FramesY = new int[] { 128 };
                    return new PipeBlockSprites(BlockTexture, FramesX, FramesY, FrameSize, entity);

                case "FlagTip":
                    FramesX = new int[] { 16*8 };
                    FramesY = new int[] { 128 };
                    return new PipeBlockSprites(BlockTexture, FramesX, FramesY, FrameSize, entity);

                case "FlagLength":
                    FramesX = new int[] { 16 * 8 };
                    FramesY = new int[] { 16*  9  };
                    return new PipeBlockSprites(BlockTexture, FramesX, FramesY, FrameSize, entity);

                case "UnderFloor":
                    FramesX = new int[] { 0 };
                    FramesY = new int[] { 32 };
                    return new UnderGroundFBSprite(BlockTexture, FramesX, FramesY, FrameSize, entity);
                case "UnderBrick":
                    FramesX = new int[] { 32 };
                    FramesY = new int[] { 32 };
                    return new UndergroundBBSprite(BlockTexture, FramesX, FramesY, FrameSize, entity);
                case "BossBB":
                    FramesX = new int[] { 32 };
                    FramesY = new int[] { 64 };
                    return new BossBrickSprite(BlockTexture, FramesX, FramesY, FrameSize, entity);
                case "BossFB":
                    FramesX = new int[] { 0 };
                    FramesY = new int[] { 64 };
                    return new BossBrickSprite(BlockTexture, FramesX, FramesY, FrameSize, entity);
                case "Bridge":
                    FramesX = new int[] { 64 };
                    FramesY = new int[] { 384 };
                    return new BridgeSprite(BlockTexture, FramesX, FramesY, FrameSize, entity);
                case "Flames":
                    FramesX = new int[] {48};
                    FramesY = new int[] { 384 };
                    return new FlameSprite(BlockTexture, FramesX, FramesY, FlameSize, entity);
                default:
                    throw new NotSupportedException();

            }

        }
    }
}


