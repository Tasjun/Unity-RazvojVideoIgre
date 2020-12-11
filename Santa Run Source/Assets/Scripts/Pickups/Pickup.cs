using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    [SerializeField]
    private AEffectConfig effect;

    private bool isMoving = false;
    private PlayerController playerCon = null;
    public float magnetForce = 1.2f;

    public AEffectConfig GetEffect()
    {
        return effect;
    }

    private float timeStarted;
    private Vector3 startPos;

    public void MagnetToPlayer(PlayerController playerCon)
    {
        if (isMoving) return;

        startPos = transform.position;
        timeStarted = Time.time;
        this.playerCon = playerCon;
        isMoving = true;
    }

    void Update()
    {
        if (isMoving)
        {
            float moveDist = (Time.time - timeStarted) * magnetForce;
            transform.position = Vector3.Lerp(startPos, playerCon.transform.position, moveDist);
        }

        transform.Rotate(50 * Time.deltaTime, 0, 0);
    }
}
