using UnityEngine;

/// <summary>
/// This script is responsible for loading the selected character model for the player.
/// It uses the CharacterManager to get the currently selected character prefab and 
/// instantiates it as a child of the model holder transform.
/// This allows the player to see their selected character model in the game, providing 
/// a personalized gaming experience. By centralizing the character loading logic in this 
/// script, it simplifies the process of managing character models and ensures that the 
/// correct model is displayed based on the player's selection.
/// </summary>

public class PlayerCharacterLoader : MonoBehaviour
{
    public Transform modelHolder;

    void Start()
    {
        LoadCharacter();
    }

    void LoadCharacter()
    {
        // clear old model
        foreach (Transform child in modelHolder)
        {
            Destroy(child.gameObject);
        }

        // spawn selected model
        GameObject prefab = CharacterManager.Instance.GetCharacter();
        Instantiate(prefab, modelHolder);
    }
}