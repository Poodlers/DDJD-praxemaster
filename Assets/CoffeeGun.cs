using UnityEngine;
using UnityEngine.InputSystem;

public class CoffeeGun : MonoBehaviour
{

    private Camera mainCamera;

    private float timer;
    public float timeBetweenShots = 0.5f;
    private Vector3 mousePos;


    public float minimumChargeUpTime = 1f;
    public GameObject bulletPrefab;

    public PlayerController playerController;

    public Transform playerTrasform;
    public GameObject chargeUpPrefab;

    private bool canFire = true;

    private bool isCharging = false;

    private float chargeUpTimer = 0f;
    M2_cooldown m2_cooldown;

    public UpgradeMenu upgradeMenu;
    void Start()
    {
        m2_cooldown = GameObject.Find("M2_Cooldown").GetComponent<M2_cooldown>();
        mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        playerTrasform = GameObject.Find("Player").GetComponent<Transform>();
    }


    public void reduceTimeBetweenShots(float amount)
    {

        if (timeBetweenShots > 0.1f)
            timeBetweenShots -= amount;
        upgradeMenu.ResumeGame();
    }

    public void OnFire()
    {
        if (!canFire) return;

        canFire = false;
        GameObject bullet = Instantiate(bulletPrefab, playerTrasform.position, Quaternion.identity);
        bullet.SetActive(true);
        if (bullet.GetComponent<ProjectileBehavior>().burnTicks > 0)
        {
            bullet.GetComponent<SpriteRenderer>().color = Color.red;
        }


    }

    void handleChargeUp()
    {
        if (!isCharging) return;

        chargeUpTimer += Time.deltaTime;
        m2_cooldown.SetCoolDown(chargeUpTimer);
        if (chargeUpTimer >= minimumChargeUpTime)
        {
            m2_cooldown.fill.color = new Color(1, 0, 0, 0.5f);
        }


    }
    public void OnRightClick(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            isCharging = true;
            //make player slow a bit
            playerController.moveSpeed = playerController.moveSpeed / 2;


        }
        if (context.canceled)
        {
            playerController.moveSpeed = playerController.moveSpeed * 2;
            if (chargeUpTimer < minimumChargeUpTime)
            {
                Debug.Log("Cancelled charge-up attack !");
            }
            else
            {
                mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
                Vector3 rotation = mousePos - transform.position;

                float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;

                GameObject chargeUpBullet = Instantiate(chargeUpPrefab, playerTrasform.position, Quaternion.Euler(0, 0, rotZ));
                chargeUpBullet.SetActive(true);
            }
            isCharging = false;
            chargeUpTimer = 0f;
            m2_cooldown.SetCoolDown(chargeUpTimer);
            m2_cooldown.fill.color = new Color(1, 1, 1, 0.5f);
        }


    }

    void Update()
    {
        handleChargeUp();
        mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        //descomentar para rodar a arma do café

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
