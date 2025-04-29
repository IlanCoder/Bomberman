using UnityEngine;

public class Bomb : MonoBehaviour
{
    [Header("Explotion Cast")]
    [SerializeField] float sphereCastRad;
    [SerializeField] LayerMask layerMask;

    [Header("Bomb Stats")]
    [SerializeField] int range;
    [SerializeField] float explosionTimer;
    float spawnTime;

    private void OnEnable()
    {
        spawnTime = Time.time;
    }

    void Update()
    {
        if(Time.time - spawnTime >= explosionTimer)
        {
            Explode();
            gameObject.SetActive(false);
        }
    }

    void Explode()
    {
        Ray ray = new Ray(transform.position, Vector3.right);

        RaycastHit[] hits = Physics.SphereCastAll(ray, sphereCastRad, range, layerMask);

        if (hits.Length > 0)
        {
            foreach (RaycastHit hit in hits)
            {
                if (hit.transform.tag == "Unbreakable") break;
                hit.transform.gameObject.SetActive(false);
                if (hit.transform.tag == "Breakable") break;
            }
        }
    }
}
