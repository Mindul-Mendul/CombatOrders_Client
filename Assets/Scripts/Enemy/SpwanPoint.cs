using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpwanPoint : MonoBehaviour
{
    new Transform transform;
    public GameObject enemyPrefab;
    void Awake()
    {
        transform = GetComponent<Transform>();
    }
}
