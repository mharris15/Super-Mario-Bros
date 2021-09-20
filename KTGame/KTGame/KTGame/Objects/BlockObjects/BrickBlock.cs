using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using KTGame.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using KTGame.Factories;
using KTGame.Sprites;
using KTGame.States.BlockStates;
using static KTGame.Factories.BlockFactory;
using KTGame.Objects.BlockObjects;

namespace KTGame.Objects.BlockObjects
{
    public class BrickBlock : Block
    {
        public BrickBlock(Texture2D texture, Vector2 position)
            : base(texture, position)
        {
            EntityName = "Brick";
            SpriteFactory = new BlockFactory(Texture);
            Sprite = SpriteFactory.GetSprite(this);
            BlockState = new BlockBrickState(this, null);
        }
    }
}
