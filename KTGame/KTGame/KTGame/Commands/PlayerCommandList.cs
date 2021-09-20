using KTGame.Interfaces;
using KTGame.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTGame.Commands
{
    /* Commands applied to the player object */
    class UpCommand : PlayerCommand
    {
        public UpCommand(IEntity receiver)
            : base(receiver)
        {
        }
        public override void Execute()
        {
            ((Player)Receiver).InputUp();
        }
    }
    class LeftCommand : PlayerCommand
    {
        public LeftCommand(IEntity receiver)
            : base(receiver)
        {
        }
        public override void Execute()
        {
            ((Player)Receiver).InputLeft();
        }
    }
    class DownCommand : PlayerCommand
    {
        public DownCommand(IEntity receiver)
            : base(receiver)
        {
        }
        public override void Execute()
        {
            ((Player)Receiver).InputDown();
        }
    }
    class RightCommand : PlayerCommand
    {
        public RightCommand(IEntity receiver)
            : base(receiver)
        {
        }
        public override void Execute()
        {
            ((Player)Receiver).InputRight();
        }
    }

    class FireballCommand : PlayerCommand
    {
        public FireballCommand(IEntity receiver)
            : base(receiver)
        {
        }
        public override void Execute()
        {
            ((Player)Receiver).Attack();
        }
    }
    class LeftReleaseCommand : PlayerCommand
    {
        public LeftReleaseCommand(IEntity receiver)
            : base(receiver)
        {
        }
        public override void Execute()
        {
            ((Player)Receiver).ReleaseLeft();
        }
    }
    class RightReleaseCommand : PlayerCommand
    {
        public RightReleaseCommand(IEntity receiver)
            : base(receiver)
        {
        }
        public override void Execute()
        {
            ((Player)Receiver).ReleaseRight();
        }
    }
    class UpReleaseCommand : PlayerCommand
    {
        public UpReleaseCommand(IEntity receiver)
            : base(receiver)
        {
        }
        public override void Execute()
        {
            ((Player)Receiver).ReleaseUp();
        }
    }
    class DownReleaseCommand : PlayerCommand
    {
        public DownReleaseCommand(IEntity receiver)
            : base(receiver)
        {
        }
        public override void Execute()
        {
            ((Player)Receiver).ReleaseDown();
        }
    }
    class ToStandardCommand : PlayerCommand
    {
        public ToStandardCommand(IEntity receiver)
            : base(receiver)
        {
        }
        public override void Execute()
        {
            ((Player)Receiver).ToStandardStateCheat();
        }
    }

    class ToSuperCommand : PlayerCommand
    {
        public ToSuperCommand(IEntity receiver)
            : base(receiver)
        {
        }
        public override void Execute()
        {
            ((Player)Receiver).ToSuperStateCheat();
        }
    }

    class ToFireCommand : PlayerCommand
    {
        public ToFireCommand(IEntity receiver)
            : base(receiver)
        {
        }
        public override void Execute()
        {
            ((Player)Receiver).ToFireStateCheat();
        }
    }

    class TakeDamageCommand : PlayerCommand
    {
        public TakeDamageCommand(IEntity receiver)
            : base(receiver)
        {
        }
        public override void Execute()
        {
            ((Player)Receiver).TakeDamage();
        }
    }
}
