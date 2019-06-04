using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Animator), typeof(BoxCollider2D), typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
	private Rigidbody2D m_rigidbody;
	private Animator m_animator;
	private BoxCollider2D m_boxCollider;
	//[SerializeField]
	private bool m_isGround = false;
    private Vector3 m_initialPosition;
	private bool isJumping;
	[SerializeField]
	private float RunSpeed = 5f;

	public float moveSpeed = 5.0f;
	public float jumpPower = 400.0f;
	public LayerMask whatIsGround;

	private void Awake()
	{
		this.m_rigidbody = this.GetComponent<Rigidbody2D>();
		this.m_animator = this.GetComponent<Animator>();
		this.m_boxCollider = this.GetComponent<BoxCollider2D> ();
        this.m_initialPosition = this.transform.position;
	}

	private void Update()
	{
		float x = Input.GetAxis("Horizontal");
		bool isJump = Input.GetButtonDown ("Jump");
		bool isRun = Input.GetKey(KeyCode.LeftShift);
		this.Move(x, isJump, isRun);
	}

	private void FixedUpdate(){
		Vector2 pos = this.transform.position;

		float characterBottom = pos.y + (this.m_boxCollider.offset.y - (this.m_boxCollider.size.y * 0.5f)) * this.transform.lossyScale.y;
		Vector2 groundCheck = new Vector2 (pos.x, characterBottom);
		Vector2 groundArea = new Vector2 (this.m_boxCollider.size.x * 0.49f, 0.05f);
		this.m_isGround = Physics2D.OverlapArea (
			groundCheck + groundArea, 
			groundCheck - groundArea, 
			this.whatIsGround
		);

		if(m_isGround) isJumping = false;

		this.m_animator.SetBool ("IsGround", this.m_isGround);
	}

	void Move(float speedX, bool isJump, bool isRun)
	{
		if(isJump) isJumping = true;
		if(Mathf.Abs(speedX) > 0)
		{
			Vector3 tmpEuler = this.transform.eulerAngles;

			if(Mathf.Sign(speedX) > 0)
			{
				tmpEuler.y = 0;
				this.transform.eulerAngles = tmpEuler;
			}
			else
			{
				tmpEuler.y = 180;
				this.transform.eulerAngles = tmpEuler;
			}
		}

		this.m_rigidbody.velocity = 
			new Vector2(speedX * this.moveSpeed, this.m_rigidbody.velocity.y);
		this.m_animator.SetFloat("InputX", Mathf.Abs(speedX));

		if (this.m_isGround && isJump) {
			this.m_rigidbody.AddForce (Vector2.up * this.jumpPower);
		}
		
        this.m_animator.SetFloat("Vertical", this.m_rigidbody.velocity.y);

        
	}

    public void enemyJumpForce(float power)
    {
        Vector3 tmpVelocity = this.m_rigidbody.velocity;
        tmpVelocity.y = 0;
        this.m_rigidbody.velocity = tmpVelocity;

        if(Input.GetButton("Jump"))
        {
            this.m_rigidbody.AddForce(new Vector2(0, this.jumpPower));
        }
        else
        {
            this.m_rigidbody.AddForce(new Vector2(0, this.jumpPower * power));
        }
    }

    public void Initialize()
    {
        this.transform.position = this.m_initialPosition;
    }
}
