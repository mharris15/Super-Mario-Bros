using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KTGame.Collision;
using KTGame.Commands;
using KTGame.Controllers;
using KTGame.GameStates;
using KTGame.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using KTGame.Objects;
using KTGame.States.PlayerPowerUp;
using Microsoft.Xna.Framework.Content;
using KTGame.Objects.Items;
using KTGame.Objects.Enemies;
using KTGame.States.PlayerActions;
using Microsoft.Xna.Framework.Media;
using KTGame.Objects.BlockObjects;

namespace KTGame
{
    public class MarioWorld
    {
        private readonly List<IEntity> BlockList;
        private readonly List<IEntity> EnemyList;
        private readonly List<IEntity> ItemList;
        private readonly List<IEntity> ProjectileList;

        private readonly IEntity Mario;
        private CollisionTracking Collision;
        private PauseMenu PauseMenu;
        private IController KeyboardController;
        private IController GamepadController;
        private readonly Game1 Game;
        private int ResetDelay;
        int ResetBlinking;
        private Vector2 ResetPos;
        private Camera GameCamera;
        private ISprite Background;
        private readonly Texture2D Parallax;
        private readonly Texture2D BlockText;
        private HUD hud;
        private readonly SpriteFont Spritefont;
        public Soundbank Soundbank { get; set; }
        private bool WarningPlayed = false;
        private bool Open = true;
        private int period;
        private int NumBridge;
        private bool paused;

