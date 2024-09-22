using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonAnimation : MonoBehaviour
{
    [SerializeField] public Image _currentImage;

    [SerializeField] public Sprite _imagen1;

    [SerializeField] public Sprite _imagen2;

    [SerializeField] public float _timeToChange;


    private void Start()
    {
        StartCoroutine(ChangeImage1());
    }

    IEnumerator ChangeImage1()
    {
        _currentImage.sprite = _imagen1;
        yield return new WaitForSeconds(_timeToChange);
        StartCoroutine(ChangeImage2());
    }

    IEnumerator ChangeImage2()
    {
        _currentImage.sprite = _imagen2;
        yield return new WaitForSeconds(_timeToChange);
        StartCoroutine(ChangeImage1());
    }
}
