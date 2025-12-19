using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class s : MonoBehaviour
{
    public class AllocationDetector : MonoBehaviour
    {
        private long _lastTotalMemory;

        void Update()
        {
            long current = System.GC.GetTotalMemory(false);
            long diff = current - _lastTotalMemory;

            if (diff > 1024 * 1024) // >1MB за кадр
            {
                Debug.LogError($"Большая аллокация: {diff / 1024}KB");
                // Поставь брейкпоинт здесь
            }

            _lastTotalMemory = current;
        }
    }
}
