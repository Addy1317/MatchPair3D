using SlowpokeStudio.Generic;
using SlowpokeStudio.Level;
using System.Collections.Generic;
using UnityEngine;

namespace SlowpokeStudio.Manager
{
    public class GameManager : MonoBehaviour
    {
        private List<CubeSelector> allLevelCubes = new List<CubeSelector>();
        public List<CubeSelector> activeRackCubes = new List<CubeSelector>();

        public bool IsGamePlayable = false;

        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out RaycastHit hit))
                {
                    Debug.Log("Raycast hit: " + hit.collider.gameObject.name);
                }
                else
                {
                    Debug.Log("Raycast missed.");
                }
            }
        }

        public void OnLevelCompleted(int coinsEarned = 0)
        {
            Debug.Log("Level Completed!");
            GameService.Instance.uiManager.levelCompleteUI.levelCompletionObject.SetActive(true);
            GameService.Instance.audioManager.PlaySFX(Audio.SFXType.OnLevelCompletedSFX);
            GameService.Instance.uiManager.levelCompleteUI.coinText.text = $"Coins Earned: {coinsEarned}";
        }

        public void LoadNextLevel()
        {
            Debug.Log("Loading next level...");
            GameService.Instance.levelManager.LoadNextLevel();
        }
        public void RegisterLevelCubes(List<CubeSelector> cubes)
        {
            allLevelCubes = cubes;
        }

        public void CheckWinCondition()
        {
            bool allInactive = true;

            foreach (var cube in allLevelCubes)
            {
                if (cube != null && cube.gameObject.activeInHierarchy)
                {
                    allInactive = false;
                    break;
                }
            }

            if (allInactive)
            {
                OnLevelCompleted(50);
            }
        }

        public void CheckForFailCondition()
        {
            Dictionary<ObjectColor, int> colorCounts = new Dictionary<ObjectColor, int>();

            foreach (var cube in activeRackCubes)
            {
                if (cube == null) continue;

                ObjectColor color = cube.cubeColor;

                if (!colorCounts.ContainsKey(color))
                    colorCounts[color] = 1;
                else
                    colorCounts[color]++;
            }

            bool hasValidPair = false;
            foreach (var count in colorCounts.Values)
            {
                if (count >= 2)
                {
                    hasValidPair = true;
                    break;
                }
            }

            if (!hasValidPair)
            {
                Debug.Log("No more valid color pairs! Level Failed.");
                GameService.Instance.audioManager.PlaySFX(Audio.SFXType.OnLevelFailSFX);
                //GameService.Instance.uiManager.levelFailUI.levelFailObject.SetActive(true);
            }
        }

    }
}
