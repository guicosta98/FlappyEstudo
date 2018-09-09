using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour {
    public static SaveManager Instance { set; get; }
    public SaveState state;
    player player;

    private void Awake()
    {
        player = GameObject.Find("player").GetComponent<player>();
        DontDestroyOnLoad(gameObject);
        Instance = this;
        Load();
        player.recordNum.text = ""+ state.record;
    }

    private void Update()
    {
        if(player.isAlive == false)
        {
            Save();
        }
    }
    private void OnApplicationQuit()
    {
        Save();
        Debug.Log(Helper.Serialize<SaveState>(state));
    }
    //Save
    public void Save()
    {
        if(player.points > state.record)
        {
            state.record = player.points;
        }
        
        PlayerPrefs.SetString("save", Helper.Serialize<SaveState>(state));
    }

    //Load
    public void Load()
    {
        if (PlayerPrefs.HasKey("save"))
        {
            state = Helper.Deserialize<SaveState>(PlayerPrefs.GetString("save"));
        }
        else
        {
            state = new SaveState();
            Save();
            Debug.Log("No save file found, creating a new one!");
        }
    }
}
