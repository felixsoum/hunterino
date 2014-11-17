using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
	public float maxRangeMouseY = 45f;
	public float turnSpeed = 200f;
	public float moveSpeed = 200f;
	public float jumpForce = 2f;
	public float timeBetweenShots = 0.1f;
	public GameObject playerBullet;
	public Transform gunTip;
	public TextMesh damageText;
	public float pushForce = 100f;
	Transform head;
	Transform feet;
	float rotationX = 0;
	float rotationY = 180f;
	float width;
	float velocityY;
	bool canJump = true;
	const float JUMP_DELAY = 0.5f;
	float shotCooldown;
	Vector3 initialPos;
	Quaternion initialRot;
	int Damage
	{
		get
		{
			return damage;
		}
		set
		{
			damage = value;
			damageText.text = damage.ToString() + "%";
		}
	}
	int damage;
	Enemies enemies;

	void Awake()
	{
		initialPos = transform.position;
		initialRot = transform.rotation;
		head = transform.Find("Head").transform;
		feet = transform.Find("Feet").transform;
		BoxCollider box = GetComponent<BoxCollider>();
		width = box.size.x;
		enemies = GameObject.FindGameObjectWithTag("Enemies").GetComponent<Enemies>();
	}

	void Start()
	{
		Damage = 0;
	}

	void Update()
	{
		UpdateRotation();
		UpdateMovement();
		CheckJump();
		CheckShoot();

		if (transform.position.y < -20f)
		{
			Reset();
		}
	}

	void UpdateRotation()
	{
		float mouseX = Input.GetAxis("Mouse X");
		float mouseY = Input.GetAxis("Mouse Y");
		rotationY += mouseX * turnSpeed * Time.deltaTime;
		rotationX -= mouseY * turnSpeed * Time.deltaTime;
		rotationX = Mathf.Clamp(rotationX, -maxRangeMouseY, maxRangeMouseY);
		transform.rotation = Quaternion.Euler(new Vector3(0 , rotationY, 0));
		head.rotation = Quaternion.Euler(new Vector3(rotationX, rotationY, 0));
	}

	void UpdateMovement()
	{
		float axisX = Input.GetAxis("Horizontal");
		float axisZ = Input.GetAxis("Vertical");
		var speed = new Vector3(0, rigidbody.velocity.y, 0);
		speed += Quaternion.Euler(0, rotationY, 0) * Vector3.right * axisX * moveSpeed * Time.deltaTime;
		speed += Quaternion.Euler(0, rotationY, 0) * Vector3.forward * axisZ * moveSpeed * Time.deltaTime;
		rigidbody.AddForce(speed);
	}

	void CheckJump()
	{
		if (!canJump)
		{
			return;
		}
		if (Input.GetButton("Jump") && IsGrounded())
		{
			canJump = false;
			Invoke("ResetJump", JUMP_DELAY);
			rigidbody.AddForce(new Vector3(0, jumpForce, 0));
		}
	}

	void CheckShoot()
	{
		shotCooldown = Mathf.Max(shotCooldown - Time.deltaTime, 0);
		if (shotCooldown == 0 && Input.GetButton("Fire1"))
		{
			shotCooldown = timeBetweenShots;
			Instantiate(playerBullet, gunTip.position, Quaternion.Euler(rotationX, rotationY - 2, 0));
		}
	}

	bool IsGrounded()
	{
		foreach (Collider c in Physics.OverlapSphere(feet.transform.position, width))
		{
			float distance = feet.transform.position.y - c.ClosestPointOnBounds(feet.transform.position).y;
			if (distance > 0.01f || c.gameObject == this.gameObject)
			{
				continue;
			}
			else
			{
				return true;
			}
		}
		return false;
	}

	void Reset()
	{
		transform.position = initialPos;
		transform.rotation = initialRot;
		rigidbody.velocity = Vector3.zero;
		rotationX = 0;
		rotationY = 180;
		Damage = 0;
		enemies.Clear();
	}

	void OnHit(int d)
	{
		Damage += d;
	}

	void OnPush(Vector3 pusherPos)
	{
		var direction = transform.position - pusherPos;
		var force = direction.normalized * pushForce * (1f + damage/100f);
		rigidbody.AddForce(force, ForceMode.Impulse);
	}

	void ResetJump()
	{
		canJump = true;
	}
}
