using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{

    public float speed = 2.0f;
    
    // Update is called once per frame
    void Update()
    {
        transform.position -= transform.right * speed * Time.deltaTime;
    }
}
