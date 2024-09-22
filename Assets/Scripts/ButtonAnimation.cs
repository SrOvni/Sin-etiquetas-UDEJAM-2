using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ButtonAnimation : MonoBehaviour
{
    [SerializeField] public Image _currentImage;

    [SerializeField] public Image _imagen1;

    [SerializeField] public Image _imagen2;

    [SerializeField] public float _timeToChange;


    private void Start()
    {
        StartCoroutine(ChangeImage1());
    }

    IEnumerator ChangeImage1()
    {
        _currentImage = _imagen1;
        yield return new WaitForSeconds(_timeToChange);
        StartCoroutine(ChangeImage2());
    }

    IEnumerator ChangeImage2()
    {
        _currentImage = _imagen2;
        yield return new WaitForSeconds(_timeToChange);
        StartCoroutine(ChangeImage1());
    }
}
