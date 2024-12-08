using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;  
using TMPro;
[System.Serializable]




public class SaveSystem
{
    private string savePath;


    void Start()
    {
        savePath = Application.persistentDataPath + "/savefile.json";
    }

    public void SaveGame(PlayerClass playerClass, Weapon equippedWeapon, int score)
    {
        PlayerSaveData saveData = new PlayerSaveData
        {
            //SetSelectedClass = playerClass.className,   // Save the class name
            //SetSelectedWeapon = equippedWeapon.weaponName,      // Save the weapon name
            score = score,                           // Save the player's score
        };

        string json = JsonUtility.ToJson(saveData);
        File.WriteAllText(savePath, json);
    }

    public PlayerSaveData LoadGame()
    {
        if (File.Exists(savePath))
        {
            string json = File.ReadAllText(savePath);
            return JsonUtility.FromJson<PlayerSaveData>(json);
        }
        return null;
    }
}
