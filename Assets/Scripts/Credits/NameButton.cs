using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NameButton : MonoBehaviour
{
    public string url;
    public void OnButtonClicked()
    {
        if (url.Length > 0)
            Application.OpenURL(url);
    }
}
