using UnityEngine;
using System.Collections;

public class VerticesCollider : MonoBehaviour
{
    public Color Notice = new Color(1, 0, 0, 1);
    public Color Standard = new Color(1,1,1,1);

    void Update()
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
    
            }
            else
            {
                //Debug.Log("Miss");
                colors[i] = Standard;
            }

            mesh.colors = colors;
        }


        //Debug.LogWarning("Collision check ended");
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
        //return isInVol;
    //}