using UnityEngine;

public class Cloner : MonoBehaviour {
    [SerializeField] private int total;
    [HideInInspector] public Object cachedObject;
    [HideInInspector] public string componentName;

    private void Awake() {
        if(cachedObject == null) return;

        System.Type componentType = System.Type.GetType(componentName, false);

        if(componentType == null) {
            Debug.LogError("Couldn't find a component of type: " + componentName);

            return;
        }

        GameObject componentHolder = new GameObject(componentName + " parent");
        for (int i = 0; i < total; i++) {
            componentHolder.AddComponent(componentType);
        }
    }
}