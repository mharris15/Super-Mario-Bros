using KTGame.Interfaces;
using KTGame.Objects.BlockObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTGame.States.BlockStates
{
    class BlockBrokenState : BlockState
    {
        public BlockBrokenState(IEntity entity, IBlockState previousState)
            : base(entity, previousState)
        {
            ((Block)Block).EntityName = "Broken";
            CanBeBumped = false;
        }
    }
}
