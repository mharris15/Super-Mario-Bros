using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sprint1.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sprint1.Factories;

namespace Sprint1.Objects
{
    public class BrickBlock : IBlock
    {
        public IBlockState BlockState;
        private ISprite BlockSprite;
        private BlockFactory BlockFactory;
        private Vector2 Position;
        private Texture2D Texture;

        public BrickBlock(Texture2D texture)
        {
            Texture = texture;

            BlockFactory = new BlockFactory(Texture);
            BlockSprite = BlockFactory.CreateBlock(BlockFactory.BlockType.QBlock);
            Position = new Vector2(200, 100);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            BlockSprite.Draw(spriteBatch, Position);
        }

        public void Update(GameTime gameTime)
        {
            BlockSprite.Update(gameTime);
        }
    }
}
