using DG.Tweening;
using SlowpokeStudio.Level;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SlowpokeStudio
{
    public class TrayManager : MonoBehaviour
    {
        [SerializeField] private Transform[] traySlots; // Size = 4
        private List<CubeSelector> currentTrayObjects = new List<CubeSelector>();

        public int MaxTraySize => traySlots.Length;

        public void AddToTray(CubeSelector selector)
        {
            if (currentTrayObjects.Count >= MaxTraySize)
            {
                Debug.Log("Tray is full!");
                return;
            }

            int slotIndex = currentTrayObjects.Count;
            currentTrayObjects.Add(selector);

            // Animate to tray
            Transform cube = selector.transform;
            cube.SetParent(traySlots[slotIndex]);
            cube.DOLocalMove(Vector3.zero, 0.3f).SetEase(Ease.OutBack);
            cube.DORotate(Vector3.zero, 0.3f);

            CheckForPair();
        }

        private void CheckForPair()
        {
            if (currentTrayObjects.Count < 2) return;

            var last = currentTrayObjects[^1];
            var secondLast = currentTrayObjects[^2];

            if (last.cubeColor == secondLast.cubeColor)
            {
                Debug.Log($"Matched Pair: {last.cubeColor}");

                DOVirtual.DelayedCall(0.3f, () =>
                {
                    Destroy(last.gameObject);
                    Destroy(secondLast.gameObject);
                    currentTrayObjects.RemoveRange(currentTrayObjects.Count - 2, 2);
                    RearrangeTray();
                });
            }
        }

        private void RearrangeTray()
        {
            for (int i = 0; i < currentTrayObjects.Count; i++)
            {
                var cube = currentTrayObjects[i].transform;
                cube.SetParent(traySlots[i]);
                cube.DOLocalMove(Vector3.zero, 0.3f).SetEase(Ease.OutBack);
            }
        }

        public void AddPairToTray(CubeSelector cube1, CubeSelector cube2)
        {
            if (currentTrayObjects.Count + 2 > MaxTraySize)
            {
                Debug.Log("Tray is full!");
                return;
            }

            currentTrayObjects.Add(cube1);
            currentTrayObjects.Add(cube2);

            int index1 = currentTrayObjects.Count - 2;
            int index2 = currentTrayObjects.Count - 1;

            MoveToTraySlot(cube1.transform, index1);
            MoveToTraySlot(cube2.transform, index2);

            CheckForMatch();
        }

        private void CheckForMatch()
        {
            if (currentTrayObjects.Count < 2) return;

            for (int i = 0; i < currentTrayObjects.Count - 1; i++)
            {
                var cubeA = currentTrayObjects[i];
                var cubeB = currentTrayObjects[i + 1];

                if (cubeA.cubeColor == cubeB.cubeColor)
                {
                    Debug.Log($"✅ Matched Adjacent: {cubeA.cubeColor} at {i} and {i + 1}");

                    DOVirtual.DelayedCall(0.3f, () =>
                    {
                        cubeA.gameObject.SetActive(false);
                        cubeB.gameObject.SetActive(false);

                        currentTrayObjects.RemoveAt(i + 1);
                        currentTrayObjects.RemoveAt(i); // remove earlier one first

                        RearrangeTray();
                    });

                    return; // Only match 1 adjacent pair per tap
                }
            }

            Debug.Log("❌ No adjacent match found");
        }

        private void MoveToTraySlot(Transform cube, int slotIndex)
        {
            cube.SetParent(traySlots[slotIndex]);
            cube.DOLocalMove(Vector3.zero, 0.3f).SetEase(Ease.OutBack);
            cube.DORotate(Vector3.zero, 0.3f);
        }

    }
}
