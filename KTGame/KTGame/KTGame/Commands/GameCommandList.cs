using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTGame.Commands
{
    /* Commands applied to the game object */
    class ExitCommand : GameCommand
    {
        public ExitCommand(Game receiver)
            : base(receiver)
        {
        }
        public override void Execute()
        {
            Receiver.Exit();
        }
    }

     class ResetCommand : GameCommand
    {
        public ResetCommand(Game1 receiver)
            : base(receiver)
        {
        }
        public override void Execute()
        {
            ((Game1)Receiver).Reset();
        }
    }
}
