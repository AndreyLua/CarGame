using UnityEngine;

public class Engine : MonoBehaviour
{
    [SerializeField] private float _startedForce;
    [SerializeField] private float _forceAcceleration;
    [SerializeField] private float _accelerationStartDelay = 3;

    private float _accelerationTimer;
    private bool _accelerationStarted;

    private float _force;
    private bool _isPull;

    public float Force => _force;

    private void Awake()
    {
        _accelerationTimer = _accelerationStartDelay;
    }

    private void Update()
    {
        TimerUpdate();
        ForceUpdate();
    }

    private void ForceUpdate()
    {
        if (_accelerationStarted)
            _force += _force*_forceAcceleration * Time.deltaTime;
    }

    private void TimerUpdate()
    {
        if (_isPull)
        {
            if (_accelerationTimer < 0)
                _accelerationStarted = true;
            else
                _accelerationTimer -= Time.deltaTime;
        }
        else
        {
            _accelerationStarted = false;
            _accelerationTimer = _accelerationStartDelay;
        }
    }

    public void OnMotorPullingChange(bool state)
    {
        _isPull = state;
        if (_isPull)
            _force = _startedForce;
        else
        {
            _force = 0;
        }
    }

}