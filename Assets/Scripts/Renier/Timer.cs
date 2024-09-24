using TMPro;
using UnityEngine;
using DG.Tweening;

public class Timer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;
    public bool start;
    public float time = 30;
    [SerializeField] float currentTime;
    public float CurrentTime{get { return currentTime;}set { currentTime = value; } }
    private void Start() {
        text.text = string.Empty;
        currentTime = time;
    }
    private void Update() {
        if(start)
        {
            currentTime -= Time.deltaTime;
            int intTime = Mathf.RoundToInt(currentTime);
            text.text = intTime.ToString();
        }

        if(currentTime < (time * 0.7) && currentTime > (time * 0.4))
        {
            text.DOColor(Color.yellow, 0.6f);
        }
        else if(currentTime < (time * 0.4))
        {
            text.DOColor(Color.red, 0.2f);
        }
    }
    public void RestarTimer()
    {
        currentTime = time;
    }
}
