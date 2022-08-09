using Scrips.Logic.EnemySpawner;
using Scripts.Infrastructure.Services.StaticData;
using UnityEditor;
using UnityEngine;
using System.Linq;
using Scripts.Logic;
using UnityEngine.SceneManagement;

namespace Scrips.Editor
{
    [CustomEditor(typeof(LevelStaticData))]
    public class LevelStaticDataEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            LevelStaticData levelData = (LevelStaticData)target;

            if (GUILayout.Button("Collect"))
            {
                levelData.EnemySpawner = 
                    FindObjectsOfType<SpawnMarker>()
                    .Select(x => new EnemySpawnerData(x.GetComponent<UniqueId>().Id, x.MonsterTypeId, x.transform.position))
                    .ToList();

                levelData.KeyLevel = SceneManager.GetActiveScene().name;
            }

            EditorUtility.SetDirty(target);
        }
    }
}