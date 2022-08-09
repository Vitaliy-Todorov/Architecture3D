using Scripts.Data;
using Scripts.Infrastructure.Services.PersistentProgress;
using System.Collections;
using System.Linq;
using TMPro;
using UnityEngine;

namespace Scripts.Loots
{
    public class LootPiece : MonoBehaviour, ISavedProgress
    {
        public GameObject Skull;
        public GameObject PickupFxPrefab;
        public TextMeshPro LootText;
        public GameObject PickupPopup;

        private PlayerProgress _playerProgress;
        private Loot _loot;
        private bool _picked;

        public void Construct(PlayerProgress playerProgress) =>
            _playerProgress = playerProgress;

        public void Initialize(Loot loot)
        {
            _loot = loot;
        }

        private void OnTriggerEnter(Collider other) => 
            Pickup();

        private void Pickup()
        {
            if (_picked)
                return;

            _picked = true;

            UpdateWorldData();
            HideSkull();
            // PlayPickupFx();
            ShowText();

            StartCoroutine(StartDestroyTimer());
        }

        private void UpdateWorldData() =>
            _playerProgress.WorldData.LootData.Collect(_loot);

        private void HideSkull() => 
            Skull.SetActive(false);

        private void PlayPickupFx() => 
            Instantiate(PickupFxPrefab, transform.position, Quaternion.identity);

        private void ShowText()
        {
            LootText.text = $"{_loot.Value}";
            PickupPopup.SetActive(true);
        }

        private IEnumerator StartDestroyTimer()
        {
            yield return new WaitForSeconds(1.5f);

            Destroy(gameObject);
        }

        public void UpdateProgress(PlayerProgress progress)
        {
            if (_playerProgress.LootsOnMap.Any(loot => loot.Id == _loot.Id))
                return;

            _playerProgress.LootsOnMap.Add(_loot);
        }

        public void LoadProgress(PlayerProgress progress)
        {
            //throw new System.NotImplementedException();
        }
    }
}