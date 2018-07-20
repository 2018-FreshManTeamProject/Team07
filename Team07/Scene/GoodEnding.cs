using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Oikake.Device;
using Oikake.Util;

namespace Oikake.Scene
{
    class GoodEnding : IScene
    {
        private bool isEndFlag;//死亡フラグ
        private IScene backGroundScnene;//背景シーン
        private Sound sound;//サウンド
        private Timer timer;//制限時間
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="scene"></param>
        public  GoodEnding(IScene scene)
        {
            isEndFlag = false;
            backGroundScnene = scene;
            var gameDevice = GameDevice.Instance();
            sound = gameDevice.GetSound();
        }
        /// <summary>
        /// 描画
        /// </summary>
        /// <param name="renderer"></param>
        public void Draw(Renderer renderer)
        {
            //シーンごとにrenderer.Begin()～End()を
            //書いているのに注意
            //背景となるゲームプレイシーン
            backGroundScnene.Draw(renderer);

            renderer.Begin();
            renderer.DrawTexture("ステージクリア", new Vector2(150, 150));
            renderer.End();
        }
        /// <summary>
        /// 初期化
        /// </summary>
        public void Initialize()
        {
            isEndFlag = false;
        }

        /// <summary>
        /// 終了か
        /// </summary>
        /// <returns></returns>
        public bool IsEnd()
        {
            return isEndFlag;
        }

        /// <summary>
        /// 次のシーンは？
        /// </summary>
        /// <returns></returns>
        public Scene Next()
        {
            return Scene.Title;
        }

        /// <summary>
        /// 終了
        /// </summary>
        public void Shutdown()
        {
            sound.StopBGM();
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="gameTime"></param>
        public void Update(GameTime gameTime)
        {
            if (Input.GetKeyTrigger(Keys.Space))
            {
                isEndFlag = true;
                sound.PlaySE("endingse");
            }
        }
    }
}
