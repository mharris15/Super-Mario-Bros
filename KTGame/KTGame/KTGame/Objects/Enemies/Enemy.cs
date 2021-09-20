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



namespace KTGame.Objects.Enemies
{
    /* The player object. */
    public abstract class Enemy : IEntity
    {
        public bool CollisionOn { get; set; }
        public bool CollisionOverlay { get; set; }
        public bool BorderOn { get; set; }


        public bool Falling { get; set; }
        private bool Bouncing;
        public bool PlantMoving { get; set; }


        public ISprite Sprite { get; set; }
        public Texture2D Texture { get; set; }
        public Vector2 Position { get; set; }
        public Vector2 StartPosition { get; set; }

        private Vector2 NextPos;

        public Player ReferencePlayer { get; set; }

        private const int WALKSPEED = 1;
        private const int SHELLSPEED = 4;



        public Vector2 Velocity { get; set; }
        private Vector2 Acceleration;


        public IFactory SpriteFactory { get; set; }

        public string EntityName { get; set; }

        public bool Stomped { get; set; }
        public bool Shell { get; set; }
 

        public Rectangle CollisionBox { get; set; }

        public bool Activated { get; set; }
        public bool FacingLeft { get; set; }

        public bool CanBeStomped { get; set; }

        private bool HasJumped { get; set; }

        private int TimeSinceStomped = 0;
        private readonly int StompTimeout = 100;

        private int PlantTimeTracker = 0;
        private int PlantMoveTracker = 0;

        private int BowserTimeTracker = 0;

        private int HBTimeTracker = 0;
        private int Jumpcounter = 0;
        private int FreeFallTimer = 0;

        private readonly int MillisecondsPerPlantFrame = 10;

        protected HUD HUD { get; set; }



        public Soundbank Soundbank { get; set; }


        protected Enemy(Texture2D marioTexture, Vector2 position)
        {
            HUD = HUD.Instance;
            FacingLeft = true;
            Texture = marioTexture;
            SpriteFactory = new EnemyFactory(Texture);

            Position = position;
            StartPosition = position;

            Velocity = new Vector2(0, 0);
            Acceleration = new Vector2(0,0);

            Sprite = null;
            EntityName = "Null";
            Stomped = false;
            Shell = false;


            /* Initialization of Collision booleans */
            CollisionOn = true;
            CollisionOverlay = true;
            BorderOn = false;
            Falling = false;
            PlantMoving = true;

            Bouncing = false;

            Activated = false;

            Soundbank = null;

            HasJumped = false;

            NextPos = Position + Velocity;

        }

        public void LinkSoundbank(Soundbank soundbank)
        {
            Soundbank = soundbank;
        }
            

        public void Stomp()
        {
            if (CanBeStomped)
            {
                HUD.IncreaseScore(100);

                Soundbank.PlaySound("Stomp");

                Stomped = true;
                CanBeStomped = false;
                if (this is Goomba)
                {
                    CollisionOn = false;

                }else if (this is Lakitu)
                {
                    CollisionOn = false;
                    Velocity = new Vector2(0, 7);
                }
                else if (this is HammerBro)
                {
                    CollisionOn = false;
                    Velocity = new Vector2(0, 7);
                }

                else if (this is RKoopa || this is GKoopa)
                {
                    Shell = true;

                    if (Velocity.X == 0)
                    {
                        if (FacingLeft)
                            Velocity = new Vector2(-SHELLSPEED, Velocity.Y);
                        else
                            Velocity = new Vector2(SHELLSPEED, Velocity.Y);
                    }
                    else if (Velocity.X != 0)
                        Velocity = new Vector2(0, Velocity.Y);
                }
            }
        }


        public void Activate()
        {
            Activated = true;

            if (this is Plant)
            {
                CanBeStomped = true;
            } else if (this is Bowser || this is HammerBro || this is BowserFire)
            {
                Velocity = new Vector2(-1, 0);
                FacingLeft = true;
            }else if (this is Lakitu){
                Velocity = new Vector2(-1, 0);
                FacingLeft = true;
                CanBeStomped = true;
            }
            else
            {
                if (Position.X.CompareTo(ReferencePlayer.Position.X) >= 0)
                {
                    Velocity = new Vector2(-WALKSPEED, 0);
                    FacingLeft = true;
                }
                else
                {
                    Velocity = new Vector2(WALKSPEED, 0);
                    FacingLeft = false;
                }
            }
        }
        public virtual void SetReferencePlayer(IEntity player)
        {
            ReferencePlayer = (Player)player;
        }

