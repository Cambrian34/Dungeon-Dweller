using UnityEngine;

[System.Serializable]
public class PlayerSaveData
{ 
    public static PlayerClass selectedClass;
    public static Weapon selectedWeapon;

    // You can add other game data like score, player position, etc.
    public int score;
    


    //getter and setter for player class and weapon
    public PlayerClass GetSelectedClass()
    {
        if (selectedClass == null)
        {
            return null;
        }else {
            return selectedClass;
        }
    }


    public void SetSelectedClass(PlayerClass playerClass)
    {
        selectedClass = playerClass;
    }

    public Weapon GetSelectedWeapon()
    {
        return selectedWeapon;
    }

    public void SetSelectedWeapon(Weapon weapon)
    {
        selectedWeapon = weapon;
    }

    public int GetScore()
    {
        return score;
    }

    public void SetScore(int newScore)
    {
        score = newScore;
    }
    //set default class and weapon
    public static PlayerClass defaultClass;
    public static Weapon defaultWeapon;

    public PlayerSaveData()
    {
        //get from assets folder
        defaultClass = Resources.Load<PlayerClass>("Mage");
        defaultWeapon = Resources.Load<Weapon>("Staff");
    }


   
}
