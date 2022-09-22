using JoostenProductions;
using UnityEngine;

public class ExampleEventUpdate : MonoBehaviour {
    private int i;

    private void OnEnable() {
        UpdateManager.SubscribeToUpdate(EventUpdate);
    }

    private void OnDisable() {
        UpdateManager.UnsubscribeFromUpdate(EventUpdate);
    }

    private void EventUpdate() {
        i++;
    }
}