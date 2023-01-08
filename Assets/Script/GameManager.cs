using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject objectPrefab;
    public int objCount;
    [Range(1,5)] public int nodeMinSize = 5;
    public float worldSize = 10;
    public Bounds worldBounds;
    public ObjectController[] worldObjects;
    public Octree octree;

    public static GameManager instance;


    void Start()
    {
        instance= this;
        worldBounds = new Bounds() { center = transform.position, extents = Vector3.one * worldSize };
        
        SpawnObjects();
    }

    private void SpawnObjects()
    {
        worldObjects = new ObjectController[objCount];
        for (int i = 0; i < objCount; i++)
        {
            ObjectController obj = Instantiate(objectPrefab).GetComponent<ObjectController>();
            worldObjects[i] = obj;
        }
    }

    void Update()
    {
        //world resizer
        worldBounds.extents = Vector3.one*worldSize;
        //octree update each timestep
        octree = new Octree(worldObjects, nodeMinSize, worldBounds);

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(0, 1, 0);
        //Draw all octreeNode bounding Bom Volume
        octree.rootNode.DrawBoundingBox();
        
    }
}
