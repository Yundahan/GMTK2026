using UnityEngine;

public class SFXData
{
    private int numberOfClips;

    private string clipBaseName;

    private float clipPlayChance;

    public SFXData(int numberOfClips, string clipBaseName, float clipPlayChance)
    {
        this.numberOfClips = numberOfClips;
        this.clipBaseName = clipBaseName;
        this.clipPlayChance = clipPlayChance;
    }

    public int GetNumberOfClips()
    {
        return numberOfClips;
    }
    public string GetClipBaseName()
    {
        return clipBaseName;
    }

    public float GetClipPlayChance()
    {
        return clipPlayChance;
    }
}
