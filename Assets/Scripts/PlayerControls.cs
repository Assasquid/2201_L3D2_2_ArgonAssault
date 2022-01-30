using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControls : MonoBehaviour
{
    [Header("Speed")]
    [Tooltip("How fast the ship moves on the screen.")]
    [SerializeField] float throwSpeed = 19f;
    
    [Header("Ship Range")]
    [Tooltip("How far the ship can move on screen from left to right.")]
    [SerializeField] float xRange = 10f;
    [Tooltip("How far down the ship can move on screen.")]
    [SerializeField] float yTopRange = 4f;
    [Tooltip("How far up the ship can move on screen.")]
    [SerializeField] float yBottomRange = -7f;

    [Header("Ship Rotation : Pitch")]
    [Tooltip("Factor adapting the pitch (rotation on the x axis) " 
        + "of the ship to the position on screen.")]    
    [SerializeField] float positionPitchFactor = -4f;
    [Tooltip("Maximum rotation the ship can have at the top of the screen.")]
    [SerializeField] float maxTopPitch = -20f;
    [Tooltip("Maximum rotation the ship can have at the bottom of the screen.")]
    [SerializeField] float maxBottomPitch = 30f;
    [Tooltip("Pitch due to a control input.")]
    [SerializeField] float controlPitchFactor = -15f;

    [Header("Ship Rotation : Yaw")]
    [Tooltip("Factor adapting the pitch (rotation on the y axis) " 
        + "of the ship to the position on screen.")] 
    [SerializeField] float positionYawFactor = 3.9f;
    [Tooltip("Yaw due to a control input.")]
    [SerializeField] float controlYawFactor = 19f;

    [Header("Shop Rotation : Roll")]
    [Tooltip("Roll due to a control input. Roll is the ship's rotation on the z axis.")]
    [SerializeField] float controlRollFactor = -37f;

    [Header("Canons")]
    [Tooltip("Particle System for each canon.")]
    [SerializeField] GameObject[] lasers;

    Vector2 moveInput;
    float xThrow;
    float yThrow;
    bool isFiring;

    void Start()
    {
        isFiring = false;
    }

    void Update()
    {
        Move();
        ProcessFiring();
    }

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    void Move()
    {
        ProcessTranslation();
        ProcessRotation();
    }

    void OnFireStart()
    {
        isFiring = true;
    }

    void OnFireStop()
    {
        isFiring = false;
    }

    void ProcessTranslation()
    {
        xThrow = moveInput.x;
        yThrow = moveInput.y;

        float xOffset = xThrow * throwSpeed * Time.deltaTime;
        float rawXPos = transform.localPosition.x + xOffset;
        float clampedXPos = Mathf.Clamp(rawXPos, -xRange, xRange);

        float yOffset = yThrow * throwSpeed * Time.deltaTime;
        float rawYPos = transform.localPosition.y + yOffset;
        float clampedYPos = Mathf.Clamp(rawYPos, yBottomRange, yTopRange);

        transform.localPosition = new Vector3 
        (
            clampedXPos,
            clampedYPos,
            transform.localPosition.z
        );
    }

    void ProcessRotation()
    {
        float pitchDueToPositionRaw = transform.localPosition.y * positionPitchFactor;
        float pitchDueToPosition = Mathf.Clamp(pitchDueToPositionRaw, maxTopPitch, maxBottomPitch);
        float pitchDueToControls = yThrow * controlPitchFactor;

        float pitch = pitchDueToPosition + pitchDueToControls;
        
        float yawDueToPosition = transform.localPosition.x * positionYawFactor;
        float yawDueToControls = xThrow * controlYawFactor;

        float yaw = yawDueToPosition + yawDueToControls;

        float roll = xThrow * controlRollFactor;

        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }

    void ProcessFiring()
    {
        if (isFiring)
        {
            SetLasersActive(true);
        }
        else
        {
            SetLasersActive(false);
        }
    }

    void SetLasersActive(bool isActive)
    {
        foreach (GameObject laser in lasers)
        {
            var emissionModule = laser.GetComponent<ParticleSystem>().emission;
            emissionModule.enabled = isActive;
        }
    }
}
