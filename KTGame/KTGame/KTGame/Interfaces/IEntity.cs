using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTGame.Interfaces
{
    public interface IEntity
    {
        ISprite Sprite { get; set; }
        IFactory SpriteFactory { get; set; }

        Vector2 Position { get; set; }
        bool CollisionOverlay { get; set; }

        bool Falling { get; set; }
        bool BorderOn { get; set; }



        Rectangle CollisionBox { get; set; }

        Vector2 Velocity { get; set; }

        bool CollisionOn { get; set; }

        Texture2D Texture { get; set; }
        string EntityName { get; set; }

        void LinkSoundbank(Soundbank soundbank);

        void Update(GameTime gameTime);

        void CollisionResponse(IEntity entity, String partCollided, Rectangle intersect);

        void Draw(SpriteBatch spriteBatch);
    }
}
