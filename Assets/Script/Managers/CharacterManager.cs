using UnityEngine;

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