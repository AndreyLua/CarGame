using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedPlatform : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Car car = other.gameObject.GetComponent<Car>();
        if (car)
        {
            Debug.Log("dlkflkdf");
            car.Body.velocity += transform.forward * car.Body.velocity.magnitude * 0.15f;
        }
    }
}
