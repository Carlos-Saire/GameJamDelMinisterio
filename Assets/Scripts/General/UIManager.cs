using UnityEngine;
using System.Collections;
public class UIManager : MonoBehaviour
{

    [Header("Paneles")]
    [SerializeField] private GameObject panelGame;
    [SerializeField] private ButtonUI panelPause;
    [SerializeField] private DoMove panelTutorial;
    [SerializeField] private GameObject panelDialogos;

    [Header("Game")]
    [SerializeField] private DoMove panelFail;
    [SerializeField] private GameObject game;

    [SerializeField] private DialogueManager dialogueManager;

    [Header("Reset")]
    [SerializeField] private RectTransform[] ojectReset;
    [SerializeField] private Vector2[] positionInitial;
    [SerializeField] private DoMove PanelWin;
    private void Awake()
    {
        //dialogueManager = GetComponentInChildren<DialogueManager>();
    }
    private void Start()
    {
        GameManager.instance.SetUIManager(this);
        positionInitial = new Vector2[ojectReset.Length];
        for(int i = 0; i < ojectReset.Length; ++i)
        {
            positionInitial[i]= ojectReset[i].anchoredPosition;
        }
        //panelDialogos.SetActive(false);
        //GameManager.instance.SetUIManager(this);
    }
    private void OnEnable()
    {
        //dialogueManager.OnFinishDialogue += PanelDialogos;
    }
    private void OnDisable()
    {
        //dialogueManager.OnFinishDialogue -= PanelDialogos;
    }
    public void PanelFaild()
    {
        panelFail.Go();
    }
    private void ResetPosition()
    {
        for (int i = 0; i < positionInitial.Length; ++i)
        {
            ojectReset[i].anchoredPosition = positionInitial[i];
        }
    }
    private void LoadPanel(string name)
    {
        switch (name)
        {
            case "Game":
                panelFail.Return();
                game.SetActive(true);
                break;
            case "Mapa":
                game.SetActive(false);
                break;
        }
        ResetPosition();
    }
    public void Win()
    {
        PanelWin.Go();
    }
    public void PanelTutorial(bool value)
    {
        if (value)
        {
            GameManager.instance.EnableInput(true);
            panelTutorial.Go();
            Time.timeScale = 1;
        }
        else
        {
            panelTutorial.Return();
        }
    }
    public void IniciarDialogo(DialogueSO[] value)
    {
        /*
        PanelDialogos();
        dialogueManager.SetDialogue(value);
        */
        OnClick();
    }


    //
    public  void OnClick()
    {
        
            StartCoroutine(StartAnimation());
        
    }
    public IEnumerator StartAnimation()
    {
        Transicion.Instance.TransicionCerrarPuertas();
        yield return new WaitForSecondsRealtime(2f);

        LoadSceneManager.LoadScene("a");


    }
    //
    public void PanelPause(bool value)
    {
        if (value)
        {
            panelPause.ActiveClick(value);
        }
        else
        {
            GameManager.instance.EnableInput(false);
            panelPause.ActiveClick(value, QuitPause);
        }
    }
    public void PanelDialogos()
    {
        if (!panelDialogos.gameObject.activeSelf)
        {
            panelDialogos.gameObject.SetActive(true);
        }
        else
        {
            panelDialogos.SetActive(false);
        }
    }
    private void QuitPause()
    {
        Time.timeScale = 1;
        GameManager.instance.EnableInput(true);
    }
}
