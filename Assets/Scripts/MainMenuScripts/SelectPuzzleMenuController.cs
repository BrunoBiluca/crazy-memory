using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class SelectPuzzleMenuController : MonoBehaviour
{

    [SerializeField]
    private PuzzleGameManager puzzleGameManagerAux;

    [SerializeField]
    private LevelLocker levelLockerAux;

    [SerializeField]
    private StarsLocker starsLockerAux;

    [SerializeField]
    private LoadPuzzleGame loadPuzzleGameAux;

    [SerializeField]
    private GameObject selectPuzzleMenuPanel, puzzleLevelSelectMenuPanel;

    [SerializeField]
    private Animator selectPuzzleMenuAnim, puzzleLevelSelectMenuAnim;

    private PuzzleSO selectPuzzle;
    private int selectLevel;

    private bool[] puzzles;

    public void SelectedPuzzle()
    {
        AudioEffectsManager.Instance.PlayClickSound();
        starsLockerAux.DeactivateStars();

        selectPuzzle = EventSystem.current
            .currentSelectedGameObject
            .GetComponent<PuzzleHolder>()
            .puzzleSO;

        puzzleGameManagerAux.SetSelectedPuzzle(selectPuzzle.PuzzleName);

        levelLockerAux.CheckWhichLevelsAreUnlock(selectPuzzle.PuzzleName);

        StartCoroutine(ShowPuzzleLevelSelectMenu());
    }

    IEnumerator ShowPuzzleLevelSelectMenu()
    {
        puzzleLevelSelectMenuPanel.SetActive(true);
        selectPuzzleMenuAnim.Play("SlideOut");
        puzzleLevelSelectMenuAnim.Play("SlideIn");
        yield return new WaitForSeconds(1f);
        selectPuzzleMenuPanel.SetActive(false);
    }

    public void SelectedPuzzleLevel()
    {
        AudioEffectsManager.Instance.PlayClickSound();
        selectLevel = int.Parse(EventSystem.current.currentSelectedGameObject.name);

        puzzleGameManagerAux.SetSelectedLevel(selectLevel);

        puzzles = levelLockerAux.GetPuzzleLevels(selectPuzzle.PuzzleName);

        if(puzzles[selectLevel - 1])
        {
            loadPuzzleGameAux.PuzzleLoad(selectLevel, selectPuzzle);
        }
    }


    public void BackToSelectPuzzleMenu()
    {
        StartCoroutine(ShowPuzzleSelectMenu());
    }

    IEnumerator ShowPuzzleSelectMenu()
    {
        selectPuzzleMenuPanel.SetActive(true);
        puzzleLevelSelectMenuAnim.Play("SlideOut");
        selectPuzzleMenuAnim.Play("SlideIn");
        yield return new WaitForSeconds(1f);
        puzzleLevelSelectMenuPanel.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }


}
