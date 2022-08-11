using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.XR.ARFoundation;

public class ScanningMasterScript : MonoBehaviour
{
    private static int NumberOfBoilers=6;
    public GameObject screenshotScreen;
    [Tooltip("Warning: Put the Boilers in this list in the correct order, in accordance to the order of the library canvas")]
    public GameObject[] BoilerList = new GameObject[NumberOfBoilers];

    private GameObject CurrentBoiler;
    private static int CurrentBoilerNumber;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnBoiler(int BoilerNumber)
    {
        
        //Instead of Vector3.zero, we should probably spawn the Boiler at the center of the FOV
        CurrentBoiler = Instantiate(BoilerList[BoilerNumber], Vector3.zero, Quaternion.identity);
        CurrentBoilerNumber = BoilerNumber;
    }

    public void RemoveBoiler()
    {
        //Maybe this causes Problems because it destroys the CurrentBoiler GameObject, maybe we cannnot fill this again later
        Destroy(CurrentBoiler);
    }

    public void Screenshot()
    {
        //Debug.Log("Calling TakeScreenshotAndSave() Function");
        StartCoroutine(TakeScreenshotAndSave());
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
}
