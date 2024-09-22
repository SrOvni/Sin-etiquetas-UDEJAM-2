using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class PopUpWindow : MonoBehaviour
{
    [SerializeField] private int popUpWindowClickCount = 0;
    [SerializeField] MiniJuegoSeñora miniJuegoSeñora;
    private Renderer popUpRenderer;
    public bool WindowsIsClosed{get;set;}
    private void Start() {

        WindowsIsClosed = false;
        popUpRenderer = GetComponent<Renderer>();
    }
    public void AddClick()
    {
        popUpWindowClickCount++;
    }
    public void ShowWindow()
    {
        gameObject.SetActive(true);
        StartCoroutine(StarPopUp());
    }
    public IEnumerator StarPopUp()
    {

        for(int i = 0; i < miniJuegoSeñora.NumberOfPopUps;i++)
        {
            popUpWindowClickCount = 0;
            transform.position = miniJuegoSeñora.RandomPositions[Random.Range(0, miniJuegoSeñora.RandomPositions.Length - 1)].position;
            yield return new WaitUntil(()=>popUpWindowClickCount == miniJuegoSeñora.RequiredClicksPerPopUp);
            FadeOut();
        }
        WindowsIsClosed = true;
    }
    public void FadeOut()
    {
        
        gameObject.GetComponent<Image>().DOFade(0,0.1f).OnComplete(FadeIn);
        gameObject.GetComponent<RectTransform>().DOScale(0, 0.1f);
        
    }
    private void Update() {
        if(WindowsIsClosed)
        {
            //StopAllCoroutines();
            gameObject.transform.DOScale(0, 0);
        }
    }
    void FadeIn()
    {
        
        gameObject.GetComponent<RectTransform>().DOScale(.5f,0);
        gameObject.GetComponent<Image>().DOFade(1f,0f);
        
    }
}
