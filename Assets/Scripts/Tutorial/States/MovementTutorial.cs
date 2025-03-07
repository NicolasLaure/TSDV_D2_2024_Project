using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementTutorial : TutorialState
{
    [SerializeField] private CharacterMovement player;
    private Vector3 _playerStartPos;
    private Quaternion _playerOriginalRotation;

    [Header("Panels")]
    [SerializeField] private GameObject welcomePanel;
    [SerializeField] private GameObject movementPanel;
    [SerializeField] private GameObject lookAroundPanel;
    [SerializeField] private GameObject lookGunPanel;
    [SerializeField] private float welcomeDuration;
    [SerializeField] private float panelfadeDuration;
    [SerializeField] private float endDuration;

    [Space]
    [SerializeField] private LayerMask weaponMask;

    protected override IEnumerator StartStateCoroutine()
    {
        welcomePanel.SetActive(true);
        yield return new WaitForSeconds(welcomeDuration);
        welcomePanel.SetActive(false);
        movementPanel.SetActive(true);

        _playerStartPos = player.transform.position;
        yield return new WaitUntil(HasPlayerMoved);
        yield return new WaitForSeconds(panelfadeDuration);
        movementPanel.SetActive(false);

        lookAroundPanel.SetActive(true);
        _playerOriginalRotation = player.transform.rotation;
        yield return new WaitUntil(HasPlayerRotated);
        yield return new WaitForSeconds(panelfadeDuration);
        lookAroundPanel.SetActive(false);

        lookGunPanel.SetActive(true);
        yield return new WaitUntil(isPlayerLookingTowardsGun);
        lookGunPanel.SetActive(false);

        yield return new WaitForSeconds(endDuration);

        onStateFinished?.Invoke();
    }

    private bool HasPlayerMoved()
    {
        return player.transform.position != _playerStartPos;
    }

    private bool HasPlayerRotated()
    {
        return _playerOriginalRotation != player.transform.rotation;
    }
    private bool isPlayerLookingTowardsGun()
    {
        return Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, 100f, weaponMask);
    }
}
