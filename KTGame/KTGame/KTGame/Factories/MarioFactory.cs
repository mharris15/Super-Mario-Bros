using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using KTGame.Interfaces;
using KTGame.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTGame.Factories
{
    class MarioFactory : IFactory
    {
        private readonly Texture2D Texture;
        private IDictionary<string, int[]> FramesXLeftDic;
        private IDictionary<string, int[]> FramesXRightDic;
        public MarioFactory(Texture2D marioTex)
        {
            Texture = marioTex;
            FramesXLeftDic = new Dictionary<string, int[]>();
            FramesXRightDic = new Dictionary<string, int[]>();
            SetFramesXDic();
        }
        public void SetFramesXDic()
        {
            FramesXLeftDic.Add(new KeyValuePair<string, int[]>("Idle", new int[] { 16 * 8 }));
            FramesXLeftDic.Add(new KeyValuePair<string, int[]>("Crouching", new int[] { 16 * 2 }));
            FramesXLeftDic.Add(new KeyValuePair<string, int[]>("Walking", new int[] { 16 * 7, 16 * 6, 16 * 5, 16 * 6 }));
            FramesXLeftDic.Add(new KeyValuePair<string, int[]>("Running", new int[] { 16 * 7, 16 * 6, 16 * 5, 16 * 6 }));
            FramesXLeftDic.Add(new KeyValuePair<string, int[]>("Jumping", new int[] { 16 * 3 }));
            FramesXLeftDic.Add(new KeyValuePair<string, int[]>("Falling", new int[] { 16 * 3 }));
            FramesXLeftDic.Add(new KeyValuePair<string, int[]>("Dying", new int[] { 0 }));
            FramesXLeftDic.Add(new KeyValuePair<string, int[]>("Flag", new int[] { 16 * 16 }));

            FramesXRightDic.Add(new KeyValuePair<string, int[]>("Idle", new int[] { 16 * 9 }));
            FramesXRightDic.Add(new KeyValuePair<string, int[]>("Crouching", new int[] { 16 * 15 }));
            FramesXRightDic.Add(new KeyValuePair<string, int[]>("Walking", new int[] { 16 * 10, 16 * 11, 16 * 12, 16 * 11 }));
            FramesXRightDic.Add(new KeyValuePair<string, int[]>("Running", new int[] { 16 * 10, 16 * 11, 16 * 12, 16 * 11 }));
            FramesXRightDic.Add(new KeyValuePair<string, int[]>("Jumping", new int[] { 16 * 14 }));
            FramesXRightDic.Add(new KeyValuePair<string, int[]>("Falling", new int[] { 16 * 14 }));
            FramesXRightDic.Add(new KeyValuePair<string, int[]>("Dying", new int[] { 0 }));
            FramesXRightDic.Add(new KeyValuePair<string, int[]>("Flag", new int[] { 16 * 16 }));

        }
        public ISprite GetSprite(IEntity entity)
        {
            int[] FramesX, FramesY;

            Player player = entity as Player;
            string actionName = player.ActionState.StateName;
            Point frameSize = player.PowerState.PowerFrameSize;
            int vertOffset = player.PowerState.PowerFrameVerticalOffset;
            bool Direction = player.ActionState.FacingRight;
            if(Direction) FramesXRightDic.TryGetValue(actionName, out FramesX);
            else FramesXLeftDic.TryGetValue(actionName, out FramesX);
            Console.WriteLine("{0},{1}", actionName, FramesX.Length);
            FramesY = new int[] { vertOffset };
            switch (actionName)
            {
                case "Idle":
                    //FramesX = new int[] { 16 * (Direction ? 9 : 8) };
                    //FramesY = new int[] { vertOffset };
                    return new Sprites.IdleSprite(Texture, FramesX, FramesY, frameSize, entity);
                case "Crouching":
                    //FramesX = new int[] { 16 * (Direction ? 15 : 2) };
                    //FramesY = new int[] { vertOffset };
                    return new Sprites.CrouchingSprite(Texture, FramesX, FramesY, frameSize, entity);
                case "Walking":
                    //FramesX = new int[] { 16 * (Direction ? 10 : 7), 16 * (Direction ? 11 : 6), 16 * (Direction ? 12 : 5), 16 * (Direction ? 11 : 6) };
                    //FramesY = new int[] { vertOffset };
                    return new Sprites.WalkingSprite(Texture, FramesX, FramesY, frameSize, entity);
                case "Running":
                    //FramesX = new int[] { 16 * (Direction ? 10 : 7), 16 * (Direction ? 11 : 6), 16 * (Direction ? 12 : 5), 16 * (Direction ? 11 : 6) };
                    //FramesY = new int[] { vertOffset };
                    return new Sprites.RunningSprite(Texture, FramesX, FramesY, frameSize, entity);
                case "Jumping":
                    //FramesX = new int[] { 16 * (Direction ? 14 : 3) };
                    //FramesY = new int[] { vertOffset };
                    return new Sprites.JumpingSprite(Texture, FramesX, FramesY, frameSize, entity);
                case "Falling":
                    //FramesX = new int[] { 16 * (Direction ? 14 : 3) };
                    //FramesY = new int[] { vertOffset };
                    return new Sprites.FallingSprite(Texture, FramesX, FramesY, frameSize, entity);
                case "Dying":
                    //FramesX = new int[] { 0 };
                    //FramesY = new int[] { vertOffset };
                    return new Sprites.DyingSprite(Texture, FramesX, FramesY, frameSize, entity);

                case "Flag":
                    //FramesX = new int[] { 16 * 16 };
                    //FramesY = new int[] { vertOffset };
                    return new Sprites.FlagSprite(Texture, FramesX, FramesY, frameSize, entity);

                default:
                    throw new NotSupportedException();
            }
        }
    }
}
