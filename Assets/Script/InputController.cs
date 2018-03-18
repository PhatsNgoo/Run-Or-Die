using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class InputController : MonoBehaviour {
    public KeyCode jumpButton;
    public KeyCode dashButton;
    public PlayerController playerController;

	public String nameHorizontal;
    public Animator anim;

    public Action<float> OnMovePressed;
    public Action OnJumpPressed;
    public Action OnDashPressed;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    private void Update()
    {
        if (OnMovePressed != null)
        {
			OnMovePressed(Input.GetAxisRaw(nameHorizontal));
        }

        if (OnJumpPressed != null && Input.GetKeyDown(jumpButton))
        {
            OnJumpPressed();
        }

        if(OnDashPressed != null && Input.GetKeyDown(dashButton))
        {
            OnDashPressed();
        }
    }
}
