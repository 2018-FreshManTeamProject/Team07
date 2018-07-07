using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Oikake.Def;
using Oikake.Scene;
using Oikake.Device;

namespace Oikake.Actor
{
    class RandomEnemy : Character
    {
        private static Random rnd = new Random();
        private int changeTimer;
        private Sound sound;

        public RandomEnemy(IGameMediator mediator) : base ("black",mediator)
        {
            changeTimer = 60;
            var gameDevice = GameDevice.Instance();
            sound = gameDevice.GetSound();
        }

        public override void Initialize()
        {
            position = new Vector2(
                rnd.Next(Screen.Width - 64),
                rnd.Next(Screen.Height - 64));
            changeTimer = 60 * rnd.Next(2, 5);
        }

        public override void Shutdown()
        {
        }

        public override void Update(GameTime gameTime)
        {
            changeTimer -= 1;
            if( changeTimer < 0)
            {
                Initialize();
            }
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
