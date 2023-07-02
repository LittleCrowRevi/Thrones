using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player {
    public class MovementScript : MonoBehaviour
    {

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

        Rigidbody2D body;
        public float movingDrag = 10f;
        public float stopDrag = 50f;
        Vector2 direction = Vector2.zero;
        public float movementSpeed = 5000.0F;
        Controls _controls;
        InputAction move;

        private void Awake() 
        {
            body = gameObject.GetComponent<Rigidbody2D>() as Rigidbody2D;
            // init generated Control Code
            _controls = new Controls();
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

        // Update is called once per frame
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
    
