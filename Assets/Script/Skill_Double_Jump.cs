using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InputController))]
[RequireComponent(typeof(PlayerController))]
public class Skill_Double_Jump : MonoBehaviour
{
    private InputController inputController;
    private PlayerController playerController;

    private void Awake()
    {
        inputController = GetComponent<InputController>();
        playerController = GetComponent<PlayerController>();

        inputController.OnJumpPressed += DoubleJump;
    }

    private void OnDestroy()
    {
        inputController.OnJumpPressed -= DoubleJump;
    }

    private void DoubleJump()
    {
        if (!playerController.playerStatus.isCollidingBottom && playerController.canDoubleJump && playerController.doubleJumpSkill)
        {
            playerController.Jump();
            playerController.activeDoubleJump();
        }
    }
}
