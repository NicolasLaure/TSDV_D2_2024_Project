using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSteps : MonoBehaviour
{
    [SerializeField] private CharacterMovement playerMovement;

    [SerializeField] private AudioClip walk;
    private AudioSource source;

    private void Awake()
    {
        playerMovement.onCharacterMove += OnWalk;
        source = GetComponent<AudioSource>();
    }

    private void OnDestroy()
    {
        playerMovement.onCharacterMove -= OnWalk;
    }

    private void OnWalk(Vector2 dir)
    {
        if (dir != Vector2.zero)
        {

            if (source.clip != walk || !source.isPlaying)
            {
                source.clip = walk;
                source.Play();
            }
        }
        else
            source.Stop();
    }
}
