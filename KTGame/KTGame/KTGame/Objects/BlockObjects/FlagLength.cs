using KTGame.Factories;
using KTGame.Interfaces;
using KTGame.States.BlockStates;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTGame.Objects.BlockObjects
{
    public class FlagLength : Block
    {
        public FlagLength(Texture2D texture, Vector2 position)
            : base(texture, position)
        {
            EntityName = "FlagLength";
            SpriteFactory = new BlockFactory(Texture);
            Sprite = SpriteFactory.GetSprite(this);
            BlockState = new BlockUsedState(this, null);
            CollisionBox = new Rectangle((int)Position.X + 16, (int)Position.Y, Sprite.FrameSize.X, 4 * Sprite.FrameSize.Y);


        }
        public override void Bump(IEntity player)
        {
            return;
        }
    }
}