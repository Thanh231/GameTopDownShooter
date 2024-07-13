using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Actor
{
    internal enum Controller
    {
        moveByMouse,
        moveByKeyboard
    }
    [SerializeField]private Controller controller;

    public float increaseSpeed;
    public float maxDistance;

    Vector2 moveInput;
    private float currentSpeed;
    private PlayerStates playerStates;


    Vector2 mousePos;
    Vector2 movingDir;
    public Vector2 velocityLimit;

    // Start is called before the first frame update
    public override void Init()
    {
        LoadStats();
    }

    // Update is called once per frame
    void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        movingDir = mousePos - (Vector2)transform.position;
        movingDir.Normalize();
        Move();

    }
    private void FixedUpdate()
    {
   
        //weapon.SetRotate(lookDir);
        float angle = Mathf.Atan2(movingDir.y, movingDir.x) * Mathf.Rad2Deg;
        if(weapon != null)
            weapon.SetRotate(angle);
    }
    protected override void Move()
    {
        if (IsDead || m_isKnockBack) return;
        if (controller == Controller.moveByMouse)
        {
            MoveByMouse();
        }
        else if (controller == Controller.moveByKeyboard)
        {
            MoveByKeyBoard();
        }
    }

    private void MoveByKeyBoard()
    {
        moveInput.x = Input.GetAxis("Horizontal");
        moveInput.y = Input.GetAxis("Vertical");

        if (moveInput != Vector2.zero)
        {
            currentSpeed += increaseSpeed * Time.deltaTime;
            currentSpeed = Mathf.Clamp(currentSpeed, 0, playerStates.moveSpeed);
            m_rd.velocity = moveInput * currentSpeed * Time.deltaTime;
            m_anim.SetBool(AniimationConstant.player_Run, true);
        }
        else
        {
            BackToIdle();
        }
    }

    private void BackToIdle()
    {
        currentSpeed = 0;
        m_rd.velocity = Vector2.zero;
        m_anim.SetBool(AniimationConstant.player_Run, false);
    }

    private void MoveByMouse()
    {
        if(Input.GetMouseButton(0))
        {
            currentSpeed += increaseSpeed * Time.deltaTime;
            currentSpeed = Mathf.Clamp(currentSpeed, 0, playerStates.moveSpeed);

            float delta = currentSpeed * Time.deltaTime;
            float distanceToMousePos = Vector2.Distance(transform.position,mousePos);
            distanceToMousePos = Mathf.Clamp(distanceToMousePos, 0, maxDistance/3);
            
            delta *= distanceToMousePos;

            m_rd.velocity = movingDir * delta;
            float vecityLimitX = Mathf.Clamp(m_rd.velocityX, -velocityLimit.x, velocityLimit.x);
            float vecityLimitY = Mathf.Clamp(m_rd.velocityY, -velocityLimit.y, velocityLimit.y);

            m_rd.velocity = new Vector2(vecityLimitX,vecityLimitY);

            m_anim.SetBool(AniimationConstant.player_Run, true);
        }
        else
        {
            BackToIdle();
        }
    }

    private void LoadStats()
    {
        if(statsData == null) return;
        playerStates = (PlayerStates)statsData;
        playerStates.Load();
        CurrentHP = playerStates.hp;
    }
}
