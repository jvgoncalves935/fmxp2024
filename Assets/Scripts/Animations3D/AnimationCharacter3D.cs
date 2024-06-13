using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationCharacter3D : MonoBehaviour
{
    
    [SerializeField] private bool isIdle;

    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        GetAnimations();    
    }

    private void GetAnimations() {
        animator.SetBool("IsIdle", isIdle);
    }

    public void ToggleIdle(bool toggle) {
        isIdle = toggle;
    }


}
