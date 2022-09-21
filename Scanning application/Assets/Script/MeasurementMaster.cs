using System.Collections;   
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.XR.ARFoundation;

[RequireComponent(typeof(ARRaycastManager))]
public class MeasurementMaster: MonoBehaviour
{
    //These are UI variables
    private GameObject screenshotScreen;                                                                                                      //Empty gameObject to store the screenshot flash
    private static int NumberOfButtons = 8;                                                                                                   //Number of measurements to take, used throughout the script to make making more measurements simpler
    public GameObject[] FunctionButtons = new GameObject[NumberOfButtons];                                                                    //Measurement activation buttons

    //From here on come measuring components
    public GameObject measurementPointPrefab;                                                                                                 //Prefab used for start- and endpoints of a measurement line
    public LineRenderer LineRendererPrefab;                                                                                                   //Prefab used for the measurement line body
    private float measurementFactor = 100f;                                                                                                   //This is the factor a game length unit is multiplied with. If you want inch, for example, you would need 39.37                                                         
    private ARRaycastManager arRaycastManager;                                                                                                //Used to Raycast for the measurement points to the mesh planes
    private GameObject[] startPoints = new GameObject[NumberOfButtons];                                                                       //Array that stores the measurement line start points
    private GameObject[] endPoints = new GameObject[NumberOfButtons];                                                                         //Array that stores the measurement line end points
    private Vector2 touchPosition = default;                                                                                                  //Touch position of your finger on the screen
    private Vector2 touch2Position = default;                                                                                                 //Only neccessary for the TwoHandedTouchInput
    private static List<ARRaycastHit> hits = new List<ARRaycastHit>();                                                                        //List to store the hits the ARRaycast produces 
    private static int ActiveButton;                                                                                                          //Measurement we are currently working on (0 to NumberOfButtons)
    private LineRenderer[] LRStorage = new LineRenderer[NumberOfButtons];                                                                     //Array to store the lineRenderers for the measurement lines

    void Start()
    {     
        arRaycastManager = GetComponent<ARRaycastManager>();                                                                                  //Get the ARRaycastManager from this gameObject

        for (int i = 0; i < NumberOfButtons; i++)
        {
            //Instantiate all the components for the measurement visualisation and then instantly hide them
            LRStorage[i] = Instantiate(LineRendererPrefab);
            LRStorage[i].gameObject.SetActive(false);
            startPoints[i] = Instantiate(measurementPointPrefab, Vector3.zero, Quaternion.identity);
            startPoints[i].SetActive(false);
            endPoints[i] = Instantiate(measurementPointPrefab, Vector3.zero, Quaternion.identity);
            endPoints[i].SetActive(false);
        }

        ActiveButton = -1;                                                                                                                    //Start with the boiler movement activated
    }

    //Method to deactivate Measuring and thereby activate boiler movement
    public void stopMeasuring()
    {
        ActiveButton = -1;
    }

    //These methods are for the individual function buttons. Sure, one variable method for all would suffice, but this is easier
    public void functionButton1()
    {
        ActiveButton = 0;
    }
    public void functionButton2()
    {
        ActiveButton = 1;
    }
    public void functionButton3()
    {
        ActiveButton = 2;
    }
    public void functionButton4()
    {
        ActiveButton = 3;
    }
    public void functionButton5()
    {
        ActiveButton = 4;
    }
    public void functionButton6()
    {
        ActiveButton = 5;
    }
    public void functionButton7()
    {
        ActiveButton = 6;
    }
    public void functionButton8()
    {
        ActiveButton = 7;
    }

    //Method that triggers screenshot progress
    public void Screenshot()
    {
        StartCoroutine(TakeScreenshotAndSave());
    }

    //Method that copies measurement values to clipboard
    public void CopyText()
    {
        string text = "CoLiDAR Measurements: ";
        for(int i=0; i < NumberOfButtons; i++)
        {
            text = text + "\n" + FunctionButtons[i].GetComponentInChildren<TMP_Text>().text;
        }
        UniClipboard.SetText(text);
        Debug.Log(text);
    }

    //Methods to set the measurement line and points to the function button colors
    public void ChangeToButtonColor(GameObject GetColorHere, GameObject PutColorHere)
    {
        //Get the color from you buttons material component
        Vector4 myButtonColor = GetColorHere.GetComponent<Image>().color;
        //Get the material component from your game object and set its color to the new color defined above
        PutColorHere.GetComponent<Renderer>().material.SetColor("_Color", myButtonColor);

    }
    public void ChangeLineRendererColor(GameObject GetColorHere, LineRenderer PutColorHere)
    {
        //Get the color from you buttons material component
        Vector4 myButtonColor = GetColorHere.GetComponent<Image>().color;
        //Get the material component from your game object and set its color to the new color defined above
        PutColorHere.SetColors(myButtonColor, myButtonColor);
    }

