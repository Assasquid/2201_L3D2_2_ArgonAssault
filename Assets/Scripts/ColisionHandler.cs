using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ColisionHandler : MonoBehaviour
{
    float loadDelay = 1f;

    private void OnCollisionEnter(Collision other) 
    {
        GetComponent<PlayerControls>().enabled = false;
        StartCoroutine(ReloadLevel());
    }

    IEnumerator ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        yield return new WaitForSeconds(loadDelay);
        SceneManager.LoadScene(currentSceneIndex);
    }
}
