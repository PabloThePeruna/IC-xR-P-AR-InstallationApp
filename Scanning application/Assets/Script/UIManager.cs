using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    public GameObject screenshotScreen;

    void Update()
    {
        //if (screenshotScreen != false)
        //{
            //StartCoroutine(FadeOut());
        //}
    }

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

    public void ScreenshotEffect(GameObject shotButton)
    {
        screenshotScreen.SetActive(true);
        StartCoroutine(FadeOut());
    }


    // fade from opaque to transparent
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
