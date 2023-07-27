using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerScript : MonoBehaviour
{
    public float musicVolume = 1f, sfxVolume = .4f, sfxVolumeValue = 1f;
    private const float sfxMult = .4f;
    private int currScene, nScene;
    public AudioSource audioSource;
    private bool startedLoading;
    [SerializeField]
    private AudioClip[] tracks;
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = 0;
        StartCoroutine(LoadScene(0, 1));
    }

    private void FixedUpdate()
    {
        sfxVolume = sfxVolumeValue * sfxMult;
        audioSource.volume = musicVolume;
    }

    public IEnumerator LoadScene(int currentScene, int nextScene)
    {
        if (!startedLoading)
        {
            startedLoading = true;
            currScene = currentScene;
            nScene = nextScene;
            Debug.Log("Initialising");
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(nextScene, LoadSceneMode.Additive);
            while (!asyncLoad.isDone)
            {
                yield return null;
                Debug.Log("Loading");
            }
            Debug.Log("Loaded");
            SceneManager.MoveGameObjectToScene(gameObject, SceneManager.GetSceneByBuildIndex(nextScene));
            startedLoading = false;
            AsyncOperation asyncDelete = SceneManager.UnloadSceneAsync(currentScene);
            while (!asyncDelete.isDone)
            {
                yield return null;
                Debug.Log("Unloading");
            }
            Debug.Log("Unloaded");
            SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(nextScene));
            Debug.Log("Done");
        }

    }

    public void PlayMusic(int track)
    {
        audioSource.clip = tracks[track];
        audioSource.Play();
    }
}
