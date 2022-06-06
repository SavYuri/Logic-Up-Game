using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Education : MonoBehaviour
{
    public GameManager gameManager;
    public GameObject educationHand;
    public GameObject educationHand2;
    bool edHand2inProcess;

    private void Awake()
    {
        educationHand.SetActive(false);
        educationHand2.SetActive(false);
    }

    void OnEnable()
    {
        if (gameObject.activeSelf)
        {
           StartCoroutine(startEducationLevel1());
           InvokeRepeating("chekSlotsNull", 10, 0.2f);
        }
       
    }

    private void Start()
    {
        
    }

    

    void OnDisable()
    {
        educationHand.SetActive(false);
        educationHand2.SetActive(false);
    }

    IEnumerator startEducationLevel1()
    {
        educationHand.SetActive(true);
        yield return new WaitForSeconds(25);
        educationHand.SetActive(false);
        yield break;
    }

    IEnumerator startEducationLevel2()
    {
        edHand2inProcess = true;
        educationHand2.SetActive(true);
        yield return new WaitForSeconds(10);
        educationHand2.SetActive(false);
        edHand2inProcess = false;
        yield break;
    }

    void chekSlotsNull()
    {
        for (int i = 0; i < gameManager.slot.Length; i++)
        {
            if(gameManager.slot[i].GetComponent<ItemSlot>().itemOnSlot == null)
            {
                return;
            }
        }

        if (!edHand2inProcess)
        {
            StartCoroutine(startEducationLevel2());
        }

    }
}
