using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using KTGame.Factories;
using KTGame.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KTGame.Objects.BlockObjects;
using KTGame.States.BlockStates;
using KTGame.Objects.Enemies;

namespace KTGame.Objects.Items
{
    public abstract  class Projectile :IEntity
    {
        public ISprite Sprite { get; set; }
        public Texture2D Texture { get; set; }
        public Vector2 Position { get; set; }
        public Vector2 Velocity { get; set; }
        public IFactory SpriteFactory { get; set; }
        public string EntityName { get; set; }
        public bool Activated { get; set; }
        public bool Moving { get; set; }
        public bool Falling { get; set; }
        public IEntity LinkedEntity { get; set; }


        public int TimeSinceBumped { get; set; }
        public  int MillisecondsPerBumpFrame { get; set; }

        public  int HorizontalSpeed  { get; set; }
        public int FireballHorizontalSpeed { get; set; }


        public Vector2 Acceleration { get; set; }


        public int BumpCounter { get; set; }
        public  int BumpCounterLimit { get; set; }
        public int FireCounterLimit { get; set; }


        public Rectangle CollisionBox { get; set; }
        public bool BorderOn { get; set; }
        public bool SpikeStuck { get; set; }
        public bool FireballShot { get; set; }


        public bool CollisionOn { get; set; }
        public bool CollisionOverlay { get; set; }

        public Vector2 StartPosition { get; set; }
        public Vector2 NextPosition { get; set; }
        public bool DLeft { get; set; }

        protected bool PlayedAppearanceSound { get; set; }
        protected bool PlayedCollectSound { get; set; }

        protected HUD HUD { get; set; }

        public Soundbank Soundbank { get; set; }

        protected Projectile(Texture2D itemTextures, Vector2 position, bool activated, bool initialCollision)
        {
            PlayedAppearanceSound = false;
            PlayedCollectSound = false;
            HUD = HUD.Instance;
            Texture = itemTextures;
            SpriteFactory = new ProjectileFactory(Texture);

            EntityName = "Fireball";

            Position = position;
            Velocity = new Vector2(0, 0);
            //Acceleration = new Vector2(0, .2f);
            SpikeStuck = false;
            FireballShot = false;

            NextPosition = Position + Velocity;

            Sprite = null;

            Activated = activated;
            Moving = false;

            /* Initialization of Collision booleans */
            CollisionOn = initialCollision;

            CollisionOverlay = false;
            BorderOn = false;
            Falling = false;

            DLeft = true;



            TimeSinceBumped = 0;
            MillisecondsPerBumpFrame = 10;

            HorizontalSpeed = 2;
            FireballHorizontalSpeed = 5;


            BumpCounter = 0;
            BumpCounterLimit = 300;
            FireCounterLimit = 90;


            Soundbank = null;

        }

        public void LinkSoundbank(Soundbank soundbank)
        {
            Soundbank = soundbank;
        }

        public void PlayerCollision(Player player)
        {
            if (!(this is Fireball))
            {
                player.TakeDamage();
            }
        }

        public void EnemyCollision(Enemy enemy)
        {
            if ((this is Fireball))
            {
                if (!(enemy is Bowser)){
                    enemy.Bounce();
                    enemy.Stomp();
                    enemy.CollisionOn = false;
                }
                Activated = false;
                Moving = false;
                CollisionOn = false;
            }
        }

        public void Reverse()
        {
            DLeft = !DLeft;
        }

        public void Bounce()
        {
            Velocity = new Vector2(Velocity.X, -6);
            Acceleration = new Vector2(0, .4f);
        }

        public virtual void SpikeWait(GameTime gameTime)
        {
            if (gameTime == null)
                throw new ArgumentNullException("gameTime");

            //if (!PlayedAppearanceSound)
            //{
            //    Soundbank.PlaySound("PowerUpAppears");
            //    PlayedAppearanceSound = true;
            //}

            TimeSinceBumped += gameTime.ElapsedGameTime.Milliseconds;
            if (TimeSinceBumped > MillisecondsPerBumpFrame)
            {
                TimeSinceBumped -= MillisecondsPerBumpFrame;

                if (BumpCounter < BumpCounterLimit)
                {
                    BumpCounter++;
                }
                if (BumpCounter == BumpCounterLimit)
                {
                    Activated = false;
                    SpikeStuck = false;
                    BumpCounter = 0;
                }
            }
        }
        public virtual void FireballWait(GameTime gameTime)
        {
            if (gameTime == null)
                throw new ArgumentNullException("gameTime");

            //if (!PlayedAppearanceSound)
            //{
            //    Soundbank.PlaySound("PowerUpAppears");
            //    PlayedAppearanceSound = true;
            //}

            TimeSinceBumped += gameTime.ElapsedGameTime.Milliseconds;
            if (TimeSinceBumped > MillisecondsPerBumpFrame)
            {
                TimeSinceBumped -= MillisecondsPerBumpFrame;

                if (BumpCounter < FireCounterLimit)
                {
                    BumpCounter++;
                }
                if (BumpCounter == FireCounterLimit)
                {
                    Activated = false;
                    Moving = false;
                    CollisionOn = false;
                    FireballShot = false;
                    BumpCounter = 0;
                }
            }
        }

