using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinConditions : MonoBehaviour
{
    public Conditions [] Win;
    public GameObject winConditionPanelsParent;
    [HideInInspector] public GameObject [] conditionPanels;

    private void Awake()
    {
        InitializeConditionPanels();
    }


    void InitializeConditionPanels()
    {
        conditionPanels = new GameObject[winConditionPanelsParent.transform.childCount];
        for (int i = 0; i < conditionPanels.Length; i++)
        {
            conditionPanels[i] = winConditionPanelsParent.transform.GetChild(i).gameObject;
        }
    }

    public void ActivateWinConditionPanel(int level)
    {
        for (int i = 0; i < conditionPanels.Length; i++)
        {
            if (i == level - 1)
            {
                conditionPanels[i].SetActive(true);
               
            }
            else
            {
                conditionPanels[i].SetActive(false);
            }
        }

    }

}

[System.Serializable]
   public class Conditions
{
    public string level;
    public DragAndDrop [] condition = new DragAndDrop [9];
}