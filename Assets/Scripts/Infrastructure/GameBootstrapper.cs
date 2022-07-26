using Scripts.Infrastructure.States;
using Scrips.Logic;
using UnityEngine;

namespace Scrips.Infrastructure
{
    public partial class GameBootstrapper: MonoBehaviour, ICoroutineRanner
    {
        public LoadingCurtain CurtainPrefab;

        private Game _game;

        private void Awake()
        {
            _game = new Game(this, Instantiate(CurtainPrefab));
            _game.StateMachine.Enter<BootstrapState>();

            // Существует независимо от загруженной сцены
            DontDestroyOnLoad(this);
        }
    }
}