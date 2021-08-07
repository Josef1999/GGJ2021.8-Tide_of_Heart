using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.constant;
public class playerControl : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        
        float dis = (GameObject.Find("Ending_Trigger").GetComponent<Transform>().position - this.GetComponent<Transform>().position).sqrMagnitude;
        //print("Distance to Ending_Trigger"+dis.ToString());
        float x = this.GetComponent<Transform>().position.x;
		float y = this.GetComponent<Transform>().position.y;
        if (x > constant.MAX_PLAYER_MOVABLE_WIDTH)
        {
            transform.position = new Vector3(constant.MAX_PLAYER_MOVABLE_WIDTH, y);
            return;
        }
        
        else if (x < constant.MIN_PLAYER_MOVABLE_WIDTH)
        {
            transform.position = new Vector3(constant.MIN_PLAYER_MOVABLE_WIDTH, y);
            return;
        }
        if (y > constant.MAX_PLAYER_MOVABLE_HEIGHT)
        {
            transform.position = new Vector3(x, constant.MAX_PLAYER_MOVABLE_HEIGHT);
            return;
        }

        else if (y < constant.MIN_PLAYER_MOVABLE_HEIGHT)
        {
            transform.position = new Vector3(x, constant.MIN_PLAYER_MOVABLE_HEIGHT);
            return;
        }
        

        float moveSpeed = 10;
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        transform.Translate(new Vector3(horizontalInput, verticalInput, 0) * moveSpeed * Time.deltaTime);
 

    }

}
