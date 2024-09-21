
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class CardController : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler, IPointerUpHandler
{
    [SerializeField] SignType signType;
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private Vector2 initialPositon;
    SignLanguageMiniGame signLanguageMiniGame;
    [SerializeField] private string positonsAvailableTag = "CardPositon";
    [SerializeField] private string cardTag= "Card";
    AvailableSpot availableSpot;
    Vector2 nextPosition;
    Vector2 previousPosition;
    bool inPosition = false;
    int belongsTo = 0;
    bool draggin = false;


    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        initialPositon = rectTransform.anchoredPosition;
        nextPosition = initialPositon;
        previousPosition = initialPositon;
        signLanguageMiniGame = FindAnyObjectByType<SignLanguageMiniGame>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("OnPointerDown");
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("OnPointerExit");
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        if(!draggin && !inPosition)
        {
            nextPosition = signLanguageMiniGame.NextPositonAvailable();
            if(nextPosition != Vector2.zero)
            {
                rectTransform.anchoredPosition = nextPosition;
                inPosition = true;
            }
        }else if(inPosition && !draggin)
        {
            rectTransform.anchoredPosition = initialPositon;
            signLanguageMiniGame.ReturnCard(availableSpot.belongsTo);
        }
    }
    

    public void OnBeginDrag(PointerEventData eventData)
    {
        
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        draggin = true;
        rectTransform.anchoredPosition += eventData.delta;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("End of drag");
        /*
        if(inPosition && previousPosition == rectTransform.anchoredPosition && previousPosition != initialPositon )
        {
            signLanguageMiniGame.ReturnCard(belongsTo);
            rectTransform.anchoredPosition = initialPositon;
        }else{
            rectTransform.anchoredPosition = nextPosition;
        }
        */
        if(Vector2.Distance(rectTransform.anchoredPosition, nextPosition) < 0.2)
        {
             rectTransform.anchoredPosition = nextPosition;
            
        }else{

            rectTransform.anchoredPosition = initialPositon;
        }
        
        canvasGroup.blocksRaycasts = true;
        previousPosition = rectTransform.anchoredPosition;
        draggin = false;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        
        if(other.CompareTag(positonsAvailableTag))
        {
            other.TryGetComponent(out AvailableSpot component);
            availableSpot = component;
        }
    }
    private void OnTriggerExit2D(Collider2D other) {
        if(other.CompareTag(positonsAvailableTag))
        {
            inPosition = false;
        }
    }
}
