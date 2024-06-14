using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using StarterAssets;

public class Player3D : MonoBehaviour
{
    [Header("Physics Settings")]
    [SerializeField] private InputActionReference attack;
    [SerializeField] private bool isAttacking = false;
    [SerializeField] private bool grounded;
    [SerializeField] private float attackInitDuration;
    [SerializeField] private float attackDelayDuration;

    [Header("Health Settings")]
    [SerializeField] private int coins = 0;
    [SerializeField] private int playerCurrentHealth = 100;
    [SerializeField] private float invencibilityTime = 1.0f;
   
    private bool isPlayerHit = false;
    private bool playerKilled = false;
    private int playerFullHealth;
    private bool gamePaused;
    private float originalMoveSpeed;

    private PlayerAttackCollider attackCollider;
    private ThirdPersonController controller;
    private CharacterController characterController;
    private Animator animator;
    private SkinnedMeshRenderer meshRenderer;

    public static GameObject instance;
    private static Player3D _instance;
    public static Player3D Instance {
        get {
            if(_instance == null) {
                _instance = instance.GetComponent<Player3D>();
            }
            return _instance;
        }
    }
    void Awake() {
        instance = FindObjectOfType<Player3D>().gameObject;
        playerFullHealth = playerCurrentHealth;
    }
    void Start()
    {
        InitPlayerObjects();
        ToggleAttackCollider(false);
    }

    private void OnEnable() {
        attack.action.started += PlayerAttack;
    }

    private void OnDisable() {
        attack.action.started -= PlayerAttack;
    }

    // Update is called once per frame
    void Update()
    {
        GroundCheck();
        GetAnimations();
    }

    private void GroundCheck() {
        grounded = controller.Grounded;
    }

    public void SetPlayerPositionCurrentCheckpoint() {
        Checkpoint3D currentCheckpoint = Checkpoint3DManager.Instance.GetCurrentCheckpoint();

        float x = currentCheckpoint.transform.position.x;
        float y = currentCheckpoint.transform.position.y;
        float z = currentCheckpoint.transform.position.z;

        Vector3 newPlayerPosition = new Vector3(x, y, z);

        ChangePlayerPosition(newPlayerPosition);
        PlayerAddHealth(playerFullHealth);
    }

    private void ChangePlayerPosition(Vector3 newPosition) {
        gameObject.transform.position = newPosition;
    }

    private void PlayerAttack(InputAction.CallbackContext callback) {
        if(!isAttacking && grounded) {
            StartCoroutine(PlayerAttackCoroutine());
        }
    }

    public bool IsPlayerHit() {
        return isPlayerHit;
    }

    private void InitPlayerObjects() {
        attackCollider = transform.Find("AttackCollider").GetComponent<PlayerAttackCollider>();
        controller = GetComponent<ThirdPersonController>();
        animator = GetComponent<Animator>();
        meshRenderer = transform.Find("Geometry/Armature_Mesh/Sparky").GetComponent<SkinnedMeshRenderer>();
        characterController = GetComponent<CharacterController>();
        originalMoveSpeed = controller.MoveSpeed;
    }

    public int GetPlayerMaxHealth() {
        return playerFullHealth;
    }

    public void RestartPlayer() {
        playerKilled = false;
        playerCurrentHealth = playerFullHealth;
        isPlayerHit = false;
        HealthBarPlayer3D.Instance.SetHealth(playerCurrentHealth);
        meshRenderer.enabled = true;
        controller.enabled = true;

        RestartPlayerPositionCheckpoint();
    }

    private void RestartPlayerPositionCheckpoint() {
        Checkpoint3D checkpoint = Checkpoint3DManager.Instance.GetCurrentCheckpoint();
        transform.position = checkpoint.GetCheckpointPosition();
        transform.rotation = checkpoint.GetCheckpointRotation();
    }

    public void PlayerAddHealth(int amount) {
        if(playerCurrentHealth + amount > playerFullHealth) {
            playerCurrentHealth = playerFullHealth;
        } else {
            playerCurrentHealth += amount;
        }

        HealthBarPlayer3D.Instance.SetHealth(playerCurrentHealth);
    }

    private IEnumerator PlayerAttackCoroutine() {
        //Ataque do Player
        isAttacking = true;
        ToggleAttackCollider(isAttacking);
        TogglePlayerMovement(!isAttacking);

        yield return new WaitForSeconds(attackInitDuration);
        
        //Collider do ataque desativado
        ToggleAttackCollider(!isAttacking);
        yield return new WaitForSeconds(attackDelayDuration);

        //Fim animação ataque
        isAttacking = false;
        TogglePlayerMovement(!isAttacking);

    }

    public void ToggleAttackCollider(bool toggle) {
        attackCollider.gameObject.SetActive(toggle);
    }

    public void TogglePlayerMovement(bool toggle) {
        characterController.SimpleMove(Vector3.zero);
        controller.enabled = toggle;

        

    }
    public void PlayerDamage(int damage) {
        if(!isPlayerHit) {
            StartCoroutine(PlayerDamageCoroutine(damage));
        }
    }
    private IEnumerator PlayerDamageCoroutine(int damage) {
        playerCurrentHealth -= damage;
        NormalizePlayerDamage();

        HealthBarPlayer3D.Instance.SetHealth(playerCurrentHealth);

        if(!IsPlayerKilled()) {
            isPlayerHit = true;
            PlayerDamagedAnimation();
            yield return new WaitForSeconds(invencibilityTime);
            isPlayerHit = false;
        } else {
            KillPlayer();
            yield return new WaitForSeconds(1.5f);
            RestartPlayer();
        }

    }

    private void NormalizePlayerDamage() {
        if(playerCurrentHealth <= 0) {
            playerCurrentHealth = 0;
        }
    }

    private bool IsPlayerKilled() {
        return playerCurrentHealth <= 0;
    }

    private void GetAnimations() {

    }

    private void PlayerDamagedAnimation() {
        StartCoroutine(PlayerDamagedCoroutine());
    }

    private IEnumerator PlayerDamagedCoroutine() {
        bool isEnabled = true;
        while(isPlayerHit && !playerKilled) {
            isEnabled = !isEnabled;
            meshRenderer.enabled = isEnabled;
            yield return new WaitForSeconds(0.02f);
        }
        meshRenderer.enabled = true;

    }

    private void KillPlayer() {
        playerKilled = true;
        isPlayerHit = true;
        playerCurrentHealth = 0;
        meshRenderer.enabled = false;
        controller.enabled = false;
    }

}
