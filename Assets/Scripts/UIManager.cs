﻿using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class UIManager : MonoBehaviour
{
    public GameObject mainMenu;
    public Image fadeOut;
    private bool triggerFadeOut = false;


    private void Update()
    {
        if (triggerFadeOut)
        {
            fadeOut.GetComponent<CanvasGroup>().alpha = Mathf.Lerp(fadeOut.GetComponent<CanvasGroup>().alpha, 1, 0.025f);
            Vector3 cameraTarget = new Vector3(Camera.main.GetComponent<CameraControl>().offsetPosition.x / 2,
                                                Camera.main.GetComponent<CameraControl>().offsetPosition.y,
                                                Camera.main.GetComponent<CameraControl>().offsetPosition.z);
            Camera.main.GetComponent<CameraControl>().offsetPosition = Vector3.Lerp(Camera.main.GetComponent<CameraControl>().offsetPosition,
                                                                            cameraTarget, 0.05f);
        }

    }

    public void loadScene(int sceneIndex)
    {
        StartCoroutine(scene(sceneIndex));
        mainMenu.SetActive(false);
        triggerFadeOut = true;

    }

    IEnumerator scene(int sceneIndex)
    {
        yield return new WaitForSeconds(2.3f);
        SceneManager.LoadScene(sceneIndex);

    }

}
