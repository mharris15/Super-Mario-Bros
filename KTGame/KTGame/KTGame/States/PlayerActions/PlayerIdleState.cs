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
    class PlayerIdleState : PlayerActionState
    {

        public PlayerIdleState(Player player, IPlayerActionState previousState, bool facingRight)
            : base(player, previousState, facingRight)
        {
            StateName = "Idle";
            Player.Velocity = new Vector2(0, 0);
        }

        public override IPlayerActionState InputUpTransition()
        {
            if (Player.PowerState is PowerStandardState)
                Player.Soundbank.PlaySound("StandardJump");
            else
                Player.Soundbank.PlaySound("SuperJump");
            return new PlayerJumpingState(Player, this, FacingRight);
        }

        public override IPlayerActionState InputLeftTransition()
        {
            if (!FacingRight) //facing left
            {
                return new PlayerWalkingState(Player, this, false);
            }
            else //facing right
            {
                return new PlayerIdleState(Player, this, false);
            }
        }

        public override IPlayerActionState InputRightTransition()
        {
            if (FacingRight)
            {
                return new PlayerWalkingState(Player, this, true);
            }
            else
            {
                return new PlayerIdleState(Player, this, true);
            }
        }

        public override IPlayerActionState InputDownTransition()
        {
            if (!Player.PowerState.StateName.Equals("Standard"))
            {
                return new PlayerCrouchingState(Player, this, FacingRight);
            }
            return null;
        }

        public override IPlayerActionState InputLeftReleaseTransition()
        {
            return null;
        }

        public override IPlayerActionState InputRightReleaseTransition()
        {
            return null;
        }
    }
}
