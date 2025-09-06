using System.Collections.Generic;
using UnityEngine;

namespace SlowpokeStudio.Level
{
    [System.Serializable]
    public struct LevelData
    {
        public string levelName;
        public GameObject levelObject;
    }

    [CreateAssetMenu(fileName = "LevelDatabase", menuName = "Match3D/Level Database")]
    public class LevelSO : ScriptableObject
    {
        public List<LevelData> levels = new List<LevelData>();
    }
}
