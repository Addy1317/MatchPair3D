using DG.Tweening;
using SlowpokeStudio.Generic;
using System.Collections.Generic;
using UnityEngine;

namespace SlowpokeStudio
{
    public class TrayManager : MonoBehaviour
    {
        [SerializeField] private Transform[] traySlots;
        public static TrayManager Instance { get; private set; }

        private List<CubeSelector> currentTrayObjects = new List<CubeSelector>();
        private CubeSelector selectedCube = null;

        private void Awake()
        {
            if (Instance == null) Instance = this;
            else Destroy(gameObject);
        }

        public int MaxTraySize => traySlots.Length;

        public void AddPairToTray(CubeSelector cube1, CubeSelector cube2)
        {
            GameService.Instance.audioManager.PlaySFX(Audio.SFXType.OnSelectionClickSFX);
            Debug.Log($"Adding pair to tray: {cube1.name}, {cube2.name}");
            CleanupTray();

            if (currentTrayObjects.Count + 2 > MaxTraySize)
            {
                Debug.LogWarning("Tray is full!");
                return;
            }

            currentTrayObjects.Add(cube1);
            currentTrayObjects.Add(cube2);

            int index1 = currentTrayObjects.Count - 2;
            int index2 = currentTrayObjects.Count - 1;

            MoveToTraySlot(cube1.transform, index1);
            MoveToTraySlot(cube2.transform, index2);

            CheckForMatch();
            GameService.Instance.gameManager.CheckForFailCondition();

        }

        public void OnCubeClicked(CubeSelector clickedCube)
        {
            Debug.Log("OnCubeClicked: " + clickedCube.name);

            if (!currentTrayObjects.Contains(clickedCube))
            {
                Debug.LogWarning("Clicked cube is not in tray list");
                return;
            }

            if (selectedCube == null)
            {
                selectedCube = clickedCube;
                //HighlightCube(selectedCube, true);
                GameService.Instance.audioManager.PlaySFX(Audio.SFXType.OnSelectionClickSFX);
                Debug.Log("Selected first cube: " + selectedCube.name);
            }
            else if (selectedCube == clickedCube)
            {
                //HighlightCube(selectedCube, false);
                selectedCube = null;
                Debug.Log("Deselected same cube");
            }
            else
            {
                Debug.Log($"Attempting to swap {selectedCube.name} with {clickedCube.name}");

                //HighlightCube(selectedCube, false);
                SwapCubesInTray(selectedCube, clickedCube);
                GameService.Instance.audioManager.PlaySFX(Audio.SFXType.OnSelectionClickSFX);
                selectedCube = null;

                CheckForMatch();
            }
        }

        private void SwapCubesInTray(CubeSelector a, CubeSelector b)
        {
            int indexA = currentTrayObjects.IndexOf(a);
            int indexB = currentTrayObjects.IndexOf(b);

            if (indexA == -1 || indexB == -1)
            {
                Debug.LogError("Swap failed: cubes not found in tray list");
                return;
            }

            currentTrayObjects[indexA] = b;
            currentTrayObjects[indexB] = a;

            MoveToTraySlot(a.transform, indexB);
            MoveToTraySlot(b.transform, indexA);

            Debug.Log($"Swapped cubes: {a.name} ⇄ {b.name}");
        }

        private void MoveToTraySlot(Transform cube, int slotIndex)
        {
            cube.SetParent(traySlots[slotIndex]);
            cube.DOKill(); 
            cube.DOLocalMove(Vector3.zero, 0.3f).SetEase(Ease.OutBack);
            cube.DORotate(Vector3.zero, 0.3f);
        }

        private void HighlightCube(CubeSelector cube, bool highlight)
        {
           /* if (cube == null) return;
            float scale = highlight ? 1.2f : 1f;
            cube.DOKill();
            cube.transform.DOScale(Vector3.one * scale, 0.15f);*/
        }

        private void CheckForMatch()
        {
            Debug.Log("Checking for adjacent matches...");

            if (currentTrayObjects.Count < 2) return;

            for (int i = 0; i < currentTrayObjects.Count - 1; i++)
            {
                var cubeA = currentTrayObjects[i];
                var cubeB = currentTrayObjects[i + 1];

                if (cubeA == null || cubeB == null) continue;

                if (cubeA.cubeColor == cubeB.cubeColor)
                {
                    Debug.Log($"Match found: {cubeA.name} and {cubeB.name} at index {i} and {i + 1}");

                    cubeA.transform.DOKill();
                    cubeB.transform.DOKill();

                    Sequence matchSeq = DOTween.Sequence();
                    matchSeq.Append(cubeA.transform.DOScale(Vector3.zero, 0.25f));
                    matchSeq.Join(cubeB.transform.DOScale(Vector3.zero, 0.25f));

                    matchSeq.AppendCallback(() =>
                    {
                        cubeA.gameObject.SetActive(false);
                        cubeB.gameObject.SetActive(false);

                        currentTrayObjects.RemoveAt(i + 1);
                        currentTrayObjects.RemoveAt(i);

                        RearrangeTray();
                    });

                    return;
                }
            }

            Debug.Log("No adjacent match found");
        }

        private void RearrangeTray()
        {
            CleanupTray();

            for (int i = 0; i < currentTrayObjects.Count; i++)
            {
                var cube = currentTrayObjects[i];
                cube.transform.SetParent(traySlots[i]);
                cube.transform.DOKill();
                cube.transform.DOLocalMove(Vector3.zero, 0.3f).SetEase(Ease.OutBack);
                cube.transform.DORotate(Vector3.zero, 0.3f);
            }

            Debug.Log("Tray rearranged");
            GameService.Instance.gameManager.CheckWinCondition();
        }

        private void CleanupTray()
        {
            currentTrayObjects.RemoveAll(c => c == null || !c.gameObject.activeInHierarchy);

        }
        
        internal void ClearTray()
        {
            Debug.Log("Clearing Tray...");

            foreach (var cube in currentTrayObjects)
            {
                if (cube != null)
                    Destroy(cube.gameObject);
            }

            currentTrayObjects.Clear();
            selectedCube = null;
        }
    }

}





