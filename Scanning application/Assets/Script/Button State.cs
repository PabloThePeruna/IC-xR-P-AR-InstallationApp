using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.XR.ARFoundation;

[RequireComponent(typeof(ARRaycastManager))]
public class ButtonState: MonoBehaviour
{
    public GameObject screenshotScreen;
    private static int NumberOfButtons = 8;
    public GameObject[] FunctionButtons = new GameObject[NumberOfButtons];
    //public Text[] FunctionButtonText=new Text[NumberOfButtons];
    public GameObject[] CloseButtons = new GameObject[NumberOfButtons];
    public GameObject ScreenshotTakenButton;
    //From here on come measuring components
    public GameObject measurementPointPrefab;
    public LineRenderer LineRendererPrefab;
    public float measurementFactor = 100f;

    [SerializeField]
    private ARCameraManager arCameraManager;

    private LineRenderer[] measureLines=new LineRenderer[NumberOfButtons];

    private ARRaycastManager arRaycastManager;

    private GameObject[] startPoints = new GameObject[NumberOfButtons];
    private GameObject[] endPoints = new GameObject[NumberOfButtons];

    private Vector2 touchPosition = default;
    private Vector2 touch2Position = default;

    private static List<ARRaycastHit> hits = new List<ARRaycastHit>();

    private static int ActiveButton;
    private LineRenderer[] LRStorage = new LineRenderer[NumberOfButtons];



    void Start()
    {
        for (int i = 0; i < NumberOfButtons; i++)
        {
            CloseButtons[i].SetActive(false);
            FunctionButtons[i].SetActive(false);
        }
        //ScreenshotTakenButton.SetActive(false);

        //From here is Stuff for the multiple Measurements
        arRaycastManager = GetComponent<ARRaycastManager>();

        for (int i = 0; i < NumberOfButtons; i++)
        {
            LRStorage[i] = Instantiate(LineRendererPrefab);
            LRStorage[i].gameObject.SetActive(false);
        }

        for (int i = 0; i < NumberOfButtons; i++)
        {
            startPoints[i] = Instantiate(measurementPointPrefab, Vector3.zero, Quaternion.identity);
            endPoints[i] = Instantiate(measurementPointPrefab, Vector3.zero, Quaternion.identity);
            startPoints[i].SetActive(false);
            endPoints[i].SetActive(false);
            //measureLines[i] = GetComponent<LineRenderer>();
            measureLines[i]=FunctionButtons[i].GetComponentInChildren<LineRenderer>();
            //Debug.Log(measureLines[i]);
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
        Vector4 myButtonColor = GetColorHere.GetComponent<Image>().color;
        /*Get the material component from your game object 
        and set its color to the new color defined above*/
        Debug.Log("Color of the Button: "+myButtonColor);
        PutColorHere.GetComponent<Renderer>().material.SetColor("_Color", myButtonColor);

    }
    public void ChangeLineRendererColor(GameObject GetColorHere, LineRenderer PutColorHere)
    {
        //Get the color from you buttons material component
        Vector4 myButtonColor = GetColorHere.GetComponent<Image>().color;
        /*Get the material component from your game object 
        and set its color to the new color defined above*/
        //Debug.Log("Color of the Button: " + myButtonColor);
        PutColorHere.SetColors(myButtonColor, myButtonColor);
        //PutColorHere.SetColor("_Color", myButtonColor);
    }
    public void TwoHandedTouchInput()
    {
        if (Input.touchCount == 2)
        {
            //Debug.Log("TouchCount > 0");
            Touch touch = Input.GetTouch(0);
            Touch touch2 = Input.GetTouch(1);

            //Debug.Log("TouchPhase.Began");
            touchPosition = touch.position;

            if (arRaycastManager.Raycast(touchPosition, hits, UnityEngine.XR.ARSubsystems.TrackableType.PlaneWithinPolygon))
            {
                //Debug.Log("arRaycastManager hits");
                startPoints[ActiveButton].SetActive(true);
                ChangeToButtonColor(FunctionButtons[ActiveButton], startPoints[ActiveButton]);


                Pose hitPose = hits[0].pose;
                startPoints[ActiveButton].transform.SetPositionAndRotation(hitPose.position, hitPose.rotation);
            }



            touch2Position = touch2.position;

            if (arRaycastManager.Raycast(touch2Position, hits, UnityEngine.XR.ARSubsystems.TrackableType.PlaneWithinPolygon))
            {
                //measureLines[ActiveButton].gameObject.SetActive(true);
                endPoints[ActiveButton].SetActive(true);
                ChangeToButtonColor(FunctionButtons[ActiveButton], endPoints[ActiveButton]);
                Pose hitPose = hits[0].pose;
                endPoints[ActiveButton].transform.SetPositionAndRotation(hitPose.position, hitPose.rotation);
            }
        }
    }
    public void SingleHandedTouchInput()
    {
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
                    ChangeToButtonColor(FunctionButtons[ActiveButton], startPoints[ActiveButton]);


                    Pose hitPose = hits[0].pose;
                    startPoints[ActiveButton].transform.SetPositionAndRotation(hitPose.position, hitPose.rotation);
                }
            }

