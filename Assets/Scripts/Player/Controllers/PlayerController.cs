using System.Collections;
using System.Collections.Generic;
using Events;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Controllers")]
    [SerializeField] private FirstPersonLook lookController;
    [SerializeField] private CharacterMovement moveController;
    [SerializeField] private CharacterController characterController;
    [SerializeField] private InputReader inputReader;
    [SerializeField] private Collider collider;

    [Header("Visual Fall")]
    [SerializeField] private GameObject mainCamera;
    [SerializeField] private GameObject model;
    [SerializeField] private GameObject ragdoll;

    [Header("UI")]
    [SerializeField] private GameObject deathScreen;

    public void Die()
    {
        lookController.enabled = false;
        moveController.enabled = false;
        characterController.enabled = false;
        inputReader.enabled = false;
        collider.enabled = false;

        mainCamera.SetActive(false);
        model.SetActive(false);
        ragdoll.SetActive(true);
        deathScreen.SetActive(true);
    }
}