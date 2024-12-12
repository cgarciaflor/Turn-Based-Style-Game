using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusPanelManager : MonoBehaviour
{
    SelectCharacter selectCharacter;
    bool isActive;
    [SerializeField] bool fixedCharacter;
    [SerializeField] GameObject statusPanelGameObj;
    [SerializeField] StatusPanel statusPanel;
    [SerializeField] CharacterScript currCharacterStatus;

    private void Awake()
    {
        selectCharacter = GetComponent<SelectCharacter>();
        
    }

    private void Update()
    {
        if (fixedCharacter == true)
        {
            statusPanel.UpdateStatus(currCharacterStatus);
        }
        else
        {
            MouseHoverOverObj();
        }
    }

    private void MouseHoverOverObj()
    {
        if (isActive == true)
        {
            statusPanel.UpdateStatus(currCharacterStatus);
            if (selectCharacter.hoverOverChar == null)
            {
                HideStatusPanel();
                return;
            }
            if (selectCharacter.hoverOverChar != currCharacterStatus)
            {
                currCharacterStatus = selectCharacter.hoverOverChar;
                statusPanel.UpdateStatus(currCharacterStatus);
                return;
            }

        }
        else
        {
            if (selectCharacter.hoverOverChar != null)
            {
                currCharacterStatus = selectCharacter.hoverOverChar;
                ShowStatusPanel();
                return;
            }
        }
    }

    private void HideStatusPanel()
    {
        statusPanelGameObj.SetActive(false);
        isActive = false;
    }

    private void ShowStatusPanel()
    {
        statusPanelGameObj.SetActive(true);
        isActive = true;
        statusPanel.UpdateStatus(selectCharacter.hoverOverChar);
    }

}
