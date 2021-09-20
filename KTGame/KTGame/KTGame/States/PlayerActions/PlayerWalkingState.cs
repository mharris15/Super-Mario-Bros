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
    class PlayerWalkingState : PlayerActionState
    {
        private bool isWalking = false;
        public PlayerWalkingState(Player player, IPlayerActionState previousState, bool facingRight)
            : base(player, previousState, facingRight)
        {
            StateName = "Walking";
        }


        public override IPlayerActionState InputUpTransition()
        {
            if (Player.PowerState is PowerStandardState)
                Player.Soundbank.PlaySound("StandardJump");
            else
                Player.Soundbank.PlaySound("SuperJump");
            return new PlayerJumpingState(Player, this, FacingRight);
        }
        public override IPlayerActionState InputDownTransition()
        {
            if (!Player.PowerState.StateName.Equals("Standard"))
            {
                return new PlayerCrouchingState(Player, this, FacingRight);
            }
            return null;
        }
        public override IPlayerActionState InputLeftTransition()
        {
            isWalking = true;
            if (FacingRight)
            {
                Player.Velocity = new Vector2(0, Player.Velocity.Y);
                return new PlayerIdleState(Player, this, true);
            }
            else //facing left
            {
                /* accelerate left until velocity = 5*/
                Player.Velocity = new Vector2(Player.Velocity.X - HorizontalAccelerationHeld, Player.Velocity.Y);

                if (Player.Velocity.X <= -5)
                    return new PlayerRunningState(Player, this, false);
                else return null;
            }
        }
        
        public override IPlayerActionState InputRightTransition()
        {
            isWalking = true;
            if (FacingRight)
            {
                /* accelerate right until velocity = 5*/
                Player.Velocity = new Vector2(Player.Velocity.X + HorizontalAccelerationHeld, Player.Velocity.Y);
                if (Player.Velocity.X >= 5)
                    return new PlayerRunningState(Player, this, true);
                else return null;
            }
            else
            {
                Player.Velocity = new Vector2(0, Player.Velocity.Y);
                return new PlayerIdleState(Player, this, false);
            }
        }

        
        public override IPlayerActionState InputLeftReleaseTransition()
        {
            isWalking = false;
            return null;
        }

        public override IPlayerActionState InputRightReleaseTransition()
        {
            isWalking = false;
            return null;
        }

        public override void Update(GameTime gameTime)
        {
            if (!isWalking)
            {
                if (Player.Velocity.X > 0)
                    Player.Velocity = new Vector2(Player.Velocity.X - HorizontalAccelerationReleased, Player.Velocity.Y);
                else
                    Player.Velocity = new Vector2(Player.Velocity.X + HorizontalAccelerationReleased, Player.Velocity.Y);
                //Console.WriteLine(Player.Velocity.X);
                if (Math.Abs(Player.Velocity.X) < 0.2)
                {
                    isWalking = true;
                    Player.ProcessActionInput(new PlayerIdleState(Player, this, FacingRight));
                }
            }
        }
    }
}