        public MarioWorld(IList<IEntity> blocks, IList<IEntity> enemies, IList<IEntity> items, IList<IEntity> projectiles, IEntity player, Game1 game, ISprite inputBackground, Texture2D inputParallax, SpriteFont spriteFont, Soundbank soundbank, Texture2D BlockTexture)
        {
            KeyboardController = new KeyboardController();
            GamepadController = new GamepadController();
            Collision = new CollisionTracking();
            PauseMenu = new PauseMenu();
            Collision.InitializeEntityList();
            Game = game ?? throw new ArgumentNullException("game");
            ResetDelay = 100;
            NumBridge = 60;
            BlockList = (List<IEntity>)blocks ?? throw new ArgumentNullException("blocks");
            GameCamera = new Camera(game.GraphicsDevice.Viewport)
            {
                Limits = new Rectangle(0, 0, 3530, game.GraphicsDevice.Viewport.Height)
            };
            Background = inputBackground;
            Parallax = inputParallax;
            hud = HUD.Instance;
            hud.LoadContent(game);
            ResetPos = new Vector2(2*32, 13*32);
            Spritefont = spriteFont;
            Soundbank = soundbank;
            period = 0;
            BlockText = BlockTexture;
            paused = false;
             

            foreach (IEntity block in BlockList)
            {
                Collision.AddEntity(block);
                block.LinkSoundbank(Soundbank);
            }

            EnemyList = (List<IEntity>)enemies ?? throw new ArgumentNullException("enemies");
            foreach (IEntity enemy in EnemyList)
            {
                Collision.AddEntity(enemy);
                enemy.LinkSoundbank(Soundbank);

            }
            ItemList = (List<IEntity>)items ?? throw new ArgumentNullException("items");
            foreach (IEntity item in ItemList)
            {
                Collision.AddEntity(item);
                item.LinkSoundbank(Soundbank);
            }
            ProjectileList = (List<IEntity>)projectiles ?? throw new ArgumentNullException("items");
            foreach (IEntity projectile in ProjectileList)
            {
                Collision.AddEntity(projectile);
                projectile.LinkSoundbank(Soundbank);
            }


            Mario = player;
            Mario.LinkSoundbank(Soundbank);

            Collision.AddEntity(Mario);

            Soundbank.PlayBackgroundMusic();
            #region Controllers
            KeyboardController.AddCommand(Keys.Up, new UpCommand(Mario));
            KeyboardController.AddCommand(Keys.Left, new LeftCommand(Mario));
            KeyboardController.AddCommand(Keys.Right, new RightCommand(Mario));
            KeyboardController.AddCommand(Keys.Down, new DownCommand(Mario));
            KeyboardController.AddToggleCommand(Keys.F, new FireballCommand(Mario));

            KeyboardController.AddCommand(Keys.Y, new ToStandardCommand(Mario));
            KeyboardController.AddCommand(Keys.U, new ToSuperCommand(Mario));
            KeyboardController.AddCommand(Keys.I, new ToFireCommand(Mario));
            KeyboardController.AddCommand(Keys.O, new TakeDamageCommand(Mario));
            KeyboardController.AddToggleCommand(Keys.C, new CollisionVisualCommand(Collision));
            KeyboardController.AddToggleCommand(Keys.P, new GamePauseCommand(PauseMenu));
            KeyboardController.AddToggleCommand(Keys.M, new GameMuteCommand(Soundbank));



            KeyboardController.AddCommand(Keys.R, new ResetCommand(game));
            KeyboardController.AddCommand(Keys.Escape, new ExitCommand(game));
            KeyboardController.AddCommand(Keys.Q, new ExitCommand(game));
            KeyboardController.AddReleaseCommand(Keys.Left, new LeftReleaseCommand(Mario));
            KeyboardController.AddReleaseCommand(Keys.Right, new RightReleaseCommand(Mario));
            KeyboardController.AddReleaseCommand(Keys.Up, new UpReleaseCommand(Mario));
            KeyboardController.AddReleaseCommand(Keys.Down, new DownReleaseCommand(Mario));

            GamepadController.AddCommand(Buttons.DPadUp, new UpCommand(Mario));
            GamepadController.AddCommand(Buttons.DPadLeft, new LeftCommand(Mario));
            GamepadController.AddCommand(Buttons.DPadRight, new RightCommand(Mario));
            GamepadController.AddCommand(Buttons.DPadDown, new DownCommand(Mario));

            GamepadController.AddReleaseCommand(Buttons.DPadLeft, new LeftReleaseCommand(Mario));
            GamepadController.AddReleaseCommand(Buttons.DPadRight, new RightReleaseCommand(Mario));
            GamepadController.AddReleaseCommand(Buttons.DPadDown, new DownReleaseCommand(Mario));


            #endregion
        }
        public void EntityUpdate(GameTime gameTime)
        {
            foreach (IEntity block in BlockList)
            {
                if ((int)block.Position.X > (GameCamera.Position.X - 2000) && (int)block.Position.X < (GameCamera.Position.X + 1100))
                {
                    Collision.AddEntity(block);
                    block.Update(gameTime);
                }
            }
            if (!GameCamera.Underground)
            {
                Console.WriteLine("Not Underground! {0}", gameTime.TotalGameTime);
                foreach (IEntity enemy in EnemyList)
                {
                    if (enemy.Position.X > (GameCamera.Position.X - 100) && enemy.Position.X < (GameCamera.Position.X + 900)
                         && enemy.Position.Y > (GameCamera.Position.Y - 50) && enemy.Position.Y < (GameCamera.Position.Y + 550))
                    {
                        Collision.AddEntity(enemy);
                        ((Enemy)enemy).SetReferencePlayer(Mario);
                        enemy.Update(gameTime);
                    }

                }
            }
            foreach (IEntity item in ItemList)
            {
                if (item.Position.X > (GameCamera.Position.X - 200) && item.Position.X < (GameCamera.Position.X + 950)
                     && item.Position.Y > (GameCamera.Position.Y - 80) && item.Position.Y < (GameCamera.Position.Y + 550))
                {
                    Collision.AddEntity(item);
                    ((Item)item).SetReferencePlayer(Mario);
                    item.Update(gameTime);
                }
            }
            foreach (IEntity projectile in ProjectileList)
            {
                Projectile proj = projectile as Projectile;
                if (proj.LinkedEntity.Position.X > (GameCamera.Position.X - 200) && proj.LinkedEntity.Position.X < (GameCamera.Position.X + 1500)
                    && proj.LinkedEntity.Position.Y > (GameCamera.Position.Y - 50) && proj.LinkedEntity.Position.Y < (GameCamera.Position.Y + 550))
                {
                    Collision.AddEntity(projectile);
                    projectile.Update(gameTime);

                    if (!(proj.Position.Y < (GameCamera.Position.Y + 600) && proj.Position.X > (GameCamera.Position.X - 200) && proj.Position.X < (GameCamera.Position.X + 950)))
                        proj.Activated = false;
                }
                else
                {
                    proj.Activated = false;
                }
            }
        }
        public void PauseUpdate()
        {
            if (PauseMenu.isPaused)
            {
                MediaPlayer.Pause();
                paused = true;
                KeyboardController.DeleteCommand(Keys.Up);
                KeyboardController.DeleteCommand(Keys.Down);
                KeyboardController.DeleteCommand(Keys.Left);
                KeyboardController.DeleteCommand(Keys.Right);
                KeyboardController.DeleteCommand(Keys.Up);
                KeyboardController.DeleteCommand(Keys.Left);
                KeyboardController.DeleteCommand(Keys.Right);
                KeyboardController.DeleteCommand(Keys.Down);
                KeyboardController.DeleteToggleCommand(Keys.F);
                KeyboardController.DeleteCommand(Keys.Y);
                KeyboardController.DeleteCommand(Keys.U);
                KeyboardController.DeleteCommand(Keys.I);
                KeyboardController.DeleteCommand(Keys.O);
                GamepadController.DeleteCommand(Buttons.DPadUp);
                GamepadController.DeleteCommand(Buttons.DPadLeft);
                GamepadController.DeleteCommand(Buttons.DPadRight);
                GamepadController.DeleteCommand(Buttons.DPadDown);
            }
            else if (paused)
            {
                KeyboardController.AddCommand(Keys.Up, new UpCommand(Mario));
                KeyboardController.AddCommand(Keys.Left, new LeftCommand(Mario));
                KeyboardController.AddCommand(Keys.Right, new RightCommand(Mario));
                KeyboardController.AddCommand(Keys.Down, new DownCommand(Mario));
                KeyboardController.AddToggleCommand(Keys.F, new FireballCommand(Mario));
                KeyboardController.AddCommand(Keys.Y, new ToStandardCommand(Mario));
                KeyboardController.AddCommand(Keys.U, new ToSuperCommand(Mario));
                KeyboardController.AddCommand(Keys.I, new ToFireCommand(Mario));
                KeyboardController.AddCommand(Keys.O, new TakeDamageCommand(Mario));
                GamepadController.AddCommand(Buttons.DPadUp, new UpCommand(Mario));
                GamepadController.AddCommand(Buttons.DPadLeft, new LeftCommand(Mario));
                GamepadController.AddCommand(Buttons.DPadRight, new RightCommand(Mario));
                GamepadController.AddCommand(Buttons.DPadDown, new DownCommand(Mario));
                paused = false;
            }
        }
        public void TimeUpdate()
        {
            if (hud.Time <= 100 && !WarningPlayed)
            {
                WarningPlayed = true;
                MediaPlayer.Stop();
                //Soundbank.PlaySound("TimeWarning");
                Soundbank.Hurry = true;
                Soundbank.PlayBackgroundMusic();
            }
            else
            {
                Soundbank.Hurry = false;
            }
            // If timer runs out, Player dies
            if (hud.Time == 0 && !hud.EndGame)
            {

                if (!(((Player)Mario).PowerState is PowerStandardState))
                {
                    ((Player)Mario).TakeDamage();
                }

                ((Player)Mario).CanBeDamaged = true;
                ((Player)Mario).TakeDamage();
            }
            // If game is end
            if (hud.EndGame)
            {
                KeyboardController.DeleteToggleCommand(Keys.C);
                KeyboardController.DeleteToggleCommand(Keys.P);
                KeyboardController.DeleteToggleCommand(Keys.M);
                //KeyboardController.DeleteCommand(Keys.Y);
                KeyboardController.DeleteCommand(Keys.R);
                KeyboardController.DeleteCommand(Keys.Escape);
                KeyboardController.DeleteCommand(Keys.Q);
                KeyboardController.DeleteToggleCommand(Keys.Y);
                KeyboardController.DeleteToggleCommand(Keys.N);
                KeyboardController.AddToggleCommand(Keys.Y, new ResetCommand(Game));
                KeyboardController.AddToggleCommand(Keys.N, new ExitCommand(Game));
            }
        }
        public void Update(GameTime gameTime)
        {
            Collision.InitializeEntityList();
            PauseUpdate();
            if (!PauseMenu.isPaused && !hud.EndGame)
            {
                if (!(((Player)Mario).PowerState is PowerDeadState))
                {
                    EntityUpdate(gameTime);
                    if (GameCamera.Underground) {
                        period = 0;
                        GameCamera.Limits = new Rectangle(0, 0, 3530, Game.GraphicsDevice.Viewport.Height);
                        ((Player)Mario).MinHorizontal = 0;
                        //Console.WriteLine("period=0!!!!!!!!!!");
                    }
                    if (GameCamera.Boss){
                        if (period == 1 && Mario.Position.Y >= 40 * 32+Game.GraphicsDevice.Viewport.Height/2)
                            ((Player)Mario).TakeDamage();
                    }
                    if (Mario.Position.X > (44 * 32)/*&&!GameCamera.Underground*/)
                    {
                        //Console.WriteLine("reset!!");
                        ResetPos.X = 44 * 32 + 16;
                        if (Mario.Position.Y > 32 * 37)
                        {
                            ResetPos.Y = 40 * 32;
                        }
                        else
                        {
                            ResetPos.Y = 12 * 32;
                        }
                        if (period == 0)
                        {
                            
                            GameCamera.Limits = new Rectangle(/*(int)GameCamera.Position.X*/1011, 0, 3530 - /*(int)GameCamera.Position.X*/1011, Game.GraphicsDevice.Viewport.Height);
                            ((Player)Mario).MinHorizontal = /*(int)GameCamera.Position.X*/1011;
                            if (GameCamera.Boss)
                                ((Player)Mario).MaxHorizontal = 1011 + Game.GraphicsDevice.Viewport.Width - 2*Mario.Sprite.FrameSize.X;
                            period = 1;
                            //Console.WriteLine("period=1!!!!!!!!!!");
                            //Console.WriteLine("Xposition = {0}", (int)GameCamera.Position.X);
                        }
                        if (GameCamera.Win)
                        {
                            ((Player)Mario).MaxHorizontal = 3311 + Game.GraphicsDevice.Viewport.Width - 2 * Mario.Sprite.FrameSize.X;
                        }

                    }
                    Collision.AddEntity(Mario);
                    if (hud.BossEnd)
                    {
                        if (NumBridge >= 0) {
                            if (NumBridge % 3 == 0)
                            {
                                RemoveBridge();
                            }
                            NumBridge--;
                        }
                        else {
                            GameCamera.Win = true;
                            hud.BossEnd = false;
                        }
                    }

                    if (ResetBlinking != 0)
                    {
                        ResetBlinking--;
                    }
                    Collision.UpdateCollision();
                    ResetDelay = 200;
                    hud.Update(gameTime);

                }
                else
                {
                    ResetDelay--;

                    if (ResetDelay == 0)
                    {
                        Soundbank.PlayBackgroundMusic();
                        if(hud.Lives == 0)
                        {
                           hud.GameOver();
                        }
                        ((Player)Mario).ChangePosition(ResetPos);
                        ((Player)Mario).ToStandardStateCheat();
                        if (hud.Lives > 0)
                        {
                            hud.LoseLife();
                            hud.TimeReset();
                            ResetBlinking = 40;
                        }
                    }
                }
                Mario.Update(gameTime);
                GameCamera.LookAt(Mario.Position);
                MediaPlayer.Resume();
            }
            TimeUpdate();
            KeyboardController.UpdateInput();
            GamepadController.UpdateInput();
        }
        public void CloseRoom()
        {
            for (int i = 40; i < 45; i++)
            {
                for (int j = 29; j < 33; j++) {
                    BlockList.Add(new BossBrick(BlockText, new Vector2(32 * j, 32 * i)));
                    Collision.AddEntity(BlockList.ElementAt(BlockList.Count - 1));
                }
            }
            Open = false;
            ResetPos.X = 44 * 32 + 16;
            ResetPos.Y = 40 * 32;
        }

