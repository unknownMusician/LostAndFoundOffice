using System.Collections;
using UnityEngine;


public class CameraMovementManager : MonoBehaviour
{
    public void MoveToComputer()
    {
        StartCoroutine(MoveToComputerCoroutine());
    }

    private float Easing(float x)
    {
        return x < 0.5 ? 4 * x * x * x : 1 - Mathf.Pow(-2 * x + 2, 3) / 2;
    }
    private IEnumerator MoveToComputerCoroutine()
    {
        float lerp = 0;

        while (lerp <= 1)
        {
            transform.position = Vector3.Lerp(new Vector3(-3.12f, 13.36f, -0.82f), new Vector3(0, 4.39f, 10.808f), Easing(lerp));
            transform.rotation = Quaternion.Lerp(Quaternion.Euler(51.15f, 0, 0), Quaternion.Euler(31.593f, 0, 0), Easing(lerp));
            lerp += 0.01f;
            yield return null;
        }
    }
}
