using UnityEngine;
using System.Collections;

public class TowerBtn : MonoBehaviour {

    [SerializeField]
    private GameObject towerPrefab;

    [SerializeField]
    private Sprite sprite;

    public Sprite Sprite {
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

}
