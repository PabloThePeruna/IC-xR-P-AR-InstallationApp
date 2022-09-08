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

    [HideInInspector]
    public static int NumberOfButtons = 8;

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

    [HideInInspector]
    public LineRenderer[] measureLines=new LineRenderer[NumberOfButtons];

    private ARRaycastManager arRaycastManager;

    [HideInInspector]
    public GameObject[] startPoints = new GameObject[NumberOfButtons];
    public GameObject[] endPoints = new GameObject[NumberOfButtons];

    [HideInInspector]
    public bool[] startPointsBoilerHit = new bool[NumberOfButtons];
    public bool[] endPointsBoilerHit = new bool[NumberOfButtons];

    private Vector2 touchPosition = default;
    private Vector2 touch2Position = default;

    private static List<ARRaycastHit> hits = new List<ARRaycastHit>();

    private static int ActiveButton;

    [HideInInspector]
    public LineRenderer[] LRStorage = new LineRenderer[NumberOfButtons];

    

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

    public void stopMeasuring()
    {
        ActiveButton = -1;
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

    public void LimitedAreaTouchInputAndBoiler()
    {
        
        if (Input.touchCount > 0)
        {
            //Debug.Log("TouchCount > 0");
            Touch touch = Input.GetTouch(0);

            //Here we only allow the touch input through, if it is in the area of the screen we don't use for buttons
            if (touch.position.y <= 100 || touch.position.x < 600 && touch.position.y < 600) { }
            else
            {
                if (touch.phase == TouchPhase.Began)
                {
                    //Debug.Log("TouchPhase.Began");
                    touchPosition = touch.position;

                    //Raycast to Boiler Variables
                    Ray ray = Camera.main.ScreenPointToRay(touchPosition);
                    RaycastHit hit;

                    //Raycast to Boiler needs to be first, because if you hit the boiler we dont care if there is a mesh behind it. 
                    if (Physics.Raycast(ray, out hit))
                    {
                        if (hit.collider.tag == "Boiler")
                        {
                            //Debug.Log("Hit the Boiler");
                            startPoints[ActiveButton].SetActive(true);
                            ChangeToButtonColor(FunctionButtons[ActiveButton], startPoints[ActiveButton]);
                            startPoints[ActiveButton].transform.position = hit.point;
                            startPointsBoilerHit[ActiveButton] = true;
                        }

                    }
                    else if (arRaycastManager.Raycast(touchPosition, hits, UnityEngine.XR.ARSubsystems.TrackableType.PlaneWithinPolygon))
                    {
                        //Debug.Log("arRaycastManager hits");
                        startPoints[ActiveButton].SetActive(true);
                        ChangeToButtonColor(FunctionButtons[ActiveButton], startPoints[ActiveButton]);
                        Pose hitPose = hits[0].pose;
                        startPoints[ActiveButton].transform.SetPositionAndRotation(hitPose.position, hitPose.rotation);
                        startPointsBoilerHit[ActiveButton] = false;
                    }

                    
                }

                if (touch.phase == TouchPhase.Moved)
                {
                    touchPosition = touch.position;
                    //Raycast to Boiler Variables
                    Ray ray = Camera.main.ScreenPointToRay(touchPosition);
                    RaycastHit hit;

                    

                    //Raycast to comes first, see up
                    if (Physics.Raycast(ray, out hit))
                    {
                        if (hit.collider.tag == "Boiler")
                        {
                            //Debug.Log("Hit the Boiler 2");
                            LRStorage[ActiveButton].gameObject.SetActive(true);
                            endPoints[ActiveButton].SetActive(true);
                            endPoints[ActiveButton].transform.position = hit.point;
                            ChangeToButtonColor(FunctionButtons[ActiveButton], endPoints[ActiveButton]);
                            ChangeLineRendererColor(FunctionButtons[ActiveButton], LRStorage[ActiveButton]);
                            LRStorage[ActiveButton].SetPosition(0, startPoints[ActiveButton].transform.position);
                            LRStorage[ActiveButton].SetPosition(1, endPoints[ActiveButton].transform.position);
                            FunctionButtons[ActiveButton].GetComponentInChildren<TMP_Text>().text = $"Distance {ActiveButton + 1}: {(Vector3.Distance(startPoints[ActiveButton].transform.position, endPoints[ActiveButton].transform.position) * measurementFactor).ToString("F2")} cm";
                            endPointsBoilerHit[ActiveButton] = true;
                        }

                    }
                    else if (arRaycastManager.Raycast(touchPosition, hits, UnityEngine.XR.ARSubsystems.TrackableType.PlaneWithinPolygon))
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
                        FunctionButtons[ActiveButton].GetComponentInChildren<TMP_Text>().text = $"Distance {ActiveButton + 1}: {(Vector3.Distance(startPoints[ActiveButton].transform.position, endPoints[ActiveButton].transform.position) * measurementFactor).ToString("F2")} cm";
                        endPointsBoilerHit[ActiveButton] = false;
                    }
                }
            }
        }
    }

    private void UpdateDistances()
    {
        for(int i = 0; i < NumberOfButtons; i++)
        {
            if (startPoints[i].activeSelf)                                                                                                                                                      //GameObject.active is obsolete, hope this works the same
            {
                FunctionButtons[i].GetComponentInChildren<TMP_Text>().text = $"Distance {i + 1}: {(Vector3.Distance(startPoints[i].transform.position, endPoints[i].transform.position) * measurementFactor).ToString("F2")} cm";
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
        if(ActiveButton >= 0)
        {
            //Debug.Log("A Button is active");


            //This is the function to move the measurement points with two fingers at once, which causes problems if what you want to measure can't be seen on screen as a whole
            //TwoHandedTouchInput();

            //This is the single finger option, which causes problems with the buttons
            //SingleHandedTouchInput();

            //This is the single finger option with limited touch area
            //LimitedAreaTouchInput();

            //This is the single finger option with limited touch area whoch can also measure the Boiler
            if (ActiveButton >= 0)                                      //This is so we can deactivate the measuring
            {
                LimitedAreaTouchInputAndBoiler();
            }
            else
            {
                UpdateDistances();                                      //This function will update the shown UI distances while we move the boiler
            }


        }


    }
}
