using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;

namespace Oikake.Util
{
    abstract class Timer
    {
        protected float limitTime;
        protected float currentTime;

        public Timer(float second)
        {
            limitTime = 60 * second;
        }

       public Timer() : this(1)
       {

       }

        public abstract void Initialize();
        public abstract void Update(GameTime gameTime);
        public abstract bool IsTime();

        public void SetTime(float second)
        {
            limitTime = 60 * second;
        }

        public float Now()
        {
            return currentTime / 60f;
        }

        public abstract float Rate();
    }
}
