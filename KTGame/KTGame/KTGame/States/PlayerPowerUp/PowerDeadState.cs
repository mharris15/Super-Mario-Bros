using Microsoft.Xna.Framework;
using KTGame.Interfaces;
using KTGame.Objects;
using KTGame.States.PlayerActions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTGame.States.PlayerPowerUp
{
    class PowerDeadState : PlayerPowerState
    {
        public PowerDeadState(Player player, IPlayerPowerState previousState)
            : base(player, previousState)
        {
            Player = player;
            StateName = "Dead";
            PowerFrameSize = new Point(16, 16);
            PowerFrameVerticalOffset = 32*5;
            Player.Velocity = new Vector2(0, 3);
        }

        public override IPlayerPowerState ToStandardStateCheat()
        {
            Player.ActionState = new PlayerIdleState(Player, Player.ActionState, Player.ActionState.FacingRight);
            return new PowerStandardState(Player, this);
        }

        public override IPlayerPowerState ToSuperStateCheat()
        {
            Player.ActionState = new PlayerIdleState(Player, Player.ActionState, Player.ActionState.FacingRight);
            return new PowerSuperState(Player, this);
        }

        public override IPlayerPowerState ToFireStateCheat()
        {
            Player.ActionState = new PlayerIdleState(Player, Player.ActionState, Player.ActionState.FacingRight);
            return new PowerFireState(Player, this);
        }

        public override void Update(GameTime gameTime) {
            
        }
    }
}
