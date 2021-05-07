using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerManager))]
public class ClickSelectManager : MonoBehaviour
{
    PlayerManager playerManager;

    public float MaxTimeForClick;

    float clickTime = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        playerManager = GetComponent<PlayerManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
            clickTime = 0f;
        if (Input.GetMouseButton(0))
            clickTime += Time.deltaTime;
        if (Input.GetMouseButtonUp(0) && clickTime <= MaxTimeForClick)
            DoSelection();
    }



    void DoSelection()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

        RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
        if (hit.collider != null)
        {
            GameObject go = hit.collider.gameObject;
            Node node = go.GetComponent<Node>();
            if (node != null)
                playerManager.SelectNode(node);
        }
        else
            playerManager.UnSelectNode();
    }
}
