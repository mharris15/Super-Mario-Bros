using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using KTGame.Factories;
using KTGame.Interfaces;
using KTGame.States.PlayerActions;
using KTGame.States.PlayerPowerUp;
using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KTGame.Objects.Items;
using KTGame.Objects.BlockObjects;
using KTGame.States.BlockStates;
using KTGame.Objects.Enemies;


namespace KTGame.Objects
{
    /* The player object. */
    public class Player: IEntity
    {
        public int MaxHorizontal { get; set; }
        public int MinHorizontal { get; set; }
        private const int MaxVertical = 1000;
        private int MinVertical;
        protected HUD HUD { get; set; }
        public bool Falling { get; set; }
        public bool CollisionOn { get; set; }
        public bool CollisionOverlay { get; set; }
        public bool BorderOn { get; set; }

        public bool Warpable { get; set; }
        public bool Warping { get; set; }
        Vector2 WarpExitPosition { get; set; }
        Vector2 WarpEntryPosition { get; set; }


        public IPlayerActionState ActionState { get; set; }
        public IPlayerPowerState PowerState { get; set; }
        private bool StateChanged;
        public ISprite Sprite { get; set; }
        public Texture2D Texture { get; set; }
        public Vector2 Position { get; set; }
        private Vector2 NextPos;
        public Rectangle CollisionBox { get; set; }
        public Vector2 Velocity { get; set; }
        public Vector2 Acceleration { get; set; }

        public IFactory SpriteFactory { get; set; }
        public string EntityName { get; set; }

        Projectile[] FireballPool { get; set; }

        public int DamageTimeout { get; set; }
        public int TimeElapsed { get; set; }
        public bool CanBeDamaged { get; set; }
        private readonly int MillisecondsPerFrame = 1;

        private int BumpCounter = 0;
        private readonly int BumpCounterLimit = 200;
        public bool FlagTouched { get; set; }

        public Soundbank Soundbank { get; set; }


        public Player(Texture2D marioTexture, Vector2 position)
        {
            MaxHorizontal = 3500;
            /* Initialization of action state and power up state. */
            ActionState = new PlayerIdleState(this, null, true);
            PowerState = new PowerStandardState(this, null);
            StateChanged = false;

            FlagTouched = false;
            MinHorizontal = 0;
            

            /* Initialization of Entity texture */
            Texture = marioTexture;
            EntityName = "Player";
            SpriteFactory = new MarioFactory(Texture);
            Sprite = SpriteFactory.GetSprite(this);

            /* Variables used for damaging the player. */
            TimeElapsed = 0;
            DamageTimeout = 1000;
            CanBeDamaged = true;

            /* Initialization of Collision booleans */
            CollisionOn = true;
            CollisionOverlay = true;
            BorderOn = false;
            Falling = true;


            Warpable = false;
            WarpExitPosition = position;
            WarpEntryPosition = position;

            FireballPool = null;


            /* Initialization of Player Position, Velocity, Collision Boxes. */
            Position = position;
            Velocity = new Vector2(0, 0);

            NextPos = Position + Velocity;
            CollisionBox = new Rectangle((int)NextPos.X, (int)NextPos.Y, 2 *Sprite.FrameSize.X, 2 * Sprite.FrameSize.Y);

            Soundbank = null;
            HUD = HUD.Instance;
        }

        public void LinkSoundbank(Soundbank soundbank)
        {
            Soundbank = soundbank;
        }

        public void LinkFireballPool(Projectile[] pool)
        {
            FireballPool = pool;
        }

        public void ProcessActionInput(IPlayerActionState temp)
        {
            if (temp != null)
            {
                ActionState = temp;
                StateChanged = true;
            }
        }

        public void ProcessPowerInput(IPlayerPowerState temp)
        {

            if (temp != null)
            {   
                PowerState = temp;
                StateChanged = true;
            }
        }

        public void ItemCollision(IEntity item)
        {
            if (item is Mushroom)
                ProcessPowerInput(PowerState.RetrieveMushroom());
            else if (item is Flower)
                ProcessPowerInput(PowerState.RetrieveFlower());
            else if (item is Star)
                ProcessPowerInput(PowerState.RetrieveStar());
            else if (item is Coin)
                ProcessPowerInput(PowerState); //Temp
            else if (item is OneUp)
                ProcessPowerInput(PowerState); //Temp
            //else if (item is Star)
            //    ProcessPowerInput(PowerState); //Temp
        }


