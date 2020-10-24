using Assets.JakubGmur.Scripts;
using UnityEngine;

public class MovementForce : BaseMovementScript
{
    public float forceValueFactor = 10.0f;
    public float jumpForceValue = 10.0f;

    /// <summary>
    /// Element ktorym sterujemy przy pomocy manipulacji sila.
    /// </summary>
    public Rigidbody rigidBodyToControl;

    /// <summary>
    /// Grawitacja obiektu
    /// </summary>
    public float objectGravity = 9.8f;

    /// <summary>
    /// Flaga mowiaca o tym, czy logika zwiazana z ruchem i i pobieraniem wartosci od uzytkwonika powinna sie wykonywac.
    /// </summary>
    public bool movingLogicShouldExecute;

    /// <summary>
    /// Wektor sily dodawany do poruszanego obiektu.
    /// </summary>
    protected Vector3 indicatedForce = new Vector3(0, 0, 0);

    /// <summary>
    /// Zmienna mowiaca o tym, czy obiekt jest na ziemi czy w powietrzu.
    /// </summary>
    protected bool isGrounded;

    protected float horizontalValue, verticalValue;
    /// <summary>
    /// Metoda-Entry pobierajaca podpiety komponent.
    /// </summary>
    public virtual void Start()
    {
        rigidBodyToControl = GetComponent<Rigidbody>();
        rigidBodyToControl.freezeRotation = true;
    }

    /// <summary>
    /// Aktualizacja stanu obiektu - w metodzie Update() uruchamiana w trakcie jednego odswiezenia ramki.
    /// Aktualizacja FixedUpdate() moze natomiast dzialac raz, zero lub pare razy zaleznie od konfiguracji i 
    /// tego jak duza czestotliwosc.
    /// </summary>
    public virtual void FixedUpdate()
    {
        if (movingLogicShouldExecute)
        {
            if (isGrounded)
            {
                SetInputLocalFromAxis();
                MoveWithForce(horizontalValue, verticalValue);
            }

            rigidBodyToControl.AddForce(new Vector3(0, -objectGravity * rigidBodyToControl.mass, 0));
            isGrounded = false;
        }

    }

    private void MoveWithForce(float horizontalValue, float verticalValue)
    {
        GenerateVerticalForceVector(horizontalValue, verticalValue);

        Vector3 currentForce = rigidBodyToControl.velocity;
        Vector3 forceChange = indicatedForce - currentForce;
        forceChange.y = 0;

        IncludeJumpIfPressedOnMovingVector();
        IncludeHorizontalRotationIfPressed(horizontalValue);
        rigidBodyToControl.AddForce(forceChange, ForceMode.VelocityChange);
    }

    private void IncludeHorizontalRotationIfPressed(float horizontalValue)
    {
        Quaternion deltaRotation = Quaternion.Euler(new Vector3(0, horizontalValue, 0));
        rigidBodyToControl.MoveRotation(rigidBodyToControl.rotation * deltaRotation);
    }


    /// <summary>
    /// 
    /// </summary>
    public void SetInput(float horizontalValue, float verticalValue)
    {
        this.horizontalValue = horizontalValue;
        this.verticalValue = verticalValue;
    }

    public virtual void SetInputLocalFromAxis()
    {
        SetInput(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    }

    public virtual void IncludeJumpIfPressedOnMovingVector()
    {
        if (Input.GetButton("Jump"))
        {
            rigidBodyToControl.velocity = new Vector3(indicatedForce.x, jumpForceValue, indicatedForce.z);
        }
    }


    private void GenerateVerticalForceVector(float horizontalValue, float verticalValue)
    {
        indicatedForce = new Vector3(0.0f, 0.0f, verticalValue);
        indicatedForce = transform.TransformDirection(indicatedForce);//from local to world
        indicatedForce *= forceValueFactor;
    }

    void OnCollisionStay()
    {
        isGrounded = true;
    }

    public override void TurnOffInput()
    {
        movingLogicShouldExecute = false;
    }

    public override void TurnOnInput()
    {
        movingLogicShouldExecute = true;
    }

    public override void Disable()
    {
        enabled = false;
    }

    public override void Enable()
    {
        enabled = true;
    }

}
