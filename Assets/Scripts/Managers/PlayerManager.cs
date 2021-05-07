using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    Node SelectedNode = null;

    #region Selecting and deselecting a Node
    public void SelectNode(Node node)
    {
        UnSelectNode();

        node.IsSelectedNode = true;
        SelectedNode = node;
    }

    public void UnSelectNode()
    {
        if (SelectedNode != null)
            SelectedNode.IsSelectedNode = false;
    }
    #endregion
}
