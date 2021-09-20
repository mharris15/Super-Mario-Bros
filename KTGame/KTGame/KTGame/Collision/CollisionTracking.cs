using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using KTGame.Interfaces;
using KTGame.Objects;
using KTGame.States.PlayerActions;
using System.Collections.ObjectModel;
using KTGame.Objects.BlockObjects;
using KTGame.Objects.Items;
using KTGame.Objects.Enemies;
using KTGame.States.BlockStates;
using KTGame.Sprites;

namespace KTGame.Collision
{
    public class CollisionTracking
    {
        Rectangle ColliderRectangle;
        Rectangle EntityRectangle;
        Rectangle IntersectRectangle;

        private Collection<IEntity> Entities;

        public void InitializeEntityList()
        {
            Entities = new Collection<IEntity>();
        }

        public void AddEntity(IEntity entity)
        {
            Entities.Add(entity);
        }

        public void UpdateCollision()
        {
            foreach (IEntity collider in Entities)
            {
                if (collider is Block || !collider.CollisionOn)
                    continue;

                /* Assume entity is not colliding with any other entity and disable collision visual overlay. */
                bool Overlay = false;
                bool Footing = false;
                foreach (IEntity entity in Entities) {
                    if (Math.Abs(collider.Position.X - entity.Position.X) >= 100 || Math.Abs(collider.Position.Y - entity.Position.Y) >= 100)
                        continue;

                    /* For each entity/collider pair with collision on...*/
                    if (!collider.Equals(entity) && entity.CollisionOn)
                    {
                        Collision(entity, collider);
                        Proximity(entity, collider);

                        /* Check a portion of bottom of entity for footing collision. */
                        if (!Footing)
                            Footing = IsEntityFalling(collider, entity);

                        /* If no collision has been found yet, check for any collision. */
                        if (!Overlay)
                        {
                            Overlay = ShouldCollisionOverlayDisplay(collider, entity);
                        }
                    }
                }
                /* Set collision visual overlay based on whether a given entity is colliding with any other entity (Does not overlay for block-block collision) */
                collider.CollisionOverlay = Overlay;

                /* If no collision was found for a given entity, that entity is falling. */
                collider.Falling = !Footing;
            }
        }

       private void CalculatePositions(IEntity collider, IEntity entity)
       {
            if (collider == null)
                throw new ArgumentNullException("collider");

            if (entity == null)
                throw new ArgumentNullException("entity");

            ColliderRectangle = collider.CollisionBox;
            EntityRectangle = entity.CollisionBox;

            IntersectRectangle = Rectangle.Intersect(ColliderRectangle, EntityRectangle);
        }

        private bool ShouldCollisionOverlayDisplay(IEntity collider, IEntity entity)
        {

            ColliderRectangle = collider.CollisionBox;
            EntityRectangle = entity.CollisionBox;

            /*Forgiving rectangles in a cross shape for checking visual overlay. */
            Rectangle ColliderColorDetectionHorizontal = new Rectangle(ColliderRectangle.X - 1, ColliderRectangle.Y + 1, ColliderRectangle.Width + 2, ColliderRectangle.Height - 2);
            Rectangle ColliderColorDetectionVertical = new Rectangle(ColliderRectangle.X + 1, ColliderRectangle.Y - 1, ColliderRectangle.Width - 2, ColliderRectangle.Height + 2);


            Rectangle EntityColorDetectionHorizontal = new Rectangle(EntityRectangle.X - 1, EntityRectangle.Y + 1, EntityRectangle.Width + 2, EntityRectangle.Height - 2);
            Rectangle EntityColorDetectionVertical = new Rectangle(EntityRectangle.X + 1, EntityRectangle.Y - 1, EntityRectangle.Width-2, EntityRectangle.Height + 2);

            if (collider is Block && entity is Block)
            {
                return false;
            }

            return (!Rectangle.Intersect(ColliderColorDetectionHorizontal, EntityColorDetectionHorizontal).IsEmpty || !Rectangle.Intersect(EntityColorDetectionVertical, ColliderColorDetectionVertical).IsEmpty);
        }

        private bool IsEntityFalling(IEntity collider, IEntity entity)
        {

            ColliderRectangle = collider.CollisionBox;
            EntityRectangle = entity.CollisionBox;

            /*Forgiving rectangles for checking visual overlay. */
            Rectangle ColliderFooting = new Rectangle(ColliderRectangle.X +3 , ColliderRectangle.Y + ColliderRectangle.Height/2, ColliderRectangle.Width - 6, ColliderRectangle.Height/2+1);
            Rectangle EntityPlatform = new Rectangle(EntityRectangle.X, EntityRectangle.Y - 1, EntityRectangle.Width, EntityRectangle.Height);

            if (!(entity is Block))
                return false;

            return !Rectangle.Intersect(ColliderFooting, EntityPlatform).IsEmpty;
        }


        public void ToggleCollisionBoxes()
        {
            /* Toggle border for all entities. */
            foreach (IEntity entity in Entities)
                entity.BorderOn = !entity.BorderOn;
        }

        public void Proximity(IEntity collider, IEntity entity)
        {
            Player player = entity as Player;
            Block pipe = collider as Block;

            CalculatePositions(collider, entity);

            if (player != null && pipe != null)
            {
                pipe.BlockPlayerProximity(EntityRectangle, ColliderRectangle);
                player.PlayerPipeProximity(pipe);
            }
        }

        public void Collision(IEntity collider, IEntity entity)
        {
            if (collider == null)
                throw new ArgumentNullException("collider");

            if (entity == null)
                throw new ArgumentNullException("entity");

            CalculatePositions(collider, entity);

            if (!IntersectRectangle.IsEmpty)
            {
                if (IntersectRectangle.Width < IntersectRectangle.Height)
                {
                    // Collider hits block from right
                    if (ColliderRectangle.Left < EntityRectangle.Right &&
                                ColliderRectangle.Left > EntityRectangle.Left)
                    {
                        collider.CollisionResponse(entity, "Right", IntersectRectangle);
                        entity.CollisionResponse(collider, "Left", IntersectRectangle);


                    }
                    // Collider hits block from left
                    if (ColliderRectangle.Right > EntityRectangle.Left &&
                                ColliderRectangle.Right < EntityRectangle.Right)
                    {
                        collider.CollisionResponse(entity, "Left", IntersectRectangle);
                        entity.CollisionResponse(collider, "Right", IntersectRectangle);
                    }
                }
                else if (IntersectRectangle.Width > IntersectRectangle.Height)
                {
                    //Collider hits bottom of block
                    if (ColliderRectangle.Top < EntityRectangle.Bottom &&
                        ColliderRectangle.Top > EntityRectangle.Top)
                    {
                        collider.CollisionResponse(entity, "Bottom", IntersectRectangle);
                        entity.CollisionResponse(collider, "Top", IntersectRectangle);
                    }

                    // Collider Lands on Block
                    if (ColliderRectangle.Bottom > EntityRectangle.Top &&
                                ColliderRectangle.Bottom < EntityRectangle.Bottom)
                    {
                        collider.CollisionResponse(entity, "Top", IntersectRectangle);
                        entity.CollisionResponse(collider, "Bottom", IntersectRectangle);
                    }
                }
            }
        }
    }
}


