using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTGame.Interfaces
{
    /* Interface used for Controllers */
    public interface IController
    {
        void AddCommand(object input, ICommand command);
        void AddToggleCommand(object input, ICommand command);
        void DeleteCommand(object input);
        void DeleteToggleCommand(object input);
        void AddReleaseCommand(object input, ICommand command);
        void DeleteReleaseCommand(object input);
        void UpdateInput();
    }
}
