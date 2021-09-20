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
using KTGame.Sprites.Items;
using KTGame.Objects.Items;

namespace KTGame.Factories
{
    public class ItemFactory : IFactory
    {
        private readonly Texture2D Texture;

        private Point FrameSize;
        private int Width;
        private int Height;

        public ItemFactory(Texture2D sheet)
        {
            Texture = sheet ?? throw new ArgumentNullException("sheet");

            Width = sheet.Width / 30;
            Height = sheet.Height / 9;
        }


        public ISprite GetSprite(IEntity entity)
        {
            FrameSize = new Point (Width, Height);


            int[] FramesX, FramesY;

            string Name = ((Item)entity).EntityName;
            switch (Name)
            {
                case "Coin":
                    FramesX = new int[] { 0 * Width, 1*Width, 2*Width, 3*Width };
                    FramesY = new int[] { 6*Height + 1};

                    return  new CoinSprite(Texture, FramesX, FramesY, FrameSize, entity,200);

                case "Flower":
                    FramesX = new int[] { 0 * Width, 1 * Width, 2 * Width, 3 * Width };
                    FramesY = new int[] { 2 * Height };
                     return new FlowerSprite(Texture, FramesX, FramesY, FrameSize,entity);

                case "Mushroom":
                    FramesX = new int[] { 0 * Width};
                    FramesY = new int[] { 0 * Height };
                      return  new MushroomSprite(Texture, FramesX, FramesY, FrameSize, entity);

                case "OneUp":
                    FramesX = new int[] { 1 * Width};
                    FramesY = new int[] { 0 *Height };
                    return  new OneUpSprite(Texture, FramesX, FramesY, FrameSize, entity);

                case "Star":
                    FramesX = new int[] { 0 * Width, 1 * Width, 2 * Width, 3 * Width };
                    FramesY = new int[] { 3 * Height };
                    return  new StarSprite(Texture, FramesX, FramesY, FrameSize, entity);
                case "Axe":
                    FramesX = new int[] { 0 * Width };
                    FramesY = new int[] { 8 * Height };
                    return new MushroomSprite(Texture, FramesX, FramesY, FrameSize, entity);

                default:
                    throw new NotSupportedException();

            }


        }

    }
}
