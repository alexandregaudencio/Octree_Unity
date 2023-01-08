using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

[Serializable]
public class Octree
{
    public OctreeNode rootNode;
    public Octree(ObjectController[] worldObjects, float minNodeSize, Bounds worldBound)
    {
        rootNode = new OctreeNode(worldBound, minNodeSize);
        
        foreach (ObjectController go in worldObjects)
        {
            rootNode.Subdivide(go);
            rootNode.CheckCollisions();
        }

    }

}
