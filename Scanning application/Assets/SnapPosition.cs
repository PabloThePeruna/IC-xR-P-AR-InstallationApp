using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;


//This script probably needs to be attached to the measure point prefab
public class SnapPosition : MonoBehaviour
{
    //public BoundedPlane MyBoundedPlane;
    public ARPlane MyARPlane;
    //private SphereCollider CheckArea = new SphereColider;
    private Vector2 [] BoundaryPointsInPlaneSpace;//This will store the Boundary Points, it's 2d because its place space
    private Vector3 [] BoundaryPointsInWorldSpace;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 PlaneNormal = MyARPlane.normal;//This can probably be placed in the recalculation of the vector2
        Vector3 PlaneOrigin = MyARPlane.transform.position;
        Vector3 PlaneCenterWorldSpace = MyARPlane.center;
        Vector2 PlaneCenterPlaneSpace = MyARPlane.centerInPlaneSpace;
        BoundaryPointsInPlaneSpace = MyARPlane.boundary.ToArray();

        //Somehow I need to tranform the BoundaryPoints from 2d plane space to 3d world space
        //This is really a problem, because I would need the rotational orientation of the plane space to the world space for the maths
            for (int i = 0; i < BoundaryPointsInWorldSpace.Length; i++)
            {
                if (Vector3.Distance(transform.position, BoundaryPointsInWorldSpace[i]) < 0.01f)
                {
                    transform.position = BoundaryPointsInPlaneSpace[i];
                }
            }
        
    }
}
