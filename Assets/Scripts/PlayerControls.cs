using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControls : MonoBehaviour
{
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

        float offset = .01f;
        float newXPos = transform.localPosition.x + offset;

        transform.localPosition = new Vector3 
        (
            newXPos,
            transform.localPosition.y,
            transform.localPosition.z
        );
    }
}
