using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharachterMenu : MonoBehaviour
{
    //Text Fileds
    public Text levelText, hitpointText, pesosText, upgradeCostText, xpText;

    // Logic fields

    private int currentCharacterSelection = 0;
    public Image charahcterSelectionSprite;
    public Image weaponSprite;
    public RectTransform xpBar;
    public static int indexWeaponLevel = 0;
    //Charachter Selection
    public void OnArrowClick(bool right)
    {
        if (right)
        {
            currentCharacterSelection++;



            // If we went too far away

            if (currentCharacterSelection == GameManager.instance.palyerSprites.Count)
            {
                currentCharacterSelection = 0;

            }
            OnSelectionChnaged();
        }
        else
        {
            currentCharacterSelection--;



            // If we went too far away

            if (currentCharacterSelection <0)
            {
                currentCharacterSelection = GameManager.instance.palyerSprites.Count - 1;

            }
            OnSelectionChnaged();
        }

    }

    private void OnSelectionChnaged()
    {
        charahcterSelectionSprite.sprite = GameManager.instance.palyerSprites[currentCharacterSelection];
        GameManager.instance.player.SwapSprite(currentCharacterSelection);
    }

    // Weapon Upgrade

    public void OnUpgradeClick()
    {
        // Refernece to the weapon

        if (GameManager.instance.TryUpgradeWeapon())
        {
            UpdateMenu();
        }
    }

    // Update the Charactere information

    public void UpdateMenu()
    {
        // Weapon
       // Upgrade to the next weapon
       
        if (indexWeaponLevel  < GameManager.instance.weaponPrices.Count)
        {   // Increment the index
            Debug.Log(indexWeaponLevel);
            indexWeaponLevel++;
            weaponSprite.sprite = GameManager.instance.weaponSprites[indexWeaponLevel];
            if (GameManager.instance.weapon.weaponLevel == GameManager.instance.weaponPrices.Count)
            {
                upgradeCostText.text = "Max";
            }
            else
            {
                upgradeCostText.text = GameManager.instance.weaponPrices[GameManager.instance.weapon.weaponLevel].ToString();
            }
        }
        
        // Meta

        hitpointText.text = GameManager.instance.player.hitPoint.ToString();
        pesosText.text = GameManager.instance.pesos.ToString();

        // Level
        levelText.text = GameManager.instance.GetcurrentLevel().ToString();


        // Xp Bar
        int currentLevel = GameManager.instance.GetcurrentLevel();
        if (currentLevel== GameManager.instance.xpTable.Count)
        {
            xpText.text = GameManager.instance.experience.ToString()+ "total experience points";
            xpBar.localScale = Vector3.one;
        }
        else
        {
            int prevLevelXp = GameManager.instance.GetXpToLevel(currentLevel - 1);
            int currentLevelXp = GameManager.instance.GetXpToLevel(currentLevel);

            int diff = currentLevelXp - prevLevelXp;

            int currentXpIntoLevel = GameManager.instance.experience - prevLevelXp;

            float completionRatio = (float)currentXpIntoLevel / (float)diff;

            xpBar.localScale = new Vector3(completionRatio, 1, 1);
            xpText.text = currentXpIntoLevel.ToString() + " / " + diff;
        }

   
    }
}