        public void Attack()
        {
            bool Fired = false;
            
            if (PowerState is PowerFireState &&  (!(Warping || FlagTouched)))
            {
                for (int Counter = 0; Counter<3; Counter++)
                {
                    Fireball Fireball = FireballPool[Counter] as Fireball;
                    if (!Fired && !Fireball.Activated)
                    {
                        Soundbank.PlaySound("Fireball");
                        Fireball.Activation(!ActionState.FacingRight);
                        Fired = true;
                    }
                }
                Fired = false;
            }
        }
        public void InputUp()
        {
            if (!(Warping || FlagTouched))
                ProcessActionInput(ActionState.InputUpTransition());
        }
        public void InputLeft()
        {
            if (!(Warping || FlagTouched))
                ProcessActionInput(ActionState.InputLeftTransition());
        }
        public void InputRight()
        {
            if (!(Warping || FlagTouched))
                ProcessActionInput(ActionState.InputRightTransition());
        }
        public void InputDown()
        {
            if (Warpable && !Warping)
            {
                Position = WarpEntryPosition;
                Warping = true;
            }
            if (!FlagTouched)
                ProcessActionInput(ActionState.InputDownTransition());

        }
        public void ReleaseLeft()
        {
            if (!(Warping || FlagTouched))
                ProcessActionInput(ActionState.InputLeftReleaseTransition());
        }
        public void ReleaseRight()
        {
            if (!(Warping || FlagTouched))
                ProcessActionInput(ActionState.InputRightReleaseTransition());
        }
        public void ReleaseUp()
        {
            if (!(Warping || FlagTouched))
                ProcessActionInput(ActionState.InputUpReleaseTransition());
        }
        public void ReleaseDown()
        {
            if (!FlagTouched)
                ProcessActionInput(ActionState.InputDownReleaseTransition());
        }


        public void ToStandardStateCheat()
        {
            ProcessPowerInput(PowerState.ToStandardStateCheat());
        }
        public void ToSuperStateCheat()
        {
            ProcessPowerInput(PowerState.ToSuperStateCheat());
        }

        public void ToFireStateCheat()
        {
            ProcessPowerInput(PowerState.ToFireStateCheat());
        }

        public void StarStateReversion(IPlayerPowerState buffer)
        {
            ProcessPowerInput(buffer);
        }

        public void TakeDamage()
        {
            if (CanBeDamaged)
            {
                ProcessPowerInput(PowerState.TakeDamageCheat());
                CanBeDamaged = false;
            }
        }

        public void Bounce()
        {
            ProcessActionInput(new PlayerJumpingState(this, ActionState, ActionState.FacingRight));
        }

        public void Freefall()
        {
            ProcessActionInput(new PlayerFallingState(this, ActionState, ActionState.FacingRight));
        }

        public void PlayerPipeProximity(Block pipe)
        {
            if ( Math.Abs(CollisionBox.Bottom - pipe.CollisionBox.Top) <= 2)
            {
                if (pipe.LinkedWarpPipe != null)
                {
                    Warpable = true;
                    WarpEntryPosition = new Vector2(pipe.Position.X + 15, pipe.Position.Y - 64);
                    WarpExitPosition = new Vector2(pipe.LinkedWarpPipe.Position.X + 15, pipe.LinkedWarpPipe.Position.Y);
                }
            }
            else
            {
                Warpable = false;
            }
        }

