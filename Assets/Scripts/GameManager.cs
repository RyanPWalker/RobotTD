using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class GameManager : Singleton<GameManager>
{
    public TowerBtn ClickedBtn { get; set; }

    private int currency;

    private int wave = 0;

    [SerializeField]
    private Text waveTxt;

    [SerializeField]
    private Text currencyText;

    [SerializeField]
    private GameObject waveBtn;

    private List<Monster> activeMonsters = new List<Monster>();

    public ObjectPool Pool { get; set; }

    public bool WaveActive
    {
        get
        {
            return activeMonsters.Count > 0;
        }
    }

    public int Currency
    {
        get
        {
            return currency;
        }
        set
        {
            this.currency = value;
            this.currencyText.text = "<color=lime>$</color>" + value.ToString();
        }
    }

    private void Awake()
    {
        Pool = GetComponent<ObjectPool>();;
    }

    // Use this for initialization
    void Start () {
        Currency = 100;
	}

	// Update is called once per frame
	void Update () {
        HandleEscape();
	}

    public void PickTower(TowerBtn towerBtn)
    {
        if (Currency >= towerBtn.Price && !WaveActive)
        {
            this.ClickedBtn = towerBtn;
            Hover.Instance.Activate(towerBtn.Sprite);
        }
    }

    public void BuyTower()
    {
        if (Currency >= ClickedBtn.Price)
        {
            Currency -= ClickedBtn.Price;
            Hover.Instance.Deactivate();
        }
    }

    private void HandleEscape()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Hover.Instance.Deactivate();
        }
    }

    public void StartWave()
    {
        wave++;
        waveTxt.text = string.Format("Wave: <color=lime>{0}</color>", wave);
        StartCoroutine(SpawnWave());
        waveBtn.SetActive(false);
    }

    private IEnumerator SpawnWave()
    {
        LevelManager.Instance.GeneratePath();

        for (int i = 0; i < wave; i++)
        {
            int monsterIndex = Random.Range(0, 4);
            string type = string.Empty;

            LevelManager.Instance.GeneratePath();

            switch (monsterIndex)
            {
                case 0:
                    type = "BlueMonster";
                    break;
                case 1:
                    type = "RedMonster";
                    break;
                case 2:
                    type = "GreenMonster";
                    break;
                case 3:
                    type = "PurpleMonster";
                    break;
                default:
                    break;
            }

            // Requests the monster from the pool
            Monster monster = Pool.GetObject(type).GetComponent<Monster>();
            monster.Spawn();
            activeMonsters.Add(monster);

            yield return new WaitForSeconds(2.5f);
        }
    }

    public void RemoveMonster(Monster monster)
    {
        activeMonsters.Remove(monster);

        if (!WaveActive)
        {
            waveBtn.SetActive(true);
        }
    }
}
