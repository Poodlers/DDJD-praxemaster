using UnityEngine;

public class CoffeeGun : MonoBehaviour
{

    private Camera mainCamera;

    private float timer;
    public float timeBetweenShots = 0.5f;
    private Vector3 mousePos;


    public GameObject bulletPrefab;
    public Transform playerTrasform;

    private bool canFire = true;

    void Start()
    {
        mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        playerTrasform = GameObject.Find("Player").GetComponent<Transform>();
    }


    public void OnFire()
    {
        Debug.Log("Fire with the coffee");
        canFire = false;
        GameObject bullet = Instantiate(bulletPrefab, playerTrasform.position, Quaternion.identity);
        bullet.SetActive(true);

    }

    public void OnRightClick()
    {
        Debug.Log("Right Click with the coffee");
    }

    void Update()
    {
        mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        //descomentar para rodar a arma do café
        /*
        
        Vector3 rotation = mousePos - transform.position;

        
        float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rotZ);

        */

        if (!canFire)
        {
            timer += Time.deltaTime;
            if (timer >= timeBetweenShots)
            {
                canFire = true;
                timer = 0;
            }
        }


    }
}
