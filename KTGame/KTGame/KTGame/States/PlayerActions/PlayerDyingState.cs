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
    class PlayerDyingState : PlayerActionState
    {
        public PlayerDyingState(Player player, IPlayerActionState previousState, bool facingRight)
            : base(player, previousState, facingRight)
        {
            Player.Soundbank.PlaySound("Die");
            StateName = "Dying";
        }

        public override void Update(GameTime gameTime)
        {

        }
    }
}
