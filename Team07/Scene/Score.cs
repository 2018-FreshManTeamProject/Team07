﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oikake.Device;
using Microsoft.Xna.Framework;
using Oikake.Util;

namespace Oikake.Scene
{
    class Score
    {
        private int poolScore;
        private int score;
        //private Timer timerScore;
        //private bool boolTime;
        

        public void Add()
        {
            poolScore = poolScore += 0;
        }
        public void Add(int num)
        {

           
            poolScore = num;
        }

        public void Draw(Renderer renderer)
        {

            renderer.DrawTexture("score",new Vector2(50, 10));
            renderer.DrawNumber("number", new Vector2(250, 13), score);


        }

        public void Initialize()
        {
<<<<<<< HEAD
            score = 600;
            poolScore -= 0;
=======
            //timerScore = new CountDownTimer(poolScore);
            
            score = 1000;
            poolScore = 0;
           // boolTime = true;
>>>>>>> fumie
        }

        public Score()
        {
            Initialize();

        }

        public void Shutdown()
        {

            score += poolScore;
            poolScore = 0;
        }

        public void Update(GameTime gameTime)
        {

            if(poolScore >= 0)
            {
                score -= 1;
<<<<<<< HEAD
                poolScore += 0;
=======
                poolScore -= 0;
>>>>>>> fumie
            }
            
            
          
            
        }
           
    }
}