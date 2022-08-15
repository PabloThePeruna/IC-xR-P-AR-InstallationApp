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
    private Vector2 [] BoundaryPointsInPlaneSpace = new Vector2 [100];//This here is only temporary until I can get the boundary points, to test for Errors in Unity
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 PlaneNormal = MyARPlane.normal;//This can probably be placed in the recalculation of the vector2
        Vector3 PlaneOrigin = MyARPlane.transform.position;
        
        BoundaryPointsInPlaneSpace = MyARPlane.boundary;
            for (int i = 0; i < BoundaryPointsInPlaneSpace.Length; i++)
            {
                if (Vector3.Distance(transform.position, BoundaryPointsInPlaneSpace[i]) < 0.01f)
                {
                    transform.position = BoundaryPointsInPlaneSpace[i];
                }
            }
        
    }
}
