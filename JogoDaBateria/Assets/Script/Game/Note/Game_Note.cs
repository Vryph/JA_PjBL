using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_Note : MonoBehaviour
{
    [SerializeField] private float velocity; public float GetVelocity() { return velocity; } public void SetVelocity(float value) { velocity = value; }
    [SerializeField] private Times time;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 input = new Vector3(0,-1,0);
        Vector3 direcao = input.normalized;
        Vector3 real_velocity = direcao * velocity;
        real_velocity = real_velocity * UnityEngine.Time.deltaTime;

        transform.Translate(real_velocity);

        if(time.complet || time.fail)
        {
            Destroy(gameObject);
        }
    }

    public void SetTime(Times time){ this.time = time; }
}
