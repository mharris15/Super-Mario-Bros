using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using KTGame.Factories;
using KTGame.Interfaces;
using KTGame.Objects.Items;
using KTGame.States.BlockStates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static KTGame.Factories.BlockFactory;
using KTGame.States.PlayerActions;
using KTGame.Objects.Enemies;

namespace KTGame.Objects.BlockObjects
{
    public abstract class Block : IEntity
    {
        public ISprite Sprite { get; set; }
        public IFactory SpriteFactory { get; set; }
        public Vector2 Position { get; set; }

        public Vector2 Velocity { get; set; }

        public Texture2D Texture { get; set; }

        public bool CollisionOn { get; set; }
        public bool CollisionOverlay { get; set; }

        public bool Falling { get; set; }



        public Rectangle CollisionBox { get; set; }


        private Vector2 StartPos;
        private Vector2 BreakPos1;
        private Vector2 BreakPos2;
        private Vector2 BreakPos3;
        private Vector2 BreakPos4;

        public string EntityName { get; set; }

        private bool StateChanged;
        public bool Bumped { get; set; }
        public bool Broken { get; set; }
        public bool BorderOn { get; set; }


        public bool HiddenPassedThrough { get; set; }
        private readonly int HiddenBumpTimeout = 600;

        private int TimeSinceBumped = 0;
        private readonly int MillisecondsPerFrame = 1;
        private readonly int MillisecondsPerBreakFrame = 5;


        private int BumpCounter = 0;
        private readonly int BumpCounterLimit = 16;
        private const int BrokenBaseConstant = 8;
        private const float BrokenXSpeedMultiple = 4.0f;
        private const float BrokenYSpeedMultiple = 1/1.5f;

        public IBlockState BlockState { get; set; }
        public Stack<IEntity> Items { get;}
        public bool ContainsItems { get; set; }

        public Soundbank Soundbank { get; set; }

        public Enemy EnemyPlant { get; set; }

        public bool PlayerProximity { get; set; }
        public int ProximityTimer { get; set; }
        public bool HasPlant { get; set; }
        public Pipe LinkedPipe { get; set; }
        public Pipe LinkedWarpPipe { get; set; }

        protected Block(Texture2D texture, Vector2 position)
        {
            Texture = texture;
            SpriteFactory = new BlockFactory(Texture);
            Position = position;
            EntityName = "Brick";
            Sprite = SpriteFactory.GetSprite(this);
            BlockState = null;
            StateChanged = false;
            Bumped = false;
            Broken = false;
            Items = new Stack<IEntity>();
            ContainsItems = false;
            StartPos = new Vector2(Position.X, Position.Y);
            Velocity = new Vector2(0, 0);

            /* Initialization of Collision booleans */
            CollisionOn = true;
            CollisionOverlay = true;
            BorderOn = false;
            Falling = false;

            PlayerProximity = false;
            ProximityTimer = 0;
            HasPlant = false;
            LinkedPipe = null;
            LinkedWarpPipe = null;

            HiddenPassedThrough = false;

            Vector2 NextPos = Position + Velocity;
            CollisionBox = new Rectangle((int)NextPos.X, (int)NextPos.Y, 2 * Sprite.FrameSize.X, 2 * Sprite.FrameSize.Y);

            Soundbank = null;
            EnemyPlant = null;
        }

        public void LinkSoundbank(Soundbank soundbank)
        {
            Soundbank = soundbank;
        }

        public void LinkPlant(Enemy plant)
        {
            EnemyPlant = plant;
            HasPlant = true;
        }

        public void LinkPipe(Pipe pipe)
        {
            LinkedPipe = pipe;
        }

        public void LinkWarpPipe(Pipe pipe)
        {
            LinkedWarpPipe = pipe;
        }

        public void ResetPlayerProximityCondition()
        {
            PlayerProximity = true;
            ProximityTimer = 0;

            if (HasPlant)        
                EnemyPlant.PlantMoving = false;
        }

