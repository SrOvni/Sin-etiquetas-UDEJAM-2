using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.Events;

public class WheelchairGame : MonoBehaviour
{
    [SerializeField] public bool _wheelChairGameIsCompleted = false;

    [SerializeField] private float _totalAmountValue = 100;

    [SerializeField] private float _onKeyPreseedValue = 10;

    [SerializeField] private float _currentValue;

    [SerializeField] private float _decrementValueSpeed = 10;

    [SerializeField] private Timer _timer;

    [SerializeField] private GameObject _sliderCanvas;

    [SerializeField] private Slider _slider;

    [SerializeField] private GameObject _canvasWin;

    [SerializeField] private GameObject _canvasLose;

    [SerializeField] private bool _gameIsStarted = false;

    [SerializeField] public UnityEvent OnPreesKey;

    [SerializeField] public UnityEvent OnCompleteGame;

    [SerializeField] public UnityEvent OnRestarGame;


    private void Start()
    {
        _slider.maxValue = _totalAmountValue;
        _slider.minValue = 0;
    }
    void Update()
    {
        if( _gameIsStarted)
        {
            _timer.start = true;
            StartWheelChairGame();
        }

        if(_timer.CurrentTime <= 0f && _gameIsStarted)
        {
            _gameIsStarted = false;
            _timer.start = false;
            _timer.RestarTimer();
            WinOrLoseGame(false);
        }
    }

    void WinOrLoseGame(bool result)
    {
        if (result)
        {
            OnCompletedGame();          
        }
        else
        {
            LoseGame();          
        }
    }

    public void OnCompletedGame()
    {
        StartCoroutine(CanvasWinAnimation());
        _wheelChairGameIsCompleted =true;
        OnCompleteGame.Invoke();
    }

    public void StartGame()
    {      
        _gameIsStarted = true;
        _sliderCanvas.SetActive(true);       
    }

    public void LoseGame()
    {
        StartCoroutine(CanvasLoseAnimation());
    }
    public void RestartWheelChairGame()
    {
        OnRestarGame.Invoke();
        _gameIsStarted = false;
        _timer.CurrentTime = 0;
        _currentValue = 0;
        _slider.value = _currentValue;
        _sliderCanvas.SetActive(false);      
    }

    private void DecrementValue()
    {
        if(_currentValue >= 0)
        {
            _currentValue -= _decrementValueSpeed * Time.deltaTime;
        }      
    }

    public void StartWheelChairGame()
    {
        _slider.value = _currentValue;

        DecrementValue();

        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            _currentValue += _onKeyPreseedValue;

            OnPreesKey.Invoke();

            if (_currentValue >= _totalAmountValue)
            {
                _gameIsStarted = false;
                WinOrLoseGame(true);               
            }
        }
    }

    public IEnumerator CanvasLoseAnimation()
    {
        _canvasLose.SetActive(true);
        yield return new WaitForSeconds(3);
        _canvasLose.SetActive(false);
        RestartWheelChairGame();
    }

    public IEnumerator CanvasWinAnimation()
    {
        _canvasWin.SetActive(true);
        yield return new WaitForSeconds(3);
        _canvasWin.SetActive(false);
        RestartWheelChairGame();
    }
}
