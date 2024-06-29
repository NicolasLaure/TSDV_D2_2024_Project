using UnityEngine;

[CreateAssetMenu(fileName = "New GameSave", menuName = "ScriptableObjects/SaveSystem/New Save", order = 0)]
public class GameSaveSO : ScriptableObject
{
    public bool wasTutorialFinished = false;
    public int shootingRangeHighScore = 0;
    public float bestTime = 0;
    public void ResetSave()
    {
        wasTutorialFinished = false;
    }
}
