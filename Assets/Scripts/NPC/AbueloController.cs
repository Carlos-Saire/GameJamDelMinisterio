using Mono.Cecil.Cil;
using UnityEngine;

public class AbueloController : NPC
{
    [SerializeField] private DoorController doorController;
    private const string InteractedKey = "AbueloInteracted";
    protected override void Awake()
    {
        base.Awake();
        //if (PlayerPrefs.GetInt(InteractedKey, 0) == 1)
        //{
        //    LoadSceneManager.LoadScene("Mapa");
        //}
    }
    protected override void Interaction()
    {
        base.Interaction();
        doorController.SetBool(true);
        UpdateLayer("Default");

        PlayerPrefs.SetInt(InteractedKey, 1);
        PlayerPrefs.Save();
    }
    protected override void Start()
    {
        base.Start();
    }
}
