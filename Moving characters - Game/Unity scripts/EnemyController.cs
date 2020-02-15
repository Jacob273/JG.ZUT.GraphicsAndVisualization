using System;
using UnityEngine;

public class EnemyController : MovementForce
{

    public GameObject objectToTrack;

    public int radiusToDetectOpponent = 5;
    private float distanceToObject = -1.0f;
    private float rotationSpeed = 1.0f;

    private event EventHandler<(bool detected, GameObject target)> enemyDetected;

    public Vector3 MyPosition
    {
        get
        {
            return this.transform.position;
        }
    }

    public override void Start()
    {
        base.Start();
        enemyDetected += OnEnemyDetected;
    }

    public void Update()
    {
        distanceToObject = Vector3.Distance(objectToTrack.transform.position, MyPosition);

        if (distanceToObject <= radiusToDetectOpponent)
        {
            enemyDetected.Invoke(this, (detected: true,  target: objectToTrack));
        }
        else
        {
            enemyDetected.Invoke(this, (detected: false, target: objectToTrack));
        }
    }

    private void OnEnemyDetected(object sender, (bool detected, GameObject target) e)
    {
        if (!e.detected)
        {
            //nie poruszamy sie
            SetInput(0, 0);
            return;
        }

        Vector3 targetDirection = e.target.transform.position - MyPosition;
        var angleBetweenMeAndTarget = Vector3.Angle(targetDirection, transform.forward);
        var distance = Vector3.Distance(targetDirection, transform.forward);

        if (angleBetweenMeAndTarget < 10 && distance > 5)
        {
            SetInput(1, 1);
        }
        else
        {
            SetInput(0, 0);
            float step = rotationSpeed * Time.deltaTime;
            Vector3 newDirectionIncludingSpeed = Vector3.RotateTowards(transform.forward, targetDirection, step, 0.0f);
            transform.rotation = Quaternion.LookRotation(newDirectionIncludingSpeed);
        }
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    public override void SetInputLocalFromAxis()
    {
        //no implementation
        //we're not using buttons to control movement
    }

    public override void IncludeJumpIfPressedOnMovingVector()
    {
        //no implementation
        //we're not using buttons to control jumping
    }
}
