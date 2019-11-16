using UnityEngine;

public class LvlChanger : MonoBehaviour
{
    [SerializeField] private GameObject[] levels;
    private int indexNextLvl = 0;

    private int NextLVL()
    {
        return indexNextLvl++;
    }

    public GameObject LoadNextLvl()
    {
        return levels[NextLVL()];
    }
}
