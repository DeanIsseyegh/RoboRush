using System;
using UnityEngine;
using UnityEngine.EventSystems;

public interface IPlayerControlsInput
{
    public bool IsJumpPressed();

    public bool IsPrimaryAttackPressed();

    public bool IsSecondaryAttackPressed();

    public bool IsLeftPressed();

    public bool IsRightPressed();

    public void DisableInGameActions();

    public void EnableInGameActions();
}