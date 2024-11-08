using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] Animator transitionAnim;
    void Awake()
    {
        //do Something on Awake E.g. make an object appearence false
    }

    IEnumerator LoadSceneAsync(string sceneName)
    {
        transitionAnim.SetTrigger("End");
        yield return new WaitForSeconds(1);
        SceneManager.LoadSceneAsync(sceneName);
        Player.Instance.transform.position = new(0, 0);
        transitionAnim.SetTrigger("Start");
    }

    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadSceneAsync(sceneName));
    }
}
