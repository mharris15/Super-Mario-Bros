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

    class PowerStandardState : PlayerPowerState
    {
        public PowerStandardState(Player player, IPlayerPowerState previousState)
            : base(player, previousState)
        {
            Player = player;
            StateName = "Standard";
            PowerFrameSize = new Point(16, 16);
            PowerFrameVerticalOffset = 32*5;
        }

        public override IPlayerPowerState TakeDamageCheat()
        {
            Player.ActionState = new PlayerDyingState(Player, Player.ActionState, Player.ActionState.FacingRight);
            return new PowerDeadState(Player, this);
        }

        public override IPlayerPowerState RetrieveMushroom()
        {
            return new PowerSuperState(Player, this);
        }
        public override IPlayerPowerState RetrieveFlower()
        {
            return new PowerFireState(Player, this);
        }
        public override IPlayerPowerState RetrieveStar()
        {
            return new PowerStarState(Player, this);
        }
        public override void Update(GameTime gameTime) { }
    }
}
