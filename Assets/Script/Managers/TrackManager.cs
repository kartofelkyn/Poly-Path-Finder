using UnityEngine;

/// <summary>
/// This script manages the track sections in the game. 
/// It uses a singleton pattern to allow easy access from other scripts. 
/// It provides a method to get a random track section from the predefined array of track sections. 
/// This allows for dynamic generation of the track as the player progresses through the game, 
/// creating a more engaging and varied gameplay experience. By centralizing the management of 
/// track sections in this script, it simplifies the process of adding new track sections or modifying existing ones in the future.
/// </summary> 

public class TrackManager : MonoBehaviour
{
    public static TrackManager Instance;
    public GameObject[] trackSections;
    public void Awake()
    {
        Instance = this;
    }
    public GameObject GetRandomTrackSection()
    {
        int randomIndex = Random.Range(0, trackSections.Length);
        return trackSections[randomIndex];
    }
}
