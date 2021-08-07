using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class item : MonoBehaviour
{
    // Start is called before the first frame update
    private bool is_interacted;
    public string obj_name;
    private GameObject item_instance;
    void Start()
    {
        is_interacted = false;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (is_interacted)
            return;
        if (Input.GetKey(KeyCode.Space))
        {
            print(this.GetComponent<Transform>().position);
            var scene_controller = GameObject.Find("Scene_Controller").GetComponent<sceneController>();
            scene_controller.AddItemNum();
            var parent_gameobj = GameObject.Find("obj_name");
            parent_gameobj.GetComponent<>
            is_interacted = true;
        }
    }



    // Update is called once per frame
    void Update()
    {

    }
}
