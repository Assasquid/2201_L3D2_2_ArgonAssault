using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControls : MonoBehaviour
{
    [SerializeField] float throwSpeed = 1f;
    Vector2 moveInput;

    void Update()
    {
        Move();
    }

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    void Move()
    {
        float xThrow = moveInput.x;

        float yThrow = moveInput.y;

        float xOffset = xThrow * throwSpeed * Time.deltaTime;
        float newXPos = transform.localPosition.x + xOffset;

        float yOffset = yThrow * throwSpeed * Time.deltaTime;
        float newYPos = transform.localPosition.y + yOffset;

        transform.localPosition = new Vector3 
        (
            newXPos,
            newYPos,
            transform.localPosition.z
        );
    }
}
