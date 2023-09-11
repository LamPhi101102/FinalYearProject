using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimatorGet : MonoBehaviour
{
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("w"))
        {
            animator.SetBool("isWalkForward", true);
            animator.SetBool("isJumps", false);
        }
        if (Input.GetKeyDown("s"))
        {
            animator.SetBool("isWalkBack", true);
        }
        if (Input.GetKeyDown("a"))
        {
            animator.SetBool("isWalkBackLeft", true);
        }
        if (Input.GetKeyDown("d"))
        {
            animator.SetBool("isWalkBackRight", true);
        }



        if (Input.GetKeyUp("w"))
        {
            animator.SetBool("isWalkForward", false);
        }
        if (Input.GetKeyUp("s"))
        {
            animator.SetBool("isWalkBack", false);
        }
        if (Input.GetKeyUp("a"))
        {
            animator.SetBool("isWalkBackLeft", false);
        }
        if (Input.GetKeyUp("d"))
        {
            animator.SetBool("isWalkBackRight", false);
        }

        // Jumps
        if (Input.GetButtonDown("Jump"))
        {
            animator.SetBool("isJumps", true);
            animator.SetBool("isWalkForward", false);
        }
        if(Input.GetKey(KeyCode.LeftShift))
        {
            animator.SetBool("isRunForward", true);
            animator.SetBool("isWalkForward", false);
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            animator.SetBool("isRunForward", false);
        }
    }
}
