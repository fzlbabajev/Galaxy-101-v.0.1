using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;


public class playerData : MonoBehaviour
{
    int j = 0;
    public string[] selectedFoods = { "food1", "food2" };
    public string[] unhealthyFoods = { "burger", "hotdog" };
    public string[] healthyFoods = { "carrot", "eggs" };

    int isAnyFoodSelected = 0;

    [Header("Player Prefs")]
    public int coinCount;
    public float energyCount;

    [Header("Information Bars")]
    public GameObject energyBar;
    public GameObject coinsBar;

    [Header("Food Panels")]
    public GameObject selectedFoodsPanel;
    public GameObject selectionFieldPanel;
    public Animator selectTwoFoodsTitle;

    [Header("Dialogue Box")]
    public GameObject dialogueBoxTitle;
    public GameObject dialogueBoxTextBox;
    public GameObject dialogueBoxPanel;


    void Start()
    {
        //PlayerPrefs.SetInt("Total Coins", 2500);
        //PlayerPrefs.SetFloat("Energy", 3f); // 0-10 arasi olacaq

        isAnyFoodSelected = 0;

        #region getting Data from base
        coinCount = PlayerPrefs.GetInt("Total Coins");
        energyCount = PlayerPrefs.GetFloat("Energy");

        updateEnergyBar();

        #endregion

    }

    void Update()
    {

    }

    public void selectFoodBool()
    {
        isAnyFoodSelected++;
    }

    public void updateEnergyBar()
    {
        coinsBar.transform.GetChild(0).GetComponent<Text>().text = coinCount.ToString();
        energyBar.transform.GetChild(1).GetComponent<Image>().fillAmount = energyCount / 10;

        if (energyCount < 7)
            energyBar.transform.GetChild(1).GetComponent<Image>().color = Color.red;
        else
            energyBar.transform.GetChild(1).GetComponent<Image>().color = Color.green;

    }


    public void selectFood(string foodName)
    {
        if (j % 2 == 0)
        {
            selectedFoodsPanel.transform.GetChild(0).gameObject.SetActive(true);
            selectedFoodsPanel.transform.GetChild(0).GetComponent<Image>().sprite = (Sprite)AssetDatabase.LoadAssetAtPath("Assets/Menu Elements/foods/" + foodName + ".png", typeof(Sprite));
        }
        else
        {
            selectedFoodsPanel.transform.GetChild(1).gameObject.SetActive(true);
            selectedFoodsPanel.transform.GetChild(1).GetComponent<Image>().sprite = (Sprite)AssetDatabase.LoadAssetAtPath("Assets/Menu Elements/foods/" + foodName + ".png", typeof(Sprite));
        }

        j++;

    }

    public void foodConsuming()
    {
        if (isAnyFoodSelected > 1)
        {
            selectTwoFoodsTitle.SetTrigger("foodTitleNormalState");

            dialogueBoxPanel.SetActive(true);
            selectionFieldPanel.SetActive(false);

            int healthyFoodCount = 0;
            int unhealthyFoodCount = 0;

            selectedFoods[0] = selectedFoodsPanel.transform.GetChild(0).GetComponent<Image>().sprite.ToString().Split(" ")[0];
            selectedFoods[1] = selectedFoodsPanel.transform.GetChild(1).GetComponent<Image>().sprite.ToString().Split(" ")[0];

            foreach (string item in selectedFoods)
            {
                for (int i = 0; i < healthyFoods.Length; i++)
                    if (item == healthyFoods[i])
                        healthyFoodCount++;

                for (int i = 0; i < unhealthyFoods.Length; i++)
                    if (item == unhealthyFoods[i])
                        unhealthyFoodCount++;

            }


            //Dialogue Box Text

            if (healthyFoodCount >= unhealthyFoodCount)
            {
                dialogueBoxTitle.GetComponent<Text>().text = "TƏBRİKLƏR";

                dialogueBoxTextBox.GetComponent<Text>().text = "Xeyirli yemək yediniz.\n\nXeyirli yeməklər enerjinizi artırır, zərərlilər isə yox.";

                energyCount = Mathf.Clamp(energyCount + (healthyFoodCount * 2), 0, 10);
                PlayerPrefs.SetFloat("Energy", energyCount);

                updateEnergyBar();

            }
            else if (healthyFoodCount < unhealthyFoodCount)
            {
                dialogueBoxTitle.GetComponent<Text>().text = "TƏƏSSÜF..";

                dialogueBoxTextBox.GetComponent<Text>().text = "Xeyirli yemək yemədiniz.\n\nXeyirli yeməklər enerjinizi artırır, zərərlilər isə yox.";
            }
        }
        else
        {
            //sehv ishleyir
            selectTwoFoodsTitle.SetTrigger("twoFoodsNotSelected");
        }
    }


}


