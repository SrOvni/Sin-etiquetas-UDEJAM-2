using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class LerpCamara : MonoBehaviour
{
    private void Update() {
        
        transform.DOMove(transform.GetComponentInParent<Transform>().position,.1f);
    }
}
