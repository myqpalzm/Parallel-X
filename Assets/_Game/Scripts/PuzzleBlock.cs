using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleBlock : MonoBehaviour
{
    public string blockId;
    private BoxCollider _collider;
    private bool _selected;

    private Vector3 _defaultScale;

    public bool IsSelected => _selected;

    private void Awake()
    {
        _collider = GetComponent<BoxCollider>();
        var filter = GetComponentsInChildren<MeshFilter>();
        _defaultScale = transform.localScale;

        foreach(var f in filter)
        {
            f.mesh = GameManager.Instance.GetPuzzleMesh(blockId);
        }
        
    }


    public void Selected()
    {
        _selected = true;
        transform.localScale = _defaultScale * .75f;
    }

    public void SelectedExit()
    {
        _selected = false;
        transform.localScale = _defaultScale;
    }
}
