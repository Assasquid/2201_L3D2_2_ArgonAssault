using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ColisionHandler : MonoBehaviour
{
    [SerializeField] float loadDelay = 1f;
    [SerializeField] ParticleSystem crashVFX;
    [SerializeField] GameObject car;
    [SerializeField] GameObject canons;

    private void OnCollisionEnter(Collision other) 
    {
        StartCrashSequence();
    }

    void StartCrashSequence()
    {
        crashVFX.Play();
        GetComponent<MeshRenderer>().enabled = false;
        
        MeshRenderer[] meshRenderers;
        meshRenderers = GetComponentsInChildren<MeshRenderer>();

        foreach(MeshRenderer meshRenderer in meshRenderers)
        {
            meshRenderer.enabled = false;
        }

        GetComponent<MeshCollider>().enabled = false;
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
