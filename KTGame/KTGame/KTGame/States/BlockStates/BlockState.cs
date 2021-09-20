using KTGame.Interfaces;
using KTGame.Objects.BlockObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTGame.States.BlockStates
{
    public abstract class BlockState : IBlockState
    {
        public IEntity Block { get; set; }

        public IBlockState PreviousState { get; set; }

        public bool CanBeBumped { get; set; }
        public bool Breakable { get; set; }

        protected BlockState(IEntity entity, IBlockState previousState)
        {
            PreviousState = previousState;
            Block = entity;
            CanBeBumped = true;
            Breakable = false;
        }

        public virtual IBlockState BumpUpTransition(IEntity player)
        {
            return null;
        }
    }
}
