using UnityEngine.EventSystems;
using UnityEngine;

[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour {
    
    public float speed = 5f;
    [SerializeField]
    [Range(1,10)]
    private float HorizontalSenstivity = 3f;
    [SerializeField]
    [Range(1, 10)]
    private float VerticalSenstivity = 3f;
    
    private PlayerMotor motor;

    private void Start()
    {
        motor = GetComponent<PlayerMotor>();
    }

    private void Update()
    {
        if (InventoryUI.IsOn) {
            if (Cursor.lockState != CursorLockMode.None)
            
                Cursor.lockState = CursorLockMode.None;

            motor.Move(Vector3.zero);
            motor.Rotate(Vector3.zero);
            motor.RotateCamera(0f);
            return;
            
        }
        if (Cursor.lockState!= CursorLockMode.Locked)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        //movement
        float _xMov = Input.GetAxisRaw("Horizontal");
        float _zMov = Input.GetAxisRaw("Vertical");

        Vector3 _movHorizontal = transform.right * _xMov;
        Vector3 _movVertical = transform.forward * _zMov;

        Vector3 _velocity = (_movHorizontal + _movVertical).normalized * speed;

        motor.Move(_velocity);


        //looking horizontal
        float _yRot = Input.GetAxisRaw("Mouse X");

        Vector3 _rotation = new Vector3(0, _yRot, 0)* HorizontalSenstivity;

        motor.Rotate(_rotation);

        //looking Vertical 80 to -60

        float _xRot = Input.GetAxisRaw("Mouse Y");

        float _cameraRotation =_xRot * VerticalSenstivity;

        motor.RotateCamera(_cameraRotation);

        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

    }
}
