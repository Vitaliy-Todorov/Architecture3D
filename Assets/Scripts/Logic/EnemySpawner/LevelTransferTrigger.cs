
using Scripts.Infrastructure.Services;
using Scripts.Infrastructure.States;
using UnityEngine;

namespace Scrips.Logic.EnemySpawner
{
    public class LevelTransferTrigger : MonoBehaviour
    {
        private const string PlayerTeg = "Player";

        public string TransferTo;
        private IGameStateMachine _stateMachin;

        private bool _treggered;

        public void Construct(IGameStateMachine stateMachine) =>
            _stateMachin = stateMachine;

        private void OnTriggerEnter(Collider other)
        {
            if (_treggered)
                return;

            if (other.CompareTag(PlayerTeg))
            {
                _stateMachin.Enter<LoadLeveStatr, string>(TransferTo);
                _treggered = true;
            }
        }
    }
}