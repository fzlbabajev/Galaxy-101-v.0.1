using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class trivia : MonoBehaviour
{
    #region variables
    int questionIndex = 2;
    int questionCount = 1;
    int correctAnswers = 0;
    int incorrectAnswers = 0;
    int earnedCoins = 0;
    List<int> usedQuestionIndexes = new List<int>();
    string _currentPlanetName;

    [Header("Screens")]
    public GameObject levelsListScreen;
    public GameObject triviaScreen;
    public GameObject triviaPanel;
    public GameObject questionPanel;
    public GameObject dialoguePanel;
    public GameObject coinsBar;
    public GameObject energyBar;
    public GameObject energyInformationPanel;

    [Header("Planet Animators")]
    public Animator earthInformationPanelTransition;
    public Animator marsInformationPanelTransition;
    public Animator mercuryInformationPanelTransition;
    public Animator venusInformationPanelTransition;

    public Animator earthTransition;
    public Animator marsTransition;
    public Animator mercuryTransition;
    public Animator venusTransition;

    public Animator earthImageTransition;
    public Animator marsImageTransition;
    public Animator mercuryImageTransition;
    public Animator venusImageTransition;

    [Header("Answer Panels")]
    public GameObject answerPanel1;
    public GameObject answerPanel2;
    public GameObject answerPanel3;
    public GameObject answerPanel4;

    [Header("Questions & Answers")]
    [Tooltip("Correct Answer first, answers will be randomized later")]
    public string[] questionsList;
    public string[] answersListRaw;
    public string[] answersList;


    [Header("File Paths")]
    public string questionsFilePath;
    public string answersFilePath;

    public GameObject nextButonn;
    #endregion


    private void Start()
    {
        usedQuestionIndexes.Add(2);

    }

    public void startTrivia(string planetName)
    {
        _currentPlanetName = planetName;

        StartCoroutine(waitForInformationPanel(1.5f));

        questionsFilePath = "Assets/Data/questions" + planetName + ".txt";
        answersFilePath = "Assets/Data/answers" + planetName + ".txt";

        StreamReader reader = new StreamReader(questionsFilePath);
        questionsList = reader.ReadToEnd().Split("/");

        reader = new StreamReader(answersFilePath);
        answersListRaw = reader.ReadToEnd().Split("/");

        //enerji barede melumat panelini goster
        triviaPanel.SetActive(false);
        StartCoroutine(energyInformationWait(5f));


        //randomizeSelection
        questionSelection(questionIndex);

        #region oyuna girdiyi uchun enerji itirir
        PlayerPrefs.SetFloat("Energy", PlayerPrefs.GetFloat("Energy") - 2f);

        energyBar.transform.GetChild(1).GetComponent<Image>().fillAmount = PlayerPrefs.GetFloat("Energy") / 10;

        if (PlayerPrefs.GetFloat("Energy") < 7)
            energyBar.transform.GetChild(1).GetComponent<Image>().color = Color.red;
        else
            energyBar.transform.GetChild(1).GetComponent<Image>().color = Color.green;
        #endregion

    }

    public void backToPlanets()
    {
        //informasiya paneli animator default state qoymaq lazimdir

        triviaScreen.SetActive(false);
        levelsListScreen.SetActive(true);

        switch (_currentPlanetName)
        {
            case "earth":
                earthInformationPanelTransition.SetTrigger("earthInformationPanelMovementAnimationStart");
                earthImageTransition.SetTrigger("earthImageAnimationStart");
                earthImageTransition.SetTrigger("earthInformationAnimationStart");
                earthTransition.SetTrigger("earthAnimationStart");
                earthTransition.SetTrigger("earthInformationDescriptionAnimationStart");

                break;
            case "mars":
                marsInformationPanelTransition.SetTrigger("marsInformationPanelMovementAnimationStart");
                marsImageTransition.SetTrigger("marsImageAnimationStart");
                marsImageTransition.SetTrigger("marsInformationAnimationStart");
                marsTransition.SetTrigger("marsAnimationStart");
                marsTransition.SetTrigger("marsInformationDescriptionAnimationStart");

                break;

        }
    }


    public void randomizeQuestion()
    {
        int i = 0;

        if (questionCount < 5)
        {
            questionIndex = Random.Range(0, 6);

            while (i < usedQuestionIndexes.Count)
            {

                if (questionIndex == usedQuestionIndexes[i])
                {
                    randomizeQuestion();
                    break;
                }
                else
                {
                    questionSelection(questionIndex);
                    questionCount++;

                }

                i++;
            }

            usedQuestionIndexes.Add(questionIndex);
        }
        else
        {
            triviaPanel.SetActive(false);
            dialoguePanel.SetActive(true);

            earnedCoins = correctAnswers * 10;

            dialoguePanel.transform.GetChild(1).GetChild(0).GetComponent<Text>().text = "Siz " + correctAnswers.ToString() + " suala doğru," + incorrectAnswers.ToString() + " suala yanlış cavab verdiniz.\nQazandığınız qəpik sayı: " + earnedCoins.ToString();

            PlayerPrefs.SetInt("Total Coins", PlayerPrefs.GetInt("Total Coins") + earnedCoins);

            coinsBar.GetComponent<Text>().text = (PlayerPrefs.GetInt("Total Coins") + earnedCoins).ToString();


            //duz cavab/sehv cavab sayini yazmaq
            //bu qeder coin qazandiz yazmaq
        }



    }


    public void questionSelection(int indexCount)
    {
        questionPanel.transform.GetChild(1).GetComponent<Text>().text = questionsList[indexCount];

        answersList = answersListRaw[indexCount].Split("-");

        answerPanel1.transform.GetChild(0).GetComponent<Text>().text = answersList[0];
        answerPanel2.transform.GetChild(0).GetComponent<Text>().text = answersList[1];
        answerPanel3.transform.GetChild(0).GetComponent<Text>().text = answersList[2];
        answerPanel4.transform.GetChild(0).GetComponent<Text>().text = answersList[3];

    }

    //[duzgun cavab/umumi cavab] elave etmek lazimdir
    //qepik vermek lazimdi 

    GameObject _clickedButton;

    public void checkAnswer(GameObject clickedButton)
    {
        _clickedButton = clickedButton;

        string pickedAnswer = clickedButton.transform.GetChild(0).GetComponent<Text>().text;

        //questionIndex ve cavab axtarmaq elave etmek lazim olacaq
        if (pickedAnswer == answersList[0])
        {
            _clickedButton.GetComponent<Image>().color = Color.green;

            StartCoroutine(waitForAnswer(4f));

            correctAnswers++;
            //novbeti suala kecid
        }
        else
        {
            _clickedButton.GetComponent<Image>().color = Color.red;

            StartCoroutine(waitForAnswer(4f));

            incorrectAnswers++;
            //novbeti suala kecid
        }


    }

    IEnumerator waitForAnswer(float delay)
    {

        answerPanel1.GetComponent<CanvasGroup>().blocksRaycasts = false;
        answerPanel2.GetComponent<CanvasGroup>().blocksRaycasts = false;
        answerPanel3.GetComponent<CanvasGroup>().blocksRaycasts = false;
        answerPanel4.GetComponent<CanvasGroup>().blocksRaycasts = false;
        nextButonn.GetComponent<CanvasGroup>().blocksRaycasts = false;

        yield return new WaitForSeconds(delay);


        _clickedButton.GetComponent<Image>().color = Color.white;

        answerPanel1.GetComponent<CanvasGroup>().blocksRaycasts = true;
        answerPanel2.GetComponent<CanvasGroup>().blocksRaycasts = true;
        answerPanel3.GetComponent<CanvasGroup>().blocksRaycasts = true;
        answerPanel4.GetComponent<CanvasGroup>().blocksRaycasts = true;
        nextButonn.GetComponent<CanvasGroup>().blocksRaycasts = true;


        randomizeQuestion();

    }

    IEnumerator waitForInformationPanel(float delay)
    {
        switch (_currentPlanetName)
        {
            case "earth":
                earthInformationPanelTransition.SetTrigger("earthInformationPanelMovementAnimationEnd");
                earthImageTransition.SetTrigger("earthImageAnimationEnd");
                earthImageTransition.SetTrigger("earthInformationAnimationEnd");
                earthTransition.SetTrigger("earthAnimationEnd");
                earthTransition.SetTrigger("earthInformationDescriptionAnimationEnd");

                break;
            case "mars":
                marsInformationPanelTransition.SetTrigger("marsInformationPanelMovementAnimationEnd");
                marsImageTransition.SetTrigger("marsImageAnimationEnd");
                marsImageTransition.SetTrigger("marsInformationAnimationEnd");
                marsTransition.SetTrigger("marsAnimationEnd");
                marsTransition.SetTrigger("marsInformationDescriptionAnimationEnd");

                break;

        }


        yield return new WaitForSeconds(delay);

        triviaScreen.SetActive(true);
        levelsListScreen.SetActive(false);


    }



    IEnumerator energyInformationWait(float delay)
    {
        energyInformationPanel.SetActive(true);

        yield return new WaitForSeconds(delay);

        energyInformationPanel.SetActive(false);
        triviaPanel.SetActive(true);


    }

}
