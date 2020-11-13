using Assets.JakubGmur.Scripts;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace JakubGmur.Animations
{
    [RequireComponent(typeof(JGThirdPersonCharacter))]
    public class JGThirdPersonUserControl : BaseMovementScript
    {
        private JGThirdPersonCharacter m_Character;
        public GameObject ownerOfPointsLoader;
        public float limitAngle = 60;

        private GameObjectPointsLoader pointsLoader;

        private LinkedList<Vector3> targetPoints;
        private LinkedListNode<Vector3> currentTarget;

        public float movementSpeed = 0.5f;
        public float rotationSpeed = 0.5f;
        public bool shouldCrouch = false;
        public bool shouldJump = false;
        public bool infiniteWalking = true;
        public AudioSource walkingSound;

        private void Start()
        {
            targetPoints = new LinkedList<Vector3>();
            m_Character = GetComponent<JGThirdPersonCharacter>();
            pointsLoader = ownerOfPointsLoader.GetComponent<GameObjectPointsLoader>();

            if (pointsLoader == null)
            {
                Debug.Log($"Could not load GameObjectPointsLoader!!!!!");
                return;
            }

            foreach (var point in ownerOfPointsLoader.GetComponent<GameObjectPointsLoader>().GetPoints())
            {
                targetPoints.AddLast(point);
            }

            currentTarget = targetPoints.First;
        }

        // Fixed update is called in sync with physics
        private void FixedUpdate()
        {
            if (currentTarget != null)
            {
                Vector3 targetDirection = (currentTarget.Value - MyPosition).normalized * movementSpeed;
                var distanceToTarget = Vector3.Distance(m_Character.transform.position, currentTarget.Value);
                var angleBetweenMeAndTarget = Vector3.Angle(targetDirection, m_Character.transform.forward);

                //duza odleglosc && maly kat = poruszamy sie!
                if (distanceToTarget > 0.5f && angleBetweenMeAndTarget <= limitAngle)
                {
                    m_Character.Move(targetDirection, shouldCrouch, shouldJump);
                    PlayWalkingSound();
                }//duza odleglosc && duzy kat  = obracamy sie!
                else if(angleBetweenMeAndTarget >= limitAngle)
                {
                    StopWalkingSound();
                    m_Character.Move(new Vector3(0, 0, 0), shouldCrouch, shouldJump, false);
                    float singleStep = rotationSpeed * Time.deltaTime;
                    Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, singleStep, 0.0f);
                    transform.rotation = Quaternion.LookRotation(newDirection);
                }//mala odleglosc && maly kat = bierzemy nastepnego targeta i poruszamy sie!
                else 
                {
                    currentTarget = currentTarget.Next ?? currentTarget.List.First;
                }

                if (currentTarget.Next == null && !infiniteWalking)
                {
                    currentTarget = null;
                    m_Character.Move(new Vector3(0, 0, 0), shouldCrouch, shouldJump);
                    return;
                }

            }
        }

        void PlayWalkingSound()
        {
            if(walkingSound != null && !walkingSound.isPlaying)
            {
                walkingSound?.Play(0);
            }
        }

        void StopWalkingSound()
        {
            if (walkingSound != null && walkingSound.isPlaying)
            {
                walkingSound?.Stop();
            }
        }

        public override void Disable()
        {
            enabled = false;
        }

        public override void Enable()
        {
            enabled = true;
        }

        public override void TurnOffInput()
        {
            
        }

        public override void TurnOnInput()
        {
            
        }

        public Vector3 MyPosition
        {
            get
            {
                return m_Character.transform.position;
            }
        }
    }
}
