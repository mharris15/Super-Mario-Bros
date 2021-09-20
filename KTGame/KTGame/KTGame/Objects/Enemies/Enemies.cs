using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTGame.Objects.Enemies
{
    class Goomba : Enemy
    {
        public Goomba(Texture2D spriteSheet, Vector2 position)
            : base(spriteSheet, position)
        {
            EntityName = "Goomba";
            Sprite = SpriteFactory.GetSprite(this);



            CollisionBox = new Rectangle((int)Position.X, (int)Position.Y, 2 * Sprite.FrameSize.X, Sprite.FrameSize.Y);

        }
    }

    class GKoopa : Enemy
    {
        public GKoopa(Texture2D spriteSheet, Vector2 position)
            : base(spriteSheet, position)
        {
            EntityName = "GKoopa";
            Sprite = SpriteFactory.GetSprite(this);

            CollisionBox = new Rectangle((int)Position.X, (int)Position.Y, 2 * Sprite.FrameSize.X, Sprite.FrameSize.Y);

        }
    }

    class RKoopa : Enemy
    {
        public RKoopa(Texture2D spriteSheet, Vector2 position)
            : base(spriteSheet, position)
        {
            EntityName = "RKoopa";
            Sprite = SpriteFactory.GetSprite(this);

            CollisionBox = new Rectangle((int)Position.X, (int)Position.Y, 2 * Sprite.FrameSize.X, Sprite.FrameSize.Y);
        }
    }

    public class Plant : Enemy
    {
        public Plant(Texture2D spriteSheet, Vector2 position)
            : base(spriteSheet, position)
        {
            EntityName = "Plant";
            Sprite = SpriteFactory.GetSprite(this);

            CollisionBox = new Rectangle((int)Position.X, (int)Position.Y, 2 * Sprite.FrameSize.X, Sprite.FrameSize.Y);
        }
    }

    public class HammerBro : Enemy
    {
        public HammerBro(Texture2D spriteSheet, Vector2 position)
            : base(spriteSheet, position)
        {
            EntityName = "HammerBro";
            Sprite = SpriteFactory.GetSprite(this);

            CollisionBox = new Rectangle((int)Position.X, (int)Position.Y, 2 * Sprite.FrameSize.X, Sprite.FrameSize.Y);
        }
    }

    public class Lakitu : Enemy
    {
        public Lakitu(Texture2D spriteSheet, Vector2 position)
            : base(spriteSheet, position)
        {
            EntityName = "Lakitu";
            Sprite = SpriteFactory.GetSprite(this);

            CollisionBox = new Rectangle((int)Position.X, (int)Position.Y, 2 * Sprite.FrameSize.X, Sprite.FrameSize.Y);
        }
    }

    public class Bowser : Enemy
    {
        public Bowser(Texture2D spriteSheet, Vector2 position)
            : base(spriteSheet, position)
        {
            EntityName = "Bowser";
            Sprite = SpriteFactory.GetSprite(this);

            CollisionBox = new Rectangle((int)Position.X, (int)Position.Y, 2 * Sprite.FrameSize.X, Sprite.FrameSize.Y);
        }
    }
    public class BowserFire : Enemy
    {
        public BowserFire(Texture2D spriteSheet, Vector2 position)
            : base(spriteSheet, position)
        {
            EntityName = "BowserFire";
            Sprite = SpriteFactory.GetSprite(this);

            CollisionBox = new Rectangle((int)Position.X, (int)Position.Y, 2 * Sprite.FrameSize.X, Sprite.FrameSize.Y);
        }
    }


}
