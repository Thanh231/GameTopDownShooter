using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    Vector3 test;
    private void Awake()
    {
        test = transform.up;
        test.Normalize();

        Debug.Log(transform.position);

    }
    private void Update()
    {
        Debug.Log(transform.up);
        transform.Translate(test * 10 * Time.deltaTime);
    }
}
