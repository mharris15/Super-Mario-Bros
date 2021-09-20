using KTGame.Interfaces;
using KTGame.Objects.BlockObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTGame.States.BlockStates
{
    class BlockUsedState : BlockState
    {
        public BlockUsedState(IEntity entity, IBlockState previousState)
            : base(entity, previousState)
        {
            ((Block)Block).EntityName = "Used";
            CanBeBumped = false;
        }
    }
}