        private void BlockCollision(Block block, String partCollided, Rectangle intersect)
        {
            if (!(this is Plant)){
                switch (partCollided)
                {

                    case "Top":
                        if (!(block.BlockState is BlockHiddenState))
                        {
                            Bouncing = false;
                            Velocity = new Vector2(Velocity.X, 0);
                            Acceleration = new Vector2(0, 0);
                            Position = new Vector2(NextPos.X, block.CollisionBox.Top - 31);

                            if (block.Bumped)
                            {
                                Bounce();
                                Stomp();
                            }
                        }
                        break;

                    case "Bottom":
                        if (!(block.BlockState is BlockHiddenState))
                        {
                            Velocity = new Vector2(Velocity.X, 0);
                            Position = new Vector2(NextPos.X, NextPos.Y + intersect.Height);
                        }
                        break;

                    case "Left":
                        if (!(block.BlockState is BlockHiddenState))
                        {
                            if (Shell)
                                Velocity = new Vector2(-SHELLSPEED, 0);
                            else
                                Velocity = new Vector2(-WALKSPEED, Velocity.Y);

                            Position = new Vector2(NextPos.X - intersect.Width, NextPos.Y);
                        }

                        break;

                    case "Right":
                        if (!(block.BlockState is BlockHiddenState))
                        {
                            if (Shell)
                                Velocity = new Vector2(SHELLSPEED, 0);

                            else
                                Velocity = new Vector2(WALKSPEED, Velocity.Y);

                            Position = new Vector2(NextPos.X + intersect.Width, NextPos.Y);
                        }

                        break;

                    default:
                        throw new NotSupportedException();
                }
            }
        }

