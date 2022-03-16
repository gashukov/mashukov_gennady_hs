using UnityEngine;

namespace Creatures
{
    [RequireComponent(typeof(Rigidbody))]
    public class HeroMove : MonoBehaviour
    {

        private Rigidbody _rigidbody;
        private Vector3 _direction;
        private Vector3 _target;
        private float _speed;
        private Vector3 _rotationMask = new Vector3(0, 1, 0);
        private bool _moove = false;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        public void MoveTo(Vector3 target, float speed)
        {
            _moove = true;
            _target = target;
            _speed = speed;
        }

        public void StopMove()
        {
            _rigidbody.velocity = Vector3.zero;
            _moove = false;
        }

        private void FixedUpdate()
        {
            if (!_moove)
                return;
            _direction = (_target - transform.position).normalized;
            Vector3 rotation = Quaternion.LookRotation(Vector3.RotateTowards(transform.forward, _direction, 15 * Time.fixedDeltaTime, 0.0f)).eulerAngles;
            _rigidbody.MoveRotation(Quaternion.Euler(Vector3.Scale(rotation, _rotationMask)));

            _rigidbody.velocity = _speed * transform.forward;
        }
    }
}