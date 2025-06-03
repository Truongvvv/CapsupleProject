using UnityEngine;

public class WeaponShoot : MonoBehaviour
{
    public Animator animator;
    public float range = 100f;
    public int damage = 20;
    public string[] targetTags = { "Ground", "Enemy" };

    public GameObject arrowPrefab;
    public Transform arrowSpawnPoint;
    public float arrowSpeed = 50f;

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            animator.SetTrigger("Shoot");

            // Bắn mũi tên prefab
            GameObject arrow = Instantiate(arrowPrefab, arrowSpawnPoint.position, arrowSpawnPoint.rotation);

            Rigidbody rb = arrow.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.linearVelocity = arrowSpawnPoint.forward * arrowSpeed;
            }
            else
            {
                Debug.LogWarning("Arrow prefab chưa có Rigidbody!");
            }

            // Raycast để kiểm tra trúng đạn
            Ray ray = new Ray(transform.position, transform.forward);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, range))
            {
                Debug.DrawRay(ray.origin, ray.direction * range, Color.red, 1f);
                Debug.Log("Đã bắn trúng: " + hit.collider.name + " (Tag: " + hit.collider.tag + ")");

                foreach (string tag in targetTags)
                {
                    if (hit.collider.CompareTag(tag))
                    {
                        if (tag == "Enemy")
                        {
                            EnemyHealth enemy = hit.collider.GetComponent<EnemyHealth>();
                            if (enemy == null)
                                enemy = hit.collider.GetComponentInParent<EnemyHealth>();

                            if (enemy != null)
                            {
                                enemy.TakeDamage(damage);
                            }
                        }

                        break;
                    }
                }
            }
        }
    }
}