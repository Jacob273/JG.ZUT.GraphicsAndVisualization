using UnityEngine;

public class MovementForce : MonoBehaviour
{
    public float forceValueFactor = 10.0f;
    public float jumpForceValue = 10.0f;

    public Rigidbody rigidBody;
    public float gravity = 9.8f;
    public bool logicShouldExecute;


    private Vector3 indicatedForce = new Vector3(0, 0, 0);
    private bool isGrounded;
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        rigidBody.freezeRotation = true;
    }

    /// <summary>
    /// Update runs once per frame. 
    /// FixedUpdate can run once, zero, or several times per frame, depending on how many physics 
    /// frames per second are set in the time settings, and how fast/slow the framerate is.
    /// </summary>
    void FixedUpdate()
    {
        if (logicShouldExecute)
        {
            if (isGrounded)
            {
                float horizontalValue, verticalValue;
                GetInput(out horizontalValue, out verticalValue);

                GenerateVerticalForceVector(horizontalValue, verticalValue);

                Vector3 currentForce = rigidBody.velocity;
                Vector3 forceChange = indicatedForce - currentForce;
                forceChange.y = 0;

                //Debug.Log($"we_want_x::::{indicatedForce.x} we_want_y::::{indicatedForce.y} we_want_z:::{indicatedForce.z}");
                //Debug.Log($"curr_x::::{currentForce.x} curr_y::::{currentForce.y} curr_z:::{currentForce.z}");
                //Debug.Log($"we_set_x::::{forceChange.x} we_set_y::::{forceChange.y} we_set_z:::{forceChange.z}");

                IncludeJumpIfPressedOnMovingVector();
                IncludeHorizontalRotationIfPressed(horizontalValue);
                rigidBody.AddForce(forceChange, ForceMode.VelocityChange);
            }

            rigidBody.AddForce(new Vector3(0, -gravity * rigidBody.mass, 0));
            isGrounded = false;
        }

    }

    private void IncludeHorizontalRotationIfPressed(float horizontalValue)
    {
        Quaternion deltaRotation = Quaternion.Euler(new Vector3(0, horizontalValue, 0));
        rigidBody.MoveRotation(rigidBody.rotation * deltaRotation);
    }

    private static void GetInput(out float horizontalValue, out float verticalValue)
    {
        horizontalValue = Input.GetAxis("Horizontal");
        verticalValue = Input.GetAxis("Vertical");
    }


    private void GenerateVerticalForceVector(float horizontalValue, float verticalValue)
    {
        indicatedForce = new Vector3(0.0f, 0.0f, verticalValue);
        indicatedForce = transform.TransformDirection(indicatedForce);//from local to world
        indicatedForce *= forceValueFactor;
    }

    private void IncludeJumpIfPressedOnMovingVector()
    {
        if (Input.GetButton("Jump"))
        {
            rigidBody.velocity = new Vector3(indicatedForce.x, jumpForceValue, indicatedForce.z);
        }
    }

    void OnCollisionStay()
    {
        isGrounded = true;
    }
}
