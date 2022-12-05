using UnityEngine;

public class Car : MonoBehaviour
{
    [SerializeField] private Engine _engine;
    [SerializeField] private Transmission _transmission;
    [SerializeField] private UserInputCarControl _userInputCarControl;

    private Rigidbody _body;
    private float _pastAngularSpeed;
    private float _angularAcceleration;
    private CarDirection _carDirection;
    private bool _isCarDirectionForward;
    private bool _isCarDirectionBack;

    public Rigidbody Body => _body;

    private void Awake()
    {
        _userInputCarControl.MotorPullingForwardChangeEvent += IsCarDirectionForward;
        _userInputCarControl.MotorPullingBackChangeEvent += IsCarDirectionBack;

        _userInputCarControl.TransmissionAngleChangeEvent += _transmission.OnTransmissionAngleStateChange;

        _userInputCarControl.BrakingActiveForwardChangeEvent += OnBrakingActiveForwardChange;
        _userInputCarControl.BrakingActiveBackChangeEvent += OnBrakingActiveBackChange;

        _body = gameObject.GetComponent<Rigidbody>();
        _body.centerOfMass = _transmission.CenterMass.position;
    }

    private void Update()
    {
        Move();    
    }

    private void OnBrakingActiveForwardChange(bool value)
    {
        if (_carDirection == CarDirection.Back)
            _transmission.OnBrakingActiveChange(value);
    }

    private void OnBrakingActiveBackChange(bool value)
    {
        if (_carDirection == CarDirection.Forward)
            _transmission.OnBrakingActiveChange(value);
    }

    private void IsCarDirectionForward(bool value)
    {
        _isCarDirectionForward = value;
    }

    private void IsCarDirectionBack(bool value)
    {
        _isCarDirectionBack = value;
    }

    private void ChangeCarDirection()
    {
        CarDirection oldCarDirection = _carDirection;
        if (_body.velocity.magnitude < 0.2f)
        {
            if (_isCarDirectionForward)
                _carDirection = CarDirection.Forward;

            if (_isCarDirectionBack)
                _carDirection = CarDirection.Back;
        }
        if (oldCarDirection != _carDirection)
            _transmission.ResetBrakingState();
        EnginePull();
    }

    private void EnginePull()
    {
        if (_carDirection == CarDirection.Forward)
            _engine.OnMotorPullingChange(_isCarDirectionForward);

        if (_carDirection == CarDirection.Back)
            _engine.OnMotorPullingChange(_isCarDirectionBack);
    }

    private void Move()
    {
        ChangeCarDirection();
        _angularAcceleration  = _pastAngularSpeed - _body.angularVelocity.y;
        _transmission.TransferPowerToWheels(_engine.Force* (int)_carDirection);

        _body.AddForceAtPosition(_transmission.RearFrictionForce * _angularAcceleration*2,
        _transmission.CenterMass.position - transform.forward * _transmission.Length / 2, ForceMode.Force);

        _pastAngularSpeed = _body.angularVelocity.y;
    }
}
