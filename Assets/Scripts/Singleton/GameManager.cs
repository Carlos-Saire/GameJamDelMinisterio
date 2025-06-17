using System;
using System.Collections;
using TMPro;
using UnityEngine;
public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public static Action<bool> OnEnableInput;

    [Header("Player Data")]
    private Transform player;
    private TMP_Text text_Player;
    private int life;

    [Header("UI Data")]
    private UIManager uiManager;

    [Header("Cinematica Dara")]
    private CinematicaController cinematica;

    [Header("Boss Data")]
    private Boss boss;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject); 
        }
    }
    private void OnEnable()
    {
        InputReader.OnInputPause += Pause;
    }
    private void OnDisable()
    {
        InputReader.OnInputPause -= Pause;
    }
    public void SetPlayer(Transform value,TMP_Text text)
    {
        player=value;
        text_Player=text;
    }
    public void SetPlayer(Transform value)
    {
        player = value;
    }
    public Transform GetTransformPlayer()
    {
        return player;
    }
    public TMP_Text GetTextPlayer()
    {
        return text_Player;
    }
    public void SetUIManager(UIManager value)
    {
        uiManager = value;
    }

    public void SetBoos(Boss boss)
    {
        this.boss = boss;
    }
    public void SetCinematica(CinematicaController cinematica)
    {
        this.cinematica = cinematica;
    }
    public void StartCinematica()
    {
        if (!boss.gameObject.activeSelf)
        {
            boss.gameObject.SetActive(true);
            boss.StartCombat();
        }
        cinematica.StartCinematica();
    }
    public void Dialogos(DialogueSO[] value)
    {
        uiManager.IniciarDialogo(value);
        EnableInput(false);
    }
    public void Fail()
    {
        Time.timeScale = 0;
        OnEnableInput?.Invoke(false);
        uiManager.PanelFaild();
    }
    public void Win()
    {
        Time.timeScale = 0;
        uiManager.Win();
        OnEnableInput?.Invoke(false);
    }
    public void Pause()
    {
        if (!MyButton.IsInteractue)
            return;
        if (Time.timeScale == 0)
        {
            uiManager.PanelPause(false);
        }
        else
        {
            Time.timeScale = 0;
            uiManager.PanelPause(true);
        }
    }
    public void EnableInput(bool value)
    {
        OnEnableInput?.Invoke(value);
    }
    public void Tutorial(bool value)
    {
        uiManager.PanelTutorial(value);
    }
    public void BuffTime(float duration,float timeScale)
    {
        StartCoroutine(BuffTimeCoroutine(duration, timeScale));
    }
    public void BuffLife(int vida)
    {
        this.life += vida;
    }
    private IEnumerator BuffTimeCoroutine(float duration, float timeScale)
    {
        float originalTimeScale = Time.timeScale;

        Time.timeScale = timeScale;

        yield return new WaitForSecondsRealtime(duration); 

        Time.timeScale = originalTimeScale;
    }
}
