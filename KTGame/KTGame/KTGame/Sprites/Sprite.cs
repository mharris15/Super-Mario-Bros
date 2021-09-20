using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using KTGame.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KTGame.Objects;
using KTGame.Objects.Items;
using KTGame.Objects.Enemies;

namespace KTGame.Sprites
{
    abstract class Sprite : ISprite//, IDisposable
    {
        readonly private Texture2D Texture;
        readonly private Texture2D pixel;
        
        public Point FrameSize { get; set; } //X and Y dimensions of each animation frame
        private Point currentFrame = new Point(0, 0); //Current Frame

        protected int[] FramesX { get; set; } //X value(s) for the animation frame
        protected int[] FramesY { get; set; } //Y value(s) for the animation framesC:\Users\djrpp\source\repos\KomputaTroopas\KTGame\KTGame\Sprites\Sprite.cs

        private Color BorderColor;

        private int TimeSinceLastFrame = 0;
        private readonly int MillisecondsPerFrame = 60;

        private readonly IEntity Entity;

        /* Constructor for default animation speed. */
        protected Sprite(Texture2D texture,int[] framesX, int[] framesY, Point frameSize, IEntity entity)
        {
            Texture = texture;
            FrameSize = frameSize;
            FramesX = framesX;
            FramesY = framesY;
            Entity = entity;


            pixel = new Texture2D(Texture.GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
            pixel.SetData(new[] { Color.White});

            if (Entity is Player)
            {
                BorderColor = Color.Yellow;
            }
            else if (Entity is Item)
            {
                BorderColor = Color.Green;
            }
            else if (Entity is Enemy)
            {
                BorderColor = Color.Red;
            }
            else
            {
                BorderColor = Color.Blue;
            }
        }
        //public void SetFramesX(int[] framesX)
        //{
        //    FramesX = framesX;
        //}
        //public void SetFramesY(int[] framesY)
        //{
        //    FramesY = framesY;
        //}
        public int[] GetFramesX()
        {
            return (int[])FramesX.Clone();
        }
        public int[] GetFramesY()
        {
            return (int[])FramesY.Clone();
        }
        /* Constructor for custom animation speeds. */
        protected Sprite(Texture2D texture, int[] framesX, int[] framesY, Point frameSize, IEntity entity, int msPerFrame)
        {
            Texture = texture;
            FrameSize = frameSize;
            FramesX = framesX;
            FramesY = framesY;
            MillisecondsPerFrame = msPerFrame;

            Entity = entity;

            pixel = new Texture2D(Texture.GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
            pixel.SetData(new[] { Color.White });

            if (Entity is Player)
            {
                BorderColor = Color.Yellow;
            }
            else if (Entity is Item)
            {
                BorderColor = Color.Green;
            }
            else if (Entity is Enemy)
            {
                BorderColor = Color.Red;
            }
            else
            {
                BorderColor = Color.Blue;
            }
        }

        public virtual void Update(GameTime gameTime)
        {
            if (gameTime == null)
                throw new ArgumentNullException("gameTime");


            /* Updates the animation if there exist multiple frames specified by the array arguments. */
            if (FramesX.Length > 1)
            {
                TimeSinceLastFrame += gameTime.ElapsedGameTime.Milliseconds;
                if (TimeSinceLastFrame > MillisecondsPerFrame)
                {
                    TimeSinceLastFrame -= MillisecondsPerFrame;
                    ++currentFrame.X;
                    if (currentFrame.X >= FramesX.Length)
                    {
                        currentFrame.X = 0;
                    }
                }
            }
        }

        private void DrawBorder(SpriteBatch spriteBatch, Vector2 position, int thickness, Color color)
        {
            Rectangle rectangle = new Rectangle((int)position.X, (int)position.Y - 2 * FrameSize.Y, 2 * FrameSize.X, 2 * FrameSize.Y);

            Rectangle topLine = new Rectangle(rectangle.X, rectangle.Y, rectangle.Width, thickness);
            Rectangle bottomLine = new Rectangle(rectangle.X,rectangle.Y + rectangle.Height,rectangle.Width,thickness);
            Rectangle leftLine = new Rectangle(rectangle.X , rectangle.Y, thickness, rectangle.Height);
            Rectangle rightLine = new Rectangle((rectangle.X + rectangle.Width), rectangle.Y, thickness, rectangle.Height);
           
            // Draw top line
            spriteBatch.Draw(pixel, topLine , color);

            // Draw left line
            spriteBatch.Draw(pixel, leftLine, color);

            // Draw right line
            spriteBatch.Draw(pixel, rightLine, color);
            // Draw bottom line
            spriteBatch.Draw(pixel, bottomLine, color);
        }

        public virtual void Draw(SpriteBatch spriteBatch, Vector2 position)
        {
            if (spriteBatch == null)
                throw new ArgumentNullException("spriteBatch");

            /* Creates the source rectangle around the location of the current sprite in the spritesheet.*/
            Rectangle sourceRectangle = new Rectangle(FramesX[currentFrame.X], FramesY[currentFrame.Y], FrameSize.X, FrameSize.Y);

            /* Draws the Sprite. */
            //spriteBatch.Begin();
            
            /* If Entity is colliding and collision visuals are on, draw purple overlay. */
            if (!(Entity.CollisionOverlay && Entity.BorderOn))
            {
                spriteBatch.Draw(Texture, position, sourceRectangle, Color.White, 0f, new Vector2(0, FrameSize.Y), new Vector2(2, 2), SpriteEffects.None, 1f);
            }
            else
            {
                spriteBatch.Draw(Texture, position, sourceRectangle, Color.Purple, 0f, new Vector2(0, FrameSize.Y), new Vector2(2, 2), SpriteEffects.None, 1f);
            }

            /* If Entity is able to collide and collision visuals are on, draw borders. */
            if (Entity.CollisionOn && Entity.BorderOn)
            {
                DrawBorder(spriteBatch, position, 2, BorderColor);
            }

            //spriteBatch.End();
        }

        //public void Dispose()
        //{
        //    Texture.Dispose();
        //    pixel.Dispose();
        //}
    }
}
