using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonStateScript : MonoBehaviour
{
    public static int NumberOfButtons = 8;
    public GameObject[] FunctionButtons = new GameObject[NumberOfButtons];
    public GameObject[] CloseButtons = new GameObject[NumberOfButtons];
   

    void Start()
    {
        for(int i=0; i < NumberOfButtons; i++)
        {
            CloseButtons[i].SetActive(false);
            FunctionButtons[i].SetActive(false);
        }
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
}
