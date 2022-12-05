using System;

public interface IUserInputCarControl
{
    public event Action<bool> MotorPullingForwardChangeEvent;

    public event Action<bool> BrakingActiveForwardChangeEvent;
    public event Action<bool> BrakingActiveBackChangeEvent;

    public event Action<bool> MotorPullingBackChangeEvent;
    public event Action<TransmissionAngleState> TransmissionAngleChangeEvent;
}