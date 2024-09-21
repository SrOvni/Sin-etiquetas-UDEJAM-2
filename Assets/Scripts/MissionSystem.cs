using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class MissionSystem : MonoBehaviour
{
    private List<string> _currentMissions; 

    [SerializeField]
    private GameObject _panelMission;

    private int _currentIndex = 0;
    public void AddMissions(string mission)
    {
        _currentMissions.Add(mission);
        _currentIndex++;
        Debug.Log("Mision agregada: " + mission);
        UpdateUI();
    }

    private void Start()
    {
        _currentMissions = new List<string>(3);
    }
    public void DeleteMissions(int index)
    {
        _currentMissions.RemoveAt(index);
        _currentIndex--;
        Debug.Log("Mision borrada");
        UpdateUI();
    }

    public int CheckCurrentIndex()
    {
        return _currentIndex;
    }
    
    private void UpdateUI()
    {
        foreach (Transform child in _panelMission.transform)
        {
            Destroy(child.gameObject);
        }

  
        foreach (string textContent in _currentMissions)
        {
            GameObject newTextObject = new GameObject("DynamicText");

            Text newText = newTextObject.AddComponent<Text>();

            newText.text = _currentMissions.ToString();
            
            newText.fontSize = 24;  
            newText.alignment = TextAnchor.MiddleCenter;  
            newText.color = Color.black;  

            RectTransform rectTransform = newText.GetComponent<RectTransform>();
            rectTransform.SetParent(_panelMission.transform);  
            rectTransform.localScale = Vector3.one;    
            rectTransform.sizeDelta = new Vector2(200, 50);  
        }
    }
}
