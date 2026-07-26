using UnityEngine;

public class SFXData
{
    private int numberOfClips;

    private string clipBaseName;

    private float clipPlayChance;

    private bool isQuote;

    public SFXData(int numberOfClips, string clipBaseName, float clipPlayChance, bool isQuote)
    {
        this.numberOfClips = numberOfClips;
        this.clipBaseName = clipBaseName;
        this.clipPlayChance = clipPlayChance;
        this.isQuote = isQuote;
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

    public bool IsQuote()
    {
        return isQuote;
    }
}
