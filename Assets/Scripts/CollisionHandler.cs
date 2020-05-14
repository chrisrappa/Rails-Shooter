using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.Effects;

public class CollisionHandler : MonoBehaviour
{
    [Tooltip("In seconds")] [SerializeField] float levelLoadDelay = 1f;
    [Tooltip("FX prefab on player")] [SerializeField] GameObject deathFX;

    private void OnTriggerEnter(Collider other)
    {
        startDeathSequence();
        deathFX.SetActive(true);
    }

    private void startDeathSequence()
    {

        Invoke("LoadNextScene", levelLoadDelay);
        gameObject.SendMessage("playerDied");

    }
    private void LoadNextScene()
    {
        SceneManager.LoadScene(1);
    }


}
