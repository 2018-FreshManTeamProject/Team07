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


        public BoundEnemy(IGameMediator mediator) : base("エネミー（通常）", mediator)
        {
            velocity = Vector2.Zero;
            var gameDevice = GameDevice.Instance();
            sound = gameDevice.GetSound();
        }

        public override void Initialize()
        {

            position = new Vector2(
                rnd.Next(600, 1200),
                rnd.Next(200, 500));
            velocity = new Vector2(rnd.Next(1, 3), rnd.Next(1, 3));
        }

        public override void Shutdown()
        {

        }

        public override void Update(GameTime gameTime)
        {
            Random rnd = new Random();
       
            {
                //フラスコの円の当たり判定


                Vector2 vector = new Vector2(Screen.Width / 2, Screen.Height / 2);
                float length = (position - vector).Length();
                if (Screen.Radius - 12 < length)
                {
                    velocity.X *= -1;
                    velocity.Y *= -1;
                }
                position += velocity;
            }
        }

        public override void Hit(Character other)
        {

            isDeadFlag = true;
            mediator.AddScore(100);
            for (int i = 0; i < 20; i++)
            {
                mediator.AddActor(new BoundEnemy(mediator));
            }
            //mediator.AddActor(new BurstEffect(position, mediator));
            sound.PlaySE("gameplayse");

        }
    }
}
