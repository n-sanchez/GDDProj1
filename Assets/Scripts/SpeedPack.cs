﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedPack : MonoBehaviour
{
    #region healthPack_variables
    [SerializeField]
    [Tooltip("Assign the healing value of the health pack!")]
    private int speedAmount;
    #endregion

    #region functions
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            collision.transform.GetComponent<PlayerController>().IncreaseSpeed(speedAmount);
            Destroy(this.gameObject);
        }
    }
    #endregion
}
