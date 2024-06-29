using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Medal : MonoBehaviour
{
    private float originalSize = 0;
    [SerializeField] private float InitialSizeMultiplier;
    [SerializeField] private float animDuration;
    private RectTransform rectTransform;

    public UnityEvent OnAnimFinished;
    void OnEnable()
    {
        rectTransform = this.GetComponent<RectTransform>();
        originalSize = rectTransform.localScale.x;
        rectTransform.localScale = new Vector3(originalSize * InitialSizeMultiplier, originalSize * InitialSizeMultiplier);

        StartCoroutine(MedalAnimation());
    }

    private IEnumerator MedalAnimation()
    {
        float startTime = Time.time;
        float timer = 0;
        while (timer < animDuration)
        {
            timer = Time.time - startTime;
            float size = Mathf.Lerp(originalSize * InitialSizeMultiplier, originalSize, timer / animDuration);
            rectTransform.localScale = new Vector3(size, size);
            yield return null;
        }
        OnAnimFinished?.Invoke();
    }
}
