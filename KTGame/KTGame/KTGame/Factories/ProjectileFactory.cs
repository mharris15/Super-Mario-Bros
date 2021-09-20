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
using KTGame.Sprites.Items;
using KTGame.Objects.Items;

namespace KTGame.Factories
{
    public class ProjectileFactory : IFactory
    {
        private readonly Texture2D Texture;

        private Point FrameSize;

        public ProjectileFactory(Texture2D sheet)
        {
            Texture = sheet ?? throw new ArgumentNullException("sheet");
        }


        public ISprite GetSprite(IEntity entity)
        {
            int[] FramesX, FramesY;
            FramesY = new int[] { 0 };

            string Name = ((Projectile)entity).EntityName;
            switch (Name)
            {
                case "BowserFireball":
                    FramesX = new int[] { 0 };
                    FrameSize = new Point(32, 16);
                    return new BowserFireaballSprite(Texture, FramesX, FramesY, FrameSize, entity);
                case "Hammer":
                    FramesX = new int[] { 48 };
                    FrameSize = new Point(16, 16);

                    return new HammerSprite(Texture, FramesX, FramesY, FrameSize,entity);
                case "Spikeball":
                    FramesX = new int[] {32};
                    FrameSize = new Point(16, 16);

                    return new SpikeballSprite(Texture, FramesX, FramesY, FrameSize, entity);
                case "Fireball":
                    FrameSize = new Point(4, 4);
                    FramesX = new int[] { 64 };
                    return new FireballSprite(Texture, FramesX, FramesY, FrameSize, entity);

                default:
                    throw new NotSupportedException();
            }
        }

    }
}
