using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    // Changes the scene
    public void ChangeToScene(string sceneToChangeTo)
    {
        SceneManager.LoadScene(sceneToChangeTo);
    }

    // Disables the button given
    public void DisableButton(GameObject buttonToDisable)
    {
        buttonToDisable.SetActive(false);
    }

    // Enables the button given
    public void EnableButton(GameObject buttonToEnable)
    {
        buttonToEnable.SetActive(true);
    }
}
