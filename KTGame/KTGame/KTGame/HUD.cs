using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using KTGame.Factories;
using KTGame.Interfaces;
using KTGame.Objects;
using KTGame.Objects.Items;
using KTGame.Sprites;
using KTGame.Sprites.Items;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace KTGame
{
    public class HUD
    {
        private static HUD _instance = null;
        
        public int Time { get; set; }
        public int Lives { get; set; }
        private int Coins;
        private int Score;
        private Coin coin;
        public Boolean BossEnd { get; set; }
        //private Texture2D coinTexture;
        private Texture2D marioTexture;
        private float currentTime = 0;
        private readonly float countDuration = 1;
        private readonly float winCountDuration = 0.01f;
        private bool isWon; //player reaches flag
        public bool EndGame { get; set; } //game is end, i.e. running out of lives or after counting points when winning
        public bool IsOver { get; set; } //player runs out of lives
        public HUD()
        {
            Score = 0;
            Coins = 0;
            Lives = 3;
            Time = 400;
            isWon = false;
            IsOver = false;
            EndGame = false;
            BossEnd = false;
        }

        public static HUD Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new HUD();
                return _instance;
            }
        }

        public void LoadContent(Game game)
        {
            marioTexture = game.Content.Load<Texture2D>("Mario");
            //coinTexture = game.Content.Load<Texture2D>("Coin");
            Texture2D itemsTexture = game.Content.Load<Texture2D>("Sprites/AllItems");
            coin = new Coin(itemsTexture, new Vector2(0,0),true,true);
        }


        public void Draw(SpriteBatch spriteBatch, SpriteFont font,Camera camera)
        {
            

            // Draw Player Name and Score
            spriteBatch.DrawString(font, "MARIO" ,new Vector2(camera.Position.X + 50,camera.Position.Y + 10), Color.White);
            spriteBatch.DrawString(font,Score.ToString("000000"), new Vector2(camera.Position.X + 50, camera.Position.Y + 30), Color.White);

            // Draw Coin
            coin.Position = new Vector2(camera.Position.X + 270, camera.Position.Y + 60);
            coin.Draw(spriteBatch);

            spriteBatch.DrawString(font, "X " + Coins.ToString("00"), new Vector2(camera.Position.X + 315, camera.Position.Y + 30), Color.White);
            
            // Draw Lives
            spriteBatch.Draw(marioTexture, new Vector2(camera.Position.X + 470, camera.Position.Y + 30), Color.White);
            spriteBatch.DrawString(font, "X " + Lives.ToString("00"), new Vector2(camera.Position.X + 515, camera.Position.Y + 30), Color.White);

            // Draw Time 
            spriteBatch.DrawString(font, "TIME", new Vector2(camera.Position.X + 700, camera.Position.Y + 10), Color.White);
            spriteBatch.DrawString(font, Time.ToString("000"), new Vector2(camera.Position.X + 715, camera.Position.Y + 30), Color.White);

            // Draw Winning HUD
            if (isWon&&EndGame)
            {
                spriteBatch.GraphicsDevice.Clear(Color.Black);
                spriteBatch.DrawString(font, "You win! Replay?", new Vector2(camera.Position.X + 290, camera.Position.Y + 170), Color.White);
                spriteBatch.DrawString(font, "Yes(Y)", new Vector2(camera.Position.X + 270, camera.Position.Y + 230), Color.White);
                spriteBatch.DrawString(font, "No(N)", new Vector2(camera.Position.X + 450, camera.Position.Y + 230), Color.White);
            }

            // Draw GameOver HUD
            if (IsOver)
            {
                spriteBatch.GraphicsDevice.Clear(Color.Black);
                spriteBatch.DrawString(font, "GAME OVER", new Vector2(camera.Position.X + 330, camera.Position.Y + 170), Color.White);
                spriteBatch.DrawString(font, "Replay?", new Vector2(camera.Position.X + 360, camera.Position.Y + 220), Color.White);
                spriteBatch.DrawString(font, "Yes(Y)", new Vector2(camera.Position.X + 270, camera.Position.Y + 260), Color.White);
                spriteBatch.DrawString(font, "No(N)", new Vector2(camera.Position.X + 450, camera.Position.Y + 260), Color.White);
            }
        }


        public void Update(GameTime gameTime)
        {
            currentTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (!isWon)
            {
                //currentTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (currentTime >= countDuration)
                {
                    Time--;
                    currentTime -= countDuration;
                }
            }
            else
            {
                if (Time>0 && currentTime >= winCountDuration) {
                    Time-=5;
                    if (Time < 0)
                    {
                        Time = 0;
                        EndGame = true;
                    }
                    Score += 50;
                    currentTime -= winCountDuration;
                }
            }
            coin.Update(gameTime);
        }

        public void AddCoins()
        {
            IncreaseScore(200);
            if (Coins == 99)
            {
                ExtraLife();
                Coins = 0;
            }
            Coins++;
            
        }

        public void ExtraLife()
        {
            Lives++;
        }

        public void ResetAll()
        {
            Lives = 3;
            Score = 0;
            Coins = 0;
            Time = 400;
            isWon = false;
            IsOver = false;
            EndGame = false;
        }

        public void LoseLife()
        {
            Lives--;
        }

        public void IncreaseScore(int score)
        {
            Score += score;

        }

        public void TimeReset()
        {
            Time = 400;
        }

        public void Win(/*float height*/)
        {
            isWon = true;
            //Score += 10 * (int)height;
            //Console.WriteLine(height);
        }

        public void GameOver()
        {
            IsOver = true;
            EndGame = true;
        }
        public void BeatGame()
        {
            BossEnd = true;
        }
    }
}
