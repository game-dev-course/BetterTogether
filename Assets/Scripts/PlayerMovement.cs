using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	[SerializeField] public float speed = 5f;
	[SerializeField] public float maxSpeed = 10f;
	[SerializeField] public float jumpSpeed = 5f;
	[SerializeField] public float horizontalDrag = 2f;
	[SerializeField] public Vector2 groundCheckPosition;
	[SerializeField] public float groundCheckRadius;
	[SerializeField] public LayerMask whatIsGround;
	[SerializeField] public string horizontalInput;
	[SerializeField] public string jumpInput;
	[SerializeField] public Animator animator;
	Rigidbody2D rb;
	[SerializeField] private float movement;
	[SerializeField] private bool jump;
	[SerializeField] private bool isGrounded;
	[SerializeField] private bool isFlipped;

    void Start()
    {
		rb = GetComponent<Rigidbody2D>();
    }

	// Update is called once per frame
	void Update()
    {
		movement = Input.GetAxisRaw(horizontalInput) * speed;
		if (Input.GetButtonDown(jumpInput) && isGrounded)
			jump = true;

		animator.SetFloat("Speed", Mathf.Abs(movement));

		Collider2D collider = Physics2D.OverlapCircle(rb.position + groundCheckPosition, groundCheckRadius, whatIsGround);
		if (collider != null)
		{
			if (rb.velocity.y < 0f)
			{
				isGrounded = true;
				animator.SetBool("IsGrounded", isGrounded);
			}
		} else
		{
			isGrounded = false;
			animator.SetBool("IsGrounded", isGrounded);
		}
    }

	private void FixedUpdate()
	{
		float t = rb.velocity.x / maxSpeed;

		float lerp = 0f;

		if (t >= 0f)
			lerp = Mathf.Lerp(maxSpeed, 0f, t);
		else if (t < 0f)
			lerp = Mathf.Lerp(maxSpeed, 0f, Mathf.Abs(t));

		Vector2 force = new Vector2(movement * lerp * speed * Time.fixedDeltaTime, 0f);
		Vector2 drag = new Vector2(-rb.velocity.x * horizontalDrag * Time.fixedDeltaTime, 0f);

		rb.AddForce(force, ForceMode2D.Force);
		rb.AddForce(drag, ForceMode2D.Force);

		if (movement >= .1f && isFlipped)
		{
			Vector2 flipScale = new Vector2(-1f, 1f);
			animator.transform.localScale *= flipScale;
			isFlipped = false;
		} else if (movement <= -.1f && !isFlipped)
		{
			Vector2 flipScale = new Vector2(-1f, 1f);
			animator.transform.localScale *= flipScale;
			isFlipped = true;
		}

		if (jump)
		{
			Vector2 vel = rb.velocity;
			vel.y = jumpSpeed;
			rb.velocity = vel;
			jump = false;
			isGrounded = false;
			animator.SetBool("IsGrounded", isGrounded);
			animator.SetTrigger("Jump");
		}
	}

	private void OnDrawGizmosSelected()
	{
		Gizmos.DrawWireSphere((Vector2)transform.position + groundCheckPosition, groundCheckRadius);
	}

	private void OnDisable()
	{
		animator.SetFloat("Speed", 0f);
	}
}
