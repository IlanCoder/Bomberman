using System;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Bomb : MonoBehaviour {
    [SerializeField] int gridOffset;
    [SerializeField] int spawnHeight;

    [Header("Explotion Cast")]
    [SerializeField] float sphereCastRad;
    [SerializeField] LayerMask layerMask;

    [Header("Bomb Stats")]
    [SerializeField] int range;
    [SerializeField] float explosionTimer;
    float spawnTime;

    Animator animator;

    private void Awake() {
        animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        spawnTime = Time.time;
        Vector2 spawnPos = new Vector2(transform.position.x, transform.position.z);
        //x
        int divMain = (int)MathF.Floor(Mathf.Abs(spawnPos.x / gridOffset));
        float module = spawnPos.x % gridOffset;
        if (Mathf.Abs(module) > gridOffset / 2) divMain++;
        spawnPos.x = divMain * gridOffset;
        //y (z)
        divMain = (int)MathF.Floor(Mathf.Abs(spawnPos.y / gridOffset));
        module = spawnPos.y % gridOffset;
        if (Mathf.Abs(module) > gridOffset / 2) divMain++;
        spawnPos.y = divMain * -gridOffset;

        transform.position = new Vector3(spawnPos.x, spawnHeight, spawnPos.y);
    }

    void Update()
    {
        if(Time.time - spawnTime >= explosionTimer)
        {
            animator.SetTrigger("Explode");
            spawnTime = Time.time;
        }
    }

    public void Explode() {
        ExplodeInDirection(Vector3.right);
        ExplodeInDirection(Vector3.left);
        ExplodeInDirection(Vector3.forward);
        ExplodeInDirection(Vector3.back);
    }

    void ExplodeInDirection(Vector3 direction) {
        Ray ray = new Ray(transform.position, direction);
        RaycastHit[] hits = Physics.SphereCastAll(ray, sphereCastRad, range, layerMask);
        Array.Sort(hits, (a, b) => a.distance.CompareTo(b.distance));
        if (hits.Length > 0) {
            foreach (RaycastHit hit in hits) {
                if (hit.transform.tag == "Unbreakable") break;
                if (hit.transform.tag == "Player") 
                    hit.transform.GetComponent<MovementController>().DisablePlayer();
                else 
                    hit.transform.gameObject.SetActive(false);
                if (hit.transform.tag == "Breakable") break;
            }
        }
    }

    public void DisableObject()
    {
        gameObject.SetActive(false);
    }

    public void SetBombRange(int range) {
        this.range = range * gridOffset;
    }
}