        private void EnemyCollision(Enemy enemy, String partCollided, Rectangle intersect)
        {
            if (enemy.CollisionOn)
            {
                if (Shell && enemy.Shell)
                {
                    enemy.Bounce();

                    enemy.CollisionOn = false;
                }

                else
                {
                    switch (partCollided)
                    {
                        case "Top":

                            if (!Shell)
                                Bounce();

                            enemy.Stomp();

                            break;

                        case "Bottom":
                            break;

                        case "Left":
                            Position = new Vector2(NextPos.X - intersect.Width, NextPos.Y);

                            if (!Shell)
                                Velocity = new Vector2(-WALKSPEED, Velocity.Y);

                            if (Shell && !enemy.Shell && Velocity.X != 0)
                            {
                                enemy.Bounce();
                                enemy.CollisionOn = false;
                                enemy.Stomp();
                                Velocity = new Vector2(-SHELLSPEED, Velocity.Y);
                            }

                            break;

                        case "Right":

                            Position = new Vector2(NextPos.X + intersect.Width, NextPos.Y);

                            if (!Shell)
                                Velocity = new Vector2(WALKSPEED, Velocity.Y);

                            if (Shell && !enemy.Shell && Velocity.X != 0)
                            {
                                enemy.Bounce();
                                enemy.CollisionOn = false;
                                enemy.Stomp();
                                Velocity = new Vector2(SHELLSPEED, Velocity.Y);
                            }

                            break;

                        default:
                            throw new NotSupportedException();
                    }
                }
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
            Enemy CurrentEnemy = entity as Enemy;

            if (CurrentBlock != null)
                BlockCollision(CurrentBlock, partCollided, intersect);

            if (CurrentEnemy != null)
                EnemyCollision(CurrentEnemy, partCollided, intersect);
        }

        public void Freefall()
        {
            Velocity = new Vector2(Velocity.X, 5);
        }

        public void Jump()
        {
            Velocity = new Vector2(Velocity.X, -5);
        }


        public void Bounce()
        {
            if (Velocity.Y == 0)
            {
                Bouncing = true;
                Velocity = new Vector2(Velocity.X, -4);
                Acceleration = new Vector2(0, .4f);
            }
        }

        public void BowserBehavior(GameTime gameTime)
        {
            //BowserTimeTracker = +gameTime.ElapsedGameTime.Milliseconds;
            BowserTimeTracker++;
            if(Position.Y > 32 * 46)
            {
                CollisionOn = false;
            }
            if (BowserTimeTracker == 1)
            {
                Velocity = new Vector2(Velocity.X, -5); // bowser jumps
                Acceleration = new Vector2(0, .2f);
            } else if (BowserTimeTracker % 40 == 0) {
                Velocity = new Vector2(-Velocity.X, Velocity.Y);
            }
            else if (BowserTimeTracker % 24 == 0) {

                //put the fireball into the world

            } else if (BowserTimeTracker > 100) { 

                BowserTimeTracker = 0;
            }

            if (Velocity.Y == 0 && (!CollisionOn || Falling))
                Freefall();

            if (Velocity.Y <= 8)
                Velocity += Acceleration;


            Sprite.Update(gameTime);
            NextPos = Position + Velocity;
            CollisionBox = new Rectangle((int)NextPos.X, (int)NextPos.Y, 2 * Sprite.FrameSize.X, Sprite.FrameSize.Y);

            Position = NextPos;

        }

        public void LakituBehavoir(GameTime gameTime)
        {
            if (CollisionOn)
            {
                if (Position.X - ReferencePlayer.Position.X > 200)
                {
                    Velocity = new Vector2(-3, 0);
                }
                else if (Position.X - ReferencePlayer.Position.X < -100)
                {
                    Velocity = new Vector2(3, 0);
                }

                Sprite.Update(gameTime);

            }
            if(Position.Y > 1000)
            {
                Velocity = new Vector2(0, 0);
            }
                NextPos = Position + Velocity;
                CollisionBox = new Rectangle((int)NextPos.X, (int)NextPos.Y, 2 * Sprite.FrameSize.X, Sprite.FrameSize.Y);

                Position = NextPos;
            
        }

        public void HBBehavior(GameTime gameTime)
        {
            if (!Stomped)
            {
                Jumpcounter++;
                HBTimeTracker++;
                FreeFallTimer++;
                if (!CollisionOverlay)
                {
                    Freefall();
                }

                // Horizontal Movement
                if (HBTimeTracker == 1)
                {
                    Velocity = new Vector2(Velocity.X, Velocity.Y);
                }
                else if (HBTimeTracker == 75)
                {
                    Velocity = new Vector2(-Velocity.X, Velocity.Y);
                }

                if (HBTimeTracker > 140)
                {
                    HBTimeTracker = 0;
                }

                // Vertical Movement
                if (Jumpcounter > 500 && !HasJumped)
                {
                    Jump();
                    HasJumped = true;
                    CollisionOn = false;
                }

                if (Jumpcounter > 550)
                {
                    CollisionOn = true;
                    Freefall();
                    Jumpcounter = 0;

                }

                // Jump and go Through Blocks
                if (FreeFallTimer > 1000 && HasJumped)
                {
                    Jump();
                    CollisionOn = false;
                }

                if (FreeFallTimer > 1025)
                {
                    Freefall();
                }

                if (FreeFallTimer > 1075)
                {
                    CollisionOn = true;
                    FreeFallTimer = 0;
                }
            }
            
           
            Sprite.Update(gameTime);
            NextPos = Position + Velocity;
            CollisionBox = new Rectangle((int)NextPos.X, (int)NextPos.Y, 2 * Sprite.FrameSize.X, Sprite.FrameSize.Y);

            /* Detect and set player damage condition. */
            if (!CanBeStomped)
            {
                TimeSinceStomped += gameTime.ElapsedGameTime.Milliseconds;
                if (TimeSinceStomped > StompTimeout)
                {
                    TimeSinceStomped -= StompTimeout;
                    CanBeStomped = true;
                }
            }

            Position = NextPos;
        }



        public void PlantBehavior(GameTime gameTime)
        {
            if (!CollisionOn)
                Freefall();

            PlantTimeTracker += gameTime.ElapsedGameTime.Milliseconds;
            if (PlantTimeTracker > MillisecondsPerPlantFrame)
            {
                PlantTimeTracker -= MillisecondsPerPlantFrame;

                if (PlantMoveTracker < 54)
                {
                    Position = new Vector2(Position.X, Position.Y - 1);
                    PlantMoveTracker++;
                }
                else if (PlantMoveTracker >= 54 && PlantMoveTracker < 200)
                {
                    PlantMoveTracker++;
                }
                else if (PlantMoveTracker >= 200 && PlantMoveTracker < 254)
                {
                    Position = new Vector2(Position.X, Position.Y + 1);
                    PlantMoveTracker++;
                }
                else if (PlantMoveTracker >= 254 && PlantMoveTracker < 400)
                {
                    PlantMoveTracker++;
                }
                else if (PlantMoveTracker == 400)
                {
                    PlantMoveTracker = 0;

                }
            }
        }

        public void ActivatedBehavior(GameTime gameTime)
        {
            if (!(this is Plant))
            {
                if (!Bouncing && (!CollisionOn || Falling))
                    Freefall();

                CollisionOverlay = false;

                if (Velocity.Y <= 15)
                    Velocity += Acceleration;



                /* Detect and set player damage condition. */
                if (!CanBeStomped)
                {
                    TimeSinceStomped += gameTime.ElapsedGameTime.Milliseconds;
                    if (TimeSinceStomped > StompTimeout)
                    {
                        TimeSinceStomped -= StompTimeout;
                        CanBeStomped = true;
                    }
                }
            }
            else if (PlantMoveTracker != 0 || PlantMoving)
            {
                PlantBehavior(gameTime);
            }

            NextPos = Position + Velocity;

            bool DirectionChange = false;

            if (Velocity.X > 0 && FacingLeft)
            {
                FacingLeft = false;
                DirectionChange = true;
            }
            if (Velocity.X <= 0 && !FacingLeft)
            {
                FacingLeft = true;
                DirectionChange = true;
            }


            CollisionBox = new Rectangle((int)NextPos.X, (int)NextPos.Y, 2 * Sprite.FrameSize.X, Sprite.FrameSize.Y);

            Position = NextPos;

            Sprite.Update(gameTime);
            if (Stomped || DirectionChange)
            {
                Sprite = SpriteFactory.GetSprite(this);
            }
        }


        public void Update(GameTime gameTime)
        {
            if (gameTime == null)
                throw new ArgumentNullException("gameTime");


            if (!Activated)
            {
                Activate();
            }

            if (Activated)
            {
                if (this is Bowser)
                {
                    BowserBehavior(gameTime);
                }
                else if (this is HammerBro)
                {
                    HBBehavior(gameTime);
                }
                else if (this is Lakitu)
                {
                    LakituBehavoir(gameTime);
                }else
                {
                    ActivatedBehavior(gameTime);
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Sprite.Draw(spriteBatch, Position);

        }

    }
}
      
