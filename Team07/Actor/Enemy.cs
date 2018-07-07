using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Oikake.Actor;
using Oikake.Device;
using Oikake.Scene;

namespace Oikake.Actor
{
    class Enemy : Character
    {
        //private Vector2 position;
        Random rnd = new Random();
        private Sound sound;
        public Enemy(IGameMediator mediator) : base("black",mediator)
        {
            position = Vector2.Zero;
            var gameDevice = GameDevice.Instance();
            sound = gameDevice.GetSound();
        }

        public override void Initialize()
        {
            position = new Vector2(300, 400);
        }

       

        public override void Update(GameTime gameTime)
        {
            position += new Vector2(rnd.Next(-1, 2), rnd.Next(-1,2));
        }

        public override void Shutdown()
        {

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
