using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class sceneController : MonoBehaviour
{
    private List<GameObject> items;
    private int item_num;
    private int interacted_item_num;
    private int cur_scene_no;
    private int scenes_cnt;
    private List<Scene> scenes;
    private Object[] target_icons;
    private Object[] maps;
    private GameObject cur_map;

    void Start()
    {
        InitScenes();
        GenerateScene();
    }
    private void Update()
    {

        if (interacted_item_num >= item_num)
            SwitchScene();
    }

    void InitScenes()
    {
        target_icons = Resources.LoadAll("Target_Icon", typeof(Sprite));
        maps = Resources.LoadAll("Map", typeof(GameObject));
        scenes_cnt = maps.Length;
        scenes = new List<Scene>();
        for (int i = 0; i < scenes_cnt; i++)
        {
            scenes.Add(new Scene((Sprite)target_icons[i * 2], (Sprite)target_icons[i * 2 + 1], (GameObject)maps[i]));
        }
        cur_scene_no = 0;
    }
    void SwitchScene()
    {
        DestoryItems();
        if (cur_scene_no >= scenes_cnt - 1)
        {
            cur_scene_no = 0;
            ShuffleScene();
        }
        else
            cur_scene_no++;
        GenerateScene();
    }
    void GenerateScene()
    {
        print(cur_scene_no);
        print(scenes[cur_scene_no]._map.name);
        if (cur_map != null)
            Destroy(cur_map);
        cur_map = Instantiate(scenes[cur_scene_no]._map);
        cur_map.transform.position = new Vector3(0, 0);
        GenerateItems(scenes[cur_scene_no]._uninteracted, scenes[cur_scene_no]._interacted);

    }

    GameObject GenerateItem(Vector3 Pos, string name, Sprite S_uninteracted, Sprite S_interacted)
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
        target.GetComponent<SpriteRenderer>().sprite = S_uninteracted;

        target.AddComponent<item>();
        target.GetComponent<item>().obj_name = name;
        target.GetComponent<item>().SetSprite(S_uninteracted, S_interacted);
        return target;
    }
    void True_Ending()
    {
        //todo
    }
    public void AddItemNum()
    {
        interacted_item_num++;
    }
    void GenerateItems(Sprite S_uninteracted, Sprite S_interacted)
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
            items.Add(GenerateItem(Pos, "item_" + i.ToString(), S_uninteracted, S_interacted));
        }
    }
    void DestoryItems()
    {
        foreach (var item in items)
            Destroy(item);
        interacted_item_num = 0;
        item_num = 0;
    }
    



    void ShuffleScene()
    {
        for (int i = scenes_cnt-1; i >0; i--)
        {
            int rnd = Random.Range(0,i+1);
            var temp = scenes[rnd];
            scenes[rnd] = scenes[i];
            scenes[i] = temp;
        }
        foreach (var s in scenes)
            print(s._map.name);
    }
}


public class Scene
{
    public Sprite _uninteracted, _interacted;
    public GameObject _map;
    public Scene(Sprite S_uninteracted, Sprite S_interacted, GameObject map)
    {
        _uninteracted = S_uninteracted;
        _interacted = S_interacted;
        _map = map;
    }
}