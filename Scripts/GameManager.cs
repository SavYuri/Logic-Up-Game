using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public WinConditions winConditions;

    
    public int currentGameLevel;

    public GameObject [] slot;

    public MainItems[] mainItems;

    public GameObject [] helperItemNumber;
    public GameObject [] helperColorNumber;
    public GameObject workPanel;
    public Sounds sounds;
    public SceneSelector sceneSelector;

    

    public void CheckWinCondition ()
    {
        if (!NoNullItemOnSlot())
        {
            WrongAnswer();
            return;
        }

        for (int i = 0; i < slot.Length; i++)
        {
            for (int x = 0; x < 3; x++)
            {
                //Форма айтема
                bool gameItem = slot[i].GetComponent<ItemSlot>().itemOnSlot.itIsItemNumber[x];
                bool conditionItem = winConditions.Win[currentGameLevel].condition[i].itIsItemNumber[x];

                if (gameItem != conditionItem )
                {
                    WrongAnswer();
                    Debug.Log("Win Condition = false");
                    return;
                }

                //Цвет айтема
                bool gameItemColor = slot[i].GetComponent<ItemSlot>().itemOnSlot.ItemColorNumber[x];
                bool conditionItemColor = winConditions.Win[currentGameLevel].condition[i].ItemColorNumber[x];

                if (gameItemColor != conditionItemColor)
                {
                    WrongAnswer();
                    Debug.Log("Win Condition = false");
                    return;
                }

            }
        }

        Debug.Log("Win Condition = true");
        if (currentGameLevel == PlayerPrefs.GetInt("OpenLevel") && PlayerPrefs.GetInt("OpenLevel") != (sceneSelector.levelButtons.Length - 1))
        {
            int saveLevel = currentGameLevel + 1;

            PlayerPrefs.SetInt("OpenLevel", saveLevel);
        }
        
        sceneSelector.OpenWinMenu();
        sounds.playWin();
        

    }

    int numberItem;
    int colorItem;

    public void ShowWinSolution()
    {
        CancelItemLocation();
        sceneSelector.CloseHelpMenu();

        for (int i = 0; i < slot.Length; i++) // количество ячеек
        {
            

            //определяем правильный айтем в ячейке
            //определяем форму
            for (int x = 0; x < 3; x++)
            {
               if (winConditions.Win[currentGameLevel].condition[i].itIsItemNumber[x])
                {
                    numberItem = x;
                    break;
                }
            }
            //определяем цвет
            for (int x = 0; x < 3; x++)
            {
                if (winConditions.Win[currentGameLevel].condition[i].ItemColorNumber[x])
                {
                    colorItem = x;
                    break;
                }
            }
            //перетаскиваем айтем в слот
            mainItems[numberItem].itemNumberColor[colorItem].transform.position = slot[i].transform.position;
            mainItems[numberItem].itemNumberColor[colorItem].GetComponent<DragAndDrop>().droppedOnSlot = true;
            mainItems[numberItem].itemNumberColor[colorItem].GetComponent<DragAndDrop>().itemSlot = slot[i].GetComponent<ItemSlot>();
            ItemSlot itemSlot = slot[i].GetComponent<ItemSlot>();
            itemSlot.itemOnSlot = mainItems[numberItem].itemNumberColor[colorItem].GetComponent<DragAndDrop>();
            itemSlot.ItemNumberOnSlot[numberItem] = true;
            itemSlot.itemColorNumber[colorItem] = true;
            itemSlot.slotBusy = true;

        }
    }

    void WrongAnswer()
    {
        workPanel.GetComponent<Animator>().SetTrigger("Wrong");
        sounds.playWrongAnswer();
    }

    bool NoNullItemOnSlot ()
    {
        int i = 0;
        while (i < slot.Length)
        {
            if (slot[i].GetComponent<ItemSlot>().itemOnSlot == null)
            {
                Debug.Log("Some itemOnSlot = null");
                return false;
                
            }
            i++;
        }
        Debug.Log("ItemsOnSlot != null");
        return true;
    }


    public void CancelItemLocation()
    {
        sounds.playClearWorkField();

        GameObject[] items = GameObject.FindGameObjectsWithTag("Item");
        foreach (GameObject item in items)
        {
            item.transform.position = item.GetComponent<DragAndDrop>().defaultPos;
            item.GetComponent<DragAndDrop>().droppedOnSlot = false;
            item.GetComponent<DragAndDrop>().itemSlot = null;
        }

        GameObject[] itemSlots = GameObject.FindGameObjectsWithTag("ItemSlot");
        foreach (GameObject itemSlot in itemSlots)
        {
           ItemSlot slot = itemSlot.GetComponent<ItemSlot>();
            slot.slotBusy = false;
            slot.helperItemOnSlot = false;
            slot.helperColorOnSlot = false;
            slot.itemOnSlot = null;

            for (int i = 0; i < 3; i++)
            {
                slot.ItemNumberOnSlot[i] = false;
                slot.itemColorNumber[i] = false;
                slot.helperItemNumberOnSlot[i] = false;
                slot.helperColorNumberOnSlot[i] = false;
            }
        }
    }
}

[System.Serializable]
public class MainItems
{
   
    public GameObject [] itemNumberColor;

}