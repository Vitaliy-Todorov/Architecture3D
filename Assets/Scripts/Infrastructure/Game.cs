
using Scripts.Infrastructure.Services;
using Scripts.Infrastructure.States;
using Scrips.Logic;

namespace Scrips.Infrastructure
{
    public class Game
    {
        public GameStateMachine StateMachine;

        public Game(ICoroutineRanner coroutineRanner, LoadingCurtain curtain)
        {
            SceneLoader sceneLoader = new SceneLoader(coroutineRanner);
            StateMachine = new GameStateMachine(sceneLoader, curtain, AllServices.Container);
        }
    }
}