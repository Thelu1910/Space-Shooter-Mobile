using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ButtonIcon : MonoBehaviour
{
    [SerializeField] private Button[] lvlButton;
    [SerializeField] private Sprite unlockIcon;
    [SerializeField] private Sprite lockIcon;
    [SerializeField] private int firstLevelBuildIndex;

    private void Awake()
    {
        int unlockedLvl = PlayerPrefs.GetInt(EndGameManager.endManager.lvlUnlock, firstLevelBuildIndex);
        for (int i = 0; i < lvlButton.Length; i++)
        {
            if (i + firstLevelBuildIndex <= unlockedLvl)
            {
                lvlButton[i].interactable = true;
                lvlButton[i].image.sprite = unlockIcon;
                TextMeshProUGUI textButton = lvlButton[i].GetComponentInChildren<TextMeshProUGUI>();
                textButton.text = (i + 1).ToString();
                textButton.enabled = true;
            }
            else
            {
                lvlButton[i].interactable = false;
                lvlButton[i].image.sprite = lockIcon;
                lvlButton[i].GetComponentInChildren<TextMeshProUGUI>().enabled = false;
            }
        }
    }
}
