using UnityEngine;

namespace Assets.Scripts.Managers
{
    public class PlayerManager : MonoBehaviour
    {
        private Node _SelectedNode = null;
        [SerializeField] public UIManager UI;

        #region Selecting and deselecting a Node
        public void SelectNode(Node node)
        {
            UnSelectNode();

            node.IsSelectedNode = true;
            _SelectedNode = node;
            if(UI != null)
                UI.SetNode(_SelectedNode);
        }

        public void UnSelectNode()
        {
            if (_SelectedNode != null)
                _SelectedNode.IsSelectedNode = false;
            if (UI != null)
                UI.SetNode(null);
        }
        #endregion
    }
}
