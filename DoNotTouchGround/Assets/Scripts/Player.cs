using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    // Maximum speed allowed for an enemy
    [SerializeField] private float m_MaxSpeed;

    [SerializeField] public float MoveSpeed = 10;
    [SerializeField] public float RotateSpeed = 40;

    private Rigidbody2D m_RigidBody;


    private void Start()
    {

        m_RigidBody = this.GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update () {
	    // Amount to Move
	    float MoveForward = Input.GetAxis("Vertical") * MoveSpeed * Time.deltaTime;
	    float MoveRotate = Input.GetAxis("Horizontal") * RotateSpeed * Time.deltaTime;

        // Move the player
        //transform.Translate(Vector2.up * MoveForward);
	    m_RigidBody.AddRelativeForce(Vector2.up * MoveForward);
        transform.Rotate(Vector3.back * MoveRotate);
    }
}
