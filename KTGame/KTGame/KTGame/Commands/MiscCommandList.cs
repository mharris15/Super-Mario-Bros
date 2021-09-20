using KTGame.Collision;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KTGame.GameStates;

namespace KTGame.Commands
{
    /* Commands applied to the game object */
    class CollisionVisualCommand : MiscCommand
    {
        public CollisionVisualCommand(CollisionTracking receiver)
            : base(receiver)
        {
        }
        public override void Execute()
        {
            ((CollisionTracking)Receiver).ToggleCollisionBoxes();
        }
    }

    class GameMuteCommand : MiscCommand
    {
        public GameMuteCommand(Soundbank receiver)
            : base(receiver)
        {
        }
        public override void Execute()
        {
            ((Soundbank)Receiver).MuteCommand();
        }
    }

    class GamePauseCommand : MiscCommand
    {
        public GamePauseCommand(PauseMenu receiver)
            : base(receiver)
        {
        }

        public override void Execute()
        {
            ((PauseMenu)Receiver).PauseCommand();
        }
    }
}
