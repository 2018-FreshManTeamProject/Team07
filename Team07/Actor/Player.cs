using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Oikake.Actor;
using Oikake.Device;
using Oikake.Def;
using Oikake.Scene;
using Oikake.Util;

namespace Oikake.Actor
{
    class Player : Character
    {
        //private Vector2 position;
        private Motion motion;
        private enum Direction
        {
            DOWN,UP,RIGHT,LEFT
        };
        private Direction direction;
        private Vector2 stepVelocity;
        private float stepTime =0.5f;
        private Timer stepTimer;
        private bool stepRelease;


        


        private Dictionary<Direction, Range> directionRange;

        public Player(IGameMediator mediator) : base("renbanPlayer", mediator)
        {
            position = Vector2.Zero;
        }
        public override void Initialize()
        {
            position = new Vector2(300, 400);
            motion = new Motion();

            //下向き
            for (int i = 0; i < 4; i++)
            {
                motion.Add(i, new Rectangle(32 * (i % 4), 32 * (i / 4), 32, 32));
            }
            //上向き
            for (int i = 4; i < 8; i++)
                motion.Add(i, new Rectangle(32 * (i % 4), 32 * (i / 4), 32, 32));

            //右向き
            for (int i = 8; i < 12; i++)
            {
                motion.Add(i, new Rectangle(32 * (i % 4), 32 * (i / 4), 32, 32));
            }
            //左向き
            for (int i = 12; i < 16; i++)
            {
                motion.Add(i, new Rectangle(32 * (i % 4), 32 * (i / 4), 32, 32));
            }

            motion.Initialize(new Range(0, 3), new CountDownTimer(0.2f));

            direction = Direction.DOWN;
            directionRange = new Dictionary<Direction, Range>()
            {
                {Direction.DOWN,new Range(0,3) },
                {Direction.UP,new Range(4,7) },
                {Direction.RIGHT,new Range(8,11) },
                {Direction.LEFT,new Range(12,15) }
            };

            stepVelocity = Vector2.Zero;
            stepTimer = new CountUpTimer(stepTime);
            stepRelease = true;
        }
        public override void Update(GameTime gameTime)
        {
            stepTimer.Update(gameTime);
            float speed = 5.0f;
            
            if(stepVelocity == Input.Velocity() && stepRelease == true && stepTimer.IsTime() == false )
            {
                speed = 100;
                
            }
           
            position = position + Input.Velocity() * speed;
            if (Input.Velocity() == new Vector2(0, 0))
            {
                stepRelease = true;

            }
            else
            {
                stepVelocity = Input.Velocity();
                stepRelease = false;
                stepTimer.Initialize();

            }


           
            


            var min = Vector2.Zero;
            var max = new Vector2(Screen.Width - 64, Screen.Height - 64);
            position = Vector2.Clamp(position, min, max);

            UpdateMotion();
            motion.Update(gameTime);



        } 
        //public void Draw(Renderer renderer)
        //{
        //    renderer.DrawTexture("white", position);
        //}
        public override void Shutdown()
        {

        }

        public override void Hit(Character other)
        {
            
        }

        public override void Draw(Renderer renderer)
        {
            renderer.DrawTexture(name, position, motion.DrawingRange());
        }

        private void ChangeMotion(Direction direction)
        {
            this.direction = direction;
            motion.Initialize(directionRange[direction], new CountDownTimer(0.2f));
        }

        private void UpdateMotion()
        {
            Vector2 velocity = Input.Velocity();
            if(velocity.Length()<=0.0f)
            {
                return;
            }
            if((velocity.Y >0.0f)&&(direction!=Direction.DOWN))
            {
                ChangeMotion(Direction.DOWN);
            }
            else if((velocity.Y<0.0f)&&(direction != Direction.UP))
            {
                ChangeMotion(Direction.UP);
            }
            else if((velocity.X >0.0f)&&(direction!=Direction.RIGHT))
            {
                ChangeMotion(Direction.RIGHT);
            }
            else if((velocity.X <0.0f)&&(direction != Direction.LEFT))
            {
                ChangeMotion(Direction.LEFT);
            }
        }

    }
}
