using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public static bool _DeBug = true;
    public static string _MenuAtual = "JogoLivre";

    public static int musica_01 = 0;
    public static int musica_02 = 0;
    public static int musica_03 = 0;
    public static int musica_04 = 0;
    public static int musica_05 = 0;

    public static int tarefa_01 = 0;
    public static int tarefa_02 = 0;
    public static int tarefa_03 = 0;
    public static int tarefa_04 = 0;
    public static int tarefa_05 = 0;

    public bool game_menu = false;

    public bool DeBug = true;
    public bool Change_DeBug = false;

    [SerializeField] private int MusicProgress = 0 ;
    [SerializeField] private int TarefasProgress = 0;

    // Animadores do Post It

    [SerializeField] private Animator button;
    [SerializeField] private Animator jogoLivre;
    [SerializeField] private Animator musicas;
    [SerializeField] private Animator tarefas;

    public void Update()
    {
        if(game_menu)
        {
            bool active_menu = !GetComponent<Animator>().GetBool("Ativo");

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                GetComponent<Animator>().SetBool("Ativo", active_menu);
            }

            if(!active_menu)
            {
                Time.timeScale = 0.0f;
            }
            else
            {
                Time.timeScale = 1.0f;
            }
        }

        if (Change_DeBug)
        {
            if (MenuManager._DeBug == true) { MenuManager._DeBug = false; }
            else { MenuManager._DeBug = true; }

            Change_DeBug = false;
        }

        this.DeBug = MenuManager._DeBug;
    }
    
    public void OpenOrCloseMenu()
    {
        Animator anim = GetComponent<Animator>();

        anim.SetBool("Ativo", !anim.GetBool("Ativo"));
        button.SetBool("Ativo", anim.GetBool("Ativo"));
    }
    #region  # Menu Transition #

    public static void ChangeMenu(String nestMenu)
    {
        if (MenuManager._MenuAtual != nestMenu)
        {
            if (MenuManager._DeBug) { Debug.Log(nestMenu); }

            MenuManager._MenuAtual = nestMenu;
            SceneManager.LoadScene(nestMenu);
        }

    }
    public void ChangeMenuButton(String newMenu)
    {
        switch (newMenu)
        {
            case "JogoLivre":
                if (!jogoLivre.GetAnimatorTransitionInfo(0).IsName("Clicou"))
                {
                    jogoLivre.SetTrigger("Clicou");
                }
                break;

            case "Musicas":
                if (!musicas.GetAnimatorTransitionInfo(0).IsName("Clicou"))
                {
                    musicas.SetTrigger("Clicou");
                }
                break;

            case "Tarefas":
                if (!tarefas.GetAnimatorTransitionInfo(0).IsName("Clicou"))
                {
                    tarefas.SetTrigger("Clicou");
                }
                break;
        }
    }

    #endregion

    #region # Stars Manager #

    public static void Stars_Set(Musica musica, int value)
    {

        if(musica.tarefa == true)
        {
            switch (musica.number)
            {
                case 1:
                    tarefa_01 = value; break;
                case 2:
                    tarefa_02 = value; break;
                case 3:
                    tarefa_03 = value; break;
                case 4:
                    tarefa_04 = value; break;
                case 5:
                    tarefa_05 = value; break;
            }
        }
        else
        {
            switch (musica.number)
            {
                case 1:
                    musica_01 = value; break;
                case 2:
                    musica_02 = value; break;
                case 3:
                    musica_03 = value; break;
                case 4:
                    musica_04 = value; break;
                case 5:
                    musica_05 = value; break;
            }
        }
        
    }
    public static int Stars_Get(int number, bool tarefa)
    {
        if(tarefa)
        {
            switch (number)
            {
                case 1:
                    return tarefa_01;
                case 2:
                    return tarefa_02;
                case 3:
                    return tarefa_03;
                case 4:
                    return tarefa_04;
                case 5:
                    return tarefa_05;
            }
        }
        else
        {
            switch (number)
            {
                case 1:
                    return musica_01;
                case 2:
                    return musica_02;
                case 3:
                    return musica_03;
                case 4:
                    return musica_04;
                case 5:
                    return musica_05;
            }
        }

        throw new StarsException("StarsException: A musica/tarefa solicitada não existe." +
                                 " Numero buscado: "+number+".");
    }
    public static int Stars_All()
    {
        int return_value = 0;

        for (int i = 0; i < 6; i++)
        {
            try
            {
                return_value += MenuManager.Stars_Get(i+1,false);
                return_value += MenuManager.Stars_Get(i+1,true);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        UnityEngine.Debug.Log("Valor de retorno: "+return_value);
        return return_value;
    }

    #endregion
}

public class StarsException : Exception
{
    public StarsException()
    {
        Console.WriteLine("StarsException: Algum erro envolvendo as estrelas do jogo não foi tratado.");
    }

    public StarsException(string msg) : base(msg)
    {

    }
}
