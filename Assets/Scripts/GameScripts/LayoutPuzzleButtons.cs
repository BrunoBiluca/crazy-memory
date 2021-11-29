using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class LayoutPuzzleButtons : MonoBehaviour
{

    [SerializeField]
    private SetupPuzzleGame setupPuzzleGameAux;

    [SerializeField]
    private Transform puzzleGameHolder1, puzzleGameHolder2, puzzleGameHolder3, puzzleGameHolder4, puzzleGameHolder5;

    public List<Button> level1Buttons, level2Buttons, level3Buttons, level4Buttons, level5Buttons;
    public List<Animator> level1Anims, level2Anims, level3Anims, level4Anims, level5Anims;

    private int selectedLevel;
    private PuzzleSO selectedPuzzle;

    public void LayoutButtons(int level, PuzzleSO puzzle)
    {
        this.selectedLevel = level;
        this.selectedPuzzle = puzzle;

        setupPuzzleGameAux.SetLevelAndPuzzle(selectedLevel, selectedPuzzle);

        LayoutPuzzle();
    }

    private void LayoutPuzzle()
    {
        switch(selectedLevel)
        {
            case 1:
                SetupPuzzleLevelButtons(puzzleGameHolder1, level1Buttons, level1Anims);
                break;
            case 2:
                SetupPuzzleLevelButtons(puzzleGameHolder2, level2Buttons, level2Anims);
                break;
            case 3:
                SetupPuzzleLevelButtons(puzzleGameHolder3, level3Buttons, level3Anims);
                break;
            case 4:
                SetupPuzzleLevelButtons(puzzleGameHolder4, level4Buttons, level4Anims);
                break;
            case 5:
                SetupPuzzleLevelButtons(puzzleGameHolder5, level5Buttons, level5Anims);
                break;
        }
    }

    private void SetupPuzzleLevelButtons(
        Transform puzzleGameHolder, 
        List<Button> levelButtons, 
        List<Animator> levelAnims
    )
    {
        foreach(Button btn in levelButtons)
        {
            if(!btn.gameObject.activeInHierarchy)
            {
                btn.gameObject.SetActive(true);
                btn.gameObject.transform.SetParent(puzzleGameHolder, false);

                btn.image.sprite = selectedPuzzle.Background;
            }
        }

        setupPuzzleGameAux.SetButtonsAndAnimators(levelButtons, levelAnims);
    }
}
