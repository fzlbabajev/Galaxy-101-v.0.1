using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class onBoardingScreen : MonoBehaviour
{

    public float delay = 3;
    public Animator transition;

    void Start()
    {
        StartCoroutine(loadNewScene(delay));
    }

    IEnumerator loadNewScene(float delay)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(delay);

        transition.SetTrigger("End");

        yield return new WaitForSeconds(delay);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

}
