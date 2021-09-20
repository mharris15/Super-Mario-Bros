using Microsoft.Xna.Framework;
using KTGame.Interfaces;
using KTGame.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTGame.States.PlayerActions
{
    class PlayerFallingState : PlayerActionState
    {
        public PlayerFallingState(Player player, IPlayerActionState previousState, bool facingRight)
            : base(player, previousState, facingRight)
        {

            Player.Velocity = new Vector2(Player.Velocity.X, 1);
            StateName = PreviousState.StateName;
        }

        public override IPlayerActionState InputLeftTransition()
        {
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
            if (!Player.Velocity.X.Equals(0))
            {
                Player.Velocity = new Vector2(Player.Velocity.X + HorizontalAccelerationReleased, Player.Velocity.Y);
            }
            return null;
        }
        public override IPlayerActionState InputRightReleaseTransition()
        {
            if (!Player.Velocity.X.Equals(0))
            {
                Player.Velocity = new Vector2(Player.Velocity.X - HorizontalAccelerationReleased, Player.Velocity.Y);
            }
            return null;
        }
        public override void Update(GameTime gameTime)
        {

            /* Detect landing. */
            if (Player.Velocity.Y == 0)
            {
                /* If falling and Y Velocity is 0, no longer falling. */
                Player.Falling = false;
                if (Player.Velocity.X.Equals(0))
                    Player.ProcessActionInput(new PlayerIdleState(Player, this, FacingRight));
                else
                    Player.ProcessActionInput(new PlayerWalkingState(Player, this, FacingRight));
            }

            if (Player.Velocity.Y <= 15)
                Player.Velocity = new Vector2(Player.Velocity.X, Player.Velocity.Y + VerticalAccelerationReleased);
        }
    }
}
