using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicPlayer : MonoBehaviour
{

    static MusicPlayer instance = null;//need static var to stop creating new

    public AudioClip startClip;
    public AudioClip gameClip;
    public AudioClip endClip;

    private AudioSource music;

    //awake - start - update

    //singletion pattern 
    //http://gameprogrammingpatterns.com/singleton.html
    void Awake()
    {
        //Debug.Log("MP" + GetInstanceID());
        //fix the double playing glitch
        if (instance != null && instance != this)//!=this dont destroy the last instacnce
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            GameObject.DontDestroyOnLoad(gameObject);//make bgm continue in every scene
            music = GetComponent<AudioSource>();
        }

    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnLevelLoaded;
    }
    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnLevelLoaded;
    }

    void OnLevelLoaded(Scene scene, LoadSceneMode mode)
    {
        music.Stop();
        if (scene.name == "Game")
        {
            music.clip = gameClip;
        }
        if (scene.name == "Win")
        {
            music.clip = endClip;
        }
        if (scene.name == "Start")
        {
            music.clip = startClip;
        }

        music.loop = true;
        music.Play();

    }

    //void OnLevelWasLoaded(int level)
    //{
    //    Debug.Log("MusicPlayer: loaded level" + level);
    //    music.Stop();
    //    if(level == 0)
    //    {
    //        music.clip = startClip;
    //    }

    //    if(level == 1)
    //    {
    //        music.clip = gameClip;
    //    }

    //    if(level == 2)
    //    {
    //        music.clip = endClip;
    //    }

    //    music.loop = true;
    //    music.Play();

    //}

    // Use this for initialization
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {

    }
}
