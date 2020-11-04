using Assets.Global;
using Assets.Helpers;
using UnityEngine;

public class SlidingDoorScript : MonoBehaviour
{
    public enum Openingdirection 
    { 
        X = 0, 
        Y = 1
    }

    protected enum Execution
    {
        None = 0,
        StartOpening = 1,
        StartClosing = 2
    }

    public Openingdirection direction = Openingdirection.Y;
    public float openDistance = 1.0f;
    public float openSpeed = 1.0f;
    public Transform doorObj;
    public AudioSource openingDoorsSound;
    public AudioSource closingDoorsSound;

    protected Execution _currentExecutionLoggic = Execution.None;
    private Vector3 defaultDoorPosition;
    private const int DelayForClosing = 3500;
    private bool canNowCloseDoor = true;

    void Start()
    {
        if (doorObj)
        {
            defaultDoorPosition = doorObj.localPosition;
        }
    }

    // Main function
    void Update()
    {
        if (!doorObj || _currentExecutionLoggic == Execution.None)
        {
            return;
        }

        float targetDistance = 0.0f;
        switch (_currentExecutionLoggic)
        {
            case Execution.StartOpening:
                targetDistance = openDistance;
                break;
            case Execution.StartClosing:
                targetDistance = 0.0f;
                break;
        }

        PlaySoundOnceIfExecutionStateChanged();

        switch (direction)
        {
            case Openingdirection.X:
                MoveDoorsOverX(targetDistance);
                break;
            case Openingdirection.Y:
                MoveDoorOverY(targetDistance);
                break;
        }

        if(_currentExecutionLoggic == Execution.StartClosing 
            && VectorComparer.AreEqual(doorObj.localPosition, defaultDoorPosition))
        {
            _currentExecutionLoggic = Execution.None;
        }
    }


    Execution _lastExecution = Execution.None;

    private void PlaySoundOnceIfExecutionStateChanged()
    {
        if (_lastExecution != _currentExecutionLoggic)
        {
            if (_currentExecutionLoggic == Execution.StartOpening)
            {
                openingDoorsSound?.Play();
            }
            else if (_currentExecutionLoggic == Execution.StartClosing)
            {
                closingDoorsSound?.Play();
            }
        }
        _lastExecution = _currentExecutionLoggic;
    }

    private void MoveDoorOverY(float newDistanceValue)
    {
        var newYPosition = Mathf.Lerp(doorObj.localPosition.y, defaultDoorPosition.y + newDistanceValue, Time.deltaTime * openSpeed);
        doorObj.localPosition = new Vector3(doorObj.localPosition.x, newYPosition, doorObj.localPosition.z);
    }

    private void MoveDoorsOverX(float newDistanceValue)
    {
        var newXPosition = Mathf.Lerp(doorObj.localPosition.x, defaultDoorPosition.x + newDistanceValue, Time.deltaTime * openSpeed);
        doorObj.localPosition = new Vector3(newXPosition, doorObj.localPosition.y, doorObj.localPosition.z);
    }

    protected virtual void HandleEnter(Collider other)
    {
        if (other.CompareTag(Tags.PlayableTag))
        {
            _currentExecutionLoggic = Execution.StartOpening;
        }
    }

    protected virtual void HandleExit(Collider other)
    {
        if (other.CompareTag(Tags.PlayableTag) && canNowCloseDoor)
        {
            canNowCloseDoor = false;
            Executor.PauseAndExecute(() =>
            {
                _currentExecutionLoggic = Execution.StartClosing;
                canNowCloseDoor = true;
            }, DelayForClosing);
        }
    }


    void OnTriggerEnter(Collider other)
    {
        HandleEnter(other);
    }

    void OnTriggerExit(Collider other)
    {
        HandleExit(other);
    }
}