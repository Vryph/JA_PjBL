using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_Note : MonoBehaviour
{
    [SerializeField] private float velocity; public float GetVelocity() { return velocity; } public void SetVelocity(float value) { velocity = value; }
    [SerializeField] public Times time = null;

    [SerializeField] private GameObject error_note;
    [SerializeField] private float error_time;
    void Start()
    {
        error_time = Time.time + 5f;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 input = new Vector3(0,-1,0);
        Vector3 direcao = input.normalized;
        Vector3 real_velocity = direcao * velocity;
        real_velocity = real_velocity * UnityEngine.Time.deltaTime;

        transform.Translate(real_velocity);

        if(error_note != null)
        {
            if(time.complet)
            {
                Destroy(gameObject);
            }
            else if(time.fail)
            {
                GameObject new_note = Instantiate(error_note, transform.position, transform.rotation);
                new WaitForFixedUpdate();
                Destroy(gameObject);
            }
        }
        else
        {
            if(error_time <= Time.time)
            {
                Destroy(gameObject);
            }
        }

    }

    public void SetTime(Times time){ this.time = time; }
}
