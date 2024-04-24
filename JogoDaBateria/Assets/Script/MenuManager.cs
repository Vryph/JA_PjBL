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

    public bool DeBug = true;
    public bool Change_DeBug = false;

    [SerializeField] private int MusicProgress = 0 ;
    [SerializeField] private int TarefasProgress = 0;

    // Animadores do Post It

    [SerializeField] private Animator jogoLivre;
    [SerializeField] private Animator musicas;
    [SerializeField] private Animator tarefas;

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

    #region  # Menu Transition #

    public void JogoLivre()
    {
        jogoLivre.SetTrigger("Clicou");

        if (MenuManager._MenuAtual != "JogoLivre")
        {
            if (DeBug) { Debug.Log("Jogo Livre"); }

            MenuManager._MenuAtual = "JogoLivre";
            SceneManager.LoadScene("JogoLivre");
        }
    }
    public void Musicas()
    {
        musicas.SetTrigger("Clicou");

        if (MenuManager._MenuAtual != "Musicas")
        {
            if (DeBug) { Debug.Log("Musicas"); }

            MenuManager._MenuAtual = "Musicas";
            SceneManager.LoadScene("Musicas");
        }
        
    }
    public void Tarefas()
    {
        tarefas.SetTrigger("Clicou");

        if (MenuManager._MenuAtual != "Tarefas")
        {
            if (DeBug) { Debug.Log("Tarefas"); }

            MenuManager._MenuAtual = "Tarefas";
            SceneManager.LoadScene("Tarefas");
        }
    }

    #endregion


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

        throw new StarsException("StarsException: A musica/tarefa solicitada n�o existe." +
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
}

public class StarsException : Exception
{
    public StarsException()
    {
        Console.WriteLine("StarsException: Algum erro envolvendo as estrelas do jogo n�o foi tratado.");
    }

    public StarsException(string msg) : base(msg)
    {

    }
}
