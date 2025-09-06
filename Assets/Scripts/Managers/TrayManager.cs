using DG.Tweening;
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
    }
}
