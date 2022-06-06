using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IDropHandler, IPointerDownHandler
{
    GameManager gameManager;

    public DragAndDrop itemOnSlot; // айтем, который в данный момент на слоте
    public bool slotBusy;
    public bool helperItemOnSlot;
    public bool helperColorOnSlot;
    public bool[] ItemNumberOnSlot = new bool[3];
    public bool[] itemColorNumber = new bool[3];
    public bool[] helperItemNumberOnSlot = new bool[3];
    public bool[] helperColorNumberOnSlot = new bool[3];

    int numberOfItem;
    int numberOfItemColor;

    private void Start()
    {
        gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
    }

    

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("OnDrop 1");
        if (eventData.pointerDrag != null)
        {
            DragAndDrop item = eventData.pointerDrag.GetComponent<DragAndDrop>();

            if (slotBusy)
            {
                
                //если перетаскиваемый айтем это помошник цвета, а в слоте есть помошник формы, то совмещаем их в основной айтем
                if (item.itIsHelperColor && helperItemOnSlot)
                {
                    for (int i = 0; i < helperItemNumberOnSlot.Length; i++)
                    {
                        //определяем форму помошника
                        if (helperItemNumberOnSlot[i])
                        {
                            numberOfItem = i;
                            break;
                        }
                    }

                    //определяем цвет помошника
                    for (int x = 0; x < item.itIsHelperColorNumber.Length; x++)
                    {
                        if (item.itIsHelperColorNumber[x])
                        {
                            numberOfItemColor = x;
                            break;
                        }
                    }

                    //возвращаем айтем слота на исходную
                    itemOnSlot.transform.position = itemOnSlot.GetComponent<DragAndDrop>().defaultPos;
                    itemOnSlot.GetComponent<DragAndDrop>().droppedOnSlot = false; ;
                    itemOnSlot.GetComponent<DragAndDrop>().itemSlot = null;
                    //перетаскиваем нужный айтем в слот
                    CancelAllInformation();
                    slotBusy = true;
                    ItemNumberOnSlot[numberOfItem] = true;
                    itemColorNumber[numberOfItemColor] = true;
                    DragAndDrop mainItem = gameManager.mainItems[numberOfItem].itemNumberColor[numberOfItemColor].GetComponent<DragAndDrop>();
                    mainItem.gameObject.transform.position = GetComponent<RectTransform>().position;
                    if(mainItem.itemSlot != null)
                    {
                        mainItem.itemSlot.CancelAllInformation();
                    }
                    mainItem.droppedOnSlot = true;
                    mainItem.itemSlot = this;
                        itemOnSlot = mainItem;
                    gameManager.sounds.playSetItem();
                    return;

                    
                }
                    //если айтем это помошник формы, а в слоте есть помошник цвета, то совмещаем их в основной айтем
                    if (item.itIsHelperItem && helperColorOnSlot)
                    {

                      for (int i = 0; i < item.itIsHelperItemNumber.Length; i++)
                      {
                        //определяем форму помошника
                        if (item.itIsHelperItemNumber[i])
                        {
                            numberOfItem = i;
                            break;
                        }
                      }

                      //определяем цвет помошника
                      for (int x = 0; x < helperColorNumberOnSlot.Length; x++)
                      {
                        if (helperColorNumberOnSlot[x])
                        {
                            numberOfItemColor = x;
                            break;
                        }
                      }

                       //возвращаем айтем слота на исходную
                       itemOnSlot.transform.position = itemOnSlot.GetComponent<DragAndDrop>().defaultPos;
                       itemOnSlot.GetComponent<DragAndDrop>().droppedOnSlot = false; ;
                       itemOnSlot.GetComponent<DragAndDrop>().itemSlot = null;
                       //перетаскиваем нужный айтем в слот
                       CancelAllInformation();
                       slotBusy = true;
                       ItemNumberOnSlot[numberOfItem] = true;
                       itemColorNumber[numberOfItemColor] = true;
                       DragAndDrop mainItem = gameManager.mainItems[numberOfItem].itemNumberColor[numberOfItemColor].GetComponent<DragAndDrop>();
                       mainItem.gameObject.transform.position = GetComponent<RectTransform>().position;

                         if (mainItem.itemSlot != null)
                         {
                           mainItem.itemSlot.CancelAllInformation();
                         }

                       mainItem.droppedOnSlot = true;
                       mainItem.itemSlot = this;
                       itemOnSlot = mainItem;
                       gameManager.sounds.playSetItem();
                       return;
                    }








                    //айтем на исходную
                    eventData.pointerDrag.GetComponent<RectTransform>().position = eventData.pointerDrag.GetComponent<DragAndDrop>().defaultPos;

                    return;
                
            }
                eventData.pointerDrag.GetComponent<RectTransform>().position = GetComponent<RectTransform>().position;
                eventData.pointerDrag.GetComponent<DragAndDrop>().droppedOnSlot = true;
                eventData.pointerDrag.GetComponent<DragAndDrop>().itemSlot = this;
                itemOnSlot = eventData.pointerDrag.GetComponent<DragAndDrop>();
                CheckKindOfItem(eventData);
                gameManager.sounds.playSetItem();

            slotBusy = true;
        }
    }
        public void OnPointerDown(PointerEventData eventData)
        {
            Debug.Log("OnPointerDown");
            slotBusy = false;
        }

        void CheckKindOfItem(PointerEventData eventData)
        {
            DragAndDrop dragAndDrop = eventData.pointerDrag.GetComponent<DragAndDrop>();

            if (dragAndDrop.itIsHelperItem)
            {
                helperItemOnSlot = true;

                for (int i = 0; i < dragAndDrop.itIsHelperItemNumber.Length; i++)
                {
                    if (dragAndDrop.itIsHelperItemNumber[i])
                    {
                        helperItemNumberOnSlot[i] = true;
                        break;
                    }
                }
            }
            else if (dragAndDrop.itIsHelperColor)
            {
                helperColorOnSlot = true;

                for (int i = 0; i < dragAndDrop.itIsHelperColorNumber.Length; i++)
                {
                    if (dragAndDrop.itIsHelperColorNumber[i])
                    {
                        helperColorNumberOnSlot[i] = true;
                        break;
                    }
                }
            }
            else
            {

                for (int i = 0; i < dragAndDrop.itIsItemNumber.Length; i++)
                {
                    if (dragAndDrop.itIsItemNumber[i])
                    {
                        ItemNumberOnSlot[i] = true;
                        break;
                    }
                }

                for (int i = 0; i < dragAndDrop.ItemColorNumber.Length; i++)
                {
                    if (dragAndDrop.ItemColorNumber[i])
                    {
                        itemColorNumber[i] = true;
                        break;
                    }
                }

            }

        }

    public void CancelAllInformation()
    {
        itemOnSlot = null;
        slotBusy = false;
        helperItemOnSlot = false;
        helperColorOnSlot = false;

        for (int i = 0; i < 3; i++)
        {
            ItemNumberOnSlot[i] = false;
            itemColorNumber[i] = false;
            helperItemNumberOnSlot[i] = false;
            helperColorNumberOnSlot[i] = false;
        }
    }
}

