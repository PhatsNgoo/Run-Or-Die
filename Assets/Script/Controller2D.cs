using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Controller2D : MonoBehaviour
{
    public LayerMask MaskWall;
    public LayerMask MaskPoison;
    public float skinWidth;
    public int numRaycastHorizontal = 2;
    public int numRaycastVertical = 2;
    public int playerTag;

    private BoxCollider2D bc2D;
    private Bounds colliderBounds;
    private RaycastOrigins raycastOrigins;

    private float horizontalRaySpacing;
    private float verticalRaySpacing;

    private void Awake()
    {
        bc2D = GetComponent<BoxCollider2D>();
        numRaycastHorizontal = numRaycastHorizontal < 2 ? 2 : numRaycastHorizontal;
        numRaycastVertical = numRaycastVertical < 2 ? 2 : numRaycastVertical;
    }
    public void Update()
    {
    }
    public PlayerStatus Move(Vector2 velocity)
    {
        PlayerStatus result = new PlayerStatus
        {
            velocity = velocity,
            isCollidingBottom = false,
            isCollidingLeft = false,
            isCollidingRight = false,
            isCollidingTop = false
        };

        UpdateColliderBounds();
        UpdateRaySpacing();
        result = RaycastHorizontal(result, MaskPoison);
        result = RaycastVertical(result, MaskPoison);
        //if(result.isCollidingBottom || result.isCollidingLeft || result.isCollidingRight || result.isCollidingTop)
        //{
        //    PlayerPrefs.SetInt("PlayerDie", playerTag);
        //    if (playerTag == 1) { PlayerPrefs.SetInt("PlayerWin", 2); }
        //    else { PlayerPrefs.SetInt("PlayerWin", 1); }
        //    SceneManager.LoadScene("EndScene");
        //}
        result = RaycastHorizontal(result, MaskWall);
        result = RaycastVertical(result, MaskWall);

        return result;
    }

    private PlayerStatus RaycastHorizontal(PlayerStatus playerStatus, LayerMask collideMask)
    {
        Vector2 raycastOriginBottom = playerStatus.velocity.x > 0 ? raycastOrigins.bottomRight : raycastOrigins.bottomLeft;

        for (int i = 0; i < numRaycastVertical; i++)
        {
            Vector2 rayOrigin = raycastOriginBottom + Vector2.up * i * verticalRaySpacing;

            RaycastHit2D hit = Physics2D.Raycast(
                rayOrigin,
                playerStatus.velocity.WithY(0),
                Mathf.Abs(playerStatus.velocity.x) + skinWidth,
                collideMask
            );

            if (hit)
            {
                if (playerStatus.velocity.x > 0)
                {
                    playerStatus.isCollidingRight = true;
                }
                else if (playerStatus.velocity.x <=0 )
                {
                    playerStatus.isCollidingLeft = true;
                }
                playerStatus.velocity.x = (hit.distance - skinWidth) * Mathf.Sign(playerStatus.velocity.x);
            }
        }

        return playerStatus;
    }

    private PlayerStatus RaycastVertical(PlayerStatus playerStatus, LayerMask collideMask)
    {
        Vector2 raycastOriginLeft = playerStatus.velocity.y > 0 ? raycastOrigins.topLeft : raycastOrigins.bottomLeft;


        for (int i = 0; i < numRaycastHorizontal; i++)
        {
            Vector2 rayOrigin = raycastOriginLeft + Vector2.right * i * horizontalRaySpacing;

            RaycastHit2D hit = Physics2D.Raycast(
                rayOrigin,
                playerStatus.velocity.WithX(0),
                Mathf.Abs(playerStatus.velocity.y) + skinWidth,
                collideMask
            );

            if (hit)
            {
                if (playerStatus.velocity.y > 0)
                {
                    playerStatus.isCollidingTop = true;
                }
                else if (playerStatus.velocity.y <= 0)
                {
                    playerStatus.isCollidingBottom = true;
                }

                playerStatus.velocity.y = (hit.distance - skinWidth) * Mathf.Sign(playerStatus.velocity.y);
            }
        }

        return playerStatus;
    }

    private void UpdateColliderBounds()
    {
        colliderBounds = bc2D.bounds;
        colliderBounds.Expand(-skinWidth * 2);
        UpdateRaycastOrigins();
    }

    private void UpdateRaycastOrigins()
    {
        raycastOrigins.topLeft = new Vector2(
            colliderBounds.min.x,
            colliderBounds.max.y
        );
        raycastOrigins.topRight = new Vector2(
            colliderBounds.max.x,
            colliderBounds.max.y
        );
        raycastOrigins.bottomLeft = new Vector2(
            colliderBounds.min.x,
            colliderBounds.min.y
        );
        raycastOrigins.bottomRight = new Vector2(
            colliderBounds.max.x,
            colliderBounds.min.y
        );
    }

    private void UpdateRaySpacing()
    {
        horizontalRaySpacing = (raycastOrigins.topRight.x - raycastOrigins.topLeft.x) / (numRaycastHorizontal - 1);
        verticalRaySpacing = (raycastOrigins.topRight.y - raycastOrigins.bottomRight.y) / (numRaycastVertical - 1);
    }
}

struct RaycastOrigins
{
    public Vector2 topLeft;
    public Vector2 topRight;
    public Vector2 bottomLeft;
    public Vector2 bottomRight;
}

[System.Serializable]
public struct PlayerStatus
{
    public Vector2 velocity;
    public bool isCollidingTop;
    public bool isCollidingRight;
    public bool isCollidingBottom;
    public bool isCollidingLeft;
}