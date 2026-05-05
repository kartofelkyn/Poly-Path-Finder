using UnityEngine;

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