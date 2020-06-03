using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerController : MonoBehaviour
{
    [Header("General")]
    [Tooltip("In ms^-1")] [SerializeField] float controlSpeed = 20f;
    [Tooltip("In m")] [SerializeField] float xRange = 15f;
    [Tooltip("In m")] [SerializeField] float yRange = 8f;
    // Start is called before the first frame update

    [Header("Screen Position Based")]
    [SerializeField] float positionPitchFactor = -2f;
    [SerializeField] float positionYawFactor = 2f;
    [SerializeField] GameObject [] guns;

    [Header("Control Throw Based")]
    [SerializeField] float controlPitchFactor = -15f;
    [SerializeField] float controlYawFactor = 5f;
    [SerializeField] float controlRollFactor = -40;

    float xThrow, yThrow;

    enum State { Alive, Dying, Transcending }
    State state = State.Alive;

    void Start()
    {
        
    }

    void playerDied() //called by string reference
    {
        state = State.Dying;
    }
    
    void Update()
    {   
        if (state != State.Alive) { return; }
        changeVerticalPosition();
        changeHorizontalPosition();
        ProcessRotation();
        ProcessFiring();
    }

    private void ProcessRotation()
    {
        float pitch = transformPitch();
        float yaw = transformYaw();
        float roll = transformRoll();

        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }

    private float transformRoll()
    {
        float rollDuetoControlThrow = xThrow * controlRollFactor;
        float roll = rollDuetoControlThrow;
        return roll;
    }

    private float transformYaw()
    {
        float yawDuetoPosition = transform.localPosition.x * positionYawFactor;
        float yawDuetoControlThrow = xThrow * controlYawFactor;
        float yaw = yawDuetoPosition + yawDuetoControlThrow;
        return yaw;
    }

    private float transformPitch()
    {
        float pitchDueToPosition = transform.localPosition.y * positionPitchFactor;
        float pitchDueToControlThrow = yThrow * controlPitchFactor;
        float pitch = pitchDueToPosition + pitchDueToControlThrow;
        return pitch;
    }

    private void changeHorizontalPosition()
    {
        yThrow = CrossPlatformInputManager.GetAxis("Vertical");
        float yOffset = yThrow * controlSpeed * Time.deltaTime * 5f;
        float rawYPos = transform.localPosition.y + yOffset;
        float clampedYPos = Mathf.Clamp(rawYPos, -yRange, yRange);

        transform.localPosition = new Vector3(transform.localPosition.x, clampedYPos, transform.localPosition.z);
    }

    private void changeVerticalPosition()
    {
        xThrow = CrossPlatformInputManager.GetAxis("Horizontal");
        float xOffset = xThrow * controlSpeed * Time.deltaTime * 5f;
        float rawXPos = transform.localPosition.x + xOffset;
        float clampedXPos = Mathf.Clamp(rawXPos, -xRange, xRange);

        transform.localPosition = new Vector3(clampedXPos, transform.localPosition.y, transform.localPosition.z);
    }

    private void ProcessFiring()
    {
        if (CrossPlatformInputManager.GetButton("Fire"))
        {
            ActivateGuns();
        }
        else
        {
            DeactivateGuns();
        }
    }

    private void ActivateGuns()
    {
        foreach (GameObject gun in guns)
        {
            gun.SetActive(true);
        }
        
    }

    private void DeactivateGuns()
    {
        foreach (GameObject gun in guns)
        {
            gun.SetActive(false);
        }
        
    }

  
}
