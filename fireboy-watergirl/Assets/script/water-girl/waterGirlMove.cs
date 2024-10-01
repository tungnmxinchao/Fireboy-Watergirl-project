using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waterGirlMove : MonoBehaviour
{
	public GameObject idleObject;
	public GameObject leftObject;
	public GameObject rightObject;

	public float moveSpeed = 5f;
	private string currentState = "Idle";

	public float jumpForce = 10f;
	private bool isGrounded = false;
	private Rigidbody2D rb;

	public Transform groundCheck;
	public float groundCheckDistance = 0.2f;

	void Start()
	{
		SetActiveState("Idle");
		rb = GetComponent<Rigidbody2D>();
	}

	void Update()
	{
		// Check if the character is on the ground
		CheckGrounded();
	

		// Horizontal movement
		float horizontalInput = Input.GetAxis("Horizontal");

		if (horizontalInput > 0) // Move right
		{
			MoveRight();
		}
		else if (horizontalInput < 0) // Move left
		{
			MoveLeft();
		}
		else // Not moving
		{
			SetActiveState("Idle");
		}

		// Jump when pressing the W key and grounded
		if (Input.GetKeyDown(KeyCode.W) && isGrounded)
		{
			Jump();
		}
	}

	private void MoveLeft()
	{
		transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);
		SetActiveState("Left");
	}

	private void MoveRight()
	{
		transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
		SetActiveState("Right");
	}

	private void Jump()
	{
		// Only jump if the character is grounded
		if (isGrounded)
		{
			rb.velocity = new Vector2(rb.velocity.x, jumpForce);
			isGrounded = false; // Set isGrounded to false to prevent multiple jumps
			SetActiveState("Idle");
		}
	}

	private void SetActiveState(string state)
	{
		if (currentState != state)
		{
			idleObject.SetActive(false);
			leftObject.SetActive(false);
			rightObject.SetActive(false);

			switch (state)
			{
				case "Idle":
					idleObject.SetActive(true);
					break;
				case "Left":
					leftObject.SetActive(true);
					break;
				case "Right":
					rightObject.SetActive(true);
					break;
			}

			currentState = state;
		}
	}

	// Check if the character is on the ground using Raycast
	private void CheckGrounded()
	{
		RaycastHit2D hit = Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance);

		// If Raycast hits a collider, the character is on the ground
		if (hit.collider != null)
		{
			isGrounded = true;
			Debug.Log("Dang o mat dat");
		}
		else
		{
			isGrounded = false;
			Debug.Log("Khong mat dat");
		}
	}

	private void OnDrawGizmos()
	{
		if (groundCheck != null)
		{
			Gizmos.color = Color.red;
			Gizmos.DrawLine(groundCheck.position, groundCheck.position + Vector3.down * groundCheckDistance);
		}
	}
}
