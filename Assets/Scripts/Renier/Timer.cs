using TMPro;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;

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
    }
    public void RestarTimer()
    {
        currentTime = time;
    }
}
