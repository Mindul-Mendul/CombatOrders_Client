using UnityEngine;
using System.Collections;

public class DamageText : MonoBehaviour
{
    public float moveSpeed;
    public int damage;

    // Use this for initialization
    void Awake()
    {
        Destroy(gameObject, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<TextMesh>().text = damage.ToString();
        transform.position += new Vector3(0, moveSpeed * Time.deltaTime, 0);
    }
}