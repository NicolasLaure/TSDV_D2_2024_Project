using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementTutorial : TutorialState
{
    [SerializeField] private CharacterMovement player;
    private Vector3 playerStartPos;

    [SerializeField] private GameObject welcomePanel;
    [SerializeField] private GameObject movementPanel;
    [SerializeField] private GameObject lookAroundPanel;


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
        yield return new WaitUntil(isPlayerLookingTowardsGun);
        lookAroundPanel.SetActive(false);

        onStateFinished.Invoke();
    }

    private bool HasPlayerMoved()
    {
        return player.transform.position != playerStartPos;
    }

    private bool isPlayerLookingTowardsGun()
    {
        return Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, 100f, weaponMask);
    }
}
