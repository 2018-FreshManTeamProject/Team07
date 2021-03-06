﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Oikake.Device;

namespace Oikake.Actor
{
    class CharacterManager
    {
        private List<Character> players;
        private List<Character> enemys;
        private List<Character> addNewCharacters;
        private bool isEndFlag;
        
        public List<Character> Players { get { return players; } }
        public CharacterManager()
        {
            Initialize();
        }

        public void Initialize()
        {
            if(players != null)
            {
                players.Clear();
            }
            else
            {
                players = new List<Character>();
            }

            if(enemys != null)
            {
                enemys.Clear();
            }
            else
            {
                enemys = new List<Character>();
            }
            if(addNewCharacters != null)
            {
                addNewCharacters.Clear();
            }
            else
            {
                addNewCharacters = new List<Character>();
            }
            isEndFlag = false;
        }

        public void Add(Character character)
        {
            if(character == null)
            {
                return;
            }
            addNewCharacters.Add(character);
        }

        public bool HitToCharacters()
        {
            foreach(var player in players)
            {
                foreach(var enemy in enemys)
                {
                    if(player.IsDead()|| enemy.IsDead())
                    {
                        continue;
                    }
                    if(player.IsCollision(enemy))
                    {
                        player.Hit(enemy);
                        enemy.Hit(player);

                    }
                }
            }
            return true;
        }

        private void RemoveDeadCharacter()
        {
            players.RemoveAll(p => p.IsDead());
            enemys.RemoveAll(e => e.IsDead());
        }

        public void Update(GameTime gameTime)
        {
            foreach(var p in players)
            {
                p.Update(gameTime);
            }
            foreach(var e in enemys)
            {
                e.Update(gameTime);
            }
            foreach(var newChara in addNewCharacters)
            {
                if(newChara is Player)
                {
                    newChara.Initialize();
                    players.Add(newChara);
                }
                else
                {
                    newChara.Initialize();
                    enemys.Add(newChara);
                }
            }
            addNewCharacters.Clear();
            HitToCharacters();
            RemoveDeadCharacter();

            
        }

        public void Draw(Renderer renderer)
        {
            foreach( var e in enemys)
            {
                e.Draw(renderer);
            }
            foreach( var p in players)
            {
                p.Draw(renderer);
            }
        }
    }
}
