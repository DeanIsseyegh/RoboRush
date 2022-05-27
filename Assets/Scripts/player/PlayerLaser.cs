using UnityEngine;

public class PlayerLaser : PlayerProjectile
{
    [SerializeField] private float playerXOffset = 0.5f;
    [SerializeField] private float playerYOffset = 2;
    [SerializeField] private float playerZOffset = -0.2f;

    protected override void Start()
    {
        base.Start();
        transform.position =
            new Vector3(player.transform.position.x + playerXOffset, player.transform.position.y + playerYOffset, playerZOffset);
    }

    public void Update()
    {
        transform.position = new Vector3(player.transform.position.x + playerXOffset, player.transform.position.y + playerYOffset, playerZOffset);
    }
}
