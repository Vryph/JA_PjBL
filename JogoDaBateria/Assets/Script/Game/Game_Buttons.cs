using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Game_Buttons : MonoBehaviour
{
    [SerializeField] private Note.music_note note;
    [SerializeField] private GameObject spawn; public GameObject getSpawn() { return spawn; }
    void Start()
    {

    }


    void Update()
    {

    }

    public void OnMouseDown()
    {
        Debug.Log("Signal Bateria = " + note.HumanName());
        Sinal.Note_Sinal = note;

    }
}