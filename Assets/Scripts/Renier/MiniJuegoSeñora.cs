using System.Collections;
using System.Linq;
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

    [SerializeField] private bool win = false;
    int currentClickCount = 1;
    private float elapsedTime = 0;

    private void Start() {
        StartCoroutine(StartPopUpWindowGame());
        popupWindow.SetActive(false);
    }
    private void Update() {
        if(startGame)
        {
            elapsedTime += Time.deltaTime; 
            timerText.text = elapsedTime.ToString();
        }
        if(elapsedTime == timeToWin)
            {
                StopAllCoroutines();
                WinOrLoseCanvas(win);
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
        win = true;
    }

    public void AddClick()
    {
        currentClickCount++;
    }
}
