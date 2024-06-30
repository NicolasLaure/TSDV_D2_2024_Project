using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CreditsReader : MonoBehaviour
{
    [SerializeField] private CreditsSO credits;
    [SerializeField] private Transform layoutTransform;
    [SerializeField] private GameObject titleTextPrefab;
    [SerializeField] private GameObject nameButtonPrefab;

    void Start()
    {
        foreach (CreditGroup group in credits.groups)
        {
            TextMeshProUGUI title = Instantiate(titleTextPrefab, layoutTransform).GetComponent<TextMeshProUGUI>();
            title.text = group.Title;

            for (int i = 0; i < group.names.Count; i++)
            {
                TextMeshProUGUI nameButton = Instantiate(nameButtonPrefab, layoutTransform).GetComponent<TextMeshProUGUI>();
                nameButton.text = group.names[i];
                if (i < group.urls.Count)
                    nameButton.GetComponent<NameButton>().url = group.urls[i];
                else
                    Destroy(nameButton.GetComponent<Button>());
            }
        }
    }
}
