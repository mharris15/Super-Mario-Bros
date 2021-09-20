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
    class PowerFireState : PlayerPowerState
    {
        public PowerFireState(Player player, IPlayerPowerState previousState)
            : base(player, previousState)
        {
            Player = player;
            StateName = "Fire";
            PowerFrameSize = new Point(16, 32);
            PowerFrameVerticalOffset = 32;
        }


        public override IPlayerPowerState TakeDamageCheat()
        {
            if (Player.PowerState.StateName.Equals("Dead"))
            {
                Player.ActionState = new PlayerIdleState(Player, Player.ActionState, Player.ActionState.FacingRight);
            }
            return new PowerStandardState(Player, this);
        }
        public override IPlayerPowerState RetrieveStar()
        {
            return new PowerStarState(Player, this);
        }

        public override void Update(GameTime gameTime) { }
    }
}
