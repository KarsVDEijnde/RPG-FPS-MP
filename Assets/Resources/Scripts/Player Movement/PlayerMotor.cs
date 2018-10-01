using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMotor : MonoBehaviour {

    [SerializeField]
    private Camera cam;

    private Vector3 jump;

    public Vector3 lastPosition;

    public Vector3 velocity = Vector3.zero;
    private Vector3 rotation = Vector3.zero;
    private float CameraRotation = 0f;

    public float jumpForce = 20f;
    private int detectionDelay;

    private Rigidbody rb;
    PlayerAnimator anim;

    public bool isGrounded;

    public GameObject pressE;

    private void Start()
    {
        anim = GetComponent<PlayerAnimator>();
        lastPosition = transform.position;
        rb = GetComponent<Rigidbody>();
        jump = new Vector3(0.0f, 2.0f, 0.0f);
    }
    // tag every floor as floor
    void OnCollisionStay(Collision other)
    {
        if (detectionDelay == 0)
        {
            if (other.collider.CompareTag("Floor"))
            {
                isGrounded = true;
                anim.SetGround(isGrounded);
            }
        }
    }

    //Assigning Player Movement Values
    public void Move(Vector3 _velocity)
    {
        velocity = _velocity;
    }

    public void Rotate(Vector3 _rotation)
    {
        rotation = _rotation;
    }

    public void RotateCamera(float _cameraRotation)
    {
        CameraRotation = _cameraRotation;
    }

    //Executing Player Movement Methods 
    private void FixedUpdate()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            PerformJump();
        }
        PerformMovement();
        PerformRotation();
        if (detectionDelay > 0)
        {
            detectionDelay --;
        }
        lastPosition = transform.position;
    }

    private void Update()
    {
       
        RaycastHit Hit;
        Ray ray = cam.ScreenPointToRay(new Vector2(Screen.width / 2f, Screen.height / 2f));
        if (Physics.Raycast(ray, out Hit, 1) && Hit.collider.CompareTag("Interactable"))
        {
            pressE.SetActive(true);
            if (Input.GetKeyUp(KeyCode.E))
            {
                //Play animation Interact
                Hit.collider.gameObject.GetComponent<ItemPickUp>().Interact();

            }

        }
        else
        {
            pressE.SetActive(false);
        }
    }

    //Player Movement Calculation Methods
    void PerformMovement()
    {
       // if (velocity != Vector3.zero)
        {
            rb.MovePosition(rb.position + velocity * (Input.GetButton("Walk")? 0.33f : 1) * Time.fixedDeltaTime);
        }
    }
    void PerformRotation()
    {
        rb.MoveRotation(rb.rotation * Quaternion.Euler(rotation));
        if (cam != null) {
            CameraRotation = -CameraRotation;
            Vector2 boundaries = new Vector2(-60f, 80f);
            float camAngle = cam.transform.eulerAngles.x - (cam.transform.eulerAngles.x > 180 ? 360f : 0f);
            if (CameraRotation > 0f && camAngle + CameraRotation > boundaries[1])
                CameraRotation = 0f;
            if (CameraRotation < 0f && camAngle + CameraRotation < boundaries[0])
                CameraRotation = 0f;
            cam.transform.Rotate(new Vector3(CameraRotation, 0, 0));
        }
    }
    // jump by adding a force upwards then we set the floor check on false
    void PerformJump()
    {
        detectionDelay = 5;
        rb.AddForce(jump * jumpForce, ForceMode.Impulse);
        isGrounded = false;
        anim.SetGround(isGrounded);
        anim.SetJump();
    }
}
