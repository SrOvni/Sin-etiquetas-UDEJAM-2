using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class Card : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    [SerializeField] private int order = -1;
    private Vector2 initialPosition;
    private bool inPosition = false;
    AvailableSpot availableSpot;

    [SerializeField] UnityEvent AddCard;
    [SerializeField] UnityEvent RemoveCard;
    [SerializeField] SLMiniGame sLMiniGame;

    private void Start() {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        initialPosition = rectTransform.anchoredPosition;

    }
    public void OnDrag(PointerEventData eventData)
    {
        if(!inPosition && availableSpot is not null)
        {
            availableSpot.HasTheCard = -1;
                availableSpot = null;
        }
        rectTransform.anchoredPosition += eventData.delta;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if(inPosition)
        {
            availableSpot.HasTheCard = order;
                AddCard?.Invoke();
            rectTransform.anchoredPosition = availableSpot.GetComponent<RectTransform>().anchoredPosition;
        }else{
            Debug.Log("Out");
            
            RemoveCard?.Invoke();
            if(availableSpot is not null)
            {
                availableSpot.HasTheCard = -1;
                availableSpot = null;
                rectTransform.anchoredPosition = initialPosition;
            }
        }
        canvasGroup.blocksRaycasts = true;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        
        canvasGroup.blocksRaycasts = false;
    }
    public void RegresarCarta()
    {
        canvasGroup.blocksRaycasts = true;
        rectTransform.anchoredPosition = initialPosition;
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.TryGetComponent(out AvailableSpot component))
        {
            if(component.HasTheCard == -1)
            {
                inPosition = true;
                availableSpot = component;
                
            }
        }
    }
    private void OnTriggerExit2D(Collider2D other) {
        if(other.GetComponent<AvailableSpot>())
        {
            inPosition = false;
            if(rectTransform.anchoredPosition == initialPosition && availableSpot is not null)
            {
                availableSpot.HasTheCard = -1;
                availableSpot = null;
            }
            
        }
    }
}
