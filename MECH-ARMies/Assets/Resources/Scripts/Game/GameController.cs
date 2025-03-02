﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameController : MonoBehaviour {
	//private bool gameOverPlayer1;
	//private bool gameOverPlayer2;
	
	public int Player1Money;
	public int Player2Money;
	public int StartingMoney;
	public GameObject MenuController;
    public GameObject Ship;
    public GameObject gunShot;
    public GameObject missileShot;

    private int enemyTick = 500;
    private GameObject infantryPrefab;
    private GameObject jeepPrefab;
    private Vector3 Spawn;
    Vector3 enemyBasePosition;
    Quaternion enemyBaseRotation;
	
    Vector3 shipspawn = new Vector3(105.0f, 34.4f, 105.0f);

    public bool gameOver = false;

    private Animator anim;


	// Use this for initialization
    void Start()
    {
        anim = GameObject.FindGameObjectWithTag("UI").GetComponent<Animator>();

        Instantiate(MenuController);

        for (int x = 0; x < BaseStaticValues.MainBaseArray.Length; x++)
        {
            BaseStaticValues.MainBaseArray[x] = GameObject.FindGameObjectWithTag("Player" + (x + 1) + "Base");
            //Debug.Log(string.Format("Main Base {0} Initialized",x+1));
        }

        MenuController = GameObject.FindGameObjectWithTag("MenuController");

        for (int i = 0; i < BaseStaticValues.SmallBaseArray.Length; i++)
        {
            BaseStaticValues.SmallBaseArray[i] = GameObject.FindGameObjectWithTag("SmallBase" + (i + 1));
            //Debug.Log(string.Format("Small Base {0} Initialized",i+1));
        }

        //initialize other values
        Player1Money = StartingMoney;
        Player2Money = StartingMoney;

        if (GameObject.FindGameObjectWithTag("Player1Health") && GameObject.FindGameObjectWithTag("Player1Health").GetComponent<Slider>() != null)
        {
            GameObject.FindGameObjectWithTag("Player1Health").GetComponent<Slider>().maxValue = BaseStaticValues.Player.MaxLife;
            GameObject.FindGameObjectWithTag("Player1Health").GetComponent<Slider>().value = BaseStaticValues.Player.MaxLife;
        }

        BaseStaticValues.Player1.Life = BaseStaticValues.Player.MaxLife;
        BaseStaticValues.Player1.Energy = BaseStaticValues.Player.MaxEnergy;
        BaseStaticValues.Player1.Guns = BaseStaticValues.Player.MaxGuns;

        GameObject jetGameObject = (GameObject)Resources.Load("LatestPrefabVersions/Jet");
        jetGameObject.GetComponent<UnitController>().curTeam = "Player1";
        jetGameObject.GetComponent<UnitController>().unitType = "PlayerPlane";
        jetGameObject.GetComponent<UnitController>().curProgram = "PlayerPlane";

        Ship = Instantiate(jetGameObject, shipspawn, Ship.transform.rotation) as GameObject;
        //if (Ship.GetComponent<UnitController>() != null)
        //{
        //    Ship.GetComponent<UnitController>().curTeam = "Player1";
        //    Ship.GetComponent<UnitController>().unitType = "PlayerPlane";
        //    Ship.GetComponent<UnitController>().curProgram = "PlayerPlane";
        //}
        infantryPrefab = (GameObject)Resources.Load("LatestPrefabVersions/Infantry2");
        infantryPrefab.GetComponent<UnitController>().curProgram = "Nearest Base";
        infantryPrefab.GetComponent<UnitController>().curTeam = "Player2";
        jeepPrefab = (GameObject)Resources.Load("LatestPrefabVersions/Jeep2");
        jeepPrefab.GetComponent<UnitController>().curProgram = "Attack Main";
        jeepPrefab.GetComponent<UnitController>().curTeam = "Player2";

        if (BaseStaticValues.MainBaseArray[1].gameObject)
        {
            Vector3 Spawn = new Vector3(BaseStaticValues.MainBaseArray[1].gameObject.transform.position.x - 30, BaseStaticValues.MainBaseArray[1].gameObject.transform.position.y, BaseStaticValues.MainBaseArray[1].gameObject.transform.position.z - 30);

            enemyBasePosition = Spawn;
            enemyBaseRotation = BaseStaticValues.MainBaseArray[1].gameObject.transform.rotation;
        }
    }
	
	// Update is called once per frame
	void Update () 
    {

        EnemyLoop();
	    if (BaseStaticValues.MainBaseArray != null)
	    {
	        foreach (var t in BaseStaticValues.MainBaseArray)
	        {
                if (t != null && t.GetComponent<UnitController>() != null && t.GetComponent<UnitController>().ThisUnit != null && t.GetComponent<UnitController>().ThisUnit._IsDead)
	            {
	                if (t.GetComponent<UnitController>().ThisUnit._CurTeam != "Player1")
	                {
	                    gameOver = true;
	                    GameOver("WIN!");
                        Application.LoadLevel(2);

	                }
	                else
	                {
	                    gameOver = true;
                        GameOver("LOSE!");
                        Application.LoadLevel(3);
	                }
	            }
	        }
	    }


	    /*
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            bool IsVisible = _menuController.gameObject.GetComponent("IsVisible");
            IsVisible = !IsVisible;
        } */



	}

    private void EnemyLoop()
    {
        enemyTick++;
        if (enemyTick % 500 == 0)
        {
            if (enemyTick == 500 * 2)
            {
                Debug.Log("Spawn infantry");

                //create infantry
                
                for (var i = 0; i < 4; i++)
                {
                    var newUnit = (GameObject)Instantiate(infantryPrefab, new Vector3(enemyBasePosition.x + Random.Range(-10, 10), enemyBasePosition.y, enemyBasePosition.z + Random.Range(-10, 10)), enemyBaseRotation);
                }
            }
            else if (enemyTick == 500 * 5)
            {
                Debug.Log("Spawn jeep");

                //create vehicles
                
                

                for (var i = 0; i < 4; i++)
                {
                    var newUnit = (GameObject)Instantiate(jeepPrefab,
                        new Vector3(enemyBasePosition.x + Random.Range(-10, 10), enemyBasePosition.y,
                            enemyBasePosition.z + Random.Range(-10, 10)), enemyBaseRotation);
                }

                enemyTick = 0;
            }
            else if (enemyTick > 500 * 5)
                enemyTick = 0;
        }
    }

    public void GameOver(string status)
    {
         anim.SetBool("game_over", true);
    }
}
