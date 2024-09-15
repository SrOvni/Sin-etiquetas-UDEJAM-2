using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAround : MonoBehaviour
{
    [SerializeField] GameObject objetive;
    [SerializeField] float rotationSpeed = 5;

    private void Update() {
        transform.RotateAround(objetive.transform.position, new Vector3(0,0,1), Time.fixedDeltaTime * rotationSpeed);
    }
}
