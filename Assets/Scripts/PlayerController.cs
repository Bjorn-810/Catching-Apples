using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody _rb;
    public Transform camHolder;
    public Transform cam;

    private Vector3 _input;
    private Vector2 _lookDir;

    public float moveSpeed = 10f;
    public float _dashForce = 10;

    public float lookSpeed = 10f;

    void Start()
    {
        Cursor.visible = false;
        _rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        HandleInputs();
        Movement();
    }

    private void HandleInputs()
    {
        _input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")); // get input

        _lookDir.x += Input.GetAxis("Mouse X") * lookSpeed;
        _lookDir.y += Input.GetAxis("Mouse Y") * lookSpeed;
        _lookDir.y = Mathf.Clamp(_lookDir.y, -90, 90);
    }

    private void Movement()
    {
        Vector3 moveDirection = new Vector3(_input.x, 0, _input.z) * moveSpeed * Time.deltaTime;
        transform.Translate(moveDirection);

        transform.rotation = Quaternion.Euler(0, _lookDir.x, 0);
        cam.rotation = Quaternion.Euler(-_lookDir.y, _lookDir.x, 0);
        cam.position = camHolder.position;
    }
}
