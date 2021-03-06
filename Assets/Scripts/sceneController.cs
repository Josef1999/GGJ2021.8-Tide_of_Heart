using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Text;
using System.Threading;
public class sceneController : MonoBehaviour
{
    private List<GameObject> items;
    private int item_num;
    private int interacted_item_num;
    private int cur_scene_no;
    private int scenes_cnt;
    private int passed_scenes_cnt;
    private List<string> Reminder;
    private List<Scene> scenes;
    private bool end = false;
    private GameObject cur_map;
    private GameObject gold_sound;
    private GameObject camera;
    private GameObject player;
    private GameObject end_ui;
    void Start()
    {
        InitScenes();
        GenerateScene();
    }

    private void Update()
    {
        UI_Update();
        if (end)
        {
            camera.GetComponent<shakeCamera>().enabled = false;

            const float speed = 3f;
            Vector3 PLAYER_FINAL_POS = new Vector3(player.transform.position.x, -17.8f, player.transform.position.z);

            camera.transform.position = Vector3.MoveTowards(camera.transform.position, constant.CAM_FINAL_POS, speed * Time.deltaTime);
            player.transform.position = Vector3.MoveTowards(player.transform.position, PLAYER_FINAL_POS, speed/2.0f * Time.deltaTime);
            if(player.transform.position == PLAYER_FINAL_POS)
                GameObject.Find("Canvas/ENDING").GetComponent<Text>().enabled = true;
            return;
        }

        GameObject.Find("Canvas/ENDING").GetComponent<Text>().enabled = false;

        if (cur_scene_no == 0)
        {
            camera.GetComponent<shakeCamera>().enabled = true;
            camera.GetComponent<shakeCamera>().shakeLevel = 10 - Mathf.Min(Mathf.Abs(Mathf.Log(GameObject.Find("Bgm").GetComponent<bgmController>().GetDis(), 0.8f)), 10);
            if (GameObject.Find("Bgm").GetComponent<bgmController>().GetDis()<0.1f && Input.GetKeyDown(KeyCode.Space))
            {
                True_Ending();
            }
        }
        else
        {
           // camera.GetComponent<shakeCamera>().enabled = false;
            camera.GetComponent<shakeCamera>().enabled = false;

        }

        if (interacted_item_num >= item_num)
            SwitchScene();
    }
    void UI_Update()
    {
        GameObject.Find("Canvas/Task_Status").GetComponent<Text>().text = TaskStatus();
        GameObject.Find("Canvas/Task_Cnt").GetComponent<Text>().color = Color.yellow;
        GameObject.Find("Canvas/Task_Cnt").GetComponent<Text>().text = "$: "+(passed_scenes_cnt*100).ToString();
        GameObject.Find("Canvas/Task_Reminder").GetComponent<Text>().text = scenes[cur_scene_no]._text;
        GameObject.Find("Canvas/Task_Reminder").GetComponent<Text>().alignment = TextAnchor.MiddleCenter;
        GameObject.Find("Canvas/New_Player_Reminder").GetComponent<Text>().text = passed_scenes_cnt < Reminder.Count ? Reminder[passed_scenes_cnt] :"";
    }
    string TaskStatus()
    {

        return interacted_item_num.ToString() + "/" + item_num.ToString();
    }
    void InitScenes()
    {
  
        var target_icons = Resources.LoadAll("Target_Icon", typeof(Sprite));
        var maps = Resources.LoadAll("Map", typeof(GameObject));
        var texts = Resources.LoadAll("Text");
        var clips = Resources.LoadAll("Sound_Effect");
        var bgms = Resources.LoadAll("BGM");
        TextAsset level_reminder = Resources.Load<TextAsset>("Level_Reminder");
        Reminder = new List<string>( level_reminder.text.Split('\n'));
        gold_sound = new GameObject();
        gold_sound.AddComponent<AudioSource>();
        gold_sound.GetComponent<AudioSource>().clip = Resources.Load<AudioClip>("gold");
        scenes_cnt = maps.Length;
        scenes = new List<Scene>();
        for (int i = 0; i < scenes_cnt; i++)
        {
            scenes.Add(new Scene(
                (Sprite)target_icons[i * 2], 
                (Sprite)target_icons[i * 2 + 1], 
                (GameObject)maps[i], 
                texts[i].ToString(),
                (AudioClip)clips[i],
                (AudioClip)bgms[i]
                )
                );
        }
        player = GameObject.Find("Player");
        camera = GameObject.Find("Main Camera");
        cur_scene_no = 1;
        passed_scenes_cnt = 0;

    }
    void UI_ON()
    {
        
        GameObject.Find("Canvas/Task_Status").GetComponent<Text>().enabled = true;
        GameObject.Find("Canvas/Task_Cnt").GetComponent<Text>().enabled = true;
        GameObject.Find("Canvas/Task_Reminder").GetComponent<Text>().enabled = true;
        GameObject.Find("Canvas/New_Player_Reminder").GetComponent<Text>().enabled = true;
    }
    void UI_Off()
    {

        GameObject.Find("Canvas/Task_Status").GetComponent<Text>().enabled = false;
        GameObject.Find("Canvas/Task_Cnt").GetComponent<Text>().enabled = false;
        GameObject.Find("Canvas/Task_Reminder").GetComponent<Text>().enabled = false;
        GameObject.Find("Canvas/New_Player_Reminder").GetComponent<Text>().text = " ";
        GameObject.Find("Canvas/New_Player_Reminder").GetComponent<Text>().enabled = false;

    }
    void SwitchScene()
    {
        gold_sound.GetComponent<AudioSource>().Play();
        DestoryItems();
        scenes[cur_scene_no]._bgm.GetComponent<AudioSource>().Pause();
        if (cur_scene_no >= scenes_cnt - 1)
        {
            cur_scene_no = 0;
            ShuffleScene();
        }
        else
            cur_scene_no++;
        passed_scenes_cnt++;
        GenerateScene();
        StartCoroutine(WaitAndDisableBlackScreen(0.5F));
        

    }

