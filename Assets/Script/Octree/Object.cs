using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Object : MonoBehaviour
{
    public float speed = 2;
    float speedX, speedY, speedZ;
    // Start is called before the first frame update
    void Start()
    {
        speedX = speedY = speedZ = speed;
    }

    // Update is called once per frame
    void Update()
    {
        CreateOctree o = FindObjectOfType<CreateOctree>();


        if (transform.position.x >= o.maxX || transform.position.x <= o.minX)
        {
            speedX = -speedX;
        }
        else if (transform.position.y >= o.maxY || transform.position.y <= o.minY)
        {
            speedY = -speedY;
        }
        else if (transform.position.z >= o.maxZ || transform.position.z <= o.minZ)
        {
            speedZ = -speedZ;
        }


        transform.position += new Vector3(1 * speedX, 1 * speedY, 1 * speedZ) * Time.deltaTime;
    }
}
