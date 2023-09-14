using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;


public class rocketAssembly : MonoBehaviour
{
    [Header("Part buttons")]
    public GameObject part1;
    public GameObject part2;
    public GameObject part3;
    public GameObject part4;

    List<string> correctParts = new List<string>();
    List<string> incorrectParts = new List<string>();

    [Header("Panels & Buttons")]
    public GameObject confirmButton;
    public GameObject declineButton;
    public GameObject dialogueBox;
    public GameObject partsPool;
    public GameObject informationTitle;


    [Header("Clicked Buttons BUffer")]
    public List<GameObject> clickedButtonsBuffer = new List<GameObject>();
    public int objectsInBuffer = -1;
    GameObject partToResetColor;

    GameObject currentRocketPart;

    // Start is called before the first frame update
    void Start()
    {
        correctParts.Add("rocketBody");
        correctParts.Add("rocketLeg");
        correctParts.Add("rocketWindow");

        incorrectParts.Add("incorrect");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void checkRocketPart(string partName)
    {
        if (objectsInBuffer < 3)
        {
            checkRocketPartHelper(partName);

            holdSelectedRocketParts(clickedButtonsBuffer);

            objectsInBuffer++;
        }
        else
        {
            partToResetColor = clickedButtonsBuffer[0];
            clickedButtonsBuffer.RemoveAt(0);

            checkRocketPartHelper(partName);

            holdSelectedRocketParts(clickedButtonsBuffer);

            objectsInBuffer--;

        }
    }

    public void checkRocketPartHelper(string partNameToCheck)
    {
        switch (partNameToCheck)
        {
            case "rocketBody":
                currentRocketPart = GameObject.FindGameObjectWithTag("rocketBody");

                clickedButtonsBuffer.Add(currentRocketPart);


                break;
            case "rocketLeg":
                currentRocketPart = GameObject.FindGameObjectWithTag("rocketLeg");

                clickedButtonsBuffer.Add(currentRocketPart);

                break;
            case "rocketWindow":
                currentRocketPart = GameObject.FindGameObjectWithTag("rocketWindow");

                clickedButtonsBuffer.Add(currentRocketPart);

                break;
            default:
                currentRocketPart = GameObject.FindGameObjectWithTag("incorrect");

                clickedButtonsBuffer.Add(currentRocketPart);

                break;
        }
    }

    public void holdSelectedRocketParts(List<GameObject> selectedObjects)
    {
        if (partToResetColor != null)
            partToResetColor.transform.GetComponent<Image>().color = Color.white;

        foreach (GameObject item in selectedObjects)
            item.transform.GetComponent<Image>().color = Color.yellow;

    }



    public void submitRocketPart()
    {
        if (clickedButtonsBuffer.Count != 0)
        {

            foreach (GameObject item in clickedButtonsBuffer)
                item.transform.GetComponent<Image>().color = Color.green;

            confirmButton.SetActive(false);
            declineButton.SetActive(false);

            StartCoroutine(waitForInformationPanel(3f));

            //roket uchsun
        }


    }


    IEnumerator waitForInformationPanel(float delay)
    {
        yield return new WaitForSeconds(delay);

        dialogueBox.SetActive(true);

        //sonra animasiya qoyariq
        partsPool.SetActive(false);
        informationTitle.SetActive(false);

    }

}
