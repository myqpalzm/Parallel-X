using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleBlockSlot : MonoBehaviour
{
    public string correctBlockId;
    private PuzzleBlock puzzleBlock;

    public static event Action<string, bool> CorrectSlotInserted;
    private MeshRenderer _renderer;



    private void Awake()
    {
        _renderer = GetComponent<MeshRenderer>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (puzzleBlock != null) return;
        if (other.TryGetComponent<PuzzleBlock>(out var puzzle) && !puzzle.IsSelected)
        {
            puzzle.transform.localPosition = transform.position;
            puzzle.transform.localRotation = transform.rotation;
            puzzleBlock = puzzle;
            if (puzzleBlock.blockId == correctBlockId)
            {
                var materials = puzzleBlock.GetComponentsInChildren<MeshRenderer>()[1].materials;
                _renderer.materials = materials;
                
                CorrectSlotInserted?.Invoke(puzzle.blockId, true);
                Destroy(puzzle.gameObject);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<PuzzleBlock>(out var puzzle) && puzzle== puzzleBlock && puzzle.IsSelected)
        {
            puzzleBlock = null;
            if (puzzle.blockId == correctBlockId)
            {
                CorrectSlotInserted?.Invoke(puzzle.blockId, false);
            }
        }
    }
}
