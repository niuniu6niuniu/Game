using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSway : MonoBehaviour
{
    // How much the sway
    public float amount;
    // How fast the sway
    public float smoothAmount;
    public float maxAmount;
    // Start position
    private Vector3 initialPosition;

    // Start is called before the first frame update
    void Start()
    {
        initialPosition = transform.localPosition; 
    }

    // Update is called once per frame
    void Update()
    {
        // (Camera) Mouse movement X and Y
        float movementX = -Input.GetAxis("Mouse X") * amount;
        float movementY = -Input.GetAxis("Mouse Y") * amount;
        // Limit the sway movement 
        movementX = Mathf.Clamp(movementX, -maxAmount, maxAmount);
        movementY = Mathf.Clamp(movementY, -maxAmount, maxAmount);
        // Weapon final position
        Vector3 finalPosition = new Vector3(movementX, movementY, 0);
        // Move from initial position to final position
        transform.localPosition = Vector3.Lerp(transform.localPosition, finalPosition + initialPosition, Time.deltaTime * smoothAmount);
    }
}
