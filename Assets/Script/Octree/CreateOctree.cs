using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateOctree : MonoBehaviour
{
    public GameObject[] worldObjects;
    public int nodeMinSize = 5;
    Octree ot;
    public float minX, maxX, minY, maxY, minZ, maxZ;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ot = new Octree(worldObjects, nodeMinSize);
        minY = ot.boundsD.min.y;
        maxY = ot.boundsD.max.y;
        minX = ot.boundsD.min.x;
        maxX = ot.boundsD.max.x;
        minZ = ot.boundsD.min.z;
        maxZ = ot.boundsD.max.z;
    }

    private void OnDrawGizmos()
    {
        if(Application.isPlaying)
        {
            Gizmos.color = new Color(0, 1, 0);
            ot.rootNode.Draw();
        }
    }
}
