using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTGame.Interfaces
{
    public interface IFactory
    {
        ISprite GetSprite(IEntity entity);
    }
}
