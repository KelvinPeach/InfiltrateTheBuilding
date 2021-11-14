using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Infiltrate
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] Transform model;

        [SerializeField] float walkMoveSpeed = 4f;
        [SerializeField] float crouchMoveSpeed = 2f;
        [SerializeField] float rotateSpeed = 3f;

        bool isCrouching;

        // Cache
        CharacterController controller;

        void Awake()
        {
            // Cache
            controller = GetComponent<CharacterController>();
        }

        void Update()
        {
            // Toggle crouching?
            if (Input.GetKeyDown(KeyCode.C))
            {
                ToggleCrouch();
            }

            // Rotate around y axis
            transform.Rotate(0, Input.GetAxis("Horizontal") * rotateSpeed, 0);

            // Movement
            Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));

            if (isCrouching)
            {
                controller.SimpleMove(move * crouchMoveSpeed);
            }
            else
            {
                controller.SimpleMove(move * walkMoveSpeed);
            }
        }

        void ToggleCrouch()
        {
            isCrouching = !isCrouching;

            // Crouching
            if (isCrouching)
            {
                // Adjust model
                model.localScale = new Vector3(1f, 0.5f, 1f);
                model.localPosition = new Vector3(0f, -0.6f, 0f);
                controller.height = 0.2f;
                controller.center = new Vector3(0f, -0.6f, 0f);
            }
            // Standing
            else
            {
                // Adjust model
                model.localScale = new Vector3(1f, 1f, 1f);
                model.localPosition = new Vector3(0f, 0f, 0f);
                controller.height = 2f;
                controller.center = new Vector3(0f, 0f, 0f);
            }
        }
    }
}