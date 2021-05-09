using UnityEngine;

namespace Assets.Scripts.Managers
{
    public class PlayerManager : MonoBehaviour
    {
        private Node _SelectedNode = null;

        #region Selecting and deselecting a Node
        public void SelectNode(Node node)
        {
            UnSelectNode();

            node.IsSelectedNode = true;
            _SelectedNode = node;
        }

        public void UnSelectNode()
        {
            if (_SelectedNode != null)
                _SelectedNode.IsSelectedNode = false;
        }
        #endregion
    }
}
