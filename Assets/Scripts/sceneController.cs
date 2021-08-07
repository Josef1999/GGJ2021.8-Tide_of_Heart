using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.constant;
public class sceneController : MonoBehaviour
{
    private List<GameObject> items;
    private int item_num;
    private int interacted_item_num;
    GameObject GenerateItem(Vector3 Pos, string name)
    {
        GameObject target = new GameObject();
        target.transform.position = Pos;
        target.name = name;
        target.AddComponent<Rigidbody2D>();
        target.GetComponent<Rigidbody2D>().simulated = true;
        target.GetComponent<Rigidbody2D>().gravityScale = 0;

        target.AddComponent<CircleCollider2D>();
        target.GetComponent<CircleCollider2D>().isTrigger = true;


        target.AddComponent<SpriteRenderer>();
        target.GetComponent<SpriteRenderer>().sprite = GameObject.Find("Player").GetComponent<SpriteRenderer>().sprite;
    
        target.AddComponent<item>();
        target.GetComponent<item>().obj_name = name;
        return target;
    }
    public void AddItemNum()
    {
        interacted_item_num++;
    }
    void GenerateItems()
    {
        interacted_item_num = 0;
        item_num = Random.Range(3, 6);
        items = new List<GameObject>();
        HashSet<Vector3> generated_pos = new HashSet<Vector3>();
        generated_pos.Add(new Vector3(GameObject.Find("Player").GetComponent<Transform>().position.x, GameObject.Find("Player").GetComponent<Transform>().position.y));
        for (int i = 0; i < item_num; i++)
        {
            var Pos = new Vector3(Random.Range(constant.MIN_ITEM_GENERATION_WIDTH, constant.MAX_ITEM_GENERATION_WIDTH), Random.Range(constant.MIN_ITEM_GENERATION_HEIGHT, constant.MAX_ITEM_GENERATION_HEIGHT));
            while (generated_pos.Contains(Pos))
                Pos = new Vector3(Random.Range(constant.MIN_ITEM_GENERATION_WIDTH, constant.MAX_ITEM_GENERATION_WIDTH), Random.Range(constant.MIN_ITEM_GENERATION_HEIGHT, constant.MAX_ITEM_GENERATION_HEIGHT));
            generated_pos.Add(Pos);

            items.Add(GenerateItem(Pos, "item_" + i.ToString()));

        }
    }
    void DestoryItems()
    {
        foreach (var item in items)
            Destroy(item);
    }
    void SwitchScene()
    {
        DestoryItems();
    }
    // Start is called before the first frame update
    void Start()
    {
        GenerateItems();
    }
    private void Update()
    {
        if (interacted_item_num >= item_num)
        {
            Debug.Log("ÇÐ»»³¡¾°");
            SwitchScene();
        }
    }


}