        public void RemoveBridge()
        {
            BlockList.RemoveAt(507);    //Remove From 507 to
        }


        public void Draw(SpriteBatch spriteBatch)
        {
            if (spriteBatch == null)
            {
                throw new ArgumentNullException("spriteBatch");
            }
                if (Mario.Position.Y < 670)
                {
                    GameCamera.Underground = false;
                    GameCamera.Boss = false;
                    spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.LinearWrap, null, null);
                    spriteBatch.Draw(Parallax, new Rectangle(0, 0, 800, 450),
                        new Rectangle(Convert.ToInt32(GameCamera.Position.X * 0.3), 0, Parallax.Width, Parallax.Height),
                        Color.White);
                    spriteBatch.End();
                }
                else if(Mario.Position.Y < 1100)
                {
                    GameCamera.Boss = false;
                    GameCamera.Underground = true;
                }
                else
                {
                    if (Open && Mario.Position.X > 1200)
                    {
                        CloseRoom();
                    }
                    GameCamera.Boss = true;
                }

                spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null,
                    GameCamera.GetViewMatrix(new Vector2(1f)));
            if (!hud.EndGame)
            {
                Background.Draw(spriteBatch, new Vector2(0, 0));
                if (ResetBlinking % 4 != 1)
                {
                    Mario.Draw(spriteBatch);
                }

                foreach (IEntity item in ItemList)
                {
                    if (item.Position.X > (GameCamera.Position.X - 100) &&
                        item.Position.X < (GameCamera.Position.X + 800))
                        item.Draw(spriteBatch);
                }
                foreach (IEntity projectile in ProjectileList)
                {
                    if (projectile.Position.X > (GameCamera.Position.X - 100) &&
                        projectile.Position.X < (GameCamera.Position.X + 800))
                        projectile.Draw(spriteBatch);
                }
                if (!GameCamera.Underground)
                {
                    foreach (IEntity enemy in EnemyList)
                    {
                        if (enemy.Position.X > (GameCamera.Position.X - 100) &&
                            enemy.Position.X < (GameCamera.Position.X + 800))
                            enemy.Draw(spriteBatch);
                    }
                }

                foreach (IEntity block in BlockList)
                {
                    if (block.Position.X > (GameCamera.Position.X - 100) &&
                        block.Position.X < (GameCamera.Position.X + 800))
                        block.Draw(spriteBatch);
                }

            }
            hud.Draw(spriteBatch, Spritefont, GameCamera);
            PauseMenu.Draw(spriteBatch, Spritefont, GameCamera);
                    
            spriteBatch.End();
        }
    }
}