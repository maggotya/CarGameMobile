using UnityEngine;
using System.Collections;
using System.Diagnostics;

public class Counter : MonoBehaviour {
    [SerializeField] private float startTime = 1f;

    private Stopwatch stopwatch;
    private long total;
    private long num;
    private long last;

    private void Awake() {
        stopwatch = new Stopwatch();
        StartCoroutine(Log());
    }

    private void Update() {
        if(Time.time < startTime) return;
        stopwatch.Start();
    }

    private void LateUpdate() {
        if(Time.time < startTime) return;
        stopwatch.Stop();
        num++;
        last = stopwatch.ElapsedTicks;
        total += last;
        stopwatch.Reset();
    }

    private IEnumerator Log() {
        WaitForSeconds delay = new WaitForSeconds(1f);
        while (true) {
            yield return delay;
            if(num > 0) UnityEngine.Debug.Log("Last time: " + (float)last / Stopwatch.Frequency * 1000f + "ms. Average time: " + (float)(total / num) / Stopwatch.Frequency * 1000f + "ms.");
        }
    }
}