        public void BlockPlayerProximity(Rectangle playerRec, Rectangle blockRec)
        {
            if (Math.Abs(playerRec.Left - blockRec.Right) <= 2 || Math.Abs(playerRec.Right - blockRec.Left) <= 2 || 
                    Math.Abs(playerRec.Bottom - blockRec.Top) <= 2)
            {
                if (LinkedPipe != null)
                    LinkedPipe.ResetPlayerProximityCondition();
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

            Player player = entity as Player;
            if (player != null && partCollided.Equals("Top") && player.ActionState is PlayerJumpingState)
                Bump(entity);
        }

        public void ProcessStateChange(IBlockState temp)
        {
            if (temp != null)
            {
                BlockState = temp;
                StateChanged = true;
            }
        }

        public virtual void AddItem(IEntity item)
        {

            if (item == null)
                throw new ArgumentNullException("item");
            item.Position = Position;
            Items.Push(item);
            ContainsItems = true;
        }


        public virtual void Bump(IEntity player)
        {
            if (!Bumped && BlockState.CanBeBumped)
            {
                ProcessStateChange(BlockState.BumpUpTransition(player));
                if (BlockState is BlockBrokenState)
                {
                    Soundbank.PlaySound("BrickBreak");
                    Broken = true;
                    CollisionOn = false;
                }
                else
                {
                    Soundbank.PlaySound("BrickBump");
                    Bumped = true;
                    if (Items.Count > 0)
                        ((Item)Items.Pop()).Activated = true;
                }
            }

        }
        
        private void MoveForBump(GameTime gameTime)
        {
            if (gameTime == null)
                throw new ArgumentNullException("gameTime");

            TimeSinceBumped += gameTime.ElapsedGameTime.Milliseconds;

            if (TimeSinceBumped > MillisecondsPerFrame)
            {
                TimeSinceBumped -= MillisecondsPerFrame;

                if (BumpCounter < BumpCounterLimit / 2)
                {
                    Position = new Vector2(Position.X, Position.Y - 1);
                    BumpCounter++;
                }
                else if (BumpCounter >= BumpCounterLimit / 2 && BumpCounter < BumpCounterLimit)
                {
                    Position = new Vector2(Position.X, Position.Y + 1);
                    BumpCounter++;
                }
                else if (BumpCounter == BumpCounterLimit)
                {
                    BumpCounter = 0;
                    Bumped = false;
                }
            }
        }

        private void MoveForBroken(GameTime gameTime)
        {
            if (gameTime == null)
                throw new ArgumentNullException("gameTime"); TimeSinceBumped += gameTime.ElapsedGameTime.Milliseconds;
            

            if (TimeSinceBumped > MillisecondsPerBreakFrame)
            {
                BumpCounter++;
                TimeSinceBumped -= MillisecondsPerBreakFrame;

                CollisionOn = false;

                Vector2 Displacement = new Vector2(BumpCounter * BrokenXSpeedMultiple, (float)(-1 * Math.Pow(BumpCounter * BrokenYSpeedMultiple - BrokenBaseConstant, 2) + Math.Pow(BrokenBaseConstant, 2)));
                BreakPos1 = new Vector2(StartPos.X+8 + Displacement.X, StartPos.Y+8 - Displacement.Y);
                BreakPos2 = new Vector2(StartPos.X+8 + Displacement.X/2, StartPos.Y - Displacement.Y);
                BreakPos3 = new Vector2(StartPos.X - Displacement.X, StartPos.Y+8 - Displacement.Y);
                BreakPos4 = new Vector2(StartPos.X - Displacement.X/2, StartPos.Y - Displacement.Y);
            }
        }

        public void PassThroughHidden()
        {
            HiddenPassedThrough = true;

        }

        public void PlantHandler(GameTime gameTime)
        {
            if (gameTime == null)
                throw new ArgumentNullException("gameTime"); TimeSinceBumped += gameTime.ElapsedGameTime.Milliseconds;

            ProximityTimer += gameTime.ElapsedGameTime.Milliseconds;

            if (ProximityTimer > 500)
            {
                ProximityTimer = 0;
                PlayerProximity = false;
                if (HasPlant)
                    EnemyPlant.PlantMoving = true;
            }
        }

        public virtual void Update(GameTime gameTime)
        {
            if (gameTime == null)
                throw new ArgumentNullException("gameTime");
            CollisionOverlay = false;

            Sprite.Update(gameTime);
            if (StateChanged)
            {
                Sprite = SpriteFactory.GetSprite(this);
                StateChanged = false;
            }

            if (Bumped)
            {
                MoveForBump(gameTime);
            }
            if (Broken)
            {
                MoveForBroken(gameTime);
            }

            if (HiddenPassedThrough)
            {
                TimeSinceBumped += gameTime.ElapsedGameTime.Milliseconds;
                if (TimeSinceBumped > HiddenBumpTimeout)
                {
                    TimeSinceBumped -= HiddenBumpTimeout;
                    HiddenPassedThrough = false;
                }
            }


            if (!(this is Pipe || this is PipeLength || this is FlagLength || this is FlagTip))
            {
                CollisionBox = new Rectangle((int)Position.X, (int)Position.Y, 2 * Sprite.FrameSize.X, 2 * Sprite.FrameSize.Y);
            }

            if (PlayerProximity)
                PlantHandler(gameTime);
        }
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if (!Broken)
                Sprite.Draw(spriteBatch, Position);
            else
            {
                Sprite.Draw(spriteBatch, BreakPos1);
                Sprite.Draw(spriteBatch, BreakPos2);
                Sprite.Draw(spriteBatch, BreakPos3);
                Sprite.Draw(spriteBatch, BreakPos4);
            }
        }
    }
}
