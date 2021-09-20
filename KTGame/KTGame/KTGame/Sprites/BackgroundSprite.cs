using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using KTGame.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTGame.Sprites
{
    public class BackgroundSprite : ISprite
    {
        private readonly Texture2D Structures;

        public Point FrameSize { get; set; }

        private int[] FramesX; //X value(s) for the animation frame
        private int[] FramesY; //X value(s) for the animation frame

        public BackgroundSprite(Texture2D structures)
        {
            Structures = structures;
            FramesX = new int[0];
            FramesY = new int[0];
        }
        public int[] GetFramesX()
        {
            return (int[])FramesX.Clone();
        }
        public int[] GetFramesY()
        {
            return (int[])FramesY.Clone();
        }
        public void Update(GameTime gameTime)
        {
        }
        private void BigHill(SpriteBatch spriteBatch, int X, int Y)
        {
            Rectangle sourceRectangle = new Rectangle(80, 0, 90, 50);
            Rectangle destinationRectangle = new Rectangle(X, Y, 260, 120);
            spriteBatch.Draw(Structures, destinationRectangle, sourceRectangle, Color.White);
        }
        private void SmallHill(SpriteBatch spriteBatch, int X, int Y)
        {
            Rectangle sourceRectangle = new Rectangle(170, 0, 50, 50);
            Rectangle destinationRectangle = new Rectangle(X, Y, 100, 70);
            spriteBatch.Draw(Structures, destinationRectangle, sourceRectangle, Color.White);
        }
        private void LongBush(SpriteBatch spriteBatch, int X, int Y)
        {
            Rectangle sourceRectangle = new Rectangle(217, 0, 70, 50);
            Rectangle destinationRectangle = new Rectangle(X, Y, 150, 150);
            spriteBatch.Draw(Structures, destinationRectangle, sourceRectangle, Color.White);
        }
        private void ShortBush(SpriteBatch spriteBatch, int X, int Y)
        {
            Rectangle sourceRectangle = new Rectangle(287, 0, 50, 50);
            Rectangle destinationRectangle = new Rectangle(X, Y, 120, 110);
            spriteBatch.Draw(Structures, destinationRectangle, sourceRectangle, Color.White);
        }
        public void Draw(SpriteBatch spriteBatch, Vector2 position)
        {
            if (spriteBatch == null)
                throw new ArgumentNullException("spriteBatch");

            BigHill(spriteBatch, 600, 360);
            SmallHill(spriteBatch, 70, 395);
            LongBush(spriteBatch, 300, 330);
            ShortBush(spriteBatch, 570, 360);
        }
    }
}
