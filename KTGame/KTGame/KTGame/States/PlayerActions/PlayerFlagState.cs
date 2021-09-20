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
    class PlayerFlagState : PlayerActionState
    {
        public PlayerFlagState(Player player, IPlayerActionState previousState, bool facingRight)
            : base(player, previousState, facingRight)
        {
            //Player.Soundbank.PlaySound("Die");
            Player.Velocity = new Vector2(0, 0);
            StateName = "Flag";
        }

        public override void Update(GameTime gameTime)
        {

        }
    }
}
