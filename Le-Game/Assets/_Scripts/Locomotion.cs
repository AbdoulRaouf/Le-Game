using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Locomotion : MonoBehaviour
{
    Animator animator;
    Vector2 input;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        input.x = Input.GetAxis("Horizontal");
        input.y = Input.GetAxis("Vertical");
        animator.SetFloat("InputX", input.x);
        animator.SetFloat("InputY",input.y);
        
    }
}
