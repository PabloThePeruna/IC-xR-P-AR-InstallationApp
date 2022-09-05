using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragObject2 : MonoBehaviour
{
    //IMPORTANT////////////////////////////////////////////////////////////////////////////////////////////////////////////

    //Objects you want to interact with need to have 'Boiler' as their tag

    //IMPORTANT////////////////////////////////////////////////////////////////////////////////////////////////////////////

    private float dist;
    private bool dragging = false;
    private bool rotating = false;
    [Tooltip("This allows you to activate (true) or deactivate (false) 3d movement for the boiler. It is recommended you leave this off")]
    public bool move3d = false;
    private Vector3 offset;
    private Transform toDrag;
    private Transform toRotate;


    public void SwitchMovementType()
    {
        if (move3d) {move3d = false;}
        else {move3d= true;}
    }

    // Update is called once per frame
    void Update()
    {

        Vector3 v3;

        if (Input.touchCount == 1)
        {
            Touch touch = Input.touches[0];
            Vector3 pos = touch.position;

            if (touch.phase == TouchPhase.Began)
            {
                //Debug.Log("TouchPhase.Began");
                Ray ray = Camera.main.ScreenPointToRay(pos);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    //Debug.Log("Raycast hit");
                    if (hit.collider.tag == "Boiler")
                    {                                               //Make sure to spell the tag correctly. Ask me how I know. Ask how long it took to find out  script didn't work because of this.
                        //Debug.Log("hit.collider.tag == Boiler");
                        toDrag = hit.transform;
                    }                                                //Get GameObject
                    dist = hit.transform.position.z - Camera.main.transform.position.z;         //Distance between GameObject and Camera in z
                    dist = Mathf.Abs(dist);                                                     //2.////////////
                    v3 = new Vector3(pos.x, pos.y, dist);
                    v3 = Camera.main.ScreenToWorldPoint(v3);                                    //Camera spawn point
                    offset = toDrag.position - v3;                                              //Vector between GameObject and Camera
                    dragging = true;                                                            //Start moving
                }
            }


            if (dragging && touch.phase == TouchPhase.Moved)
            {
                //Somewhere here causes movement to be inverted when you the camera is facing opposite it's spawn facing direction
                //1.Try if updating of dist helps, look for //1.//////////// in Code
                //2.Try if absolute of dist helps, look for //2.//////////// in Code

                if (move3d)
                {
                    dist = toDrag.position.z - Camera.main.transform.position.z;
                }
                                     //1.//////////// This allows movement in 3 axis, but that is problematic to say the least
                //v3 = new Vector3(Input.mousePosition.x, Input.mousePosition.y, dist);           //Deprecated
                v3 = new Vector3(pos.x, pos.y, dist);
                v3 = Camera.main.ScreenToWorldPoint(v3);
                toDrag.position = v3 + offset;                                                      //Move GameObject to new position
            }

            if (dragging && (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled))
            {
                dragging = false;
            }
        }


        if(Input.touchCount == 2){
            Touch touch = Input.touches[0];
            Vector3 pos = touch.position;

            if (touch.phase == TouchPhase.Began)
            {
                //Debug.Log("TouchPhase.Began");
                Ray ray = Camera.main.ScreenPointToRay(pos);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    //Debug.Log("Raycast hit");
                    if (hit.collider.tag == "Boiler")                                               //Make sure to spell the tag correctly.
                    {
                        //Debug.Log("hit.collider.tag == Boiler");
                        toRotate = hit.transform;
                        rotating = true;
                    }
                }
            }

            if (rotating && touch.phase == TouchPhase.Moved)
            {
                toRotate.Rotate(0f, -touch.deltaPosition.x/5, 0f);                                  //tune turning sensitivity here
            }

            if (rotating && (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled))
            {
                rotating = false;
            }
        }
    }
}