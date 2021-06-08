using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game/Puzzle")]
public class PuzzleSO : ScriptableObject
{
    public string PuzzleName;
    public Sprite Background;
    public Sprite[] PuzzleSprites;
}
