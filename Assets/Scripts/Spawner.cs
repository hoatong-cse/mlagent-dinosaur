using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    public GameObject[] objects;

    public Transform obstacleRoot;

    public float acceleration;
    public void Restart()
    {
        acceleration = 0f;
        foreach (Transform child in obstacleRoot.transform) {
            Destroy(child.gameObject);
        }
        
        CancelInvoke("SpawnObject");
        InvokeRepeating( "SpawnObject" , 0f, 2f );

    }

   
    public void SpawnObject()
    {
        acceleration += 0.01f;
            
        var i = Random.Range(0, objects.Length);
        
        Vector3 pos = transform.position;

        float posY = pos.y + objects[i].transform.position.y;

        if (objects[i].tag.Equals("Bird"))
        {
            //-0.1 or 0.16
            if (Random.Range(0, 2) == 1)
            {
                posY = pos.y - 0.1f;
            }

            else
            {
                posY = pos.y + 0.16f;
            }

        }
        Vector3 newPos = new Vector3(pos.x, posY , pos.z);
        var go = Instantiate(objects[i], newPos, Quaternion.identity, transform);
        go.transform.SetParent(obstacleRoot);

        go.GetComponent<Obstacle>().speed += acceleration;
    }
}
