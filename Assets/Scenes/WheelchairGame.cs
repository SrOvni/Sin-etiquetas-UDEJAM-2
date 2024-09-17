using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class WheelchairGame : MonoBehaviour
{
    [SerializeField] public bool _wheelChairGameIsCompleted = false;

    [SerializeField] private float _totalAmountValue = 100;

    [SerializeField] private float _onKeyPreseedValue = 10;

    [SerializeField] private float _currentValue;

    [SerializeField] private float _decrementValueSpeed = 10;

    [SerializeField] private float _timeToCompletedGame = 10;

    [SerializeField] private GameObject _sliderCanvas;

    [SerializeField] private Slider _slider;

    [SerializeField] private GameObject _canvasWin;

    [SerializeField] private GameObject _canvasLose;

    [SerializeField] private bool _gameIsStarted = false;

    public bool _temporalActivator = false;

    private void Start()
    {
        _slider.maxValue = _totalAmountValue;
        _slider.minValue = 0;
    }
    void Update()
    {
        if(_temporalActivator == true && _gameIsStarted == false)
        {
            StartWheelChairGame();
        }

        if (_gameIsStarted)
        {
            _slider.value = _currentValue;

            DecrementValue();
         
            if (Keyboard.current.spaceKey.wasPressedThisFrame)
            {
                _currentValue += _onKeyPreseedValue;

                if(_currentValue >= _totalAmountValue)
                {
                    OnCompletedLevel();
                }
            }
        }
    }

    public void OnCompletedLevel()
    {
        _wheelChairGameIsCompleted=true;
        StartCoroutine(CanvasWinAnimation());
        RestartWheelChairGame();
    }

    public void StartWheelChairGame()
    {
        _gameIsStarted = true;
        _sliderCanvas.SetActive(true);
        StartCoroutine(TimeToPreesKey());
    }

    public void RestartWheelChairGame()
    {
        _gameIsStarted = false;
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

    public IEnumerator TimeToPreesKey()
    {
        yield return new WaitForSeconds(_timeToCompletedGame);
        RestartWheelChairGame();
        if(_wheelChairGameIsCompleted == false)
        {
            StartCoroutine(CanvasLoseAnimation());
        }
    }

    public IEnumerator CanvasLoseAnimation()
    {
        _canvasLose.SetActive(true);
        yield return new WaitForSeconds(3);
        _canvasLose.SetActive(false);
    }

    public IEnumerator CanvasWinAnimation()
    {
        _canvasWin.SetActive(true);
        yield return new WaitForSeconds(3);
        _canvasWin.SetActive(false);
    }
}
