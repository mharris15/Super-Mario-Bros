using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using KTGame.Commands;
using KTGame.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTGame.Controllers
{
    public class KeyboardController : IController
    {
        private KeyboardState previousKeyboardState;
        private Dictionary<Keys, ICommand> commandList;
        private Dictionary<Keys, ICommand> toggleCommandList;
        private Dictionary<Keys, ICommand> releaseCommandList;
        public KeyboardController()
        {
            previousKeyboardState = Keyboard.GetState();
            commandList = new Dictionary<Keys, ICommand>();
            toggleCommandList = new Dictionary<Keys, ICommand>();
            releaseCommandList = new Dictionary<Keys, ICommand>();
        }

        public void AddCommand(object input, ICommand command)
        {
            if(!commandList.ContainsKey((Keys)input))
                commandList.Add((Keys)input, command);
        }
        public void AddToggleCommand(object input, ICommand command)
        {
            toggleCommandList.Add((Keys)input, command);
        }
        public void AddReleaseCommand(object input, ICommand command)
        {
            releaseCommandList.Add((Keys)input, command);
        }
        public void DeleteCommand(object input)
        {
            commandList.Remove((Keys)input);
        }
        public void DeleteToggleCommand(object input)
        {
            toggleCommandList.Remove((Keys)input);
        }
        public void DeleteReleaseCommand(object input)
        {
            releaseCommandList.Remove((Keys)input);
        }
        public void UpdateInput()
        {
            KeyboardState currentKeyboardState = Keyboard.GetState();

            Keys[] keysPressed = currentKeyboardState.GetPressedKeys();
            Keys[] keysOncePressed = previousKeyboardState.GetPressedKeys();
            foreach (Keys key in keysPressed)
            {
                if (commandList.TryGetValue(key, out ICommand command))
                {
                    command.Execute();
                }

                if (!previousKeyboardState.IsKeyDown(key)) //keys just pressed
                {
                    if (toggleCommandList.TryGetValue(key, out ICommand toggleCommand))
                    {
                        toggleCommand.Execute();
                    }
                }

                else // keys being held down
                {

                }
            }
            foreach (Keys key in keysOncePressed)
            {
                if (!currentKeyboardState.IsKeyDown(key)) //keys just released
                {
                    if (releaseCommandList.TryGetValue(key, out ICommand command))
                    {
                        command.Execute();
                    }
                }
            }

            // Update previous Keyboard state.
            previousKeyboardState = currentKeyboardState;
        }


    }
}
