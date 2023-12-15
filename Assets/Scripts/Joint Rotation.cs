using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JointRotation : MonoBehaviour
{

    public float speed = 2.3f;
    public float amplitude = 3;

    private float _timer;

    private void Update()
    {
        _timer += Time.deltaTime;
        transform.localRotation = Quaternion.Euler(0, 0,Mathf.Sin(speed * _timer) * amplitude); 
    }

}
