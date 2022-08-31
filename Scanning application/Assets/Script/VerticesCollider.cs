using UnityEngine;
using System.Collections;


//This script needs to be attached to the mesh prefab used by the Mesh Manager

// It would be nice if we could get some optimization here


public class VerticesCollider : MonoBehaviour
{
    [Tooltip("The colour that appears if there is collision")]
    public Color Notice = new Color(1, 0, 0, 1);

    [Tooltip("The standard scanning colour")]
    public Color Standard = new Color(1,1,1,1);

    [Tooltip("The colour that the part of the mesh without collision takes during collision")]
    public Color NonColliding = new Color(0, 0, 0, 0);

    [Tooltip("This will be used to switch between the inefficient Collision Test and the inefficient Collision Test that only happens every WaitNFrames as a Coroutine")]
    public bool InvokeRepeatingTest = false;

    [Tooltip("This is the number of Frames the Program waits between Collision Checks")]
    public int WaitNFrames = 20;
    private static bool colliding = false;
    private static int NumberOfWaitFrames = 0;
    private static Color[] ColorStorage;

    void Start()
    {
        //Using the following function, we only Scan the Mesh every 0.1 seconds for collision, for Performance reasons
        //This may not be strictly necessary but it definitely can't hurt. - As it turns out, this causes flickering of the mesh color, where the mesh turn to it's base color.
        //It would be very good for performance if we could use this.
        //InvokeRepeating("ScanMeshForCollision", 0.5f, 0.1f);//While this may be a nice idea to improve performance, it causes the mesh to turn white and flashing. Unless this is fixed, use update()
    }

    void Update()
    {
        //The idea is, we choose between the inefficient Collision Test and the inefficient Collision Test that only happens every WaitNFrames as a coroutine
        //Should therefore hopefully keep things fluid
        if (InvokeRepeatingTest)
        {          
            if (NumberOfWaitFrames == WaitNFrames)                          //If we are in a TestFrame
            {
                NumberOfWaitFrames = 0;                                     //Reset the Test Frames
                StartCoroutine(ScanMeshForCollision());                     //Have the Collision Check as a Coroutine
            }
            else
            {
                NumberOfWaitFrames++;                                       //Increase the Number of Frames
                Mesh mesh = GetComponent<MeshFilter>().mesh;                //Get the mesh
                for (int i = 0; i < ColorStorage.Length; i++)               //Since I am not sure that the number of vertices stayed the same, only use as many colors as we have
                {
                    mesh.colors[i] = ColorStorage[i];                       //This could cause major Problems if it turns out the Mesh Generation changes the 
                }
            }
        }
        else
        {
            Mesh mesh = GetComponent<MeshFilter>().mesh;
            Vector3[] vertices = mesh.vertices;
            //Vector3[] normals = mesh.normals;
            Color[] colors = new Color[vertices.Length];

            for (var i = 0; i < vertices.Length; i++)
            {
                if (Physics.OverlapSphere(transform.position + vertices[i], 0f).Length != 0)
                {
                    //Debug.Log("colliding");
                    colors[i] = Notice;
                    colliding = true;

                }
                else if (colliding == true)
                {
                    //Debug.Log("No collision here");
                    colors[i] = NonColliding;//This makes the mesh invisble where there is no collision, if there is collision elsewhere
                }
                else
                {
                    //Debug.Log("Miss");
                    colors[i] = Standard;//This is if there is no collision anywhere
                }

                mesh.colors = colors;//is it maybe faster if we instantly write to mesh.colors instead of colors first?
            }


            //Debug.LogWarning("Collision check ended");
        }

    }

    IEnumerator ScanMeshForCollision()
    {
        
        Mesh mesh = GetComponent<MeshFilter>().mesh;
        Vector3[] vertices = mesh.vertices;
        //Vector3[] normals = mesh.normals;
        ColorStorage = new Color[vertices.Length];

        for (var i = 0; i < vertices.Length; i++)
        {
            if (Physics.OverlapSphere(transform.position + vertices[i], 0f).Length != 0)
            {
                //Debug.Log("colliding");
                ColorStorage[i] = Notice;
                colliding = true;

            }
            else if (colliding == true)
            {
                //Debug.Log("No collision here");
                ColorStorage[i] = NonColliding;//This makes the mesh invisble where there is no collision, if there is collision elsewhere
            }
            else
            {
                //Debug.Log("Miss");
                ColorStorage[i] = Standard;//This is if there is no collision anywhere
            }

            mesh.colors = ColorStorage;//is it maybe faster if we instantly write to mesh.colors instead of colors first?
        }
        
        yield return null;
    }
}
    //This is the Code Johannes wrote for us, it is probably more efficient/better than what we have but I don't understand it.
    //public bool isPointInVol(obj, pos) //checks if supplied position is inside supplied mesh object
    //{
        //var = obj.mesh;
        //var nVerts = tMesh.numverts;
        //bool isInVol = true;

       // for (v = 1 to nVerts while isInVol)
        //{
                //if (asin(dot(getNormal tMesh v)(normalize(((getVert tMesh v) * obj.transform) - pos))) <= 0.0)
            //{
            //isInVol = false;
            //tMesh = nVerts = vPos = undefined;
            //}
        //return isInVol;
    //}