using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour {
	
	public float speed = 6.0f;
	public float gravity = 30.0f;
	public float rotateSpeed = 3.0f;

	public GameObject hitPrefab;

	private CharacterController controller;
	private Animation anim;

	public int score = 0;
	public int health = 0;

	void Start()
	{
		controller = GetComponent<CharacterController>();
		anim = GetComponent<Animation> ();
		health = 100;
	}

	void Update() {
		Vector3 moveDirection = Vector3.zero;

        if (controller.isGrounded) {			
			transform.Rotate(0, Input.GetAxis("Horizontal") * rotateSpeed, 0);

			moveDirection = transform.TransformDirection(Vector3.forward);
			moveDirection *= speed * Input.GetAxis("Vertical") ;

			if(Input.GetAxis("Vertical") != 0.0f)
			{
				anim.CrossFade("walk", 0.02f);
			}
			else
			{
				if(anim.IsPlaying("walk"))
					anim.Stop("walk");
			}

			if(Input.GetButtonUp("Fire1"))
			{
				doPunch();
			}

			if(Input.GetButtonUp("Jump"))
			{
				anim.CrossFade("jump",0.02f);
				GetComponent<AppWarpHandler>().sendJump();
			}

			if(anim.isPlaying == false)
				anim.CrossFade("idle", 0.02f);
        }

        moveDirection.y -= gravity * Time.deltaTime;
		controller.Move(moveDirection * Time.deltaTime);

		Vector3 ray = transform.position;
		ray.y += 1.0f;
	}

	void OnGUI()
	{
		GUI.Label (new Rect (10, Screen.height - 48, Screen.width - 20, 48), "Health : "+health+"\nScore : " + score);
	}

	void doPunch()
	{
		anim.CrossFade("punch",0.02f);
		GetComponent<AppWarpHandler>().sendPuch();

		Vector3 ray = transform.position;
		ray.y += 1.0f;
		RaycastHit hit;
		if(Physics.Raycast (ray, transform.TransformDirection (Vector3.forward), out hit, 0.8f))
		{
			RemoteCharacter rChar = hit.collider.gameObject.GetComponent<RemoteCharacter>();
			if(rChar != null)
			{
				score += 10;
				GetComponent<AppWarpHandler>().sendHit();
				PunchEffect(hit.collider.gameObject.transform.position);
			}
		}

	}

	void PunchEffect(Vector3 pos)
	{
		GameObject obj = (GameObject)GameObject.Instantiate(hitPrefab,pos,Quaternion.identity);
		Destroy(obj, 1.0f);
	}
}
