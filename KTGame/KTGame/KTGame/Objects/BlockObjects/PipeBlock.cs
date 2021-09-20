
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sprint1.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using Sprint1.Factories;
using Sprint1.States.BlockState;
using static Sprint1.Factories.BlockFactory;
using Sprint1.Objects.BlockObjects;

namespace Sprint1.Objects
{
    public class PipeBlock : Block
    {
        private ISprite BlockSprite;
        private BlockFactory BlockFactory;
        private Vector2 Position;
        private Texture2D Texture;

        public PipeBlock(Texture2D texture)
            : base(texture)
        {
            Texture = texture;
            BlockFactory = new BlockFactory(Texture);
            Position = new Vector2(600, 150);
            BlockSprite = BlockFactory.GetBlockSprite(BlockType.PBlock);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            BlockSprite.Draw(spriteBatch, Position);
        }

        public override void Update(GameTime gameTime)
        {
            BlockSprite.Update(gameTime);
        }


    }
}
