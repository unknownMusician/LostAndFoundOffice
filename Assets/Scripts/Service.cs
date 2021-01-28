using UnityEngine;

public static class Service {
    public static Vector2 SnapToGrid(Vector2 v) {
        return new Vector2(Mathf.Round(v.x + 0.5f) - 0.5f, Mathf.Round(v.y + 0.5f) - 0.5f);
    }

    public static Vector3 SnapToGrid(Vector3 v, bool keepY) {
        return new Vector3(Mathf.Round(v.x + 0.5f) - 0.5f, keepY ? v.y : 0, Mathf.Round(v.z + 0.5f) - 0.5f);
    }

    public static Vector3 SnapToGrid(Vector3 v, float newY) {
        return new Vector3(Mathf.Round(v.x + 0.5f) - 0.5f, newY, Mathf.Round(v.z + 0.5f) - 0.5f);
    }

    public static Vector2 Project(Vector3 v) => new Vector2(v.x, v.z);
    public static Vector3 ReProject(Vector2 v) => new Vector3(v.x, 0, v.y);
}
