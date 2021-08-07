using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bgmController : MonoBehaviour
{
    private AudioSource audioSource;
    // Update is called once per frame
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void Update()
    {
        
        float dis = (GameObject.Find("Ending_Trigger").GetComponent<Transform>().position - GameObject.Find("Player").GetComponent<Transform>().position).sqrMagnitude;
        if (dis > 1f)
            dis = 1f;
        set_volume(dis);
    }
    public void set_volume(float volume)
    {
        audioSource.volume = volume;
    }
}
