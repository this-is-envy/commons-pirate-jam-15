using UnityEngine;
using UnityEngine.SceneManagement;

public static class SystemExtensions {
    public static T GetRootComponent<T>(this Scene self)
        where T : Component {
        foreach (var go in self.GetRootGameObjects()) {
            var t = go.GetComponent<T>();
            if (t != null) {
                return t;
            }
        }
        return null;
    }
}