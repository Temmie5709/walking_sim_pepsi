using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] float speed = 10f;

    private CharacterController characterController;

    float x;
    float z;

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        x = Input.GetAxis("Horizontal");
        z = Input.GetAxis("Vertical");
    }
    private void FixedUpdate()
    {
        Vector3 move = Vector3.Normalize(transform.right * x + transform.forward * z) * Time.fixedDeltaTime * speed;
        characterController.Move(move);
        
    }
}
