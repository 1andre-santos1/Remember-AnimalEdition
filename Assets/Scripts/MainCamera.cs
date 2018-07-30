using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    public float minX = -7f;
    public float maxX = 25f;
    public float speed = 5f;

    private void Update()
    {
        transform.position += new Vector3(speed * Time.deltaTime, 0f,0f);
        if (transform.position.x > maxX || transform.position.x < minX)
            speed = -speed;
    }

}
