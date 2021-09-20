using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTGame.Objects.Items
{
    class Mushroom : Item
    {
        public Mushroom(Texture2D spriteSheet, Vector2 position, bool activated, bool initialCollision)
            : base(spriteSheet, position, activated, initialCollision)
        {
            EntityName = "Mushroom";
            Sprite = SpriteFactory.GetSprite(this);
            CollisionBox = new Rectangle((int)Position.X, (int)Position.Y, 2 * Sprite.FrameSize.X, 2 * Sprite.FrameSize.Y);
        }
        public override void LeaveBlock(GameTime gameTime)
        {
            base.LeaveBlock(gameTime);
            if (TimeSinceBumped > MillisecondsPerBumpFrame
                && BumpCounter == BumpCounterLimit)
            {
                if (Position.X - ReferencePlayer.Position.X >= 0)
                    Velocity = new Vector2(-HorizontalSpeed, Velocity.Y);
                else
                    Velocity = new Vector2(HorizontalSpeed, Velocity.Y);
            }
        }
    }

    class Flower : Item
    {
        public Flower(Texture2D spriteSheet, Vector2 position, bool activated, bool initialCollision)
            : base(spriteSheet, position, activated, initialCollision)
        {
            EntityName = "Flower";
            Sprite = SpriteFactory.GetSprite(this);
            CollisionBox = new Rectangle((int)Position.X, (int)Position.Y, 2 * Sprite.FrameSize.X, 2 * Sprite.FrameSize.Y);
        }
    }
    class Axe : Item
    {
        public Axe(Texture2D spriteSheet, Vector2 position, bool activated, bool initialCollision)
            : base(spriteSheet, position, activated, initialCollision)
        {
            EntityName = "Axe";
            Sprite = SpriteFactory.GetSprite(this);
            CollisionBox = new Rectangle((int)Position.X, (int)Position.Y, 2 * Sprite.FrameSize.X, 2 * Sprite.FrameSize.Y);
        }
    }

    class Star : Item
    {
        public Star(Texture2D spriteSheet, Vector2 position, bool activated, bool initialCollision)
            : base(spriteSheet, position, activated, initialCollision)
        {
            EntityName = "Star";
            Sprite = SpriteFactory.GetSprite(this);
            CollisionBox = new Rectangle((int)Position.X, (int)Position.Y, 2 * Sprite.FrameSize.X, 2 * Sprite.FrameSize.Y);
        }
        public override void LeaveBlock(GameTime gameTime)
        {
            base.LeaveBlock(gameTime);
            if (TimeSinceBumped > MillisecondsPerBumpFrame
                && BumpCounter == BumpCounterLimit)
            {
                if (Position.X - ReferencePlayer.Position.X >= 00)
                    Velocity = new Vector2(-HorizontalSpeed, Velocity.Y);
                else
                    Velocity = new Vector2(HorizontalSpeed, Velocity.Y);
            }
        }

    }

    class Coin : Item
    {
        public Coin(Texture2D spriteSheet, Vector2 position, bool activated, bool initialCollision)
            : base(spriteSheet, position, activated, initialCollision)
        {
            EntityName = "Coin";
            Sprite = SpriteFactory.GetSprite(this);
            CollisionBox = new Rectangle((int)Position.X, (int)Position.Y, 2 * Sprite.FrameSize.X, 2 * Sprite.FrameSize.Y);
        }
        public void CoinJump(GameTime gameTime)
        {
            if (gameTime == null)
                throw new ArgumentNullException("gameTime");

            TimeSinceBumped += gameTime.ElapsedGameTime.Milliseconds;
            if (TimeSinceBumped > MillisecondsPerCoinFrame)
            {
                BumpCounter++;
                TimeSinceBumped -= MillisecondsPerCoinFrame;


                Vector2 Displacement = new Vector2(0, (float)(-1 * Math.Pow(BumpCounter - CoinBaseConstant, 2) + Math.Pow(CoinBaseConstant, 2)));
                Position = new Vector2(StartPosition.X + Displacement.X, StartPosition.Y - Displacement.Y);
            }
            if (BumpCounter == CoinCounterLimit)
            {
                BumpCounter = 0;
                Activated = false;
                HUD.AddCoins();
            }
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (!CollisionOn && Activated)
            {
                if (!PlayedCollectSound)
                {
                    Soundbank.PlaySound("Coin");
                    PlayedCollectSound = true;
                }
                CoinJump(gameTime);
            }
        }
    }

    class OneUp : Item
    {
        public OneUp(Texture2D spriteSheet, Vector2 position, bool activated, bool initialCollision)
            : base(spriteSheet, position, activated, initialCollision)
        {
            EntityName = "OneUp";
            Sprite = SpriteFactory.GetSprite(this);
            CollisionBox = new Rectangle((int)Position.X, (int)Position.Y, 2 * Sprite.FrameSize.X, 2 * Sprite.FrameSize.Y);
        }
        public override void LeaveBlock(GameTime gameTime)
        {
            base.LeaveBlock(gameTime);
            if (TimeSinceBumped > MillisecondsPerBumpFrame
                && BumpCounter == BumpCounterLimit)
            {
                if (Position.X - ReferencePlayer.Position.X >= 0)
                    Velocity = new Vector2(HorizontalSpeed, Velocity.Y);
                else
                    Velocity = new Vector2(-HorizontalSpeed, Velocity.Y);
            }
        }
    }
}
