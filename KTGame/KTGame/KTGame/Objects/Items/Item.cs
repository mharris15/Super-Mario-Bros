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

namespace KTGame.Objects.Items
{
    public abstract  class Item :IEntity
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
        public IEntity ReferencePlayer { get; set; }


        public int TimeSinceBumped { get; set; }
        public  int MillisecondsPerBumpFrame { get; set; }
        public  int MillisecondsPerCoinFrame { get; set; }

        public  float CoinBaseConstant { get; set; }
        public  float CoinYSpeedMultiple { get; set; }

        public  int HorizontalSpeed  { get; set; }


        private Vector2 Acceleration;


        public int BumpCounter { get; set; }
        public  int BumpCounterLimit { get; set; }
        public  int CoinCounterLimit { get; set; }

        public Rectangle CollisionBox { get; set; }
        public bool BorderOn { get; set; }

        public bool CollisionOn { get; set; }
        public bool CollisionOverlay { get; set; }

        public Vector2 StartPosition { get; set; }
        public Vector2 NextPosition { get; set; }
        public bool DLeft { get; set; }
        private bool BouncingFromBlock;

        protected bool PlayedAppearanceSound { get; set; }
        protected bool PlayedCollectSound { get; set; }

        protected HUD HUD { get; set; }

        public Soundbank Soundbank { get; set; }



        protected Item(Texture2D itemTextures, Vector2 position, bool activated, bool initialCollision)
        {
            PlayedAppearanceSound = false;
            PlayedCollectSound = false;
            HUD = HUD.Instance;
            Texture = itemTextures;
            SpriteFactory = new ItemFactory(Texture);

            EntityName = "Mushroom";

            Position = position;
            Velocity = new Vector2(0, 0);
            Acceleration = new Vector2(0, .2f);


            NextPosition = Position + Velocity;

            Sprite = null;
            BouncingFromBlock = false;

            Activated = activated;
            Moving = false;

            /* Initialization of Collision booleans */
            CollisionOn = initialCollision;
            if (CollisionOn)
                Activated = true;

            CollisionOverlay = false;
            BorderOn = false;
            Falling = false;
            Moving = false;

            DLeft = true;



            TimeSinceBumped = 0;
            MillisecondsPerBumpFrame = 10;
            MillisecondsPerCoinFrame = 20;

            CoinBaseConstant = 11.5f;
            CoinYSpeedMultiple = 1 / 1.5f;

            HorizontalSpeed = 2;


            BumpCounter = 0;
            BumpCounterLimit = 32;
            CoinCounterLimit = 19;

            Soundbank = null;

        }

        public void LinkSoundbank(Soundbank soundbank)
        {
            Soundbank = soundbank;
        }

        public void PlayerCollision()
        {
            Activated = false;
            Moving = false;

            if (this is Coin && !PlayedCollectSound)
            {
                Soundbank.PlaySound("Coin");
                PlayedCollectSound = true;
                HUD.AddCoins();
            }
            if (!(this is Star) && !PlayedCollectSound)
            {
                if (this is OneUp && !PlayedCollectSound)
                {
                    HUD.ExtraLife();
                    Soundbank.PlaySound("OneUpCollect");
                }
                else if (this is Mushroom && !PlayedCollectSound)
                {
                    HUD.IncreaseScore(1000);
                    Soundbank.PlaySound("PowerUpCollect");
                }
                else if (this is Flower && !PlayedCollectSound)
                {
                    HUD.IncreaseScore(1000);
                    Soundbank.PlaySound("PowerUpCollect");
                }
                else if (this is Axe && !PlayedCollectSound)
                {
                    HUD.BeatGame();
                }
                PlayedCollectSound = true;
            }
            if (this is Star && !PlayedCollectSound)
            {
                Soundbank.PlaySound("PowerUpCollect");
                HUD.IncreaseScore(1000);
                PlayedCollectSound = true; //makes sure the points are only collected once
            }


        }

        public void Reverse()
        {
            DLeft = !DLeft;
        }

        public void BlockBounce()
        {
            BouncingFromBlock = true;
            Velocity = new Vector2(Velocity.X, -5);
            Acceleration = new Vector2(0, .3f);
        }

        public virtual void LeaveBlock(GameTime gameTime)
        {
            if (gameTime == null)
                throw new ArgumentNullException("gameTime");

            if (!PlayedAppearanceSound)
            {
                Soundbank.PlaySound("PowerUpAppears");
                PlayedAppearanceSound = true;
            }

            TimeSinceBumped += gameTime.ElapsedGameTime.Milliseconds;
            if (TimeSinceBumped > MillisecondsPerBumpFrame)
            {
                TimeSinceBumped -= MillisecondsPerBumpFrame;

                if (BumpCounter < BumpCounterLimit)
                {
                    Position = new Vector2(Position.X, Position.Y - 1);
                    BumpCounter++;
                }
                if (BumpCounter == BumpCounterLimit)
                {
                    Moving = true;
                    CollisionOn = true;
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
                        Falling = false;

                        if (!(this is Star)){
                            BouncingFromBlock = false;
                            Acceleration = new Vector2(0, 0);
                            Velocity = new Vector2(Velocity.X, 0);
                        }
                        else
                        {
                            BouncingFromBlock = true;

                            Velocity = new Vector2(Velocity.X, -10);
                            Acceleration = new Vector2(0, .3f);
                        }
                       
                        Position = new Vector2(NextPosition.X, block.CollisionBox.Top - 32);

                        if (block.Bumped)
                        {
                            BlockBounce();

                        }
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
                        DLeft = false;
                        Velocity = new Vector2(-HorizontalSpeed, Velocity.Y);
                        Position = new Vector2(NextPosition.X - intersect.Width, NextPosition.Y);
                    }

                    break;

                case "Right":
                    if (!(block.BlockState is BlockHiddenState))
                    {
                        DLeft = true;
                        Velocity = new Vector2(HorizontalSpeed, Velocity.Y);
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

            if (CurrentBlock != null)
                BlockCollision(CurrentBlock, partCollided, intersect);
            else if (CurrentPlayer != null)
            {
                PlayerCollision();
            }
        }
        public virtual void SetReferencePlayer(IEntity player)
        {
            ReferencePlayer = player;
        }
        public void Freefall()
        {
            Velocity = new Vector2(Velocity.X, 4);
        }
        public virtual void Update(GameTime gameTime)
        {
            if (!Activated)
            {
                StartPosition = new Vector2(Position.X, Position.Y);
            }

            Sprite.Update(gameTime);

            /* If falling, enter freefall. */
            if (!(this is Star) && Falling && !BouncingFromBlock)
                Freefall();

            if (!CollisionOn && Activated)
            {
                if(!(this is Coin))
                {
                    LeaveBlock(gameTime);
                }
            }

            if (Moving && CollisionOn)
            {
                NextPosition = Position + Velocity;
                Position = NextPosition;

                if (Velocity.Y <= 10)
                    Velocity += Acceleration;
            }

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
