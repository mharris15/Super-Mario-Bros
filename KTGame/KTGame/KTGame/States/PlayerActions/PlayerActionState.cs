using KTGame.Interfaces;
using KTGame.Objects;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTGame.States.PlayerActions
{
    public abstract class PlayerActionState : IPlayerActionState
    {
        protected Player Player { get; set; }
        public string StateName { get; set; }
        public bool FacingRight { get; set; }
        protected HUD HUD { get; set; }
        public IPlayerActionState PreviousState { get; set; }
        protected const float HorizontalAccelerationHeld = 0.2f; //Horizontal acceleration when holding left/right
        protected const float HorizontalAccelerationReleased = 0.2f; //Horizontal acceleration when holding left/right
        protected const float VerticalAccelerationHeld = 0.45f; //Vertical acceleration when holding up
        protected const float VerticalAccelerationReleased = 0.5f; //Vertical acceleration when up released
        protected PlayerActionState(Player player, IPlayerActionState previousState,bool facingRight)
        {
            Player = player;
            FacingRight = facingRight;
            PreviousState = previousState;
            StateName = "Null";
            HUD = HUD.Instance;
        }
        public virtual IPlayerActionState InputUpTransition() {
            return null;
        }

        public virtual IPlayerActionState InputLeftTransition()
        {
            return null;
        }
        public virtual IPlayerActionState InputRightTransition()
        {
            return null;
        }
        public virtual IPlayerActionState InputDownTransition()
        {
            return null;
        }
        public virtual IPlayerActionState InputLeftReleaseTransition()
        {
            return null;
        }
        public virtual IPlayerActionState InputRightReleaseTransition()
        {
            return null;
        }
        public virtual IPlayerActionState InputUpReleaseTransition()
        {
            return null;
        }
        public virtual IPlayerActionState InputDownReleaseTransition()
        {
            return null;
        }
        public virtual void Update(GameTime gameTime) {
            if (Math.Abs(Player.Velocity.X) < HorizontalAccelerationReleased)
            {
                Player.Velocity = new Vector2(0, Player.Velocity.Y);
            }
            //if(HUD.Lives == 0)
            //{
                
            //}
        }
    }
}
