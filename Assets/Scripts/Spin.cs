using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spin : MonoBehaviour
{
    [SerializeField] float spinSpeed = 360f;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0, spinSpeed * Time.deltaTime);
    }
}
