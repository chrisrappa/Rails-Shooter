using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.Effects;

public class CollisionHandler : MonoBehaviour
{
    [Tooltip("In seconds")] [SerializeField] float levelLoadDelay = 1f;
    [Tooltip("FX prefab on player")] [SerializeField] public GameObject deathFX;

    private void OnTriggerEnter(Collider other)
    {
        startDeathSequence();
        deathFX.SetActive(true);
        Invoke("LoadNextScene", levelLoadDelay);
    }

    private void startDeathSequence()
    {
        gameObject.SendMessage("playerDied");
    }
    private void LoadNextScene()
    {
        SceneManager.LoadScene(1);
    }


}
