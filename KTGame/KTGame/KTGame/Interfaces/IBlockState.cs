using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTGame.Interfaces
{
    public interface IBlockState
    {
        IEntity Block { get; set; }

        bool CanBeBumped { get; set; }
        bool Breakable { get; set; }

        IBlockState PreviousState { get; set; }

        IBlockState BumpUpTransition(IEntity player);
    }
}
