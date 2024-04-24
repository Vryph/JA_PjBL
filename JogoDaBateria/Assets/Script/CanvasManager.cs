using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    [SerializeField] private GameObject MenuInicial;
    static string sinal = "";

    [SerializeField] private Animator seta;

    void Start()
    {

    }


    void Update()
    {

    }

    public void OpenOrClose(String input)
    {
        switch (input)
        {
            case "MenuInicial":
                MenuInicial.GetComponent<Animator>().SetBool("MenuInicial", !MenuInicial.GetComponent<Animator>().GetBool("MenuInicial"));

                if (MenuInicial.GetComponent<Animator>().GetBool("MenuInicial"))
                {
                    CanvasManager.sinal = "MenuInicial_True";
                }
                else
                {
                    CanvasManager.sinal = "MenuInicial_False";
                }

                break;
        }
    }
    public void OpenOrClose(GameObject button)
    {
        TextMeshProUGUI text = button.GetComponent<TextMeshProUGUI>();

        switch (CanvasManager.sinal)
        {
            case "MenuInicial_True":
                seta.SetBool("Ativo", false);
                text.text = ">"; break;
            case "MenuInicial_False":
                seta.SetBool("Ativo", true);
                text.text = "<"; break;
        }

        CanvasManager.sinal = "";
    }
}
