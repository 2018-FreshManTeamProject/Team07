using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Oikake.Def;
using Oikake.Scene;
using Oikake.Device;
using Oikake.Util;

namespace Oikake.Actor
{
    class BoundEnemy : Character
    {
        private Vector2 velocity;
        private static Random rnd = new Random();
        private Sound sound;
        private Timer timer = new CountUpTimer();
        private Player player;

        public BoundEnemy(IGameMediator mediator) : base ("エネミー（通常）", mediator)
        {
            velocity = Vector2.Zero;
            var gameDevice = GameDevice.Instance();
            sound = gameDevice.GetSound();
        }

        public override void Initialize()
        {
            while (true)
            {
                position = new Vector2(
                    rnd.Next(Screen.Width -64),
                    rnd.Next(Screen.Height - 64));

                if (position.X > 450)
                {
                    break;
                }
                if (position.X < 150)
                {
                    break;
                }
                if (position.Y> 550)
                {
                    break;
                }
                if (position.Y < 250)
                {
                    break;
                }
            }
            
            velocity = new Vector2(rnd.Next(1, 3), rnd.Next(1, 3));
        }

        public override void Shutdown()
        {
            
        }

        public override void Update(GameTime gameTime)
        {
            Random rnd = new Random();
            //左壁の当たり判定
            if(position.X <0.0f)
            {
                velocity.X *= -1;
            }
            //右壁の当たり判定
          else  if (position.X > Screen.Width-64)
            {
                velocity.X *= -1;
            }
            //上の壁
            if (position.Y <0.0f)
            {
                velocity.Y *= -1;
            }
            //下の壁
           else if(position.Y>Screen.Height-64)
            {
                velocity.Y *= -1;
            }
            
            position += velocity;

        }

        public override void Hit(Character other)
        {

            isDeadFlag = true;
            mediator.AddScore(100);
            mediator.AddActor(new BoundEnemy(mediator));
            mediator.AddActor(new BoundEnemy(mediator));
            //mediator.AddActor(new BurstEffect(position, mediator));
            sound.PlaySE("gameplayse");

        }
    }
}
