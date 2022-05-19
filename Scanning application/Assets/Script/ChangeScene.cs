using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChangeScene : MonoBehaviour
{
    // Changes the scene
    public void ChangeToScene(string sceneToChangeTo)
    {
        SceneManager.LoadScene(sceneToChangeTo);
    }

    // Disables the button given
    public void DisableButton (GameObject buttonToDisable)
    {
        buttonToDisable.SetActive(false);
    }

    public void EnableButton (GameObject buttonToEnable)
    {
        buttonToEnable.SetActive(true);
    }


}
