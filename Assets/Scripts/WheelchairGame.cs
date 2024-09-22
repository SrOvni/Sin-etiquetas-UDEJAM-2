using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.Events;
using DG.Tweening;

public class WheelchairGame : MonoBehaviour
{
    [SerializeField] public bool _wheelChairGameIsCompleted = false;

    [SerializeField] private float _totalAmountValue = 100;

    [SerializeField] private float _onKeyPreseedValue = 10;

    [SerializeField] private float _currentValue;

    [SerializeField] private float _decrementValueSpeed = 10;

    [SerializeField] private Timer _timer;

    [SerializeField] private GameObject _canvasGame;

    [SerializeField] private GameObject _sliderCanvas;

    [SerializeField] private Slider _slider;

    [SerializeField] private GameObject _canvasWin;

    [SerializeField] private GameObject _canvasLose;

    [SerializeField] private bool _gameIsStarted = false;

    [SerializeField] public UnityEvent OnPreesKey;

    [SerializeField] public UnityEvent OnCompleteGame;

    [SerializeField] public UnityEvent OnRestarGame;

    [Header("Animation")]

    [SerializeField] Vector3 scaleUpSize; 
    public float rotateAmount = 15f;  
    public float animationDuration = 0.1f;  
    private Vector3 originalScale;
    private Quaternion originalRotation;


    private void Start()
    {
        _slider.maxValue = _totalAmountValue;
        _slider.minValue = 0;
        originalScale = _sliderCanvas.gameObject.transform.localScale;
        originalRotation = _sliderCanvas.gameObject.transform.localRotation;
        DOTween.SetTweensCapacity(200, 200);
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
        _canvasGame.SetActive(true);      
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
        _timer.start = false;
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

            OnkeyPressedAnimation();

            if (_currentValue >= _totalAmountValue)
            {
                _gameIsStarted = false;
                WinOrLoseGame(true);               
            }
        }
    }

    void OnkeyPressedAnimation()
    {
       
        _sliderCanvas.transform.DOKill(true);

        Sequence feedbackSequence = DOTween.Sequence();

        feedbackSequence
          .Append(_sliderCanvas.transform.DOScale(scaleUpSize, animationDuration).SetEase(Ease.OutQuad)) 
          .Join(_sliderCanvas.transform.DORotate(Vector3.forward * rotateAmount, animationDuration / 2).SetEase(Ease.InOutSine)) 
          .Append(_sliderCanvas.transform.DORotate(Vector3.forward * -rotateAmount, animationDuration / 2).SetEase(Ease.InOutSine)) 
          .Append(_sliderCanvas.transform.DOScale(originalScale, animationDuration).SetEase(Ease.OutQuad))  
          .Append(_sliderCanvas.transform.DORotateQuaternion(originalRotation, animationDuration).SetEase(Ease.OutQuad));   
    }

    public IEnumerator CanvasLoseAnimation()
    {
        _canvasLose.SetActive(true);
        yield return new WaitForSeconds(3);
        _canvasLose.SetActive(false);
        _canvasGame.SetActive(false);
        RestartWheelChairGame();
    }

    public IEnumerator CanvasWinAnimation()
    {
        _canvasWin.SetActive(true);
        yield return new WaitForSeconds(3);
        _canvasWin.SetActive(false);
        _canvasGame.SetActive(false);
        RestartWheelChairGame();
    }
}
