using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementTutorial : TutorialState
{
    [SerializeField] private CharacterMovement player;
    private Vector3 playerStartPos;
    private Quaternion playerOriginalRotation;

    [SerializeField] private GameObject welcomePanel;
    [SerializeField] private GameObject movementPanel;
    [SerializeField] private GameObject lookAroundPanel;
    [SerializeField] private GameObject lookGunPanel;


    [SerializeField] private LayerMask weaponMask;

    protected override IEnumerator StartStateCoroutine()
    {
        welcomePanel.SetActive(true);
        yield return new WaitForSeconds(3);
        welcomePanel.SetActive(false);
        movementPanel.SetActive(true);

        playerStartPos = player.transform.position;
        yield return new WaitUntil(HasPlayerMoved);
        yield return new WaitForSeconds(1);
        movementPanel.SetActive(false);

        lookAroundPanel.SetActive(true);
        playerOriginalRotation = player.transform.rotation;
        yield return new WaitUntil(HasPlayerRotated);
        yield return new WaitForSeconds(1);
        lookAroundPanel.SetActive(false);

        lookGunPanel.SetActive(true);
        yield return new WaitUntil(isPlayerLookingTowardsGun);
        lookGunPanel.SetActive(false);

        onStateFinished.Invoke();
    }

    private bool HasPlayerMoved()
    {
        return player.transform.position != playerStartPos;
    }

    private bool HasPlayerRotated()
    {
        return playerOriginalRotation != player.transform.rotation;
    }
    private bool isPlayerLookingTowardsGun()
    {
        return Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, 100f, weaponMask);
    }
}
