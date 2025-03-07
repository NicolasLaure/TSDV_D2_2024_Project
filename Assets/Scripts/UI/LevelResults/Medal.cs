using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Medal : MonoBehaviour
{
    private float _originalSize = 0;
    [SerializeField] private float InitialSizeMultiplier;
    [SerializeField] private float animDuration;
    private RectTransform _rectTransform;

    public UnityEvent OnAnimFinished;
    void OnEnable()
    {
        _rectTransform = this.GetComponent<RectTransform>();
        _originalSize = _rectTransform.localScale.x;
        _rectTransform.localScale = new Vector3(_originalSize * InitialSizeMultiplier, _originalSize * InitialSizeMultiplier);

        StartCoroutine(MedalAnimation());
    }

    private IEnumerator MedalAnimation()
    {
        float startTime = Time.time;
        float timer = 0;
        while (timer < animDuration)
        {
            timer = Time.time - startTime;
            float size = Mathf.Lerp(_originalSize * InitialSizeMultiplier, _originalSize, timer / animDuration);
            _rectTransform.localScale = new Vector3(size, size);
            yield return null;
        }
        OnAnimFinished?.Invoke();
    }
}
