using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTGame.Interfaces
{
    /* Interface used for Sprites */
    public interface ISprite
    {

        Point FrameSize { get; set; }

        //int[] FramesX { get; set; } //X value(s) for the animation frame
        //int[] FramesY { get; set; } //X value(s) for the animation frame
        //void SetFramesX(int[] frameX);
        //void SetFramesY(int[] frameY);
        int[] GetFramesX();
        int[] GetFramesY();
        void Update(GameTime gameTime);
        void Draw(SpriteBatch spriteBatch, Vector2 position);
 
    }
}
