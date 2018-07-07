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
        private Dictionary<Direction, Range> directionRange;

        public Player(IGameMediator mediator) : base("oikake_player_4anime",mediator)
        {
            position = Vector2.Zero;
        }
        public override void Initialize()
        {
            position = new Vector2(300, 400);
            motion = new Motion();

            for(int i = 0;i<16;i++)
            {
                motion.Add(i, new Rectangle(64 * (i%4), 64 * (i/4), 64, 64));
            }

            //motion.Add(0, new Rectangle(64 * 0, 64 * 0, 64, 64));
            //motion.Add(1, new Rectangle(64 * 1, 64 * 0, 64, 64));
            //motion.Add(2, new Rectangle(64 * 2, 64 * 0, 64, 64));
            //motion.Add(3, new Rectangle(64 * 3, 64 * 0, 64, 64));

            //motion.Add(4, new Rectangle(64 * 0, 64 * 1, 64, 64));
            //motion.Add(5, new Rectangle(64 * 1, 64 * 1, 64, 64));
            //motion.Add(6, new Rectangle(64 * 2, 64 * 1, 64, 64));
            //motion.Add(7, new Rectangle(64 * 3, 64 * 1, 64, 64));

            //motion.Add(8, new Rectangle(64 * 0, 64 * 2, 64, 64));
            //motion.Add(9, new Rectangle(64 * 1, 64 * 2, 64, 64));
            //motion.Add(10, new Rectangle(64 * 2, 64 * 2, 64, 64));
            //motion.Add(11, new Rectangle(64 * 3, 64 * 2, 64, 64));

            //motion.Add(12, new Rectangle(64 * 0, 64 * 3, 64, 64));
            //motion.Add(13, new Rectangle(64 * 1, 64 * 3, 64, 64));
            //motion.Add(14, new Rectangle(64 * 2, 64 * 3, 64, 64));
            //motion.Add(15, new Rectangle(64 * 3, 64 * 3, 64, 64));

            motion.Initialize(new Range(0, 15), new CountDownTimer(0.2f));

            direction = Direction.DOWN;
            directionRange = new Dictionary<Direction, Range>()
            {
                {Direction.DOWN,new Range(0,3) },
                {Direction.UP,new Range(4,7) },
                {Direction.RIGHT,new Range(8,11) },
                {Direction.LEFT,new Range(12,15) }
            };
        }
        public override void Update(GameTime gameTime)
        {
            Vector2 velocity = Vector2.Zero;

            if( Input.GetKeyState(Keys.Right))
            {
                velocity.X = 1f;
            }
            if(Input.GetKeyState(Keys.Left))
            {
                velocity.X = -1f;
            }
            if(Input.GetKeyState(Keys.Up))
            {
                velocity.Y = -1f;
            }
            if(Input.GetKeyState(Keys.Down))
            {
                velocity.Y = 1f;
            }
            if( velocity.Length()!=0)
            {
                velocity.Normalize();
            }

            float speed = 5.0f;
            position = position + Input.Velocity() * speed;



            if (position.X < 0.0f)
            {
                position.X = 0;
            }
            if (position.X >= Screen.Width - 64)
            {
                position.X = Screen.Width - 64;
            }
            if (position.Y < 0.0f)
            {
                position.Y = 0.0f;
            }
            if (position.Y >= Screen.Height - 64)
            {
                position.Y = Screen.Height - 64;
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
