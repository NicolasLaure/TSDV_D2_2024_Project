using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSteps : MonoBehaviour
{
    [SerializeField] private CharacterMovement playerMovement;

    [SerializeField] private AudioClip walk;
    private AudioSource _source;

    private void Awake()
    {
        playerMovement.onCharacterMove += OnWalk;
        _source = GetComponent<AudioSource>();
    }

    private void OnDestroy()
    {
        playerMovement.onCharacterMove -= OnWalk;
    }

    private void OnWalk(Vector2 dir)
    {
        if (dir != Vector2.zero)
        {

            if (_source.clip != walk || !_source.isPlaying)
            {
                _source.clip = walk;
                _source.Play();
            }
        }
        else
            _source.Stop();
    }
}
