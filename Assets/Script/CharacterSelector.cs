using UnityEngine;
using TMPro;

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
        Debug.Log("Selected: " + characters[index].characterName);
    }
}