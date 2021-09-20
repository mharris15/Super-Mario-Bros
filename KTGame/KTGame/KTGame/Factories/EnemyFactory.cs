using KTGame.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using KTGame.Objects;
using KTGame.Sprites;
using KTGame.Sprites.Enemies;
using KTGame.Objects.Enemies;

namespace KTGame.Factories
{
    public class EnemyFactory : IFactory
    {
        private readonly Texture2D Texture;
        private Point FrameSize;


        private int Width;
        private int Height;



        public EnemyFactory(Texture2D sheet)
        {
            Texture = sheet ?? throw new ArgumentNullException("sheet");

            Width = 16;
            Height = 32;
            

        }

        public ISprite GetSprite(IEntity entity) 
        {

            Enemy CurrentEnemy = entity as Enemy;
            bool Stomped = CurrentEnemy.Stomped;
            bool facingLeft = CurrentEnemy.FacingLeft;
            FrameSize = new Point(Width, Height);
            int[] FramesX, FramesY;
            int Time = 200;
            string Name = CurrentEnemy.EntityName;

            switch (Name)
            {
                case "Goomba":
                    if (!Stomped)
                    {
                        FramesX = new int[] { 0 * Width, 1 * Width };
                        FramesY = new int[] { 0 * Height };
                    }
                    else
                    {
                        FramesX = new int[] { 2*Width};
                        FramesY = new int[] { 0 * Height };
                        Time = 500;
                    }
                
                  return  new GoombaSprite(Texture, FramesX, FramesY, FrameSize, entity, Time);

                case "GKoopa":
                    if (!Stomped)
                    {
                        if (facingLeft)
                        {
                            FramesX = new int[] { 6 * Width, 7 * Width };
                            FramesY = new int[] { 0 * Height };
                        }
                        else
                        {
                            FramesX = new int[] { 6 * Width, 7 * Width }; //need to updated to reverse sprites
                            FramesY = new int[] { 1 * Height };
                        }
                    }


                    else
                    {
                        FramesX = new int[] {10 * Width };
                        FramesY = new int[] { 0 * Height };
                    }
                    
                    
                 return  new GKoopaSprite(Texture, FramesX, FramesY, FrameSize, entity, Time);


                case "RKoopa":
                    if (!Stomped)
                    {
                        if (facingLeft)
                        {
                            FramesX = new int[] { 6 * Width, 7 * Width };
                            FramesY = new int[] { 4 * Height + 1 };
                        }
                        else
                        {
                            FramesX = new int[] { 6 * Width, 7 * Width }; //needs updating to reverse sprites
                            FramesY = new int[] { 5 * Height + 1 };
                        }
                    }
                    else
                    {
                        FramesX = new int[] { 10 * Width };
                        FramesY = new int[] { 4 * Height + 1 };
                    }
                  
                  return  new RKoopaSprite(Texture, FramesX, FramesY, FrameSize, entity, Time);

                case "Plant":

                        FramesX = new int[] { 12 * Width, 13 * Width };
                        FramesY = new int[] { 0 * Height };
                    


                    return new PlantSprite(Texture, FramesX, FramesY, FrameSize, entity, Time/2);

                case "HammerBro":
                    FramesX = new[] {20 * Width, 21 * Width, 22 * Width, 23 * Width};
                    FramesY = new int[] { 0 * Height };

                    return new HammerBroSprite(Texture,FramesX,FramesY, FrameSize,entity);

                case "Lakitu":
                    if (Stomped)
                    {
                        FramesX = new[] {27 * Width };
                        Time = 500;
                    }
                    else
                    {
                        FramesX = new[] { 26 * Width, 27 * Width };
                    }
                    FramesY = new int[] { 0 * Height };

                    return new LakituSprite(Texture, FramesX, FramesY, FrameSize, entity, 7 * Time);

                case "Bowser":
                    FramesX = new[] { 28 * Width, 30 * Width, 32 * Width, 34 * Width };
                    FramesY = new int[] { 0 * Height };

                    FrameSize = new Point(2*Width, Height);

                    return new BowserSprite(Texture, FramesX, FramesY, FrameSize, entity, 3*Time);

                default:
                    throw new NotSupportedException();

            }

        }

    }
}