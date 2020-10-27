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

    private enum Execution
    {
        None = 0,
        StartOpening = 1,
        StartClosing = 2
    }

    public Openingdirection direction = Openingdirection.Y;
    public float openDistance = 1.0f;
    public float openSpeed = 1.0f;
    public Transform doorObj;

    private Execution currentExecutionLoggic = Execution.None;
    private Vector3 defaultDoorPosition;

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
        if (!doorObj || currentExecutionLoggic == Execution.None)
        {
            return;
        }

        float targetDistance = 0.0f;
        switch (currentExecutionLoggic)
        {
            case Execution.StartOpening:
                targetDistance = openDistance;
                break;
            case Execution.StartClosing:
                targetDistance = 0.0f;
                break;
        }

        switch (direction)
        {
            case Openingdirection.X:
                MoveDoorsOverX(targetDistance);
                break;
            case Openingdirection.Y:
                MoveDoorOverY(targetDistance);
                break;
        }

        if(currentExecutionLoggic == Execution.StartClosing 
            && VectorComparer.AreEqual(doorObj.localPosition, defaultDoorPosition))
        {
            currentExecutionLoggic = Execution.None;
        }
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

    // Activate the Main function when Player enter the trigger area
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Tags.PlayableTag))
        {
            currentExecutionLoggic = Execution.StartOpening;
        }
    }

    private const int DelayForClosing = 3500;
    private bool canCloseDoor = true;
    // Deactivate the Main function when Player exit the trigger area
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(Tags.PlayableTag) && canCloseDoor)
        {
            canCloseDoor = false;
            Executor.PauseAndExecute(() =>
            {
                currentExecutionLoggic = Execution.StartClosing;
                canCloseDoor = true;
            }, DelayForClosing);
        }
    }
}