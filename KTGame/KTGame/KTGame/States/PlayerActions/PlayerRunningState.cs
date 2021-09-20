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
    class PlayerRunningState : PlayerActionState
    {
        public PlayerRunningState(Player player, IPlayerActionState previousState, bool facingRight)
            : base(player, previousState, facingRight)
        {
            StateName = "Running";
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
        public override IPlayerActionState InputLeftReleaseTransition()
        {
            return new PlayerWalkingState(Player, this, FacingRight);
        }
        public override IPlayerActionState InputRightReleaseTransition()
        {
            return new PlayerWalkingState(Player, this, FacingRight);
        }
    }
}
