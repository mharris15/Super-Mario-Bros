using KTGame.Interfaces;
using KTGame.Objects;
using KTGame.Objects.BlockObjects;
using KTGame.States.PlayerPowerUp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTGame.States.BlockStates
{
    class BlockBrickState : BlockState
    {
        public BlockBrickState(IEntity entity, IBlockState previousState)
            : base(entity, previousState)
        {
            ((Block)Block).EntityName = "Brick";
            Breakable = true;
        }

        public override IBlockState BumpUpTransition(IEntity player)
        {
            if (((Block)Block).ContainsItems && ((Block)Block).Items.Count == 1)
                return new BlockUsedState(Block, this);

            if (!((Block)Block).ContainsItems && !(((Player)player).PowerState is PowerStandardState))
                return new BlockBrokenState(Block, this);

            return this;
        }
    }
}
