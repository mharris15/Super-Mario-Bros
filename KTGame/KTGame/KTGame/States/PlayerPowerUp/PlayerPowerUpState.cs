using Microsoft.Xna.Framework;
using KTGame.Interfaces;
using KTGame.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTGame.States.PlayerPowerUp
{
    public abstract class PlayerPowerState : IPlayerPowerState
    {
        protected Player Player { get; set; }

        public string StateName { get; set; }

        public Point PowerFrameSize { get; set; }

        public int PowerFrameVerticalOffset { get; set; }

        public IPlayerPowerState PreviousState { get; set; }

        protected PlayerPowerState(Player player, IPlayerPowerState previousState)
        {
            Player = player;
            StateName = "Null";
            PowerFrameSize = new Point(0, 0);
            PowerFrameVerticalOffset = 0;
            PreviousState = previousState;
        }

        public virtual IPlayerPowerState ToStandardStateCheat()
        {
            return new PowerStandardState(Player, this);
        }

        public virtual IPlayerPowerState ToSuperStateCheat()
        {
            return new PowerSuperState(Player, this);
        }

        public virtual IPlayerPowerState ToFireStateCheat()
        {
            return new PowerFireState(Player, this);
        }

        public virtual IPlayerPowerState RetrieveMushroom()
        {
            return null;
        }

        public virtual IPlayerPowerState RetrieveFlower()
        {
            return null;
        }

        public virtual IPlayerPowerState RetrieveStar()
        {
            return null;
        }

        public virtual IPlayerPowerState TakeDamageCheat()
        {
            return null;
        }

        public virtual void Update(GameTime gameTime) { }
    }
}
