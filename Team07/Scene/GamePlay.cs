using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Oikake.Device;
using Oikake.Actor;
using Oikake.Util;
using Oikake.Def;

namespace Oikake.Scene
{
    class GamePlay : IScene,IGameMediator
    {

        private CharacterManager characterManager;
        private Score score;
        private Timer timer;
        private TimerUI timerUI;
        private bool isEndFlag;
        private Sound sound;
        private Player player;

        public GamePlay()
        {
            isEndFlag = false;
            var gameDevice = GameDevice.Instance();
            sound = gameDevice.GetSound();
        }

        public void AddActor(Character character)
        {
            characterManager.Add(character);
        }

        public void AddScore()
        {
            score.Add();
        }

        public void AddScore(int num)
        {
            score.Add(num);
        }

        public void Draw(Renderer renderer)
        {
            renderer.Begin();
            renderer.DrawTexture("hurasuko", new Vector2(Screen.Width / 2, Screen.Height / 2) - new Vector2(470, 770)); ;
            characterManager.Draw(renderer);

            score.Draw(renderer);

            timerUI.Draw(renderer);
            renderer.End();
        }

        public void Initialize()
        {
            isEndFlag = false;

            characterManager = new CharacterManager();
            characterManager.Add(new Player(this));
            characterManager.Add(new BoundEnemy(this));

            for (int i = 0; i < 20; i++)
            {
                characterManager.Add(new BoundEnemy(this));
            }
          
            
            timer = new CountUpTimer(1000);
            timerUI = new TimerUI(timer);
            score = new Score();


        }

        public bool IsEnd()
        {
            return isEndFlag;
        }

        public Scene Next()
        {
            
            
                return Scene.Ending;
            
        
        }

        public void Shutdown()
        {
            
        }

        public void Update(GameTime gameTime)
        {
            
                
            score.Update(gameTime);
            timer.Update(gameTime);
            characterManager.Update(gameTime);
            sound.PlayBGM("gameplaybgm");
            
             if (timer.IsTime())
             {
                score.Shutdown();
                isEndFlag = true;
             }
             
            foreach(var l in characterManager.Players)
            {
                if (l is Player && ((Player)l).dead)
                {
                    isEndFlag = true;
                }
            }

        }

    }
}
