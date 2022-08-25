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
    private Vector3 offset;
    private Transform toDrag;
    private Transform toRotate;

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
                    if (hit.collider.tag == "Boiler")//I had a fcking space here, so " Boiler" and that broke everything...
                    {
                        //Debug.Log("hit.collider.tag == Boiler");
                        toDrag = hit.transform;
                        dist = hit.transform.position.z - Camera.main.transform.position.z;
                        v3 = new Vector3(pos.x, pos.y, dist);
                        v3 = Camera.main.ScreenToWorldPoint(v3);
                        offset = toDrag.position - v3;
                        dragging = true;
                    }
                }
            }

            if (dragging && touch.phase == TouchPhase.Moved)
            {
                v3 = new Vector3(Input.mousePosition.x, Input.mousePosition.y, dist);
                v3 = Camera.main.ScreenToWorldPoint(v3);
                toDrag.position = v3 + offset;
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
                    if (hit.collider.tag == "Boiler")//I had a fcking space here, so " Boiler" and that broke everything...
                    {
                        //Debug.Log("hit.collider.tag == Boiler");
                        toRotate = hit.transform;
                        rotating = true;
                    }
                }
            }

            if (rotating && touch.phase == TouchPhase.Moved)
            {
                toRotate.Rotate(0f, -touch.deltaPosition.x/5, 0f);//tune turning sensitivity here
            }

            if (rotating && (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled))
            {
                rotating = false;
            }
        }
    }
}