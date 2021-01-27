using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {
    [SerializeField] private int sizeX;
    [SerializeField] private int sizeY;
    [SerializeField, Range(7f, 15f)] private float detail;
    [SerializeField] private GameObject[] pieces;

    private void Start() {
        SpawnMap(sizeX, sizeY);
    }

    private void SpawnMap(int sizeX, int sizeY) {
        GameObject[,] pieceMatrix = new GameObject[sizeX, sizeY];
        float random = Random.Range(0, 100_000f);
        for (int i = 0; i < sizeX; i++) {
            for (int j = 0; j < sizeY; j++) {
                Instantiate(
                    GetGameObject(Mathf.PerlinNoise(random + i / detail, random + j / detail)), 
                    transform.position + new Vector3(i, 0.1f, j), 
                    Quaternion.identity, 
                    transform);
            }
        }
    }

    private GameObject GetGameObject(float perlinResult) {
        if (perlinResult > 0.66f) {
            return pieces[0];
        } else if (perlinResult > 0.33f) {
            return pieces[1];
        } else {
            return pieces[2];
        }
    }
}
