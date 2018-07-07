using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Oikake.Device;

namespace Oikake.Scene
{
    class SceneManager
    {
        private Dictionary<Scene, IScene> scenes = new Dictionary<Scene, IScene>();
        private IScene currntScene = null;

        public SceneManager()
        {

        }

        public void Add( Scene name,IScene scene)
        {
            if( scenes.ContainsKey(name))
            {
                return;
            }
            scenes.Add(name, scene);
        }

        public void Change( Scene name)
        {
            if( currntScene != null)
            {
                currntScene.Shutdown();
            }
            currntScene = scenes[name];
            currntScene.Initialize();
        }

        public void Update( GameTime gameTime)
        {
            if( currntScene == null)
            {
                return;
            }
            currntScene.Update(gameTime);

            if( currntScene.IsEnd())
            {
                Change(currntScene.Next());
            }
        }

        public void Draw(Renderer renderer)
        {
            if(currntScene == null)
            {
                return;
            }
            currntScene.Draw(renderer);
        }
    }
}