        private void BlockCollision(Block block, String partCollided, Rectangle intersect)
        {
            switch (partCollided)
            {
                case "Top":
                    if (!(block.BlockState is BlockHiddenState) && !(block is FlagLength))
                    {
                        Velocity = new Vector2(Velocity.X, 0);
                        Position = new Vector2(NextPos.X, block.CollisionBox.Top - 32);
                    }
                    else
                    {
                        block.PassThroughHidden();
                    }

                    break;

                case "Bottom":
                    if (!block.HiddenPassedThrough)
                    {
                        Velocity = new Vector2(Velocity.X, 0);
                        Position = new Vector2(NextPos.X, NextPos.Y + intersect.Height);
                    }
                    break;

                case "Left":
                    if (!(block.BlockState is BlockHiddenState))
                    {
                        Velocity = new Vector2(0, Velocity.Y);
                        Position = new Vector2(NextPos.X - intersect.Width, NextPos.Y);

                        if (block is FlagTip || block is FlagLength)
                        {
                            FlagTouched = true;
                            block.CollisionOn = false;
                            Position = new Vector2(NextPos.X - intersect.Width/2, NextPos.Y);
                        }
                    }
                    else
                    {
                        block.PassThroughHidden();
                    }
                    break;

                case "Right":
                    if (!(block.BlockState is BlockHiddenState))
                    {
                        Velocity = new Vector2(0, Velocity.Y);
                        Position = new Vector2(NextPos.X + intersect.Width, NextPos.Y);
                    }
                    else
                    {
                        block.PassThroughHidden();
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
            Enemy CurrentEnemy = entity as Enemy;
            Item CurrentItem = entity as Item;

            if (CurrentBlock != null)
                BlockCollision(CurrentBlock, partCollided, intersect);
            else if (CurrentItem != null && CurrentItem.Activated)
                ItemCollision(entity);
            else if (CurrentEnemy != null && CurrentEnemy.CollisionOn)
            {
                if (PowerState is PowerStarState)
                {
                    CurrentEnemy.Bounce();
                    CurrentEnemy.Stomp();
                    CurrentEnemy.CollisionOn = false;
                }
                else if (CurrentEnemy.Shell)
                {
                    CurrentEnemy.FacingLeft = !ActionState.FacingRight;
                    Bounce();

                    CurrentEnemy.Bounce();
                    CurrentEnemy.Stomp();
                }
                else if (CurrentEnemy is Plant || CurrentEnemy is Bowser || CurrentEnemy is BowserFire|| !(ActionState is PlayerFallingState))
                {
                    TakeDamage();
                }
                else if (ActionState is PlayerFallingState)
                {
                    Bounce();
                    CurrentEnemy.FacingLeft = !ActionState.FacingRight;
                    CurrentEnemy.Stomp();
                }
                else
                {
                    Bounce();
                    CurrentEnemy.Stomp();
                }
            }
        }

        public void DeathFall(GameTime gameTime)
        {
            TimeElapsed += gameTime.ElapsedGameTime.Milliseconds;

            if (TimeElapsed > MillisecondsPerFrame)
            {
                TimeElapsed = 0;
                BumpCounter++;


                if (BumpCounter >= 40 && BumpCounter < BumpCounterLimit / 2)
                {
                    Position = new Vector2(Position.X, Position.Y - 3);
                }
                else if (BumpCounter >= BumpCounterLimit / 4 && BumpCounter < BumpCounterLimit)
                {
                    Position = new Vector2(Position.X, Position.Y + 4);
                }
                else if (BumpCounter == BumpCounterLimit)
                {
                    BumpCounter = 0;
                }
            }
        }

        public void Warp(GameTime gameTime)
        {
            TimeElapsed += gameTime.ElapsedGameTime.Milliseconds;
            if (TimeElapsed > MillisecondsPerFrame)
            {
                TimeElapsed = 0;
                BumpCounter++;
                
                if (BumpCounter == 1)
                {
                    Soundbank.PlaySound("PipeTravel");
                }
                if (BumpCounter < 64)
                {
                    Position = new Vector2(Position.X, Position.Y + 1);
                }
                else if (BumpCounter == 100)
                {
                    Position = WarpExitPosition;
                    Soundbank.PlaySound("PipeTravel");
                    if (!(PowerState is PowerStarState))
                    {
                        if (Position.Y < 670)
                        {
                            Soundbank.SwitchSong("overworld");
                        }
                        else
                        {
                            Soundbank.SwitchSong("underworld");
                        }
                    }
                }
                else if (BumpCounter > 110 && BumpCounter < 174)
                {
                    Position = new Vector2(Position.X, Position.Y - 1);
                }
                else if (BumpCounter == 200)
                {
                    Warping = false;
                    BumpCounter = 0;
                }
            }
        }

        public void Winning(GameTime gameTime)
        {
            TimeElapsed += gameTime.ElapsedGameTime.Milliseconds;
            if (TimeElapsed > MillisecondsPerFrame)
            {
                TimeElapsed = 0;
                if (BumpCounter <=150)
                    BumpCounter++;

                if (BumpCounter == 1)
                {
                    HUD.IncreaseScore(10*(1000 - (int)Position.Y));
                    ProcessActionInput(new PlayerFlagState(this, ActionState, true));
                }
                if (BumpCounter == 50)
                {
                    Velocity = new Vector2(0, 4);
                    Soundbank.PlaySound("Flag");
                }
                if (BumpCounter == 150 && Velocity.Y == 0)
                {
                    Soundbank.SwitchSong("victory");

                    Velocity = new Vector2(5, 0);
                    ProcessActionInput(new PlayerWalkingState(this, ActionState, true));
                    HUD.Win();

                }
            }
        }


        public void Update(GameTime gameTime)
        {

            if (gameTime == null)
                throw new ArgumentNullException("gameTime");

            //Debug.WriteLine(Position);
            if (HUD.Lives < 0)
            {
                ProcessActionInput(new PlayerDyingState(this, ActionState, ActionState.FacingRight));
            }
            /* Update the actionstate. */
            ActionState.Update(gameTime);
            PowerState.Update(gameTime);

            /* Update sprite. */
            if (PowerState is PowerStarState)
            {
                Sprite.GetFramesY()[0] = PowerState.PowerFrameVerticalOffset;
            }
            Sprite.Update(gameTime);


            if (StateChanged)
            {
                Sprite = SpriteFactory.GetSprite(this);
                StateChanged = false;
            }

            if (ActionState is PlayerDyingState)
            {
                DeathFall(gameTime);
            }
            else
            {
                if (Warping)
                {
                    Warp(gameTime);
                }
                else
                {
                    /* Detect and set player damage condition. */
                    if (!CanBeDamaged)
                    {
                        TimeElapsed += gameTime.ElapsedGameTime.Milliseconds;
                        if (TimeElapsed >= DamageTimeout)
                        {
                            TimeElapsed = 0;
                            CanBeDamaged = true;
                        }
                    }

                    /* If Player isn't jumping/dying, but is falling, enter freefall. */
                    if (!(ActionState is PlayerJumpingState||ActionState is PlayerFallingState|| ActionState is PlayerFlagState) && Falling)
                        Freefall();

                    if (FlagTouched)
                        Winning(gameTime);
                    /* Calculate next player position. */
                    NextPos = Position + Velocity;


                    /* Keep collision consistent for different player sizes */
                    MinVertical = 2 * Sprite.FrameSize.Y;
                    if (PowerState is PowerStarState)
                    {
                        if(!(((PowerStarState)PowerState).BufferedState is PowerStandardState))
                            CollisionBox = new Rectangle((int)NextPos.X, (int)NextPos.Y - 32, 2 * Sprite.FrameSize.X, 2 * Sprite.FrameSize.Y);
                        else
                            CollisionBox = new Rectangle((int)NextPos.X, (int)(NextPos.Y), 2 * Sprite.FrameSize.X, 2 * Sprite.FrameSize.Y);
                    }
                    else
                    {
                        if (!(PowerState is PowerStandardState))
                            CollisionBox = new Rectangle((int)NextPos.X, (int)NextPos.Y - 32, 2 * Sprite.FrameSize.X, 2 * Sprite.FrameSize.Y);
                        else
                            CollisionBox = new Rectangle((int)NextPos.X, (int)(NextPos.Y), 2 * Sprite.FrameSize.X, 2 * Sprite.FrameSize.Y);
                    }
                   

                    /* Update player position. */
                    Position = NextPos;

                    /* Keep player within horizontal boundaries. */
                    if (Position.X.CompareTo(MinHorizontal) < 0)
                        Position = new Vector2(MinHorizontal, Position.Y);
                    else if (Position.X.CompareTo(MaxHorizontal) > 0)
                        Position = new Vector2(MaxHorizontal, Position.Y);

                    else if (Position.Y.CompareTo(MinVertical) <= 0)
                    {
                        Position = new Vector2(Position.X, MinVertical);
                        Velocity = new Vector2(Velocity.X, 0);
                    }
                }
                
            }

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Sprite.Draw(spriteBatch, Position);
        }

        public void ChangePosition(Vector2 Checkpoint)
        {
            Position = Checkpoint;
        }


    }
}
