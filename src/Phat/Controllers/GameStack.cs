using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Phat
{
    public class GameStack
    {
        private static readonly Stack<GameMode> _stack;
        private readonly Configurator _config;

        public static GameMode Current
        {
            get { return _stack.Peek(); }
        }

        static GameStack()
        {
            _stack = new Stack<GameMode>();
        }
        
        public GameStack(Configurator config)
        {
            this._config = config;
        }

        public TGameMode Push<TGameMode>(Object pushState)
            where TGameMode : GameMode
        {
            if (_stack.Count > 0)
            {
                var currentGame = _stack.Peek();
                currentGame.Suspend();
            }
            return _config.Run<TGameMode>(pushState);
        }

        public GameMode Peek()
        {
            return _stack.Peek();
        }

        public void Push(GameMode gameMode, Object pushState)
        {
            _stack.Push(gameMode);
            gameMode.Initialize(pushState);
        }

        public void Pop(Object popState)
        {
            _stack.Pop();

            var game = _stack.Peek();

            game.Resume(popState);
        }        
    }
}
