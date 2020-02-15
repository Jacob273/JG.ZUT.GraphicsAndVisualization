using UnityEngine;

public class MovementController : MonoBehaviour
{

    public GameObject objectToBeMoved;
    CharacterController characterController;

    public float movementSpeed = 5.5f;
    public float maxJumpValue = 5.5f;
    public float gravityEffectValue = 9.8f;
    public bool logicShouldExecute;
    public float rotationSpeed = 2.3f;

    private Vector3 moveDirection = new Vector3(0, 0, 0);
    private Vector3 rotationVector = new Vector3(0, 0, 0);

    // Start is called before the first frame update
    void Start()
    {
        if(objectToBeMoved != null)
        {
            characterController = objectToBeMoved.GetComponent<CharacterController>();
        }
        else
        {
            Debug.Log("MovementController will not work properly...objectToBeMoved has to be set");
        }
    }

    // Update is called once per frame
    void Update()
    {
        var isGrounded = characterController.isGrounded;
        if (logicShouldExecute)
        {
            if (isGrounded)
            {
                float horizontalValue, verticalValue;
                GetInput(out horizontalValue, out verticalValue);
                GenerateMovingVector(0, verticalValue);
                GenerateRotatingVector(horizontalValue);
                //Debug.Log(horizontalValue + "   " + verticalValue);
                IncludeJumpIfPressedOnMovingVector();
                moveDirection.y -= gravityEffectValue * Time.deltaTime;
            }
            moveDirection.y -= gravityEffectValue * Time.deltaTime;
            RotateSelf();
            Go();
        }

        if(!isGrounded)
        {
            GenerateRotatingVector(0);
            moveDirection.y -= gravityEffectValue * Time.deltaTime;
            RotateSelf();
            Go();
        }
    }

    private void Go()
    {
        characterController.Move(moveDirection * Time.deltaTime);
    }

    private static void GetInput(out float horizontalValue, out float verticalValue)
    {
        horizontalValue = Input.GetAxis("Horizontal");
        verticalValue = Input.GetAxis("Vertical");
    }

    private void GenerateMovingVector(float horizontalValue, float verticalValue)
    {
        moveDirection = new Vector3(0, 0.0f, verticalValue);//horizontal is 0 as we move only forward and backwards...
        moveDirection *= movementSpeed;
        moveDirection = transform.TransformDirection(moveDirection);
    }

    private void IncludeJumpIfPressedOnMovingVector()
    {
        //Debug.Log($"moving vector: {moveDirection.x} {moveDirection.y} {moveDirection.z}");
        if (Input.GetButton("Jump"))
        {
            moveDirection.y = maxJumpValue;
        }
    }


    private void GenerateRotatingVector(float horizontalValue)
    {
        int sign = 0;
        if (!Input.anyKey || horizontalValue == 0)
        {
            rotationVector = new Vector3(0, rotationSpeed * sign, 0);
            return;
        }

        if (horizontalValue < 0)
        {
            sign = -1;
            rotationVector = new Vector3(0, rotationSpeed * sign, 0);
            return;
        }

        if (horizontalValue > 0)
        {
            sign = 1;
            rotationVector = new Vector3(0, rotationSpeed * sign, 0);
            return;
        }
    }

    private void RotateSelf()
    {
        characterController.transform.Rotate(rotationVector , Space.Self);
    }
}
