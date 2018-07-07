using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oikake.Util
{
    class Motion
    {
        private Range range;
        private Timer timer;
        private int motionNumber;


        private Dictionary<int, Rectangle> rectangeles = new Dictionary<int, Rectangle>();

        public Motion()
        {
            Initialize(new Range(0, 0), new CountDownTimer());
        }

        public Motion(Range range,Timer timer)

        {
           Initialize(range, timer);
        }

        public void Initialize(Range range,Timer timer)
        {
            this.range = range;
            this.timer = timer;
            motionNumber = range.First();
        }

        public void Add(int index,Rectangle rect)
        {
            if(rectangeles.ContainsKey(index))
            {
                return;
            }
            rectangeles.Add(index, rect);

        }

        private void MotionUpdate()
        {
            motionNumber += 1;
            if(range.IsOutOfRange(motionNumber))
            {
                motionNumber = range.First();
            }
        }

        public void Update(GameTime gameTime)
        {
            if(range.IsOutOfRange())
            {
                return;
            }
            timer.Update(gameTime);
            if(timer.IsTime())
            {
                timer.Initialize();
                MotionUpdate();
            }
        }

        public Rectangle DrawingRange()
        {
            return rectangeles[motionNumber];
        }

    }
}
