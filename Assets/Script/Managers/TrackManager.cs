using UnityEngine;

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
