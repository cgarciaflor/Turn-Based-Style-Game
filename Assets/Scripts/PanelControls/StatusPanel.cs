using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class StatusPanel : MonoBehaviour
{
    

    [SerializeField] TMPro.TextMeshProUGUI characterName;
    [SerializeField] UnityEngine.UI.Slider hpBar;

    [SerializeField] TMPro.TextMeshProUGUI lvlText;
    [SerializeField] UnityEngine.UI.Slider expbar;

    [SerializeField] CharacterAttributesText strAttributeText;
    [SerializeField] CharacterAttributesText magAttributeText;
    [SerializeField] CharacterAttributesText sklAttributeText;
    [SerializeField] CharacterAttributesText spdAttributeText;
    [SerializeField] CharacterAttributesText defAttributeText;
    [SerializeField] CharacterAttributesText resAttributeText;
    



    public void UpdateStatus(CharacterScript character)
    {
        hpBar.maxValue = character.hp.max;
        hpBar.value = character.hp.current;
        characterName.text = character.characterData.Name;

        lvlText.text = "LVL: " + character.characterData.level.level.ToString();
        expbar.maxValue = character.characterData.level.RequiredExpToLvlUp;
        expbar.value = character.characterData.level.exp;

        strAttributeText.UpdateText(character.characterData.attributes.Get(CharacterAttributeEnum.Strength));
        magAttributeText.UpdateText(character.characterData.attributes.Get(CharacterAttributeEnum.Magic));
        sklAttributeText.UpdateText(character.characterData.attributes.Get(CharacterAttributeEnum.Skill));
        defAttributeText.UpdateText(character.characterData.attributes.Get(CharacterAttributeEnum.Defence));
        resAttributeText.UpdateText(character.characterData.attributes.Get(CharacterAttributeEnum.Resistance));
        spdAttributeText.UpdateText(character.characterData.attributes.Get(CharacterAttributeEnum.Speed));
    }
}
