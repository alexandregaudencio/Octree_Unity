using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject objectPrefab;
    public int objCount;
    [Range(1,10)] public int nodeMinSize = 5;
    public float worldSize = 10;
    public Bounds worldBounds;
    public GameObject[] worldObjects;
    public Octree octree;

    public static GameManager instance;


    // Start is called before the first frame update
    void Start()
    {
        instance= this;
        worldBounds = new Bounds() { center = transform.position, extents = Vector3.one * worldSize };

        SpawnObjects();
    }

    private void SpawnObjects()
    {
        worldObjects = new GameObject[objCount];
        for (int i = 0; i < objCount; i++)
        {
            GameObject obj = Instantiate(objectPrefab);
            worldObjects[i] = obj;
        }
    }

    // Update is called once per frame
    void Update()
    {
        worldBounds.extents = Vector3.one*worldSize;
        octree = new Octree(worldObjects, nodeMinSize, worldBounds);

    }

    private void OnDrawGizmos()
    {
        if(Application.isPlaying)
        {
            Gizmos.color = new Color(0, 1, 0);
            octree.rootNode.Draw();
        }
    }
}
