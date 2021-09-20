using KTGame.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using KTGame.Objects;
using Microsoft.Xna.Framework.Content;
using System.Xml;
using KTGame.Objects.BlockObjects;
using KTGame.Objects.Enemies;
using KTGame.Objects.Items;
using KTGame.Sprites;
using Microsoft.Xna.Framework.Input;





namespace KTGame
{
    public class LevelLoader
    {
        private MarioWorld MarioWorld;
        public Soundbank Soundbank { get; set; }


        public MarioWorld LoadWorld(string fileName, ContentManager content, Game1 game, ISprite background)
        {
            if (fileName == null)
                throw new ArgumentNullException("fileName");
            if (content == null)
                throw new ArgumentNullException("content");
            if (game == null)
                throw new ArgumentNullException("game");
            if (background == null)
                throw new ArgumentNullException("background");

            fileName = string.Concat("../../../../Content/", fileName);
            Soundbank = new Soundbank(content);



            XmlReader reader = XmlReader.Create(fileName);
            try
            {
                Texture2D marioTexture = content.Load<Texture2D>("Sprites/Sprites-Mario-3");
                Texture2D itemsTexture = content.Load<Texture2D>("Sprites/AllItems");
                Texture2D projTexture = content.Load<Texture2D>("Sprites/Projectiles");

                Texture2D enemyTexture = content.Load<Texture2D>("Sprites/AllEnemies-2");
                Texture2D blockTexture = content.Load<Texture2D>("Sprites/BlockSpriteSheet3");
                Texture2D ParallaxBackground = content.Load<Texture2D>("parallaxBackground");
                SpriteFont spriteFont = content.Load<SpriteFont>("GameFont");
                List<IEntity> enemies = new List<IEntity>();
                List<IEntity> blocks = new List<IEntity>();
                List<IEntity> items = new List<IEntity>();
                List<IEntity> projectiles = new List<IEntity>();


                IEntity mario = new Player(marioTexture, new Vector2(75, 400));

                Projectile[] Fireball = new Fireball[5];

                for (int x = 0; x < 3; x++)
                {
                    Projectile fire = new Fireball(projTexture, mario.Position, false, true);
                    fire.LinkEntity(mario);
                    projectiles.Add(fire);

                    Fireball[x] = fire;
                }

                ((Player)mario).LinkFireballPool(Fireball);


                // IEntity mario = new Player(marioTexture, new Vector2(32*2, 32*30));//Underground testing
                // IEntity mario = new Player(marioTexture, new Vector2(32 * 2, 32 * 60));//Boss testing

                while (reader.Read())
                {
                    reader.MoveToNextAttribute();
                    string entity = reader.Value;
                    reader.MoveToNextAttribute();
                    int Xpos = 0;
                    int Ypos = 0;
                    int Xpos2 = 0;
                    int Ypos2 = 0;
                    int PipeLength = 0;
                    Pipe newPipeExit;
                    Pipe newPipeExit2;

                    if (reader.Name == "xpos")
                    {
                        Xpos = Convert.ToInt32(reader.Value, null);
                        Xpos = Xpos * 32;
                        reader.MoveToNextAttribute();
                        Ypos = Convert.ToInt32(reader.Value, null);
                        Ypos = Ypos * 32;
                    }
                    switch (entity)
                    {
                        case "Bridge":
                            blocks.Add(new Bridge(blockTexture, new Vector2(Xpos, Ypos)));
                            break;
                        case "Flame":
                            blocks.Add(new Flames(blockTexture, new Vector2(Xpos, Ypos)));
                            break;
                        case "BrickBlock":
                            blocks.Add(new BrickBlock(blockTexture, new Vector2(Xpos, Ypos)));
                            break;
                        case "UnderBB":
                            blocks.Add(new UndergroundBB(blockTexture, new Vector2(Xpos, Ypos)));
                            break;
                        case "BossBB":
                            blocks.Add(new BossBrick(blockTexture, new Vector2(Xpos, Ypos)));
                            break;
                        case "FloorBlock":
                            blocks.Add(new FloorBlock(blockTexture, new Vector2(Xpos, Ypos)));
                            break;
                        case "UnderFloor":
                            blocks.Add(new UnderGroundFB(blockTexture, new Vector2(Xpos, Ypos)));
                            break;
                        case "BossFB":
                            blocks.Add(new BossFloorBlock(blockTexture, new Vector2(Xpos, Ypos)));
                            break;
                        case "HiddenBlock":
                            blocks.Add(new HiddenBlockState(blockTexture, new Vector2(Xpos, Ypos)));
                            break;
                        case "QuestionBlock":
                            blocks.Add(new QuestionBlock(blockTexture, new Vector2(Xpos, Ypos)));
                            break;
                        case "StairBlock":
                            blocks.Add(new StairBlock(blockTexture, new Vector2(Xpos, Ypos)));
                            break;
                        case "UsedBlock":
                            blocks.Add(new UsedBlock(blockTexture, new Vector2(Xpos, Ypos)));
                            break;
                        case "Flag":
                            blocks.Add(new StairBlock(blockTexture, new Vector2(Xpos, Ypos)));
                            for (int x = 1; x < 10; x++)
                            {
                                FlagLength newFlagLength = new FlagLength(blockTexture, new Vector2(Xpos, Ypos - 32 * x));
                                blocks.Add(newFlagLength);
                            }
     
                            blocks.Add(new FlagTip(blockTexture, new Vector2(Xpos, Ypos - 32 * 10)));
                            blocks.Add(new Flag(blockTexture, new Vector2(Xpos-20, Ypos - 32 * 9)));

                            break;

                        case "Mushroom":
                            items.Add(new Mushroom(itemsTexture, new Vector2(Xpos, Ypos), CheckActive(reader), false));
                            break;
                        case "OneUp":
                            items.Add(new OneUp(itemsTexture, new Vector2(Xpos, Ypos), CheckActive(reader), false));
                            break;
                        case "Star":
                            items.Add(new Mushroom(itemsTexture, new Vector2(Xpos, Ypos), CheckActive(reader), false));
                            break;
                        case "Flower":
                            items.Add(new Flower(itemsTexture, new Vector2(Xpos, Ypos), CheckActive(reader), false));
                            break;
                        case "Coin":
                            items.Add(new Coin(itemsTexture, new Vector2(Xpos, Ypos), false, true));
                            break;
                        case "Axe":
                            items.Add(new Axe(itemsTexture, new Vector2(Xpos, Ypos), false, true));
                            break;
                        case "Goomba":
                            enemies.Add(new Goomba(enemyTexture, new Vector2(Xpos, Ypos)));
                            break;
                        case "RKoopa":
                            enemies.Add(new RKoopa(enemyTexture, new Vector2(Xpos, Ypos)));
                            break;
                        case "GKoopa":
                            enemies.Add(new GKoopa(enemyTexture, new Vector2(Xpos, Ypos)));
                            break;
                        case "HammerBro":
                            Enemy HammerBro = new HammerBro(enemyTexture, new Vector2(Xpos, Ypos));

                            for (int x = 0; x < 1; x++)
                            {
                                Projectile Hammer = new Hammer(projTexture, HammerBro.Position, false, true);
                                Hammer.LinkEntity(HammerBro);
                                projectiles.Add(Hammer);
                            }

                            enemies.Add(HammerBro);

                            break;
                        case "Lakitu":

                            Enemy Lakitu = new Lakitu(enemyTexture, new Vector2(Xpos, Ypos));

                            for (int x = 0; x < 1; x++)
                            {
                                Projectile Spikeball = new Spikeball(projTexture, Lakitu.Position, false, true);
                                Spikeball.LinkEntity(Lakitu);
                                projectiles.Add(Spikeball);
                            }

                            enemies.Add(Lakitu);
                            break;
                        case "Bowser":
                            Enemy Bowser = new Bowser(enemyTexture, new Vector2(Xpos, Ypos));

                            for (int x = 0; x < 1; x++)
                            {
                                Projectile BowserFireball = new BowserFireball(projTexture, Bowser.Position, true, true);
                                BowserFireball.LinkEntity(Bowser);
                                projectiles.Add(BowserFireball);
                            }

                            enemies.Add(Bowser);
                            break;
                        case "BowserFire":
                            enemies.Add(new BowserFire(enemyTexture, new Vector2(Xpos, Ypos)));
                            break;
                        case "Mario":
                            mario = new Player(marioTexture, new Vector2(350, 325));
                            break;
                        case "QBlockMush":
                            blocks.Add(new QuestionBlock(blockTexture, new Vector2(Xpos, Ypos)));
                            items.Add(new Mushroom(itemsTexture, new Vector2(Xpos, Ypos), false, false));
                            ((QuestionBlock)blocks.ElementAt(blocks.Count - 1)).AddItem(items.ElementAt(items.Count - 1));
                            break;
                        case "QBlockOneUp":
                            blocks.Add(new QuestionBlock(blockTexture, new Vector2(Xpos, Ypos)));
                            items.Add(new OneUp(itemsTexture, new Vector2(Xpos, Ypos), false, false));
                            ((QuestionBlock)blocks.ElementAt(blocks.Count - 1)).AddItem(items.ElementAt(items.Count - 1));
                            break;
                        case "QBlockStar":
                            blocks.Add(new QuestionBlock(blockTexture, new Vector2(Xpos, Ypos)));
                            items.Add(new Star(itemsTexture, new Vector2(Xpos, Ypos), false, false));
                            ((QuestionBlock)blocks.ElementAt(blocks.Count - 1)).AddItem(items.ElementAt(items.Count - 1));
                            break;
                        case "QBlockFlower":
                            blocks.Add(new QuestionBlock(blockTexture, new Vector2(Xpos, Ypos)));
                            items.Add(new Flower(itemsTexture, new Vector2(Xpos, Ypos), false, false));
                            ((QuestionBlock)blocks.ElementAt(blocks.Count - 1)).AddItem(items.ElementAt(items.Count - 1));
                            break;
                        case "QBlockCoin":
                            blocks.Add(new QuestionBlock(blockTexture, new Vector2(Xpos, Ypos)));
                            reader.MoveToNextAttribute();
                            int numCoins = System.Convert.ToInt32(reader.Value, null);
                            for (int x = 0; x < numCoins; x++)
                            {
                                items.Add(new Coin(itemsTexture, new Vector2(Xpos, Ypos), false, false));
                                ((QuestionBlock)blocks.ElementAt(blocks.Count - 1)).AddItem(items.ElementAt(items.Count - 1));
                            }
                            break;
                        case "BrickCoin":
                            blocks.Add(new BrickBlock(blockTexture, new Vector2(Xpos, Ypos)));
                            reader.MoveToNextAttribute();
                            numCoins = System.Convert.ToInt32(reader.Value, null);
                            for (int x = 0; x < numCoins; x++)
                            {
                                items.Add(new Coin(itemsTexture, new Vector2(Xpos, Ypos), false, false));
                                ((BrickBlock)blocks.ElementAt(blocks.Count - 1)).AddItem(items.ElementAt(items.Count - 1));
                            }
                            break;
                        case "HiddenCoin":
                            blocks.Add(new HiddenBlockState(blockTexture, new Vector2(Xpos, Ypos)));
                            items.Add(new Coin(itemsTexture, new Vector2(Xpos, Ypos), false, false));
                            ((HiddenBlockState)blocks.ElementAt(blocks.Count - 1)).AddItem(items.ElementAt(items.Count - 1));
                            break;
                        case "HiddenMush":
                            blocks.Add(new HiddenBlockState(blockTexture, new Vector2(Xpos, Ypos)));
                            items.Add(new Mushroom(itemsTexture, new Vector2(Xpos, Ypos), false, false));
                            ((HiddenBlockState)blocks.ElementAt(blocks.Count - 1)).AddItem(items.ElementAt(items.Count - 1));
                            break;
                        case "Pipe":
                            newPipeExit = new Pipe(blockTexture, new Vector2(Xpos, Ypos));
                            newPipeExit.LinkPipe(newPipeExit);

                            reader.MoveToNextAttribute();
                            PipeLength = System.Convert.ToInt32(reader.Value, null);

                            for (int x = 1; x < PipeLength; x++)
                            {
                                PipeLength newPipeLength = new PipeLength(blockTexture, new Vector2(Xpos, Ypos + 64 * x));
                                newPipeLength.LinkPipe(newPipeExit);
                                blocks.Add(newPipeLength);
                            }

                            blocks.Add(newPipeExit);
                            break;
                        case "PlantPipe":
                            newPipeExit = new Pipe(blockTexture, new Vector2(Xpos, Ypos));
                            newPipeExit.LinkPipe(newPipeExit);

                            reader.MoveToNextAttribute();
                            PipeLength = System.Convert.ToInt32(reader.Value, null);

                            for (int x = 1; x < PipeLength; x++)
                            {
                                PipeLength newPipeLength = new PipeLength(blockTexture, new Vector2(Xpos, Ypos + 64 * x));
                                newPipeLength.LinkPipe(newPipeExit);
                                blocks.Add(newPipeLength);
                            }

                            blocks.Add(newPipeExit);
                            enemies.Add(new Plant(enemyTexture, new Vector2(Xpos + 15, Ypos - 10)));
                            ((Block)blocks.ElementAt(blocks.Count - 1)).LinkPlant((Plant)enemies.ElementAt(enemies.Count - 1));
                            break;

                        case "WarpPipe":
                            newPipeExit = new Pipe(blockTexture, new Vector2(Xpos, Ypos));
                            newPipeExit.LinkPipe(newPipeExit);

                            reader.MoveToNextAttribute();
                            PipeLength = System.Convert.ToInt32(reader.Value, null);

                            for (int x = 1; x < PipeLength; x++)
                            {
                                PipeLength newPipeLength = new PipeLength(blockTexture, new Vector2(Xpos, Ypos + 64 * x));
                                newPipeLength.LinkPipe(newPipeExit);
                                blocks.Add(newPipeLength);
                            }

                            reader.MoveToNextAttribute();
                            Xpos2 = Convert.ToInt32(reader.Value, null);
                            Xpos2 = Xpos2 * 32;

                            reader.MoveToNextAttribute();
                            Ypos2 = Convert.ToInt32(reader.Value, null);
                            Ypos2 = Ypos2 * 32;

                            newPipeExit2 = new Pipe(blockTexture, new Vector2(Xpos2, Ypos2));
                            newPipeExit2.LinkPipe(newPipeExit2);

                            reader.MoveToNextAttribute();
                            PipeLength = System.Convert.ToInt32(reader.Value, null);

                            for (int x = 1; x < PipeLength; x++)
                            {
                                PipeLength newPipeLength = new PipeLength(blockTexture, new Vector2(Xpos2, Ypos2 + 64 * x));
                                newPipeLength.LinkPipe(newPipeExit2);
                                blocks.Add(newPipeLength);
                            }

                            newPipeExit.LinkWarpPipe(newPipeExit2);

                            blocks.Add(newPipeExit);
                            blocks.Add(newPipeExit2);

                            break;

                        default:

                            break;
                    }

                }

                MarioWorld = new MarioWorld(blocks, enemies, items, projectiles,mario, game, background, ParallaxBackground,spriteFont, Soundbank, blockTexture);
            }
            catch(System.IO.IOException e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                reader.Dispose();
            }
            //reader.Dispose(); 
            //MarioWorld = new MarioWorld(blocks, enemies, items, mario, game);
            return MarioWorld;
        }
        private bool CheckActive(XmlReader reader)
        {
            reader.MoveToNextAttribute();
            return (reader.Value == "True");
        }
    }
}
