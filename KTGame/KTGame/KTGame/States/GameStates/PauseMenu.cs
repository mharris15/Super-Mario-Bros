using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace KTGame.GameStates
{
    class PauseMenu
    {
        public bool isPaused;
        public PauseMenu()
        {
            isPaused = false;
        }

        public void PauseCommand()
        {
            isPaused = !isPaused;
        }

        public void Draw(SpriteBatch spriteBatch, SpriteFont font,Camera camera)
        {
            Color color = Color.Black;
            if (isPaused)
            {
                if (camera.Underground)
                {
                    color = Color.White;
                }
                spriteBatch.DrawString(font, "Paused", new Vector2(camera.Position.X + 375, camera.Position.Y+ 200), color);
                    
            }
        }
    }
}
