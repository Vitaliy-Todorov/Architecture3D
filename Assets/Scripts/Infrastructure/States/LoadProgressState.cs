using Scripts.Data;
using Scripts.Infrastructure.Services.PersistentProgress;
using Scripts.Infrastructure.Services.SaveLoad;

namespace Scripts.Infrastructure.States
{
    internal class LoadProgressState : IState
    {
        private readonly GameStateMachine _stateMachine;
        private readonly IPersistentProgressService _progressService;
        private ISaveLoadService _saveLoadServise;

        public LoadProgressState(GameStateMachine stateMachine, IPersistentProgressService progressService, ISaveLoadService saveLoadServise)
        {
            _stateMachine = stateMachine;
            _progressService = progressService;
            _saveLoadServise = saveLoadServise;
        }

        public void Enter()
        {
            LoadProgressOrInitNew();
            string levelName = _progressService.Progress.WorldData.PositionOnLevel.Level;
            _stateMachine.Enter<LoadLeveStatr, string>(levelName);
        }


        public void Exit()
        {
        }

        private void LoadProgressOrInitNew()
        {
            _progressService.Progress = _saveLoadServise.LoadProgress() ?? NewProgress();
            _ = _progressService.Progress;
        }

        private PlayerProgress NewProgress() => 
            new PlayerProgress("Main");
    }
}