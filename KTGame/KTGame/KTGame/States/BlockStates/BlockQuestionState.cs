using KTGame.Interfaces;
using KTGame.Objects.BlockObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTGame.States.BlockStates
{
    class BlockQuestionState : BlockState
    {
        public BlockQuestionState(IEntity entity, IBlockState previousState)
            : base(entity, previousState)
        {
            ((Block)Block).EntityName = "Question";
        }
        public override IBlockState BumpUpTransition(IEntity player)
        {
            return new BlockUsedState(Block, this);
        }
    }
}