    IEnumerator WaitAndDisableBlackScreen(float waitTime)
    {
        UI_Off();
        EnableBlackScreen();
        yield return new WaitForSeconds(waitTime);
        DisableBlackScreen();
        player.GetComponent<playerControl>().EnableMove();
        UI_ON();
        UI_Update();
    }  
    void GenerateScene()
    {
        SetBGM();
        print(cur_scene_no);
        print(scenes[cur_scene_no]._map.name);
        if (cur_map != null)
            Destroy(cur_map);
        cur_map = Instantiate(scenes[cur_scene_no]._map);
        cur_map.transform.position = new Vector3(0, 0);
        GenerateItems(scenes[cur_scene_no]);

    }

    GameObject GenerateItem(Vector3 Pos, string name, Scene s,bool is_icon=false)
    {
        GameObject target = new GameObject();
        target.transform.position = Pos;
        target.name = name;
        target.AddComponent<Rigidbody2D>();
        target.GetComponent<Rigidbody2D>().simulated = true;
        target.GetComponent<Rigidbody2D>().gravityScale = 0;

        target.AddComponent<CircleCollider2D>();
        target.GetComponent<CircleCollider2D>().isTrigger = true;

        target.AddComponent<AudioSource>();
        target.GetComponent<AudioSource>().clip = s._clip;
        target.AddComponent<SpriteRenderer>();
        target.GetComponent<SpriteRenderer>().sprite = s._uninteracted;
        if (is_icon)
        {
            target.GetComponent<Transform>().localScale = constant.ICON_SCALE;
            target.transform.position = Pos;
        }
        
        target.AddComponent<item>();
        target.GetComponent<item>().obj_name = name;
        target.GetComponent<item>().SetSprite(s._uninteracted, s._interacted);
        return target;
    }
    void True_Ending()
    {
        player.GetComponent<playerControl>().DisableMove();
        UI_Off();
        print("True Ending");
        end = true;
        GameObject.Find("Bgm").GetComponent<bgmController>().MaxVolume();



    }
    public void AddItemNum()
    {
        interacted_item_num++;
    }
    void GenerateItems(Scene s)
    {
        interacted_item_num = 0;
        item_num = Random.Range(3, 6);
        items = new List<GameObject>();
        HashSet<Vector3> generated_pos = new HashSet<Vector3>();
        Vector3 Pos;
        int row = Random.Range(constant.MIN_ROW, constant.MAX_ROW + 1);
        int col = Random.Range(constant.MIN_COL, constant.MAX_COL + 1);
        Pos = new Vector3((float)(col - 0.5), (float)(row - 0.5));
        generated_pos.Add(Pos);
        for (int i = 0; i < item_num; i++)
        {
            //var Pos = new Vector3(Random.Range(constant.MIN_ITEM_GENERATION_WIDTH, constant.MAX_ITEM_GENERATION_WIDTH), Random.Range(constant.MIN_ITEM_GENERATION_HEIGHT, constant.MAX_ITEM_GENERATION_HEIGHT));
            //while (generated_pos.Contains(Pos))
            //Pos = new Vector3(Random.Range(constant.MIN_ITEM_GENERATION_WIDTH, constant.MAX_ITEM_GENERATION_WIDTH), Random.Range(constant.MIN_ITEM_GENERATION_HEIGHT, constant.MAX_ITEM_GENERATION_HEIGHT));

            do
            {
                row = Random.Range(constant.MIN_ROW, constant.MAX_ROW + 1);
                col = Random.Range(constant.MIN_COL, constant.MAX_COL + 1);
                Pos = new Vector3((float)(col - 0.5), (float)(row - 0.5));
            } while (generated_pos.Contains(Pos));
            
            generated_pos.Add(Pos);
            items.Add(GenerateItem(Pos, "item_" + i.ToString(), s));
        }
        //items.Add(GenerateItem(constant.ICON_POS, "item_0", s,true));
    }
    void DestoryItems()
    {
        foreach (var item in items)
            Destroy(item);
        interacted_item_num = 0;
        item_num = 0;
    }
    
    void SetBGM()
    {
        scenes[cur_scene_no]._bgm.GetComponent<AudioSource>().Play();
        if (cur_scene_no>0)
        {
            GameObject.Find("Bgm").GetComponent<bgmController>().StopBGM();
        }
        else
        {
            GameObject.Find("Bgm").GetComponent<bgmController>().PlayBGM();
        }
    }
    void EnableBlackScreen()
    {
        GameObject.Find("Default_Background").GetComponent<SpriteRenderer>().sortingLayerName = "Front";
    }
    void DisableBlackScreen()
    {
        GameObject.Find("Default_Background").GetComponent<SpriteRenderer>().sortingLayerName = "BackGround";
    }
    void ShuffleScene()
    {
        for (int i = scenes_cnt-1; i >1; i--)
        {
            int rnd = Random.Range(1,i+1);
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
    public string _text;
    public AudioClip _clip;
    public GameObject _bgm = new GameObject();
    public Scene(Sprite S_uninteracted, Sprite S_interacted, GameObject map,string text, AudioClip clip, AudioClip bgm)
    {
        _uninteracted = S_uninteracted;
        _interacted = S_interacted;
        _map = map;
        _text = text;
        _clip = clip;
        _bgm.AddComponent<AudioSource>();
        _bgm.GetComponent<AudioSource>().volume = 0.8f;

        _bgm.GetComponent<AudioSource>().clip = bgm;
        
    }
}