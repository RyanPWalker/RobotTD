using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TowerBtn : MonoBehaviour {

    [SerializeField]
    private GameObject towerPrefab;

    [SerializeField]
    private Sprite sprite;

    [SerializeField]
    private int price;

    [SerializeField]
    private Text priceText;

    public int Price
    {
        get
        {
            return price;
        }
    }

    public Sprite Sprite
    {
        get
        {
            return sprite;
        }
    }

    public GameObject TowerPrefab
    {
        get
        {
            return towerPrefab;
        }
    }

    private void Start()
    {
        priceText.text = "$" + Price;
    }
}
