using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    Transform playerTransform;
    // Start is called before the first frame update
    void Awake()
    {
        playerTransform = transform.parent;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(playerTransform.position.x, playerTransform.position.y+2.3f, transform.position.z);
    }
}
