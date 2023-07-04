using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GlobalPlayer {
    public class MovementScript : MonoBehaviour
    {

        // exists to control linear drag/friction during movement
        private bool _isMoving;
        bool isMoving {
            set {
                _isMoving = value;
                if (isMoving) 
                {
                    body.drag = movingDrag;
                } else if (!isMoving) 
                {
                    body.drag = stopDrag;
                }
            }
            get {
                return _isMoving;
            }
        }
        [SerializeField]
        float movingDrag = 10f;
        [SerializeField]
        float stopDrag = 50f;        
        [SerializeField]
        private bool _canMove;
        bool canMove {
            set {
                _canMove = value;
            }
            get {
                return _canMove;
            }
        }

        Rigidbody2D body;
        Vector2 direction = Vector2.zero;
        public float movementSpeed = 5000.0F;
        // WASD constrols
        InputAction move;

        private void Awake() 
        {
            body = gameObject.GetComponent<Rigidbody2D>() as Rigidbody2D;
        }

        void Start()
        {
            canMove = true;
        }

        void FixedUpdate()
        {
            if (direction != Vector2.zero && _canMove) 
            {
                Vector2 velocity = new Vector2(direction.x * movementSpeed * Time.deltaTime, direction.y * movementSpeed * Time.deltaTime);
                body.AddForce(velocity);
                isMoving = true;

            } else 
            {
                isMoving = false;
            }
        }
        private void changeMovementState()
        {
            canMove = !canMove;
        }

        // enable all controles
        private void OnEnable() 
        { 

        }

        private void OnDisable()
        {

        }

        void OnMove(InputValue value)
        {
            direction = value.Get<Vector2>();
        }

        void OnCollisionEnter2D(Collision2D other) => OnCollisionHandle(other);

        void OnCollisionHandle(Collision2D other) {
            Debug.Log(other);
        }

    }

}
    