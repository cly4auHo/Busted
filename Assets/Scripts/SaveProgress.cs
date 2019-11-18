using UnityEngine;

public class SaveProgress : MonoBehaviour
{
    private int currentLvl;
    private const string key = "CurrentLVL";

    void Start()
    {
        LoadLVL();
    }


    void saveLVL()
    {
        PlayerPrefs.SetInt(key, currentLvl);
        PlayerPrefs.Save();
    }


    void LoadLVL()
    {
        if (PlayerPrefs.HasKey(key))
        {
            this.currentLvl = PlayerPrefs.GetInt(key);
        }
    }

    public void SetLVL(int currentLvl)
    {
        this.currentLvl = currentLvl;
    }

    public int GetLVL()
    {
        return currentLvl;
    }
}
