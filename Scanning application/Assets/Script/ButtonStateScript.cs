using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ButtonStateScript : MonoBehaviour
{
    public static int NumberOfButtons = 8;
    public GameObject[] FunctionButtons = new GameObject[NumberOfButtons];
    //public Text[] FunctionButtonText=new Text[NumberOfButtons];
    public GameObject[] CloseButtons = new GameObject[NumberOfButtons];
    public GameObject ScreenshotTakenButton;
    


    void Start()
    {
        for (int i = 0; i < NumberOfButtons; i++)
        {
            CloseButtons[i].SetActive(false);
            FunctionButtons[i].SetActive(false);
        }
        ScreenshotTakenButton.SetActive(false);
    }

    public void functionButton1()
    {
        //Perform Function of Button 1
        Debug.Log("Button 1 pressed");
    }
    public void functionButton2()
    {
        //Perform Function of Button 2
        Debug.Log("Button 2 pressed");
    }
    public void functionButton3()
    {
        //Perform Function of Button 3
        Debug.Log("Button 3 pressed");
    }
    public void functionButton4()
    {
        //Perform Function of Button 4
        Debug.Log("Button 4 pressed");
    }
    public void functionButton5()
    {
        //Perform Function of Button 5
        Debug.Log("Button 5 pressed");
    }
    public void functionButton6()
    {
        //Perform Function of Button 6
        Debug.Log("Button 6 pressed");
    }
    public void functionButton7()
    {
        //Perform Function of Button 7
        Debug.Log("Button 7 pressed");
    }
    public void functionButton8()
    {
        //Perform Function of Button 8
        Debug.Log("Button 8 pressed");
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
    




























    //This is the Screenshot Function, I don't undestand how it works, I got it from the Documentation pageof NativeGallery: https://github.com/yasirkula/UnityNativeGallery
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
}
