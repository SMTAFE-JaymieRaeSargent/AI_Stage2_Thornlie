namespace Player
{
    using UnityEngine;

    public class Movement : MonoBehaviour
    {
        [SerializeField] Vector3 _moveDirection = Vector3.zero;
        [SerializeField] CharacterController _characterController;
        [SerializeField] float _moveSpeed;
        [SerializeField] float _walk = 5, _run = 10, _crouch = 2.5f, _gravity = 20, _jump = 10;
        
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            _characterController = GetComponent<CharacterController>();
        }

        // Update is called once per frame
        void Update()
        {
            if (_characterController.isGrounded)
            {
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    _moveSpeed = _run;
                }
                else if (Input.GetKey(KeyCode.LeftControl))
                {
                    _moveSpeed = _crouch;
                }
                else
                {
                    _moveSpeed = _walk;
                }

                _moveDirection = transform.TransformDirection(new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")) * _moveSpeed);
                if (Input.GetButton("Jump"))
                {
                    _moveDirection.y = _jump;
                }
            }
            _moveDirection.y -= _gravity * Time.deltaTime;
            _characterController.Move(_moveDirection * Time.deltaTime);
        }
    }

}