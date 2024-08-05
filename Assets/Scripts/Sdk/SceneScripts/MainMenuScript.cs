﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System;
using Nakama;
using Nakama.TinyJson;
using UnityEngine.UI;

public class MainMenuScript : MonoBehaviour
{

    private CustomNakamaConnection nakamaInstance;
    private LeaderBoardScript leaderBoardScript;

    public GameObject tilePrefab;
    public GameObject leaderBoardContent;

    private ControlUtils controlUtils;
    // Use this for initialization
    void Start()
    {
        nakamaInstance = CustomNakamaConnection.Instance;
        leaderBoardScript = gameObject.AddComponent<LeaderBoardScript>();
        controlUtils = new ControlUtils();
    }

    private string[] MenuItems = { GameConstants.LEADERBOARD_ID, GameConstants.MAIN_MENU_ID};


    public async void OpenLeaderBoard()
    {
        try
        {
            // first enable the leaderboard canvas
            controlUtils.SetComponentActive(0,this.MenuItems);
            if(!tilePrefab || !leaderBoardContent)
            {
                Debug.LogError("tileprefab or leaderboard content is null in main menu script");
                return;
            }

            leaderBoardScript.setTilePrefab(tilePrefab, leaderBoardContent);
            Payload payload = await leaderBoardScript.loadLeaderBoardData(GameConstants.GLOBAL_LEADER_BOARD);
            await leaderBoardScript.PopulateLeaderBoard(payload);
            
        }
        catch(Exception ex)
        {
            Debug.Log("Exception in OpenLeaderBoard = " + ex.ToString());
        }
    }

    public void OpenMainMenu()
    {
        try
        {
            controlUtils.SetComponentActive(1 , this.MenuItems);
        }
        catch (Exception ex)
        {
            Debug.Log("Exception in OpenMainMenu = " + ex.ToString());
        }
    }
}
