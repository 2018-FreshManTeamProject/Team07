using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Oikake.Device;
using Oikake.Scene;

namespace Oikake.Actor
{
    abstract class Character
    {
        protected Vector2 position;
        protected string name;
        protected bool isDeadFlag;
        protected IGameMediator mediator;

        public Character( string name,IGameMediator mediator)
        {
            this.name = name;
            this.mediator = mediator;
            position = Vector2.Zero;
            isDeadFlag = false;
        }

        public abstract void Initialize();
        public abstract void Update(GameTime gameTime);
        public abstract void Shutdown();

        public abstract void Hit(Character other);

        public bool IsDead()
        {
            return isDeadFlag;
        }

        public virtual void Draw (Renderer renderer)
        {
            renderer.DrawTexture(name, position);
        }

        public bool IsCollision( Character other)
        {
            float length = (position - other.position).Length();
            float radiusSum = 16f + 16f;
            if( length <= radiusSum)
            {
                return true;
            }
            return false;
        }

        public void SetPosition(Vector2 other)
        {
            other = position;
        }

       

    }
}
