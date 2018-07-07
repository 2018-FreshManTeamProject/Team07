using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;
using Oikake.Device;
using Microsoft.Xna.Framework;

namespace Oikake.Scene
{
    class Ending : IScene
    {
        private bool isEndFlag;
        private Sound sound;
        IScene backGroundScene;

        public Ending(IScene scene)
        {
            isEndFlag = false;
            var gameDevice = GameDevice.Instance();
            sound = gameDevice.GetSound();
            backGroundScene = scene;
        }
        public void Draw(Renderer renderer)
        {
            backGroundScene.Draw(renderer);
            renderer.Begin();
            renderer.DrawTexture("ending", new Vector2(150, 150));
            renderer.End();
        }

        public void Initialize()
        {
            isEndFlag = false;
        }

        public bool IsEnd()
        {
            return isEndFlag;
        }

        public Scene Next()
        {
            return Scene.Title;

        }

        public void Shutdown()
        {
            
        }

        public void Update(GameTime gametime)
        {
            sound.PlayBGM("endingbgm");
            if(Input.GetKeyTrigger(Keys.Space))
            {
                isEndFlag = true;
                sound.PlaySE("endingse");
            }
        }
    }
}
