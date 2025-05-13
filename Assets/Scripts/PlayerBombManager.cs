using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBombManager : MonoBehaviour {
    InputManager InputManager;
    public GameObject bombPrefab;

    [SerializeField] Transform bombPoolParent;
    List<GameObject> bombsPool = new List<GameObject>();

    [Header("Bomb Stats")]
    [SerializeField] int maxBombs;
    [SerializeField] int bombRange;

    private void Awake() {
        InputManager = GetComponent<InputManager>();
    }

    private void Start() {
        for (int i = 0; i < maxBombs; i++) {
            GameObject bomb = Instantiate(bombPrefab, bombPoolParent);
            bomb.SetActive(false);
            bombsPool.Add(bomb);
        }
    }

    private void OnEnable() {
        InputManager.OnBombPressed.AddListener(DeployBomb);
    }

    private void OnDisable() {
        maxBombs = 1;
        bombRange = 1;
        InputManager.OnBombPressed.RemoveListener(DeployBomb);
    }

    private void DeployBomb() {
        foreach (GameObject bomb in bombsPool) {
            if (bomb.activeSelf) continue;
            bomb.transform.position = transform.position;
            bomb.GetComponent<Bomb>().SetBombRange(bombRange);
            bomb.SetActive(true);
            return;
        }
    }

    public void AddExtraBomb() {
        maxBombs++;
        GameObject bomb = Instantiate(bombPrefab, bombPoolParent);
        bomb.SetActive(false);
        bombsPool.Add(bomb);
    }

    public void AddExtraRange()
    {
        bombRange++;
    }
}