    //This Method is currently unused and may be outdated
    //Theoretically, it allows the user to place the measurement points with two fingers, one for the startpoint and one for the endpoint
    public void TwoHandedTouchInput()
    {
        if (Input.touchCount == 2)
        {
            
            Touch touch = Input.GetTouch(0);
            Touch touch2 = Input.GetTouch(1);

            //first finger
            touchPosition = touch.position;

            if (arRaycastManager.Raycast(touchPosition, hits, UnityEngine.XR.ARSubsystems.TrackableType.PlaneWithinPolygon))
            {
                
                startPoints[ActiveButton].SetActive(true);
                ChangeToButtonColor(FunctionButtons[ActiveButton], startPoints[ActiveButton]);


                Pose hitPose = hits[0].pose;
                startPoints[ActiveButton].transform.SetPositionAndRotation(hitPose.position, hitPose.rotation);
            }


            //second finger
            touch2Position = touch2.position;

            if (arRaycastManager.Raycast(touch2Position, hits, UnityEngine.XR.ARSubsystems.TrackableType.PlaneWithinPolygon))
            {
               
                endPoints[ActiveButton].SetActive(true);
                ChangeToButtonColor(FunctionButtons[ActiveButton], endPoints[ActiveButton]);
                Pose hitPose = hits[0].pose;
                endPoints[ActiveButton].transform.SetPositionAndRotation(hitPose.position, hitPose.rotation);
            }
        }
    }

