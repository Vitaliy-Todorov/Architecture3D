using UnityEditor;
using UnityEngine;

namespace Scrips.Editor
{
    public class Tools
    {
        [MenuItem("Tools/Crear prefs")]
        public static void ClearPrefs()
        {
            PlayerPrefs.DeleteAll();
            PlayerPrefs.Save();
        }
    }
}