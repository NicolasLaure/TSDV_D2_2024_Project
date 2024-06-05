using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New GameSave", menuName = "ScriptableObjects/SaveSystem/New Save", order = 0)]
public class GameSaveSO : ScriptableObject
{
    public bool wasTutorialFinished = false;   
}
