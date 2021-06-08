using UnityEngine;
using System.Collections;

public class StarsLocker : MonoBehaviour
{

    [SerializeField]
    private GameSaver gameSaverAux;

    [SerializeField]
    private GameObject[] level1Stars, level2Stars, level3Stars, level4Stars, level5Stars;

    [SerializeField]
    private int[] candyPuzzleLevelStars;
    private int[] transportPuzzleLevelStars;
    private int[] fruitsPuzzleLevelStars;


    void Awake()
    {
        DeactivateStars();
    }

    void Start()
    {
        GetStars();
    }

    public void ActivateStars(int level, string selectedPuzzle)
    {
        GetStars();
        int stars;
        switch(selectedPuzzle)
        {
            case "CandyPuzzle":
                stars = candyPuzzleLevelStars[level];
                ActivateLevelStars(level, stars);
                break;
            case "TransportPuzzle":
                stars = transportPuzzleLevelStars[level];
                ActivateLevelStars(level, stars);
                break;
            case "FruitPuzzle":
                stars = fruitsPuzzleLevelStars[level];
                ActivateLevelStars(level, stars);
                break;
        }
    }

    void ActivateLevelStars(int level, int looper)
    {
        switch(level)
        {
            case 0:
                ActivateStars(level1Stars, looper);
                break;
            case 1:
                ActivateStars(level2Stars, looper); 
                break;
            case 2:
                ActivateStars(level3Stars, looper); 
                break;
            case 3:
                ActivateStars(level4Stars, looper); 
                break;
            case 4:
                ActivateStars(level5Stars, looper); 
                break;
        }

    }

    private void ActivateStars(GameObject[] stars, int looper)
    {
        if(looper == 0) return;

        for(int i = 0; i < looper; i++)
        {
            stars[i].SetActive(true);
        }
    }

    public void DeactivateStars()
    {
        for(int i = 0; i < level1Stars.Length; i++)
        {
            level1Stars[i].SetActive(false);
            level2Stars[i].SetActive(false);
            level3Stars[i].SetActive(false);
            level4Stars[i].SetActive(false);
            level5Stars[i].SetActive(false);
        }
    }

    void GetStars()
    {
        candyPuzzleLevelStars = gameSaverAux.candyPuzzleLevelStars;
        transportPuzzleLevelStars = gameSaverAux.transportPuzzleLevelStars;
        fruitsPuzzleLevelStars = gameSaverAux.fruitsPuzzleLevelStars;
    }


}
