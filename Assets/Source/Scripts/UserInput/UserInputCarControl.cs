using System;
using UnityEngine;

public class UserInputCarControl : MonoBehaviour,IUserInputCarControl
{
    public event Action<bool> MotorPullingForwardChangeEvent;
    public event Action<bool> MotorPullingBackChangeEvent;
    public event Action<bool> BrakingActiveForwardChangeEvent;
    public event Action<bool> BrakingActiveBackChangeEvent;

    public event Action<TransmissionAngleState> TransmissionAngleChangeEvent;

    private bool _rotateLeft = false;
    private bool _rotateRight = false;

    private void Start()
    {
        UserInput.RegisterCallback(KeyCode.W, MotorPullingForwardChangeEvent);

        UserInput.RegisterCallback(KeyCode.W, BrakingActiveForwardChangeEvent);
        UserInput.RegisterCallback(KeyCode.S, BrakingActiveBackChangeEvent);

        UserInput.RegisterCallback(KeyCode.S, MotorPullingBackChangeEvent);

        UserInput.RegisterCallback(KeyCode.A, RotateTransmissionLeft);
        UserInput.RegisterCallback(KeyCode.D, RotateTransmissionRight);
    }

    private void RotateTransmissionLeft(bool value)
    {
        _rotateLeft = value;
        RotateTransmission();
    }

    private void RotateTransmissionRight(bool value)
    {
        _rotateRight = value;
        RotateTransmission();
    }

    private void RotateTransmission()
    {
        if (_rotateRight == _rotateLeft)
            TransmissionAngleChangeEvent?.Invoke(TransmissionAngleState.Forward);
        else
            if (_rotateRight == true)
                TransmissionAngleChangeEvent?.Invoke(TransmissionAngleState.TurnRight);
            else
                if (_rotateLeft == true)
                TransmissionAngleChangeEvent?.Invoke(TransmissionAngleState.TurnLeft);
    }
}
