using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class Octree
{
    public OctreeNode rootNode;
    public Bounds boundsD;

    public Octree(GameObject[] worldObjects, float minNodeSize)
    {
        Bounds bounds = new Bounds();
        bounds.Expand(20);
        boundsD = bounds;
        rootNode = new OctreeNode(bounds, minNodeSize);
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
