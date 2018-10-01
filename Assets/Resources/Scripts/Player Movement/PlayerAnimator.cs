using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour {

    const float locomotionAnimationSmoothTime = 0.1f;


    Animator anim;

	// Use this for initialization
	void Start () {
        anim = GetComponentInChildren<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 vel = transform.position - GetComponent<PlayerMotor>().lastPosition;
        vel.y = 0f;
        float speedPercent = vel.magnitude * 10;
        anim.SetFloat("speedPercent", speedPercent, locomotionAnimationSmoothTime, Time.deltaTime);
        anim.speed = 2.5f - (speedPercent - 0.5f) * 2 * (speedPercent - 0.5f < 0 ? -1.5f : 1.5f);
    }

    public void SetJump() {anim.SetTrigger("Jumping");}

    public void SetGround(bool grounded) { anim.SetBool("Grounded", grounded); }
}
