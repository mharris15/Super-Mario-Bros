using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTGame.Objects.Items
{
    public class Fireball : Projectile
    {
        public Fireball(Texture2D spriteSheet, Vector2 position, bool activated, bool initialCollision)
            : base(spriteSheet, position, activated, initialCollision)
        {
            EntityName = "Fireball";
            Sprite = SpriteFactory.GetSprite(this);
            //CollisionBox = new Rectangle((int)Position.X, (int)Position.Y, 2 * Sprite.FrameSize.X, 2 * Sprite.FrameSize.Y);
            CollisionBox = new Rectangle((int)Position.X, (int)Position.Y, 8 * Sprite.FrameSize.X, 8 * Sprite.FrameSize.Y);

        }

        public override void Activation(bool facingLeft)
        {
            Position = new Vector2(LinkedEntity.Position.X + 15, LinkedEntity.Position.Y - 30);


            Activated = true;
            Moving = true;
            CollisionOn = true;

            if (facingLeft)
                Velocity = new Vector2(LinkedEntity.Velocity.X - FireballHorizontalSpeed, -2);
            else
                Velocity = new Vector2(LinkedEntity.Velocity.X  + FireballHorizontalSpeed, -2);

            Acceleration = new Vector2(0, .4f);
        }
    }

    public class Hammer : Projectile
    {
        public Hammer(Texture2D spriteSheet, Vector2 position, bool activated, bool initialCollision)
            : base(spriteSheet, position, activated, initialCollision)
        {
            EntityName = "Hammer";
            Sprite = SpriteFactory.GetSprite(this);
            CollisionBox = new Rectangle((int)Position.X, (int)Position.Y, 2 * Sprite.FrameSize.X, 2 * Sprite.FrameSize.Y);
        }
        public override void Activation(bool facingLeft)
        {
            Position = LinkedEntity.Position;

            Activated = true;
            Moving = true;
            CollisionOn = true;

            if (facingLeft)
                Velocity = new Vector2(-HorizontalSpeed, -10);
            else
                Velocity = new Vector2(HorizontalSpeed, -10);
        }
    }

    public class Spikeball : Projectile
    {
        public Spikeball(Texture2D spriteSheet, Vector2 position, bool activated, bool initialCollision)
            : base(spriteSheet, position, activated, initialCollision)
        {
            EntityName = "Spikeball";
            Sprite = SpriteFactory.GetSprite(this);
            CollisionBox = new Rectangle((int)Position.X, (int)Position.Y, 2 * Sprite.FrameSize.X, 2 * Sprite.FrameSize.Y);
        }

        public override void Activation(bool facingLeft)
        {
            Position = LinkedEntity.Position;

            Activated = true;
            Moving = true;
            CollisionOn = true;

            if (facingLeft)
                Velocity = new Vector2(-HorizontalSpeed, -5);
            else
                Velocity = new Vector2(HorizontalSpeed, -5);
        }
    }

    public class BowserFireball : Projectile
    {
        public BowserFireball(Texture2D spriteSheet, Vector2 position, bool activated, bool initialCollision)
            : base(spriteSheet, position, activated, initialCollision)
        {
            EntityName = "BowserFireball";
            Sprite = SpriteFactory.GetSprite(this);
            CollisionBox = new Rectangle((int)Position.X, (int)Position.Y, 2 * Sprite.FrameSize.X, 2 * Sprite.FrameSize.Y);
        }
        public override void Activation(bool facingLeft)
        {
            Position = LinkedEntity.Position;

            Activated = true;
            Moving = true;
            CollisionOn = true;

            if (facingLeft)
                Velocity = new Vector2(-HorizontalSpeed*3, 0);
            else
                Velocity = new Vector2(HorizontalSpeed*3, 0);
        }
    }
}
