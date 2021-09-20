using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTGame.Interfaces
{
    /* Interface used for Player Action States */
    public interface IPlayerActionState
    {
        String StateName { get; set; }

        bool FacingRight { get; set; }
        IPlayerActionState PreviousState { get; set; }

        IPlayerActionState InputUpTransition();
        IPlayerActionState InputDownTransition();
        IPlayerActionState InputLeftTransition();
        IPlayerActionState InputRightTransition();
        IPlayerActionState InputLeftReleaseTransition();
        IPlayerActionState InputRightReleaseTransition();
        IPlayerActionState InputUpReleaseTransition();
        IPlayerActionState InputDownReleaseTransition();
        void Update(GameTime gameTime);

    }
}
