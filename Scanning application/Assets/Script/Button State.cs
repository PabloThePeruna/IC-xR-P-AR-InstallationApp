using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.XR.ARFoundation;

[RequireComponent(typeof(ARRaycastManager))]
public class ButtonState: MonoBehaviour
{
    private static int NumberOfButtons = 8;
    public GameObject[] FunctionButtons = new GameObject[NumberOfButtons];
    //public Text[] FunctionButtonText=new Text[NumberOfButtons];
    public GameObject[] CloseButtons = new GameObject[NumberOfButtons];
    public GameObject ScreenshotTakenButton;
    //From here on come measuring components
    public GameObject measurementPointPrefab;
    public float measurementFactor = 100f;

    [SerializeField]
    private ARCameraManager arCameraManager;

    private LineRenderer[] measureLines=new LineRenderer[NumberOfButtons];

    private ARRaycastManager arRaycastManager;

    private GameObject[] startPoints = new GameObject[NumberOfButtons];
    private GameObject[] endPoints = new GameObject[NumberOfButtons];

    private Vector2 touchPosition = default;

    private static List<ARRaycastHit> hits = new List<ARRaycastHit>();

    private static int ActiveButton;



    void Start()
    {
        for (int i = 0; i < NumberOfButtons; i++)
        {
            CloseButtons[i].SetActive(false);
            FunctionButtons[i].SetActive(false);
        }
        ScreenshotTakenButton.SetActive(false);

        //From here is Stuff for the multiple Measurements
        arRaycastManager = GetComponent<ARRaycastManager>();


        for (int i = 0; i < NumberOfButtons; i++)
        {
            startPoints[i] = Instantiate(measurementPointPrefab, Vector3.zero, Quaternion.identity);
            endPoints[i] = Instantiate(measurementPointPrefab, Vector3.zero, Quaternion.identity);
            startPoints[i].SetActive(false);
            endPoints[i].SetActive(false);
            measureLines[i] = GetComponent<LineRenderer>();
        }
        ActiveButton = -1;
        //Debug.Log("Set ActiveButton to -1");
    }

    public void functionButton1()
    {
        //Perform Function of Button 1
        Debug.Log("Button 1 pressed");

        ActiveButton = 0;

        //Debug.Log("set ActiveButton to 0");
    }
    public void functionButton2()
    {
        //Perform Function of Button 2
        Debug.Log("Button 2 pressed");
        ActiveButton = 1;
        //Debug.Log("set ActiveButton to 1");
    }
    public void functionButton3()
    {
        //Perform Function of Button 3
        Debug.Log("Button 3 pressed");
        ActiveButton = 2;
        //Debug.Log("set ActiveButton to 2");
    }
    public void functionButton4()
    {
        //Perform Function of Button 4
        Debug.Log("Button 4 pressed");
        ActiveButton = 3;
        //Debug.Log("set ActiveButton to 3");
    }
    public void functionButton5()
    {
        //Perform Function of Button 5
        Debug.Log("Button 5 pressed");
        ActiveButton = 4;
        //Debug.Log("set ActiveButton to 4");
    }
    public void functionButton6()
    {
        //Perform Function of Button 6
        Debug.Log("Button 6 pressed");
        ActiveButton = 5;
        //Debug.Log("set ActiveButton to 5");
    }
    public void functionButton7()
    {
        //Perform Function of Button 7
        Debug.Log("Button 7 pressed");
        ActiveButton = 6;
        //Debug.Log("set ActiveButton to 6");
    }
    public void functionButton8()
    {
        //Perform Function of Button 8
        Debug.Log("Button 8 pressed");
        ActiveButton = 7;
        //Debug.Log("set ActiveButton to 7");
    }
    public void Screenshot()
    {
        //Debug.Log("Calling TakeScreenshotAndSave() Function");
        StartCoroutine(TakeScreenshotAndSave());
    }
    public void CopyText()
    {
        string text = "AppName Measurements:";
        for(int i=0; i < NumberOfButtons; i++)
        {
            text = text + "\n" + FunctionButtons[i].GetComponentInChildren<TMP_Text>().text;
        }
        UniClipboard.SetText(text);
        Debug.Log(text);
    }
    public void ChangeToButtonColor(GameObject GetColorHere, GameObject PutColorHere)
    {
        //Get the color from you buttons material component
        //Vector4 myButtonsColor = GetColorHere.image.color;
        /*Get the material component from your game object 
        and set its color to the new color defined above*/
        //PutColorHere.GetComponent<Renderer>().material.SetColor("_Color", myButtonsColor);

    }

    
    
    

    




    //This is the Screenshot Function, I don't undestand how it works, I got it from the Documentation page of NativeGallery: https://github.com/yasirkula/UnityNativeGallery
    private IEnumerator TakeScreenshotAndSave()
    {
        yield return new WaitForEndOfFrame();

        Texture2D ss = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        ss.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        ss.Apply();

        // Save the screenshot to Gallery/Photos
        NativeGallery.Permission permission = NativeGallery.SaveImageToGallery(ss, "GalleryTest", "Image.png", (success, path) => Debug.Log("Media save result: " + success + " " + path));
        ScreenshotTakenButton.SetActive(true);
        //Debug.Log("Permission result: " + permission);

        // To avoid memory leaks
        Destroy(ss);
    }


    
    void Update()
    {
        //Debug.Log("ActiveButton = "+ActiveButton);
        //Somehow we are stuck here
        if(ActiveButton >= 0)
        {
            //Debug.Log("A Button is active");

            //At this point it will be necessary to change the colors of startPoints[ActiveButton], endPoints[ActiveButton], measureLines[ActiveButton]
            //to the colors of the selected FunctionButtons[ActiveButton]

            //Drag and Drop?

            //Highlight the selected FunctionButtons[ActiveButton]

            if (Input.touchCount > 0)
            {
                //Debug.Log("TouchCount > 0");
                Touch touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Began)
                {
                    //Debug.Log("TouchPhase.Began");
                    touchPosition = touch.position;

                    if (arRaycastManager.Raycast(touchPosition, hits, UnityEngine.XR.ARSubsystems.TrackableType.PlaneWithinPolygon))
                    {
                        //Debug.Log("arRaycastManager hits");
                        startPoints[ActiveButton].SetActive(true);

                        Pose hitPose = hits[0].pose;
                        startPoints[ActiveButton].transform.SetPositionAndRotation(hitPose.position, hitPose.rotation);
                    }
                }

                if (touch.phase == TouchPhase.Moved)
                {
                    touchPosition = touch.position;

                    if (arRaycastManager.Raycast(touchPosition, hits, UnityEngine.XR.ARSubsystems.TrackableType.PlaneWithinPolygon))
                    {
                        measureLines[ActiveButton].gameObject.SetActive(true);
                        endPoints[ActiveButton].SetActive(true);

                        Pose hitPose = hits[0].pose;
                        endPoints[ActiveButton].transform.SetPositionAndRotation(hitPose.position, hitPose.rotation);
                    }
                }
            }

            if (startPoints[ActiveButton].activeSelf && endPoints[ActiveButton].activeSelf)
            {
                measureLines[ActiveButton].SetPosition(0, startPoints[ActiveButton].transform.position);
                measureLines[ActiveButton].SetPosition(1, endPoints[ActiveButton].transform.position);
                //The following line of Code should work
                FunctionButtons[ActiveButton].GetComponentInChildren<TMP_Text>().text = $"Distance: {(Vector3.Distance(startPoints[ActiveButton].transform.position, endPoints[ActiveButton].transform.position) * measurementFactor).ToString("F2")} cm";

            }
        }


    }
}
