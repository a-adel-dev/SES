using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SES.Core
{
    public class CameraController : MonoBehaviour
    {
        public static CameraController instance;
        [SerializeField] Transform cameraTransform;
        [SerializeField] float fastSpeedMult;
        [SerializeField] float movementSpeed;
        [SerializeField] float movementTime;
        [SerializeField] float rotationAmount;
        [SerializeField] Vector3 zoomAmount;


        Camera mainCamera;

        Vector3 newPosition;
        Quaternion newRotation;
        Vector3 newZoom;

        Vector3 dragStartPosition;
        Vector3 dragCurrentPosition;
        Vector3 rotateStartPosition;
        Vector3 rotateCurrentPosition;

        public Transform followTransform;


        // Start is called before the first frame update
        void Start()
        {
            instance = this;
            newPosition = transform.position;
            newRotation = transform.rotation;
            newZoom = cameraTransform.localPosition;
            mainCamera = Camera.main;

        }

        // Update is called once per frame
        void Update()
        {
            if (followTransform != null)
            {
                transform.position = followTransform.position;
            }
            else
            {
                HandleMovementInput();
                HandleMouseInput();
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                followTransform = null;
            }
        }


        void HandleMouseInput()
        {
            if (Input.mouseScrollDelta.y != 0)
            {
                newZoom += Input.mouseScrollDelta.y * zoomAmount;
            }

            if (Input.GetMouseButtonDown(1))
            {
                Plane plane = new Plane(Vector3.up, Vector3.zero);
                Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

                float entry;

                if (plane.Raycast(ray, out entry))
                {
                    dragStartPosition = ray.GetPoint(entry);
                }
            }
            if (Input.GetMouseButton(1))
            {
                Plane plane = new Plane(Vector3.up, Vector3.zero);
                Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

                float entry;

                if (plane.Raycast(ray, out entry))
                {
                    dragCurrentPosition = ray.GetPoint(entry);

                    newPosition = transform.position + dragStartPosition - dragCurrentPosition;
                }
            }

            if (Input.GetMouseButtonDown(2))
            {
                rotateStartPosition = Input.mousePosition;
            }
            if (Input.GetMouseButton(2))
            {
                rotateCurrentPosition = Input.mousePosition;

                Vector3 difference = rotateStartPosition - rotateCurrentPosition;

                rotateStartPosition = rotateCurrentPosition;

                newRotation *= Quaternion.Euler(Vector3.up * (-difference.x / 5f));
            }
        }

        void HandleMovementInput()
        {
            float verticalInput = Input.GetAxis("Vertical");
            float horizontalInput = Input.GetAxis("Horizontal");

            if (Input.GetKey(KeyCode.LeftShift))
            {
                newPosition += (verticalInput * transform.forward * movementSpeed * fastSpeedMult);
                newPosition += (horizontalInput * transform.right * movementSpeed * fastSpeedMult);
            }
            else
            {
                newPosition += (verticalInput * transform.forward * movementSpeed);
                newPosition += (horizontalInput * transform.right * movementSpeed);
            }

            if (Input.GetKey(KeyCode.Q))
            {
                newRotation *= Quaternion.Euler(Vector3.up * rotationAmount);
            }

            if (Input.GetKey(KeyCode.E))
            {
                newRotation *= Quaternion.Euler(Vector3.up * -rotationAmount);
            }

            if (Input.GetKey(KeyCode.R))
            {
                newZoom += zoomAmount;
            }

            if (Input.GetKey(KeyCode.F))
            {
                newZoom -= zoomAmount;
            }
            transform.position = Vector3.Lerp(transform.position, newPosition, movementTime * Time.deltaTime);
            transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, movementTime * Time.deltaTime);
            cameraTransform.localPosition = Vector3.Lerp(cameraTransform.localPosition, newZoom, movementTime * Time.deltaTime);

        }
    }
}