    //This Method is currently unused and may be outdated
    //Theoretically, it allows the user to place the measurement points with one finger: First touch places startpoint, dragging places endpoint
    //This caused problems with the buttons, if you press a button the measurement point will be placed there
    public void SingleHandedTouchInput()
    {
        if (Input.touchCount > 0)
        {
            
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                
                touchPosition = touch.position;

                if (arRaycastManager.Raycast(touchPosition, hits, UnityEngine.XR.ARSubsystems.TrackableType.PlaneWithinPolygon))
                {
                    
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
                    
                    endPoints[ActiveButton].SetActive(true);
                    ChangeToButtonColor(FunctionButtons[ActiveButton], endPoints[ActiveButton]);
                    Pose hitPose = hits[0].pose;
                    endPoints[ActiveButton].transform.SetPositionAndRotation(hitPose.position, hitPose.rotation);
                }
            }
        }
    }

    //This Method is currently unused and may be outdated
    //Theoretically, it allows the user to place the measurement points with one finger: First touch places startpoint, dragging places endpoint
    //This limits the area of touch input and thereby prevents problems with the buttons. On the other hand, if you have a different resolution, this might cause input problems
    public void LimitedAreaTouchInput()
    {
        if (Input.touchCount > 0)
        {
            
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

    //This is the current measurement method of choice, so it will be explained in detail

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
                //Placement of startpoint
                if (touch.phase == TouchPhase.Began)
                {
                    touchPosition = touch.position;

                    //Raycast to Boiler Variables
                    Ray ray = Camera.main.ScreenPointToRay(touchPosition);
                    RaycastHit hit;

                    //Raycast to Boiler needs to be first, because if you hit the boiler we dont care if there is a mesh behind it. 
                    if (Physics.Raycast(ray, out hit))
                    {
                        if (hit.collider.tag == "Boiler")                                                                                     //if a boiler was hit
                        {
                            startPoints[ActiveButton].SetActive(true);                                                                        //reactivate the startpoint                                                                 
                            ChangeToButtonColor(FunctionButtons[ActiveButton], startPoints[ActiveButton]);                                    //change it's color to the the current functionbuttons color
                            startPoints[ActiveButton].transform.position = hit.point;                                                         //move it to the hit position
                            startPoints[ActiveButton].transform.parent = hit.transform.gameObject.transform;                                  //And lock it in place so it can be moved with the boiler (the boiler has rigidbody)
                        }
                    }
                    else if (arRaycastManager.Raycast(touchPosition, hits, UnityEngine.XR.ARSubsystems.TrackableType.PlaneWithinPolygon))     //else if the room mesh was hit
                    {
                        startPoints[ActiveButton].SetActive(true);                                                                            //reactivate the startpoint
                        ChangeToButtonColor(FunctionButtons[ActiveButton], startPoints[ActiveButton]);                                        //change it's color to the the current functionbuttons color
                        Pose hitPose = hits[0].pose;                                                                                          //Get the first hit
                        startPoints[ActiveButton].transform.SetPositionAndRotation(hitPose.position, hitPose.rotation);                       //Set it to the hit position
                        startPoints[ActiveButton].transform.parent = null;                                                                    //deactivate the attachment to the boiler
                    }
                }

                //placement of endpoint
                if (touch.phase == TouchPhase.Moved)
                {
                    touchPosition = touch.position;
                    //Raycast to Boiler Variables
                    Ray ray = Camera.main.ScreenPointToRay(touchPosition);
                    RaycastHit hit;

                    //Raycast to Boiler comes first, see up
                    if (Physics.Raycast(ray, out hit))
                    {
                        if (hit.collider.tag == "Boiler")
                        {
                            //Same as with the startpoint
                            LRStorage[ActiveButton].gameObject.SetActive(true);
                            endPoints[ActiveButton].SetActive(true);
                            endPoints[ActiveButton].transform.position = hit.point;
                            ChangeToButtonColor(FunctionButtons[ActiveButton], endPoints[ActiveButton]);
                            ChangeLineRendererColor(FunctionButtons[ActiveButton], LRStorage[ActiveButton]);

                            //Move the lineRenderer in position and update the distance text on the UI
                            LRStorage[ActiveButton].SetPosition(0, startPoints[ActiveButton].transform.position);
                            LRStorage[ActiveButton].SetPosition(1, endPoints[ActiveButton].transform.position);
                            FunctionButtons[ActiveButton].GetComponentInChildren<TMP_Text>().text = $"Distance {ActiveButton + 1}: {(Vector3.Distance(startPoints[ActiveButton].transform.position, endPoints[ActiveButton].transform.position) * measurementFactor).ToString("F2")} cm";
                            endPoints[ActiveButton].transform.parent = hit.transform.gameObject.transform;
                        }

                    }

                    else if (arRaycastManager.Raycast(touchPosition, hits, UnityEngine.XR.ARSubsystems.TrackableType.PlaneWithinPolygon))
                    {
                        //Same as with the boiler
                        LRStorage[ActiveButton].gameObject.SetActive(true);
                        endPoints[ActiveButton].SetActive(true);
                        Pose hitPose = hits[0].pose;
                        endPoints[ActiveButton].transform.SetPositionAndRotation(hitPose.position, hitPose.rotation);
                        ChangeToButtonColor(FunctionButtons[ActiveButton], endPoints[ActiveButton]);
                        ChangeLineRendererColor(FunctionButtons[ActiveButton], LRStorage[ActiveButton]);
                        LRStorage[ActiveButton].SetPosition(0, startPoints[ActiveButton].transform.position);
                        LRStorage[ActiveButton].SetPosition(1, endPoints[ActiveButton].transform.position);
                        FunctionButtons[ActiveButton].GetComponentInChildren<TMP_Text>().text = $"Distance {ActiveButton + 1}: {(Vector3.Distance(startPoints[ActiveButton].transform.position, endPoints[ActiveButton].transform.position) * measurementFactor).ToString("F2")} cm";
                        endPoints[ActiveButton].transform.parent = null;
                    }
                }
            }
        }
    }

    //Updates the distances on the UI and the lineRenderers when the measurement points are moved by the boiler
    public void UpdateDistances()
    {
        for (int i = 0; i < NumberOfButtons; i++)
        {
            //If the current startpoint has been activated, that means the measurement exists
            if (startPoints[i].activeSelf)
            {
                FunctionButtons[i].GetComponentInChildren<TMP_Text>().text = $"Distance {i + 1}: {(Vector3.Distance(startPoints[i].transform.position, endPoints[i].transform.position) * measurementFactor).ToString("F2")} cm";
                LRStorage[i].SetPosition(0, startPoints[i].transform.position);
                LRStorage[i].SetPosition(1, endPoints[i].transform.position);
            } 
        }
        
    }


    //This is the Screenshot Function
    private IEnumerator TakeScreenshotAndSave()
    {
        yield return new WaitForEndOfFrame();
        Texture2D ss = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        ss.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        ss.Apply();

        // Save the screenshot to Gallery/Photos
        NativeGallery.Permission permission = NativeGallery.SaveImageToGallery(ss, "GalleryTest", "Image.png", (success, path) => Debug.Log("Media save result: " + success + " " + path));
        screenshotScreen.SetActive(true);
        StartCoroutine(FadeOut());

        // To avoid memory leaks
        Destroy(ss);
    }
    //This causes the camera flash
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
            if (ActiveButton >= 0)                                      //This is so we can deactivate the measuring
            {
            //This is the function to move the measurement points with two fingers at once, which causes problems if what you want to measure can't be seen on screen as a whole
            //TwoHandedTouchInput();

            //This is the single finger option, which causes problems with the buttons
            //SingleHandedTouchInput();

            //This is the single finger option with limited touch area
            //LimitedAreaTouchInput();

            //This is the single finger option with limited touch area which can also measure the Boiler
            LimitedAreaTouchInputAndBoiler();
            }
            else
            {
                UpdateDistances();                                      //This function will update the shown UI distances while we move the boiler
            }
    }
}
