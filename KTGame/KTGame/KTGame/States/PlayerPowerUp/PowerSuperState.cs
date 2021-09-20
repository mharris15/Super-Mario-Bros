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
    class PowerSuperState : PlayerPowerState
    {
        public PowerSuperState(Player player, IPlayerPowerState previousState)
            : base(player, previousState)
        {
            Player = player;
            StateName = "Super";
            PowerFrameSize = new Point(16, 32);
            PowerFrameVerticalOffset = 0;
        }

        public override IPlayerPowerState TakeDamageCheat()
        {
            return new PowerStandardState(Player, this);
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
