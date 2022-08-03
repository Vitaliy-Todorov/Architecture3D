using Scripts.Logic;
using System;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using System.Linq;

namespace Scrips.Editor
{
    [CustomEditor( typeof(UniqueId) )]
    public class UniqueIdEditor : UnityEditor.Editor
    {
        private void OnEnable()
        {
            UniqueId uniqueId = (UniqueId) target;

            if (string.IsNullOrEmpty(uniqueId.Id))
                Generate(uniqueId);
            else
            {
                UniqueId[] uniqueIds = FindObjectsOfType<UniqueId>();

                if ( uniqueIds.Any(other => other != uniqueId && other.Id == uniqueId.Id) )
                    Generate(uniqueId);
            }
        }

        private void Generate(UniqueId uniqueId)
        {
            uniqueId.Id = $"{uniqueId.gameObject.scene.name}_{Guid.NewGuid().ToString()}";

            // Изменения в плей може не сохраняем
            if(!Application.isPlaying)
            {
                // Сохраняем изменение объекта на сцене в редакторе
                EditorUtility.SetDirty(uniqueId);
                // Сохраняем сцену
                EditorSceneManager.MarkSceneDirty(uniqueId.gameObject.scene);
            }
        }
    }
}