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

    [SerializeField] DialogueInteractions _dialogueInteractions;

    [SerializeField] private Timer _timer;

    [SerializeField] private GameObject _canvasGame;

    [SerializeField] private GameObject _sliderCanvas;

    [SerializeField] private Slider _slider;

    [SerializeField] private Image _fillSlider;

    [SerializeField] private GameObject _canvasWin;

    [SerializeField] private GameObject _canvasLose;

    [SerializeField] private bool _gameIsStarted = false;

    [SerializeField] public UnityEvent OnPreesKey;

    [SerializeField] public UnityEvent OnStartGame;

    [SerializeField] public UnityEvent OnCompleteGame;

    [SerializeField] public UnityEvent OnRestarGame;

    [SerializeField] WinTheGame _win;


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
            DecrementValue();
            _slider.value = _currentValue;
            _dialogueInteractions.Movement.enabled = false;           
        }

        if(_timer.CurrentTime <= 0f && _gameIsStarted)
        {
            _gameIsStarted = false;
            _timer.start = false;
            _timer.RestarTimer();
            _currentValue = 0;
            _slider.value = _currentValue;
            _dialogueInteractions.Movement.enabled = true;
            _canvasGame.SetActive(false);
            OnRestarGame.Invoke();
            LoseGame();
        }

        UpdateSliderColor();
    }

    void UpdateSliderColor()
    {
        if(_slider.value >= (_totalAmountValue * 0.3f) && _slider.value <= (_totalAmountValue * 0.5f))
        {
            _fillSlider.DOColor(Color.yellow, 0.2f);
        }
        else if(_slider.value >= (_totalAmountValue * 0.51f) && _slider.value <= (_totalAmountValue * 0.7f))
        {
            _fillSlider.DOColor(Color.magenta, 0.2f);
        }
        else if (_slider.value >= (_totalAmountValue * 0.71f))
        {
            _fillSlider.DOColor(Color.red, 0.2f);
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
        if (!_wheelChairGameIsCompleted)
        {
            _gameIsStarted = true;
            OnStartGame.Invoke();
            _canvasGame.SetActive(true);
        }
        else
        {
            OnCompleteGame.Invoke();
        }
    }

    public void LoseGame()
    {
        StartCoroutine(CanvasLoseAnimation());
    }
    public void RestartWheelChairGame()
    {
        
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
        if (_gameIsStarted)
        {
            _currentValue += _onKeyPreseedValue;

            OnPreesKey.Invoke();

            OnkeyPressedAnimation();

            if (_currentValue >= _totalAmountValue)
            {
                _gameIsStarted = false;
                _timer.start = false;
                _timer.RestarTimer();
                _currentValue = 0;
                _slider.value = _currentValue;
                _dialogueInteractions.Movement.enabled = true;
                _canvasGame.SetActive(false);
                _win._silla = true;
                OnCompletedGame();
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
    }

    public IEnumerator CanvasWinAnimation()
    {
        _canvasWin.SetActive(true);
        yield return new WaitForSeconds(3);
        _canvasWin.SetActive(false);      
    }
}
