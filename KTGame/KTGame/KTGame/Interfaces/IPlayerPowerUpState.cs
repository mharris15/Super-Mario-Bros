using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTGame.Interfaces
{
    /* Interface used for Player Power States */
    public interface IPlayerPowerState
    {
        string StateName { get; set; }

        Point PowerFrameSize { get; set; }

        int PowerFrameVerticalOffset { get; set; }

        IPlayerPowerState PreviousState { get; set; }

        IPlayerPowerState ToStandardStateCheat();

        IPlayerPowerState ToSuperStateCheat();

        IPlayerPowerState ToFireStateCheat();

        IPlayerPowerState TakeDamageCheat();

        IPlayerPowerState RetrieveMushroom();

        IPlayerPowerState RetrieveFlower();

        IPlayerPowerState RetrieveStar();


        void Update(GameTime gameTime);

    }
}
