using TMPro;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    float _timer = 0f;
    public TextMeshProUGUI StageText;
    public int StageSwitchTime = 15;
    public GameObject EndStageButton;
    Snake _snakeScript;
    bool _enteredTowerDefenseStage = false;

    private void Awake()
    {
        _snakeScript = FindFirstObjectByType<Snake>();
    }

    private void Update()
    {
        if (Pause.IsPaused) return;
        else
        {
            _timer += Time.deltaTime;
            int seconds = (int)_timer;

            StageText.text = seconds.ToString();
            
            if (Mathf.Approximately(seconds, StageSwitchTime) && !_enteredTowerDefenseStage)
            {
                EnterTowerDefenseStage();
                Debug.Log("defesa");
            }
        }
    }

    void EnterTowerDefenseStage()
    {
        _enteredTowerDefenseStage = true;

        //pause game
        PauseGame();
        _timer = StageSwitchTime;

        //ui changes
        StageText.text = "UPGRADE";
        EndStageButton.SetActive(true);
    }

    void ResetGame()
    {
        _snakeScript.ResetState();
        _timer = 0f;
    }

    public void PauseGame()
    {
        Pause.IsPaused = true;
        _snakeScript.CanMove = false;
    }

    public void ResumeGame()
    {
        Pause.IsPaused = false;
        _snakeScript.CanMove = true;
        EndStageButton.SetActive(false);
        //_timer = 0f;
        _enteredTowerDefenseStage = false;
        Debug.Log("Pause.IsPaused = " + Pause.IsPaused + ", Snake.CanMove = " + _snakeScript.CanMove);
    }
}
