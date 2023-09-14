using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class mainMenuLoadingScreen : MonoBehaviour
{
    #region Variables
    public float delay = 3;
    public Animator transition;


    [Header("Screens")]
    public GameObject mainScreen;
    public GameObject settingsScreen;
    public GameObject rulesScreen;
    public GameObject loadingScreen;
    public GameObject levelsScreen;

    [Header("Perks")]
    public GameObject coinsBar;
    public GameObject energyBar;

    [Header("Music")]
    public GameObject musicVolumeController;
    int musicVolumeControllerCount = 0;
    public GameObject homeButton;

    [Header("Planets")]
    public GameObject earth;
    public GameObject mars;
    public GameObject mercury;
    public GameObject venus;

    [Header("Planet Animations")]
    public Animator earthTransition;
    public Animator marsTransition;
    public Animator mercuryTransition;
    public Animator venusTransition;

    public Animator earthImageTransition;
    public Animator marsImageTransition;
    public Animator mercuryImageTransition;
    public Animator venusImageTransition;

    public Animator earthInformationPanelTransition;
    public Animator marsInformationPanelTransition;
    public Animator mercuryInformationPanelTransition;
    public Animator venusInformationPanelTransition;

    private Vector3 coinsBarPosition;
    #endregion

    void Start()
    {
        coinsBarPosition = new Vector3(coinsBar.transform.position.x, coinsBar.transform.position.y, 0);

        StartCoroutine(loadNewScene(delay));
    }

    IEnumerator loadNewScene(float delay)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(delay);

        loadingScreen.SetActive(false);

    }

    public void loadLevels()
    {
        mainScreen.SetActive(false);
        levelsScreen.SetActive(true);

        coinsBar.transform.position = new Vector3(160f, coinsBarPosition.y, 0);

        animatePlanetIcons();

        homeButton.SetActive(true);

    }

    public void animatePlanetIcons()
    {
        earthTransition.SetTrigger("earthAnimationStart");
        marsTransition.SetTrigger("marsAnimationStart");
        mercuryTransition.SetTrigger("mercuryAnimationStart");
        venusTransition.SetTrigger("venusAnimationStart");

        earthImageTransition.SetTrigger("earthImageAnimationStart");
        marsImageTransition.SetTrigger("marsImageAnimationStart");
        mercuryImageTransition.SetTrigger("mercuryImageAnimationStart");
        venusImageTransition.SetTrigger("venusImageAnimationStart");
    }

    public void openInformation(string planetName)
    {
        string animationTrigger = planetName + "InformationAnimationStart";
        string animationButtonTrigger = planetName + "InformationDescriptionAnimationStart";
        string animationInformationPanelTrigger = planetName + "InformationPanelMovementAnimationStart";

        switch (planetName)
        {
            case "earth":
                mars.SetActive(false);
                mercury.SetActive(false);
                venus.SetActive(false);
                earth.SetActive(true);

                //pause

                earthImageTransition.SetTrigger(animationTrigger);
                earthTransition.SetTrigger(animationButtonTrigger);

                earth.gameObject.transform.GetChild(2).gameObject.SetActive(true);
                earthInformationPanelTransition.SetTrigger(animationInformationPanelTrigger);


                break;

            case "mars":
                earth.SetActive(false);
                mars.SetActive(true);
                mercury.SetActive(false);
                venus.SetActive(false);

                //pause

                marsImageTransition.SetTrigger(animationTrigger);
                marsTransition.SetTrigger(animationButtonTrigger);

                mars.gameObject.transform.GetChild(2).gameObject.SetActive(true);
                marsInformationPanelTransition.SetTrigger(animationInformationPanelTrigger);

                break;
            case "mercury":

                earth.SetActive(false);
                mars.SetActive(false);
                mercury.SetActive(true);
                venus.SetActive(false);

                //INFORMASIYA PANELI ANIMASIYASINI ELAVE ETMEK LAZIMDIR

                mercuryImageTransition.SetTrigger(animationTrigger);
                mercuryTransition.SetTrigger(animationButtonTrigger);

                break;
            case "venus":
                earth.SetActive(false);
                mars.SetActive(false);
                mercury.SetActive(false);
                venus.SetActive(true);

                //INFORMASIYA PANELI ANIMASIYASINI ELAVE ETMEK LAZIMDIR

                venusImageTransition.SetTrigger(animationTrigger);
                venusTransition.SetTrigger(animationButtonTrigger);

                break;
        }

    }

    public void closeInformation(string planetName)
    {
        string animationTrigger = planetName + "InformationAnimationEnd";
        string animationButtonTrigger = planetName + "InformationDescriptionAnimationEnd";
        string animationInformationPanelTrigger = planetName + "InformationPanelMovementAnimationEnd";

        switch (planetName)
        {
            case "earth":
                earthImageTransition.SetTrigger(animationTrigger);
                earthTransition.SetTrigger(animationButtonTrigger);
                earthInformationPanelTransition.SetTrigger(animationInformationPanelTrigger);

                //pause
                StartCoroutine(delayForTwoSeconds("earth"));

                break;

            case "mars":
                marsImageTransition.SetTrigger(animationTrigger);
                marsTransition.SetTrigger(animationButtonTrigger);
                marsInformationPanelTransition.SetTrigger(animationInformationPanelTrigger);

                //pause
                StartCoroutine(delayForTwoSeconds("mars"));

                break;
            case "mercury":
                mercuryImageTransition.SetTrigger(animationTrigger);
                mercuryTransition.SetTrigger(animationButtonTrigger);
                mercuryInformationPanelTransition.SetTrigger(animationInformationPanelTrigger);

                //pause
                StartCoroutine(delayForTwoSeconds("mercury"));

                break;
            case "venus":
                venusImageTransition.SetTrigger(animationTrigger);
                venusTransition.SetTrigger(animationButtonTrigger);
                venusInformationPanelTransition.SetTrigger(animationInformationPanelTrigger);

                //pause
                StartCoroutine(delayForTwoSeconds("venus"));

                break;
        }

    }


    IEnumerator delayForTwoSeconds(string planetName)
    {
        switch (planetName)
        {
            case "earth":

                earth.gameObject.transform.GetChild(1).GetComponent<Button>().interactable = false;

                yield return new WaitForSeconds(2);

                earth.gameObject.transform.GetChild(1).GetComponent<Button>().interactable = true;

                break;

            case "mars":

                mars.gameObject.transform.GetChild(1).GetComponent<Button>().interactable = false;

                yield return new WaitForSeconds(2);

                mars.gameObject.transform.GetChild(1).GetComponent<Button>().interactable = true;

                break;
            case "mercury":

                mercury.gameObject.transform.GetChild(1).GetComponent<Button>().interactable = false;

                yield return new WaitForSeconds(2);

                mercury.gameObject.transform.GetChild(1).GetComponent<Button>().interactable = true;

                break;
            case "venus":

                venus.gameObject.transform.GetChild(1).GetComponent<Button>().interactable = false;

                yield return new WaitForSeconds(2);

                venus.gameObject.transform.GetChild(1).GetComponent<Button>().interactable = true;

                break;
        }

        //switch case yazmaq her planete gore
        mars.SetActive(true);
        mercury.SetActive(true);
        venus.SetActive(true);
        earth.SetActive(true);

        animatePlanetIcons();

    }


    public void exitGame()
    {
        Application.Quit();
    }

    public void musicController()
    {
        if (musicVolumeControllerCount % 2 == 0)
            musicVolumeController.GetComponent<AudioSource>().mute = true;
        else
            musicVolumeController.GetComponent<AudioSource>().mute = false;

        musicVolumeControllerCount++;
    }

    public void openMainScreen()
    {
        settingsScreen.SetActive(false);
        rulesScreen.SetActive(false);
        levelsScreen.SetActive(false);
        homeButton.SetActive(false);

        coinsBar.transform.position = new Vector3(74.5f, coinsBarPosition.y, 0);

        mainScreen.SetActive(true);

    }

    public void openSettings()
    {
        mainScreen.SetActive(false);
        settingsScreen.SetActive(true);

    }

    public void openRulesScreen()
    {
        mainScreen.SetActive(false);
        rulesScreen.SetActive(true);

    }

}
