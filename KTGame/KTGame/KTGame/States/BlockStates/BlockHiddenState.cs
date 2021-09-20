using KTGame.Interfaces;
using KTGame.Objects.BlockObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTGame.States.BlockStates
{
    class BlockHiddenState : BlockState
    {
        public BlockHiddenState(IEntity entity, IBlockState previousState)
            : base(entity, previousState)
        {
            ((Block)Block).EntityName = "Hidden";
        }

        public override IBlockState BumpUpTransition(IEntity player)
        {
            return new BlockUsedState(Block, this);
        }
    }
}
