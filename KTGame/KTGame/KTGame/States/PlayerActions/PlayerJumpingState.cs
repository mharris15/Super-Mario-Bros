using Microsoft.Xna.Framework;
using KTGame.Interfaces;
using KTGame.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KTGame.States.PlayerPowerUp;

namespace KTGame.States.PlayerActions
{
    class PlayerJumpingState : PlayerActionState
    {
        private bool isMoving;
        public PlayerJumpingState(Player player, IPlayerActionState previousState, bool facingRight)
            : base(player, previousState, facingRight)
        {
            Player.Velocity = new Vector2(Player.Velocity.X, -10);
            StateName = "Jumping";
            isMoving = false;
        }
        public override IPlayerActionState InputUpTransition()
        {
            if (Player.Velocity.Y < 0) 
                Player.Velocity = new Vector2(Player.Velocity.X, Player.Velocity.Y - VerticalAccelerationHeld);
            return null;
        }
        public override IPlayerActionState InputLeftTransition()
        {
            isMoving = true;
            if (FacingRight)
            {
                if (Player.Velocity.X > 0)
                    Player.Velocity = new Vector2(Player.Velocity.X - HorizontalAccelerationHeld, Player.Velocity.Y);
                else FacingRight = false;
            }
            else //facing left
            {
                if (Player.Velocity.X > -5)
                    Player.Velocity = new Vector2(Player.Velocity.X - HorizontalAccelerationHeld, Player.Velocity.Y);
            }
            return null;
        }

        public override IPlayerActionState InputRightTransition()
        {
            isMoving = true;
            if (FacingRight)
            {
                if (Player.Velocity.X < 5)
                    Player.Velocity = new Vector2(Player.Velocity.X + HorizontalAccelerationHeld, Player.Velocity.Y);
            }
            else
            {
                if (Player.Velocity.X < 0)
                    Player.Velocity = new Vector2(Player.Velocity.X + HorizontalAccelerationHeld, Player.Velocity.Y);
                else FacingRight = true;
            }
            return null;
        }
        public override IPlayerActionState InputLeftReleaseTransition()
        {
            //Console.WriteLine(Player.Velocity);
            isMoving = false;
            if (!Player.Velocity.X.Equals(0))
            {
                Player.Velocity = new Vector2(Player.Velocity.X + HorizontalAccelerationReleased, Player.Velocity.Y);
            }
            return null;
        }
        public override IPlayerActionState InputRightReleaseTransition()
        {
            isMoving = false;
            if (!Player.Velocity.X.Equals(0))
            {
                Player.Velocity = new Vector2(Player.Velocity.X - HorizontalAccelerationReleased, Player.Velocity.Y);
            }
            return null;
        }
        public override void Update(GameTime gameTime)
        {
            /* Detect top of jump */
            if (Player.Velocity.Y >= 0)
            {
                Player.Freefall();
            }
            Player.Velocity = new Vector2(Player.Velocity.X, Player.Velocity.Y + 0.7f);
            if (!isMoving)
            {
                if (FacingRight && Player.Velocity.X > 0)
                {
                    Player.Velocity = new Vector2(Player.Velocity.X - HorizontalAccelerationReleased, Player.Velocity.Y);
                    
                }
                else if (!FacingRight && Player.Velocity.X < 0)
                    Player.Velocity = new Vector2(Player.Velocity.X + HorizontalAccelerationReleased, Player.Velocity.Y);
            }
        }
    }
}
