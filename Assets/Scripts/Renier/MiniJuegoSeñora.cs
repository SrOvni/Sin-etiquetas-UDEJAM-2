using System.Collections;
using System.Linq;
using DG.Tweening;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MiniJuegoSeñora : MonoBehaviour
{
    [SerializeField] private GameObject popupWindow;
    [SerializeField] private GameObject positions;
    [SerializeField] private Transform[] randomPosition;
    [SerializeField] private bool startGame = false;
    [SerializeField] private int requiredClicksPerPopUp = 1;
    [SerializeField] private  TextMeshProUGUI timerText;
    [SerializeField] private Button closeButton;
    [SerializeField] private float numberOfPopUps = 5;
    [SerializeField] private float timeBetweenPopUps = 1;
    [SerializeField] private float timeToWin = 10;
    [SerializeField] private GameObject winnedGameText;
    [SerializeField] private GameObject losedgameText;
    [SerializeField] private GameObject miniGameCanvas;
    [SerializeField] private Renderer popUpRenderer;
    [SerializeField] Timer timer;
    [SerializeField] private bool win = false;
    int currentClickCount = 1;

    private void Start() {
        StartCoroutine(StartPopUpWindowGame());
        popupWindow.SetActive(false);
        popUpRenderer = popupWindow.GetComponent<Renderer>();
    }
    private void Update() {
        if(startGame)
        {
            timer.start = true;
        }
        if(timer.CurrentTime == 0)
        {
            StopAllCoroutines();
            WinOrLoseCanvas(win);
        }
        if(win == true)
        {
            WinOrLoseCanvas(win);
            startGame = false;
        }
    }

    private void WinOrLoseCanvas(bool win)
    {
        if (win)
        {
            winnedGameText.SetActive(true);
        }else{
            losedgameText.SetActive(true);
        }
    }

    IEnumerator StartPopUpWindowGame()
    {
        yield return new WaitUntil(()=> startGame);
        for(int i = 0; i < numberOfPopUps - 1;i++)
        {
            currentClickCount = 0;
            popupWindow.transform.position = randomPosition[Random.Range(0, randomPosition.Length - 1)].position;
            popupWindow.SetActive(true);
            yield return new WaitUntil(()=>currentClickCount == requiredClicksPerPopUp);
            popupWindow.SetActive(false);
        }
        startGame = false;
        win = true;
    }

    public void AddClick()
    {
        currentClickCount++;
    }

    public void FadeOut()
    {
        popupWindow.GetComponent<Image>().DOFade(0,0.1f).OnComplete(FadeIn);
        popupWindow.GetComponent<RectTransform>().DOScale(0, 0.1f);
    }
    void FadeIn()
    {
        popupWindow.GetComponent<RectTransform>().DOScale(1.5f,0);
        popupWindow.GetComponent<Image>().DOFade(1,0f);
    }
}
