using UnityEngine;
using TMPro;

/// <summary>
/// This script manages the character selection process in the game. 
/// It allows players to cycle through available characters, view 
/// their names, and select one for use in the game. The selected character 
/// is saved using PlayerPrefs, allowing the choice to persist across game 
/// sessions. The script also updates the UI to reflect the currently 
/// selected character and provides feedback when a character is selected.
/// </summary>

public class CharacterSelector : MonoBehaviour
{
    public CharacterData[] characters;
    public TextMeshProUGUI characterNameText;
    public TextMeshProUGUI selectButtonText;
    private int index = 0;

    void Start()
    {
        ShowCharacter(index);
    }

    public void Next()
    {
        index = (index + 1) % characters.Length;
        ShowCharacter(index);
    }

    public void Previous()
    {
        index--;
        if (index < 0)
            index = characters.Length - 1;

        ShowCharacter(index);
    }

    void ShowCharacter(int i)
    {
        for (int x = 0; x < characters.Length; x++)
        {
            characters[x].characterObject.SetActive(x == i);
        }
        characterNameText.text = characters[index].characterName;
        if (PlayerPrefs.GetInt("SelectedCharacter", 0) == index)
        {
            selectButtonText.text = "Selected";
        }
        else
        {
            selectButtonText.text = "Select";
        }
    }

    public void Select()
    {
        PlayerPrefs.SetInt("SelectedCharacter", index);
        PlayerPrefs.SetString("SelectedCharacterName", characters[index].characterName);
        selectButtonText.text = "Selected";
    }
}