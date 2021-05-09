using UnityEngine;

namespace Assets.Scripts.Managers
{
    [RequireComponent(typeof(PlayerManager))]
    public class ClickSelectManager : MonoBehaviour
    {
        private PlayerManager _PlayerManager;
        private float _ClickTime = 0.5f;

        public float MaxTimeForClick;

        // Start is called before the first frame update
        void Start()
        {
            _PlayerManager = GetComponent<PlayerManager>();
        }

        // Update is called once per frame
        void Update()
        {
            if(Input.GetMouseButtonDown(0))
                _ClickTime = 0f;
            if (Input.GetMouseButton(0))
                _ClickTime += Time.deltaTime;
            if (Input.GetMouseButtonUp(0) && _ClickTime <= MaxTimeForClick)
                DoSelection();
        }



        private void DoSelection()
        {
            Vector3 mousePos = UnityEngine.Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
            if (hit.collider != null)
            {
                GameObject go = hit.collider.gameObject;
                Node node = go.GetComponent<Node>();
                if (node != null)
                    _PlayerManager.SelectNode(node);
            }
            else
                _PlayerManager.UnSelectNode();
        }
    }
}
