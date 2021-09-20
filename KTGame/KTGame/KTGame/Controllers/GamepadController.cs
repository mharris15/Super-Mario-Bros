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
    public class GamepadController : IController
    {
        private GamePadState previousGamePadState;
        private Dictionary<Buttons, ICommand> commandList;
        private Dictionary<Keys, ICommand> toggleCommandList;

        private Dictionary<Buttons, ICommand> releaseCommandList;
        public GamepadController()
        {
            toggleCommandList = new Dictionary<Keys, ICommand>();

            previousGamePadState = GamePad.GetState(PlayerIndex.One);
            commandList = new Dictionary<Buttons, ICommand>();
            releaseCommandList = new Dictionary<Buttons, ICommand>();
        }

        public void AddCommand(object input, ICommand command)
        {
            commandList.Add((Buttons)input, command);
        }
        public void AddToggleCommand(object input, ICommand command)
        {
            toggleCommandList.Add((Keys)input, command);
        }
        public void DeleteCommand(object input)
        {
            commandList.Remove((Buttons)input);
        }
        public void DeleteToggleCommand(object input)
        {
            toggleCommandList.Remove((Keys)input);
        }
        public void AddReleaseCommand(object input, ICommand command)
        {
            releaseCommandList.Add((Buttons)input, command);
        }
        public void DeleteReleaseCommand(object input)
        {
            releaseCommandList.Remove((Buttons)input);
        }
        public void UpdateInput()
        {
            GamePadState emptyInput = new GamePadState(); //(Vector2.Zero, Vector2.Zero, 0, 0);

            // Get the current GamePad state.
            GamePadState currentGamePadState = GamePad.GetState(PlayerIndex.One);

            // Process input only if connected.
            if (currentGamePadState.IsConnected)
            {
                var buttonList = (Buttons[])Enum.GetValues(typeof(Buttons));
                if (currentGamePadState != emptyInput) // Button Pressed
                {
                    foreach (var button in buttonList)
                    {
                        if (currentGamePadState.IsButtonDown(button) &&
                            !previousGamePadState.IsButtonDown(button)) //buttons just pressed
                        {
                            if (commandList.TryGetValue(button, out ICommand command))
                            {
                                command.Execute();
                            }
                        }
                        else if(currentGamePadState.IsButtonDown(button) &&
                            previousGamePadState.IsButtonDown(button)) //busstons held down
                        {
                            if (commandList.TryGetValue(button, out ICommand command))
                            {
                                command.Execute();
                            }
                        }
                    }
                }
                if (previousGamePadState != emptyInput)
                {
                    foreach (var button in buttonList)
                    {
                        if (!currentGamePadState.IsButtonDown(button) &&
                            previousGamePadState.IsButtonDown(button)) //buttons just pressed
                        {
                            if (releaseCommandList.TryGetValue(button, out ICommand command))
                            {
                                command.Execute();
                            }
                        }
                    }
                }
            }

            previousGamePadState = currentGamePadState;
        }
    }
}
