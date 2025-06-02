using UnityEngine;

public class WeaponShoot : MonoBehaviour
{
    public Animator animator;               // Animator để phát animation bắn
    public float range = 100f;              // Tầm bắn raycast

    // Danh sách tag có thể bị bắn trúng (đã thêm sẵn Ground và Enemy)
    public string[] targetTags = { "Ground", "Enemy" };

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            // Gửi trigger tới Animator
            animator.SetTrigger("Shoot");

            // Tạo ray từ vị trí và hướng hiện tại
            Ray ray = new Ray(transform.position, transform.forward);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, range))
            {
                // Kiểm tra tag của vật thể bị bắn có nằm trong danh sách cho phép không
                foreach (string tag in targetTags)
                {
                    if (hit.collider.CompareTag(tag))
                    {
                        Debug.Log("Bắn trúng: " + hit.collider.name + " (Tag: " + tag + ")");
                        // TODO: xử lý trúng đạn tại đây (trừ máu, spawn hiệu ứng, v.v.)
                        break;
                    }
                }
            }
        }
    }
}