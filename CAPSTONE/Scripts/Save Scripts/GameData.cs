using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class GameData{

    public float mainVolume;
    public float dialogueVolume;
    ///public float[] level;

    public GameData(PauseGame gm)
    {
        mainVolume = gm.audioSrc1.volume;
        dialogueVolume = gm.audioSrc.volume;
    }
}
