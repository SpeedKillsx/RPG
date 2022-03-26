using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private void Awake()
    {
        if (GameManager.instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        SceneManager.sceneLoaded += LoadState;
        DontDestroyOnLoad(gameObject);
    }

    //Ressources

    public List<Sprite> palyerSprites;
    public List<Sprite> weaponSprites;
    public List<int> weaponPrices;
    public List<int> xpTable;

    //References
    public Player player;
    public Weapon weapon;
    public FloatingTextManager floatingText;
    //Logic
    public int pesos;
    public int experience;
    // Floating text
    public void ShowText(string msg, int fontSize, Color color, Vector3 pos, Vector3 motion, float duration)
    {
        floatingText.Show(msg, fontSize, color, pos, motion, duration);
    }

    //Upgrade Weapon

    public bool TryUpgradeWeapon()
    {
        // Is the weapon max level ?
        if(weaponPrices.Count <= weapon.weaponLevel)
        {
            return false;
        }

        if (pesos >= weaponPrices[weapon.weaponLevel])
        {
            pesos -= weaponPrices[weapon.weaponLevel];
            weapon.UpgradeWeapon();
            return true;
        }

        return false;
    }

    // Save the state of the game
    /*
     * INT PreferedSkin 
     * INT pesos
     * INT experience 
     * INT weaponLevel
     */
    public void SaveState()
    {
        string s = "";

        s += "0" + "|";
        s += pesos.ToString() + "|";
        s += experience.ToString() + "|";
        s += weapon.weaponLevel.ToString();
        //print(s);
        PlayerPrefs.SetString("SaveState", s);
    }

    // Load the last state of the game

    public void LoadState(Scene s, LoadSceneMode mode)
    {
        if (!PlayerPrefs.HasKey("SaveState"))
        {
            return;
        }
        string[] data = PlayerPrefs.GetString("SaveState").Split('|');

        // charge player skin

        // charge Pesos
        pesos = int.Parse(data[1]);
        //print(pesos);
        //Debug.Log(pesos);
        // charge Experience
        experience = int.Parse(data[2]);
        // charge weapons 
        weapon.SetWeaponLevel(int.Parse(data[3]));
        
    }
}
