using KTGame.Interfaces;
using KTGame.Objects;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTGame.States.PlayerActions
{
    class PlayerCrouchingState : PlayerActionState
    {
        private bool isMoving;
        public PlayerCrouchingState(Player player, IPlayerActionState previousState, bool facingRight)
            : base(player, previousState, facingRight)
        {
            StateName = "Crouching";
            isMoving = (Player.Velocity.X != 0);
        }
        public override IPlayerActionState InputLeftTransition()
        {
            if (FacingRight)
                return new PlayerCrouchingState(Player, this, false);
            else
                return null;
        }
        public override IPlayerActionState InputRightTransition()
        {
            if (FacingRight)
                return null;
            else
                return new PlayerCrouchingState(Player, this, true);
        }
        public override IPlayerActionState InputDownReleaseTransition()
        {
            if (Player.Velocity.X == 0)
                return new PlayerIdleState(Player, this, FacingRight);
            else
                return new PlayerWalkingState(Player, this, FacingRight);
        }
        public override void Update(GameTime gameTime)
        {
            if (isMoving)
            {
                if (FacingRight)
                    Player.Velocity = new Vector2(Player.Velocity.X - HorizontalAccelerationReleased, Player.Velocity.Y);
                else
                    Player.Velocity = new Vector2(Player.Velocity.X + HorizontalAccelerationReleased, Player.Velocity.Y);
                //Console.WriteLine(Player.Velocity.X);
                if (Math.Abs(Player.Velocity.X) < 0.4)
                {
                    isMoving = false;
                    Player.Velocity = new Vector2(0, Player.Velocity.Y);
                }
            }
        }
    }
}
