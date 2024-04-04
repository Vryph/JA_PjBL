using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public static bool _DeBug = true;
    public static string _MenuAtual = "JogoLivre";

    public bool DeBug = true;
    public bool Change_DeBug = false;

    [SerializeField] private int MusicProgress = 0 ;
    [SerializeField] private int TarefasProgress = 0;

    public void Update()
    {
        if (Change_DeBug)
        {
            if (MenuManager._DeBug == true) { MenuManager._DeBug = false; }
            else { MenuManager._DeBug = true; }

            Change_DeBug = false;
        }

        this.DeBug = MenuManager._DeBug;
    }
    public void JogoLivre()
    {

        if (MenuManager._MenuAtual != "JogoLivre")
        {
            if (DeBug) { Debug.Log("Jogo Livre"); }

            MenuManager._MenuAtual = "JogoLivre";
            SceneManager.LoadScene("JogoLivre");
        }
    }
    public void Musicas()
    {

        if (MenuManager._MenuAtual != "Musicas")
        {
            if (DeBug) { Debug.Log("Musicas"); }

            MenuManager._MenuAtual = "Musicas";
            SceneManager.LoadScene("Musicas");
        }
        
    }
    public void Tarefas()
    {

        if(MenuManager._MenuAtual != "Tarefas")
        {
            if (DeBug) { Debug.Log("Tarefas"); }

            MenuManager._MenuAtual = "Tarefas";
            SceneManager.LoadScene("Tarefas");
        }
    }
}
