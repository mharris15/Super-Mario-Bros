using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using KTGame.Factories;
using KTGame.Interfaces;
using KTGame.States.BlockStates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTGame.Objects.BlockObjects
{
    public class HiddenBlockState : Block
    {
        public HiddenBlockState(Texture2D texture, Vector2 position)
            : base(texture, position)
        {
            EntityName = "Hidden";
            SpriteFactory = new BlockFactory(Texture);
            Sprite = SpriteFactory.GetSprite(this);
            BlockState = new BlockHiddenState(this, null);

        }
    }
}
