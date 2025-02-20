namespace Player
{
    using UnityEngine;

    public class MouseLook : MonoBehaviour
    {
        [SerializeField] float _sensitivity = 8;
        [SerializeField] bool _invert = false;
        [SerializeField] Vector2 _verticalRotationClamp = new Vector2(-60,60);
        [SerializeField] Transform _player;
        [SerializeField] Transform _camera;
        [SerializeField] float _tempRotationValue;
        [SerializeField] float _rotationToApplyToCamera;

        // Update is called once per frame
        void Update()
        {
            _player.Rotate(0,Input.GetAxis("Mouse X")*_sensitivity, 0);

            _tempRotationValue += Input.GetAxis("Mouse Y") * _sensitivity;
            _tempRotationValue = Mathf.Clamp(_tempRotationValue, _verticalRotationClamp.x, _verticalRotationClamp.y);
            if (_invert) // plane
            {
                _rotationToApplyToCamera = _tempRotationValue;
            }
            else
            {
                _rotationToApplyToCamera = -_tempRotationValue;
            }
            _camera.localEulerAngles = new Vector3(_rotationToApplyToCamera,0,0);
        }
    }

}