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
    
    private List<Vector3> BoundaryTest;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
            
        Debug.Log(MyARPlane.ToString());//this method generated a String describing the plane properties for debugging puposes, hopefully this will help find a solution 
        Vector3 PlaneNormal = MyARPlane.normal;//This can probably be placed in the recalculation of the vector2
        Vector3 PlaneOrigin = MyARPlane.transform.position;
        Vector3 PlaneCenterWorldSpace = MyARPlane.center;//Maybe this and tranform.position are the same?
        Vector2 PlaneCenterPlaneSpace = MyARPlane.centerInPlaneSpace;
        BoundaryPointsInPlaneSpace = MyARPlane.boundary.ToArray();
        //bool gotPlaneBoundary =MyARPlane.TryGetBoundary(BoundaryTest);//Sadly,it seems this method was deprecated and so we have to be content with the vector2[]
        

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

    private Vector3 BoundaryPointsToWorldSpace(Vector2 BoundaryPoint)
    {
        
            GameObject temp = new GameObject();
            temp.transform.position = BoundaryPoint;
        return temp.transform.position;
        
    }
}