        private void BlockCollision(Block block, String partCollided, Rectangle intersect)
        {
            switch (partCollided)
            {
                case "Top":
                    if (!(block.BlockState is BlockHiddenState))
                    {
                        Acceleration = new Vector2(0, 0);
                        if (this is Spikeball)
                        {
                            Velocity = new Vector2(0, 0);
                            SpikeStuck = true;
                        }
                        else if (this is Fireball)
                        {
                            Bounce();
                            FireballShot = true;
                        }
                        else
                            Velocity = new Vector2(Velocity.X, 0);

                        Position = new Vector2(NextPosition.X, block.CollisionBox.Top - 31);
                    }
                    break;

                case "Bottom":
                    if (!(block.BlockState is BlockHiddenState))
                    {
                        Velocity = new Vector2(Velocity.X, 0);
                        Position = new Vector2(NextPosition.X, NextPosition.Y + intersect.Height);
                    }
                    break;

                case "Left":
                    if (!(block.BlockState is BlockHiddenState))
                    {
                        if (this is Fireball)
                        {
                            Velocity = new Vector2(-FireballHorizontalSpeed, 0);
                        }
                        else if (this is Spikeball)
                        {
                            Velocity = new Vector2(0, 0);
                            SpikeStuck = true;
                        }
                        else if (this is BowserFireball)
                        {
                            Moving = false;
                            CollisionOn = false;
                            Activated = false;
                        }
                        Position = new Vector2(NextPosition.X - intersect.Width, NextPosition.Y);
                    }

                    break;

                case "Right":
                    if (!(block.BlockState is BlockHiddenState))
                    {
                        if (this is Fireball)
                        {
                            Velocity = new Vector2(FireballHorizontalSpeed, 0);
                        }
                        else if (this is Spikeball)
                        {
                            Velocity = new Vector2(0, 0);
                            SpikeStuck = true;
                        }
                        else if (this is BowserFireball)
                        {
                            Moving = false;
                            CollisionOn = false;
                            Activated = false;
                        }
                            Position = new Vector2(NextPosition.X + intersect.Width, NextPosition.Y);
                    }

                    break;

                default:
                    throw new NotSupportedException();
            }
        }
        public void CollisionResponse(IEntity entity, String partCollided, Rectangle intersect)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");
            if (partCollided == null)
                throw new ArgumentNullException("partCollided");
            if (intersect == null)
                throw new ArgumentNullException("intersect");

            Block CurrentBlock = entity as Block;
            Player CurrentPlayer = entity as Player;
            Enemy CurrentEnemy = entity as Enemy;


            if (CurrentBlock != null && (this is Spikeball || this is Fireball))
                BlockCollision(CurrentBlock, partCollided, intersect);
            else if (CurrentPlayer != null)
                PlayerCollision(CurrentPlayer);
            else if (CurrentEnemy != null)
                EnemyCollision(CurrentEnemy);

        }
        public void LinkEntity(IEntity entity)
        {
            LinkedEntity = entity;

            if (this is Fireball)
                Position = new Vector2(LinkedEntity.Position.X, LinkedEntity.Position.Y - 30);
            else
                Position = LinkedEntity.Position;

        }
        public void Freefall()
        {
            Acceleration = new Vector2(0, .5f);
        }

        public virtual void Activation(bool facingLeft)
        {

        }

        public virtual void Update(GameTime gameTime)
        {
            if (!Activated && !(this is Fireball))
            {
                Activation(true);
            }

            if (SpikeStuck)
            {
                SpikeWait(gameTime);
            }

            if (FireballShot)
            {
                FireballWait(gameTime);
            }

            if (this is BowserFireball)
            {
                if (Position.X <= 840)
                {
                    Moving = false;
                    CollisionOn = false;
                    Activated = false;
                }
            }

            Sprite.Update(gameTime);

            /* If falling, enter freefall. */
            if (Falling && !(this is Fireball || this is BowserFireball))
                Freefall();

            if (Moving && CollisionOn)
            {
                NextPosition = Position + Velocity;
                Position = NextPosition;

                if (Velocity.Y <= 10)
                    Velocity += Acceleration;
            }

            if (this is Fireball)
                CollisionBox = new Rectangle((int)NextPosition.X - 8, (int)NextPosition.Y + 8, 4 * Sprite.FrameSize.X, 4 * Sprite.FrameSize.Y);
            else
                CollisionBox = new Rectangle((int)NextPosition.X, (int)NextPosition.Y, 2 * Sprite.FrameSize.X, 2 * Sprite.FrameSize.Y);


            /* Keep item within horizontal boundaries. */
            if (Position.X.CompareTo(0) <= 0)
            {
                Velocity = new Vector2(HorizontalSpeed, Velocity.Y);
            }
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if (Activated)
                Sprite.Draw(spriteBatch, Position);
        }
    }
}
