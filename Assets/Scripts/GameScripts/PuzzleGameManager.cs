using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PuzzleGameManager : MonoBehaviour
{

    [SerializeField]
    private GameSaver gameSaverAux;

    [SerializeField]
    private GameFineshed gameFineshedAux;

    [SerializeField]
    private List<Button> puzzleButtons = new List<Button>();
    private List<Animator> puzzleButtonsAnimators = new List<Animator>();

    [SerializeField]
    private List<Sprite> puzzleGameSprites = new List<Sprite>();

    private int selectedLevel;
    private string selectedPuzzle;

    private Sprite puzzleButtonBG;

    private int firstGuessIndex, secondGuessIndex;

    private bool firstGuess, secondGuess;

    private string firstGuessName, secondGuessName;

    private int tryCountGuess;
    private int gameGuess;
    private int correctGuess;

    private bool showingGuess;

    public void PickAPuzzle(int guessIndex)
    {
        if(showingGuess) return;

        showingGuess = true;

        AudioEffectsManager.Instance.PlayClickSound();

        if(!firstGuess)
        {
            firstGuess = true;

            firstGuessIndex = guessIndex;
            firstGuessName = puzzleGameSprites[guessIndex].name;

            StartCoroutine(
                TurnPuzzleButtonUp(
                    puzzleButtonsAnimators[firstGuessIndex],
                    puzzleButtons[firstGuessIndex],
                    puzzleGameSprites[firstGuessIndex]
                )
            );

        }
        else if(!secondGuess)
        {
            secondGuess = true;

            secondGuessIndex = guessIndex;
            secondGuessName = puzzleGameSprites[guessIndex].name;

            StartCoroutine(
                TurnPuzzleButtonUp(
                    puzzleButtonsAnimators[secondGuessIndex],
                    puzzleButtons[secondGuessIndex],
                    puzzleGameSprites[secondGuessIndex]
                )
            );

            StartCoroutine(CheckPuzzleMatch());

            tryCountGuess++;

        }
    }

    void CheckGameFinished()
    {
        correctGuess++;

        int HowManyGuesses = 0;

        if(correctGuess == gameGuess)
        {

            switch(selectedLevel)
            {
                case 1:
                    HowManyGuesses = 5;
                    break;
                case 2:
                    HowManyGuesses = 10;
                    break;
                case 3:
                    HowManyGuesses = 15;
                    break;
                case 4:
                    HowManyGuesses = 20;
                    break;
                case 5:
                    HowManyGuesses = 25;
                    break;
            }

            if(tryCountGuess < HowManyGuesses)
            {
                gameFineshedAux.ShowGameFineshedPanel(3);
                gameSaverAux.Save(selectedLevel, selectedPuzzle, 3);
            }
            else if(tryCountGuess < (HowManyGuesses + 5))
            {
                gameFineshedAux.ShowGameFineshedPanel(2);
                gameSaverAux.Save(selectedLevel, selectedPuzzle, 2);
            }
            else
            {
                gameFineshedAux.ShowGameFineshedPanel(1);
                gameSaverAux.Save(selectedLevel, selectedPuzzle, 1);
            }
        }
    }

    public List<Animator> ResetGameplay()
    {

        firstGuess = secondGuess = false;
        tryCountGuess = 0;
        correctGuess = 0;

        gameFineshedAux.HideGameFineshedPanel();

        return puzzleButtonsAnimators;
    }


    IEnumerator CheckPuzzleMatch()
    {

        yield return new WaitForSeconds(1f);

        if(firstGuessName == secondGuessName)
        {

            puzzleButtonsAnimators[firstGuessIndex].Play("FadeOut");
            puzzleButtonsAnimators[secondGuessIndex].Play("FadeOut");

            CheckGameFinished();

        }
        else
        {

            StartCoroutine(
                TurnPuzzleButtonBack(
                    puzzleButtonsAnimators[firstGuessIndex],
                    puzzleButtons[firstGuessIndex],
                    puzzleButtonBG
                )
            );

            StartCoroutine(
                TurnPuzzleButtonBack(
                    puzzleButtonsAnimators[secondGuessIndex],
                    puzzleButtons[secondGuessIndex],
                    puzzleButtonBG
                )
            );

        }

        yield return new WaitForSeconds(.7f);

        firstGuess = secondGuess = false;
        showingGuess = false;
    }

    IEnumerator TurnPuzzleButtonUp(Animator anim, Button btn, Sprite puzzleImage)
    {
        anim.Play("TurnUp");
        yield return new WaitForSeconds(0.4f);
        btn.image.sprite = puzzleImage;
        showingGuess = false;
    }

    IEnumerator TurnPuzzleButtonBack(Animator anim, Button btn, Sprite puzzleImage)
    {
        anim.Play("TurnBack");
        yield return new WaitForSeconds(0.4f);
        btn.image.sprite = puzzleImage;
        btn.interactable = true;
    }

    void AddListeners()
    {
        foreach(Button btn in this.puzzleButtons)
        {
            btn.onClick.RemoveAllListeners();
            btn.onClick.AddListener(() => {
                btn.interactable = false;
                var guessIdx = int.Parse(btn.name);
                PickAPuzzle(guessIdx);
            });
        }
    }

    public void SetupButtonsAndAnimators(List<Button> buttons, List<Animator> animators)
    {
        this.puzzleButtons = buttons;
        this.puzzleButtonsAnimators = animators;

        puzzleButtonBG = puzzleButtons[0].image.sprite;

        gameGuess = buttons.Count / 2;

        AddListeners();
    }

    public void SetupPuzzleGameSprites(List<Sprite> sprites)
    {
        this.puzzleGameSprites = sprites;
    }

    public void SetSelectedLevel(int level)
    {
        this.selectedLevel = level;
    }

    public void SetSelectedPuzzle(string puzzle)
    {
        this.selectedPuzzle = puzzle;
    }

}
