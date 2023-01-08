using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

[Serializable]
public class Octree
{
    public OctreeNode rootNode;

    public Octree(GameObject[] worldObjects, float minNodeSize, Bounds worldBound)
    {
        rootNode = new OctreeNode(worldBound, minNodeSize);
        AddObjects(worldObjects);
    }

    public void AddObjects(GameObject[] worldObjects)
    {
        foreach(GameObject go in worldObjects)
        {
            rootNode.AddObject(go);
        }
    }
}
