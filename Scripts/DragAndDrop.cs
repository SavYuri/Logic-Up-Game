using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

//Items

public class DragAndDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler, IInitializePotentialDragHandler
{
    [SerializeField] private Canvas canvas;

    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    [HideInInspector] public Vector3 defaultPos;
    
    public ItemSlot itemSlot;
    public GameObject newItem;
    public bool droppedOnSlot;
    public bool itIsHelperItem;
    public bool itIsHelperColor;
    [Header("Items")]
    public bool [] itIsItemNumber = new bool[3];
    
    [Header("ItemColor")]
    public bool [] ItemColorNumber = new bool[3];

    [Header("HelperItems")]
    public bool [] itIsHelperItemNumber = new bool[3];

    [Header("HelperColors")]
    public bool [] itIsHelperColorNumber = new bool[3];

    public Sounds sounds;

    private void Awake()
    {


        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        defaultPos = transform.position;
        
    }
    private void Start()
    {
        sounds = Sounds.soundsClass;

        canvas = GameObject.FindWithTag("Canvas").GetComponent<Canvas>() ;
       
        defaultPos = transform.position;
        
    }

    void BlocksRaycasts (bool x)
    {
        if (itIsHelperItem || itIsHelperColor)
        {


            GameObject[] items = GameObject.FindGameObjectsWithTag("Item");
            foreach (GameObject item in items)
            {
                DragAndDrop itmScript = item.GetComponent<DragAndDrop>();
                if (itmScript.itIsHelperItem || itmScript.itIsHelperColor)
                {
                    itmScript.canvasGroup.blocksRaycasts = x;
                }

            }
        }
        else
        {
            canvasGroup.blocksRaycasts = x;
        } 
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        

        eventData.pointerDrag.GetComponent<DragAndDrop>().droppedOnSlot = false;
        Debug.Log("OnBeginDrag");
        canvasGroup.alpha = .6f;
        //canvasGroup.blocksRaycasts = false;
        BlocksRaycasts(false);
        if (itemSlot != null)
        {
            itemSlot.CancelAllInformation();
        }  
    }

    public void OnDrag(PointerEventData eventData)
    {
       // Debug.Log("OnDrag");
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (droppedOnSlot == false)
        {
            
          transform.position = defaultPos;
        }
        Debug.Log("OnEndDrag");
        canvasGroup.alpha = 1f;
        //canvasGroup.blocksRaycasts = true;
        BlocksRaycasts(true);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("OnPointerDown");
    }

    public void OnDrop (PointerEventData eventData)
    {
        Debug.Log("OnDrop");
        throw new System.NotImplementedException();
    }

    public void OnInitializePotentialDrag(PointerEventData eventData)
    {
        
        eventData.useDragThreshold = false;
    }

   
}