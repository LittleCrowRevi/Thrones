using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player {
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
        public float movingDrag = 10f;
        public float stopDrag = 50f;        

        private bool _canMove;
        bool canMove {
            set {
                _canMove = value;
                
                // if the player can't move...disable movement Controls
                if (!_canMove)
                {
                    this.enabled = !this.enabled;
                } else
                {
                    this.enabled = !this.enabled;
                }
            }
            get {
                return _canMove;
            }
        }

        Rigidbody2D body;
        Vector2 direction = Vector2.zero;
        public float movementSpeed = 5000.0F;
        Controls _controls;
        // WASD constrols
        InputAction move;

        private void Awake() 
        {
            body = gameObject.GetComponent<Rigidbody2D>() as Rigidbody2D;
            // init generated Control Code
            _controls = new Controls();
        }

        private void ControlChangeListener()
        {

        }

        // enable all controles
        private void OnEnable() 
        {
            move = _controls.Player.Move;
            move.started += ctx => direction = ctx.ReadValue<Vector2>();
            move.performed += ctx => direction = ctx.ReadValue<Vector2>();
            move.canceled += ctx => direction = ctx.ReadValue<Vector2>();
            move.Enable();
        }

        private void OnDisable()
        {
            move.Disable();
        }

        void Start()
        {
            
        }

        void FixedUpdate()
        {
            if (direction != Vector2.zero) 
            {
                Vector2 velocity = new Vector2(direction.x * movementSpeed * Time.deltaTime, direction.y * movementSpeed * Time.deltaTime);
                body.AddForce(velocity);
                isMoving = true;

            } else 
            {
                isMoving = false;

            }

        }

        void OnCollisionEnter2D(Collision2D other) => OnCollisionHandle(other);

        void OnCollisionHandle(Collision2D other) {
            Debug.Log(other);
        }

    }

}
    
