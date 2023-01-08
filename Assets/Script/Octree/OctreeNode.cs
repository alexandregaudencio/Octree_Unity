using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[Serializable]
//public struct OctreeObject
//{
//    public Bounds bounds;
//    public GameObject gameObject;

//    public OctreeObject(GameObject go)
//    {
//        bounds = go.GetComponent<Collider>().bounds;
//        gameObject = go;
//    }
//}

[Serializable]
public class OctreeNode 
{
    Bounds nodeBounds;
    Bounds[] childBounds;
    public OctreeNode[] octreeNodeChild;
    float minSize;
    public List<ObjectController> objectControllers;

    public OctreeNode(Bounds octreeBounbs, float minNodeSize)
    {
        objectControllers =   new List<ObjectController>();   
        nodeBounds = octreeBounbs;
        minSize = minNodeSize;

        float quarter = nodeBounds.size.y / 4f;

        Vector3 childSize = Vector3.one* nodeBounds.size.y / 2f;
        childBounds = new Bounds[8];
        childBounds[0] = new Bounds(nodeBounds.center + new Vector3(-quarter, quarter, -quarter), childSize);
        childBounds[1] = new Bounds(nodeBounds.center + new Vector3(-quarter, quarter, quarter), childSize);
        childBounds[2] = new Bounds(nodeBounds.center + new Vector3(quarter, quarter, -quarter), childSize);
        childBounds[3] = new Bounds(nodeBounds.center + new Vector3(quarter, quarter, quarter), childSize);
        childBounds[4] = new Bounds(nodeBounds.center + new Vector3(-quarter, -quarter, -quarter), childSize);
        childBounds[5] = new Bounds(nodeBounds.center + new Vector3(-quarter, -quarter, quarter), childSize);
        childBounds[6] = new Bounds(nodeBounds.center + new Vector3(quarter, -quarter, -quarter), childSize);
        childBounds[7] = new Bounds(nodeBounds.center + new Vector3(quarter, -quarter, quarter), childSize);
    }

    public void Subdivide(ObjectController go)
    {
        //verify  stop condition is done
        if (nodeBounds.size.y <= minSize)
        {
            objectControllers.Add(go);
            return;
        }
        if(octreeNodeChild == null) octreeNodeChild = new OctreeNode[8];
        bool dividingOctreeNode = false;
        
        for(int i = 0; i < 8; i++)
        {
            if (octreeNodeChild[i] == null)
                octreeNodeChild[i] = new OctreeNode(childBounds[i], minSize);
            
            if (childBounds[i].Intersects(go.bound))
            {
                dividingOctreeNode = true;
                //sending ObjectController "go" to child when it is inside childBounds[i]
                octreeNodeChild[i].Subdivide(go);
            }
        }
        if(dividingOctreeNode == false)
        {
            //whenn not inside childs
            objectControllers.Add(go);
            octreeNodeChild = null;
        }
        
    }

    //check all objects collisions at finished octreeNode subidivision
    public void CheckCollisions() {
        foreach (ObjectController objController in objectControllers)
        {
            foreach (ObjectController otherObjController in objectControllers)
            {
                if (otherObjController != objController) { 
                    if (objController.bound.Intersects(otherObjController.bound))
                    {
                        Debug.Log(objController.bound.center +" contacts "+ otherObjController.bound.center);
                        //objController.Collide(otherObjController.bound);
                    }
                }
            }
        }

    }


    public void DrawBoundingBox()
    {
        Gizmos.color = new Color(0, 1, 0);
        Gizmos.DrawWireCube(nodeBounds.center, nodeBounds.size); 
        if(octreeNodeChild != null)
        {
            for(int i = 0; i < 8; i++)
            {
                if (octreeNodeChild[i] != null)
                    octreeNodeChild[i].DrawBoundingBox();
            }
        }
    }
}
