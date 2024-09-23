using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] [Range(0,2)] float timeScale = 1;
    private void Update() {
        Time.timeScale = timeScale;
    }
}
