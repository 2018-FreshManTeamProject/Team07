using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oikake.Actor;

namespace Oikake.Scene
{
    interface IGameMediator
    {
        void AddActor(Character character);
        void AddScore();
        void AddScore(int num);
    }
}
