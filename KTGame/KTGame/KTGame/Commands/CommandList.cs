using Microsoft.Xna.Framework;
using KTGame.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KTGame.GameStates;

namespace KTGame.Commands
{
    /* List of abstract classes implementing the ICommand interface. */
    public abstract class GameCommand : ICommand
    {
        protected Game Receiver { get; set; }

        protected GameCommand(Game receiver)
        {
            this.Receiver = receiver;
        }
        public abstract void Execute();
    }

    public abstract class PlayerCommand : ICommand
    {
        protected IEntity Receiver { get; set; }

        protected PlayerCommand(IEntity receiver)
        {
            this.Receiver = receiver;
        }
        public abstract void Execute();
    }

    public abstract class MiscCommand : ICommand
    {
        protected object Receiver { get; set; }

        protected MiscCommand(object receiver)
        {
            this.Receiver = receiver;
        }
        public abstract void Execute();
    }
}