            if (touch.phase == TouchPhase.Moved)
            {
                touchPosition = touch.position;

                if (arRaycastManager.Raycast(touchPosition, hits, UnityEngine.XR.ARSubsystems.TrackableType.PlaneWithinPolygon))
                {
                    //measureLines[ActiveButton].gameObject.SetActive(true);
                    endPoints[ActiveButton].SetActive(true);
                    ChangeToButtonColor(FunctionButtons[ActiveButton], endPoints[ActiveButton]);
                    Pose hitPose = hits[0].pose;
                    endPoints[ActiveButton].transform.SetPositionAndRotation(hitPose.position, hitPose.rotation);
                }
            }
        }
    }
    public void LimitedAreaTouchInput()
    {
        if (Input.touchCount > 0)
        {
            //Debug.Log("TouchCount > 0");
            Touch touch = Input.GetTouch(0);

            //Here we only allow the touch input through, if it is in the area of the screen we don't use for buttons
            if (touch.position.y <= 100 || touch.position.x < 600&&touch.position.y<600) { }
            else
            { 
                if (touch.phase == TouchPhase.Began)
                {
                    //Debug.Log("TouchPhase.Began");
                    touchPosition = touch.position;

                    if (arRaycastManager.Raycast(touchPosition, hits, UnityEngine.XR.ARSubsystems.TrackableType.PlaneWithinPolygon))
                    {
                        //Debug.Log("arRaycastManager hits");
                        startPoints[ActiveButton].SetActive(true);
                        ChangeToButtonColor(FunctionButtons[ActiveButton], startPoints[ActiveButton]);


                        Pose hitPose = hits[0].pose;
                        startPoints[ActiveButton].transform.SetPositionAndRotation(hitPose.position, hitPose.rotation);
                    }
                }

                if (touch.phase == TouchPhase.Moved)
                {
                    touchPosition = touch.position;

                    if (arRaycastManager.Raycast(touchPosition, hits, UnityEngine.XR.ARSubsystems.TrackableType.PlaneWithinPolygon))
                    {
                        //measureLines[ActiveButton].gameObject.SetActive(true);
                        LRStorage[ActiveButton].gameObject.SetActive(true);
                        endPoints[ActiveButton].SetActive(true);
                        Pose hitPose = hits[0].pose;
                        endPoints[ActiveButton].transform.SetPositionAndRotation(hitPose.position, hitPose.rotation);
                        ChangeToButtonColor(FunctionButtons[ActiveButton], endPoints[ActiveButton]);
                        ChangeLineRendererColor(FunctionButtons[ActiveButton], LRStorage[ActiveButton]);
                        LRStorage[ActiveButton].SetPosition(0, startPoints[ActiveButton].transform.position);
                        LRStorage[ActiveButton].SetPosition(1, endPoints[ActiveButton].transform.position);
                        FunctionButtons[ActiveButton].GetComponentInChildren<TMP_Text>().text = $"Distance {ActiveButton+1}: {(Vector3.Distance(startPoints[ActiveButton].transform.position, endPoints[ActiveButton].transform.position) * measurementFactor).ToString("F2")} cm";
                    }
                }
            }
        }
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
        //ScreenshotTakenButton.SetActive(true);
        //Debug.Log("Permission result: " + permission);
        screenshotScreen.SetActive(true);
        StartCoroutine(FadeOut());

        // To avoid memory leaks
        Destroy(ss);
    }
    IEnumerator FadeOut()
    {
        // loop over 1 second backwards
        for (float i = 1; i >= 0; i -= Time.deltaTime)
        {
            // set color with i as alpha
            screenshotScreen.GetComponent<Image>().color = new Color(1, 1, 1, i);
            yield return null;
        }

        screenshotScreen.SetActive(false);
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

            //This is the function to move the measurement points with two fingers at once, which causes problems if what you want to measure can't be seen on screen as a whole
            //TwoHandedTouchInput();

            //This is the single finger option, which causes problems with the buttons
            //SingleHandedTouchInput();

            //This is the single finger option with limited touch area
            LimitedAreaTouchInput();
            /*
            if (startPoints[0].activeSelf && endPoints[0].activeSelf)
            {
                //if (!measureLines[0].activeSelf) { measureLines[0].SetActive(true); }
                measureLines[0].SetPosition(0, startPoints[0].transform.position);
                measureLines[0].SetPosition(1, endPoints[0].transform.position);
                ChangeLineRendererColor(FunctionButtons[0], measureLines[0]);
                FunctionButtons[0].GetComponentInChildren<TMP_Text>().text = $"Distance 1: {(Vector3.Distance(startPoints[0].transform.position, endPoints[0].transform.position) * measurementFactor).ToString("F2")} cm";
            }
            if (startPoints[1].activeSelf && endPoints[1].activeSelf)
            {
                //if (!measureLines[1].activeSelf) { measureLines[1].SetActive(true); }
                measureLines[1].SetPosition(0, startPoints[1].transform.position);
                measureLines[1].SetPosition(1, endPoints[1].transform.position);
                ChangeLineRendererColor(FunctionButtons[1], measureLines[1]);
                FunctionButtons[1].GetComponentInChildren<TMP_Text>().text = $"Distance 2: {(Vector3.Distance(startPoints[1].transform.position, endPoints[1].transform.position) * measurementFactor).ToString("F2")} cm";
            }
            if (startPoints[2].activeSelf && endPoints[2].activeSelf)
            {
                //if (!measureLines[2].activeSelf) { measureLines[2].SetActive(true); }
                measureLines[2].SetPosition(0, startPoints[2].transform.position);
                measureLines[2].SetPosition(1, endPoints[2].transform.position);
                ChangeLineRendererColor(FunctionButtons[2], measureLines[2]);
                FunctionButtons[2].GetComponentInChildren<TMP_Text>().text = $"Distance 3: {(Vector3.Distance(startPoints[2].transform.position, endPoints[2].transform.position) * measurementFactor).ToString("F2")} cm";
            }
            if (startPoints[3].activeSelf && endPoints[3].activeSelf)
            {
                //if (!measureLines[3].activeSelf) { measureLines[3].SetActive(true); }
                measureLines[3].SetPosition(0, startPoints[3].transform.position);
                measureLines[3].SetPosition(1, endPoints[3].transform.position);
                ChangeLineRendererColor(FunctionButtons[3], measureLines[3]);
                FunctionButtons[3].GetComponentInChildren<TMP_Text>().text = $"Distance 4: {(Vector3.Distance(startPoints[3].transform.position, endPoints[3].transform.position) * measurementFactor).ToString("F2")} cm";
            }
            if (startPoints[4].activeSelf && endPoints[4].activeSelf)
            {
                //if (!measureLines[4].activeSelf) { measureLines[4].SetActive(true); }
                measureLines[4].SetPosition(0, startPoints[4].transform.position);
                measureLines[4].SetPosition(1, endPoints[4].transform.position);
                ChangeLineRendererColor(FunctionButtons[4], measureLines[4]);
                FunctionButtons[4].GetComponentInChildren<TMP_Text>().text = $"Distance 5: {(Vector3.Distance(startPoints[4].transform.position, endPoints[4].transform.position) * measurementFactor).ToString("F2")} cm";
            }
            if (startPoints[5].activeSelf && endPoints[5].activeSelf)
            {
                //if (!measureLines[5].activeSelf) { measureLines[5].SetActive(true); }
                measureLines[5].SetPosition(0, startPoints[5].transform.position);
                measureLines[5].SetPosition(1, endPoints[5].transform.position);
                ChangeLineRendererColor(FunctionButtons[5], measureLines[5]);
                FunctionButtons[5].GetComponentInChildren<TMP_Text>().text = $"Distance 6: {(Vector3.Distance(startPoints[5].transform.position, endPoints[5].transform.position) * measurementFactor).ToString("F2")} cm";
            }
            if (startPoints[6].activeSelf && endPoints[6].activeSelf)
            {
                //if (!measureLines[6].activeSelf) { measureLines[6].SetActive(true); }
                measureLines[6].SetPosition(0, startPoints[6].transform.position);
                measureLines[6].SetPosition(1, endPoints[6].transform.position);
                ChangeLineRendererColor(FunctionButtons[6], measureLines[6]);
                FunctionButtons[6].GetComponentInChildren<TMP_Text>().text = $"Distance 7: {(Vector3.Distance(startPoints[6].transform.position, endPoints[6].transform.position) * measurementFactor).ToString("F2")} cm";
            }
            if (startPoints[7].activeSelf && endPoints[7].activeSelf)
            {
                //if (!measureLines[7].activeSelf) { measureLines[7].SetActive(true); }
                measureLines[7].SetPosition(0, startPoints[7].transform.position);
                measureLines[7].SetPosition(1, endPoints[7].transform.position);
                ChangeLineRendererColor(FunctionButtons[7], measureLines[7]);
                FunctionButtons[7].GetComponentInChildren<TMP_Text>().text = $"Distance 8: {(Vector3.Distance(startPoints[7].transform.position, endPoints[7].transform.position) * measurementFactor).ToString("F2")} cm";
            }
            */
            /*if (startPoints[0].activeSelf && endPoints[0].activeSelf)
            {
                measureLines[0].SetPosition(0, startPoints[0].transform.position);
                measureLines[0].SetPosition(1, endPoints[0].transform.position);
                //The following line of Code should work
                FunctionButtons[0].GetComponentInChildren<TMP_Text>().text = $"Distance: {(Vector3.Distance(startPoints[0].transform.position, endPoints[0].transform.position) * measurementFactor).ToString("F2")} cm";

            }*/
        }


    }
}
