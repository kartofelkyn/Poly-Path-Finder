using UnityEngine;

/// <summary>
/// This script manages the character selection in the game. It uses a singleton pattern 
/// to allow easy access from other scripts. It provides methods to get the currently 
/// selected character prefab and to set the selected character index. The selected
/// character index is stored in PlayerPrefs, allowing the selection to persist between 
/// game sessions. This script centralizes the management of character selection, making 
/// it easier to add new characters or modify existing ones in the future. Other parts of 
/// the game can access the selected character through this manager, ensuring consistency across the game.
/// </summary>

public class CharacterManager : MonoBehaviour
{
    public static CharacterManager Instance;

    public GameObject[] characterPrefabs;
    public int selectedIndex;

    void Awake()
    {
        Instance = this;
        selectedIndex = PlayerPrefs.GetInt("SelectedCharacter", 2);
    }

    public GameObject GetCharacter()
    {
        return characterPrefabs[selectedIndex];
    }

    public void SetCharacter(int index)
    {
        selectedIndex = index;
        PlayerPrefs.SetInt("SelectedCharacter", index);
    }
}