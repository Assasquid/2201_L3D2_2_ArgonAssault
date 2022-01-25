using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControls : MonoBehaviour
{
    [Header("Speed")]
    [SerializeField] float throwSpeed = 19f;
    
    [Header("Ship Range")]
    [SerializeField] float xRange = 50f;
    [SerializeField] float yTopRange = 4f;
    [SerializeField] float yBottomRange = -7f;

    [Header("Ship Rotation : Pitch")]
    [SerializeField] float positionPitchFactor = -4f;
    [SerializeField] float maxTopPitch = -20f;
    [SerializeField] float maxBottomPitch = 30f;
    [SerializeField] float controlPitchFactor = -15f;

    [Header("Ship Rotation : Yaw")]
    [SerializeField] float positionYawFactor = 3.9f;
    [SerializeField] float controlYawFactor = 19f;

    [Header("Shop Rotation : Roll")]
    [SerializeField] float controlRollFactor = -37f;

    [Header("Canons")]
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
