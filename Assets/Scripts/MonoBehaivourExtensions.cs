using System;
using System.Collections;
using UnityEngine;

public static class MonoBehaivourExtensions
{
    public static void Delay(this MonoBehaviour self, Action cb)
    {
        IEnumerator WaitForNextFrame()
        {
            yield return new WaitForEndOfFrame();
            cb?.Invoke();
        }
        self.StartCoroutine(WaitForNextFrame());
    }
}