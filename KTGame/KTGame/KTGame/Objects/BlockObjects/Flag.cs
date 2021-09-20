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
    public class Flag : Block
    {
        public Flag(Texture2D texture, Vector2 position)
            : base(texture, position)
        {
            EntityName = "Flag";
            SpriteFactory = new BlockFactory(Texture);
            Sprite = SpriteFactory.GetSprite(this);
            BlockState = new BlockUsedState(this, null);
            CollisionOn = false;

        }
        public override void Bump(IEntity player)
        {
            return;
        }
    }
}