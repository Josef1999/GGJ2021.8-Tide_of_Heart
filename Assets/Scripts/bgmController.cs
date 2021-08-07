using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bgmController : MonoBehaviour
{
    private AudioSource audioSource;
    private float dis;
    // Update is called once per frame
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void Update()
    {

        dis = (GameObject.Find("Ending_Trigger").GetComponent<Transform>().position - GameObject.Find("Player").GetComponent<Transform>().position).sqrMagnitude;
        float stero_pan = GameObject.Find("Ending_Trigger").GetComponent<Transform>().position.x - GameObject.Find("Player").GetComponent<Transform>().position.x;
        audioSource.panStereo = stero_pan;
        audioSource.volume = 1/dis;
    }
    public float GetDis()
    {
        return this.dis;
    }
    public void PlayBGM()
    {
        audioSource.Play();
    }
    public void StopBGM()
    {
        audioSource.Stop();
    }

}

