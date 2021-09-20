using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using KTGame.Interfaces;
using KTGame.Objects;
using KTGame.States.PlayerActions;

namespace KTGame.States.PlayerPowerUp
{

    class PowerStarState : PlayerPowerState
    {
        public IPlayerPowerState BufferedState { get; set; }
        private int StarCounter { get; set; }
        private int TimeElapsed = 0;


        public PowerStarState(Player player, IPlayerPowerState previousState)
            : base(player, previousState)
        {
            Player = player;
            BufferedState = previousState;
            StateName = BufferedState.StateName;
            PowerFrameSize = BufferedState.PowerFrameSize;
            player.Soundbank.SwitchSong("starman");
            
            PowerFrameVerticalOffset = BufferedState.PowerFrameVerticalOffset;
            StarCounter = 0;
        }

        public override IPlayerPowerState RetrieveMushroom()
        {
            if (BufferedState is PowerStandardState)
            {
                BufferedState = new PowerSuperState(Player, this);
                PowerFrameVerticalOffset = BufferedState.PowerFrameVerticalOffset;
                StarCounter = 0;
            }
            return null;
        }
        public override IPlayerPowerState RetrieveFlower()
        {
            BufferedState = new PowerFireState(Player, this);
            PowerFrameVerticalOffset = BufferedState.PowerFrameVerticalOffset;
            StarCounter = 0;
            return null;
        }

        public override void Update(GameTime gameTime) {
            StateName = BufferedState.StateName;
            PowerFrameSize = BufferedState.PowerFrameSize;

            TimeElapsed += gameTime.ElapsedGameTime.Milliseconds;
            if (TimeElapsed > 100)
            {
                TimeElapsed = 0;
                StarCounter++;
                if (StarCounter <= 90)
                {
                    if (StarCounter % 4 == 0)
                    {
                        PowerFrameVerticalOffset -= BufferedState.PowerFrameSize.Y * 3;
                    }
                    else
                    {
                        PowerFrameVerticalOffset += BufferedState.PowerFrameSize.Y;
                    }
                }
                else
                {
                    if (StarCounter % 12 == 0)
                    {
                        PowerFrameVerticalOffset -= BufferedState.PowerFrameSize.Y * 3;
                    }
                    else if ((StarCounter % 3 == 0))
                    {
                        PowerFrameVerticalOffset += BufferedState.PowerFrameSize.Y;
                    }
                }

                if (StarCounter == 100)
                {
                    if (Player.Position.Y < 670)
                    {
                        Player.Soundbank.SwitchSong("overworld");
                    }
                    else
                    {
                        Player.Soundbank.SwitchSong("underworld");
                    }
                }
                if (StarCounter == 110)
                {
                    Player.StarStateReversion(BufferedState);
                }
            }
        }
    }
}

