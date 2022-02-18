using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPivot : MonoBehaviour
{
    [SerializeField] float _verticalRotationSpeed;
    [SerializeField] float _maxVerticalRotation;
    [SerializeField] float _minVerticalRotation;

    private void Update()
    {
        float rotationY = Input.GetAxis("Mouse Y") * _verticalRotationSpeed * Mathf.Deg2Rad;

        transform.Rotate(Vector3.right, -rotationY);

        Vector3 rot = transform.rotation.eulerAngles;
        float x = rot.x;

        if(x > 180f) x -= 360f;
        x = Mathf.Clamp(x, _minVerticalRotation, _maxVerticalRotation);

        rot.x = x;

        transform.eulerAngles = rot;

        transform.eulerAngles = new Vector3(transform.eulerAngles.x,transform.eulerAngles.y, 0);
    }
}
