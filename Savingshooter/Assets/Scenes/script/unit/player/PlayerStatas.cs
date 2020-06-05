using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatas : MonoBehaviour
{
    public GameController gameController;
    private Animator animator;
    private float playerEnergy = 20.0f; // エネルギー
    private float playerPower = 0.5f;  // 出力
    private float powerCost;            // エネルギー消費の係数
    private bool death;

    private void Start()
    {
        animator = gameObject.GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(Input.GetKey(KeyCode.LeftShift))
        {
            playerPower += 0.01f;
        }
        if (Input.GetKey(KeyCode.LeftControl))
        {
            playerPower -= 0.01f;
        }
        playerPower = Mathf.Clamp(playerPower, 0.1f, 1.0f); // 最大値1最小値0.1

        if (playerPower > 0.5)
        {
            powerCost = (playerPower - 0.5f) * 4 + 1.0f;    // 最大値1の時　3
        }
        else
        {
            powerCost = playerPower * 2;     // 最低値  0.1の時 0.2 : 0.5の時  1
        }
       // playerEnergy -= Time.deltaTime * powerCost * 3;
    
        if(playerEnergy <= 0)
        {
            death = true;
            animator.SetBool("Die", true);
            Invoke("DieCall", 2.0f);
        }
    }
    public float GetPlayerPower()
    {
        return playerPower;
    }
    public float GetPlayerEnergy()
    {
        return playerEnergy;
    }
    public void AddPlayerEnergy(float add)
    {
        playerEnergy += add;
    }
    private void DieCall()
    {
        gameController.GetComponent<GameController>().ChangeScene();
    }
    public bool IsDeath()
    {
        return death;
    }
}
