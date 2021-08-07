using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class item : MonoBehaviour
{
    // Start is called before the first frame update
    private bool is_interacted;
    public string obj_name;
    private Sprite uninteracted;
    private Sprite interacted;
    private GameObject item_instance;
    void Start()
    {
        is_interacted = false;
    }
    public void SetSprite(Sprite S_uninteracted, Sprite S_interacted)
    {
        this.interacted = S_interacted;
        this.uninteracted = S_interacted;
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
            var parent_gameobj = GameObject.Find(obj_name);

            is_interacted = true;
            GameObject.Find(obj_name).GetComponent<SpriteRenderer>().sprite = this.interacted;
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}
