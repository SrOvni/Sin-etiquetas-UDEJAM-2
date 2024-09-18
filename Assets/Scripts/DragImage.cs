using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragImage : MonoBehaviour
{
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private Vector2 originalPosition;
    public Transform correctDropZone;  // La zona de drop correcta para esta imagen

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        originalPosition = rectTransform.anchoredPosition;  // Guardamos la posición original de la imagen
    }

    // Llamado cuando se comienza a arrastrar la imagen
    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 0.6f;  
        canvasGroup.blocksRaycasts = false;  
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta;  
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;

        
        if (RectTransformUtility.RectangleContainsScreenPoint(correctDropZone.GetComponent<RectTransform>(), Input.mousePosition))
        {           
            rectTransform.position = correctDropZone.position;
        }
        else
        {
            rectTransform.anchoredPosition = originalPosition;
        }
    }
}
