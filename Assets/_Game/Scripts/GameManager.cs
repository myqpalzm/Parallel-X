using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Animator gateAnimator;
    public GameObject puzzleGameObject;
    public GameObject fadeGameObject;
    public List<PuzzleState> puzzleStates;
    public List<PuzzleMesh> puzzleMeshes;

    public TextMeshProUGUI gameEndText;

    public Image oxygenImage;

    public float oxygenTime;


    public bool gameEnd;

    private float _oxygenTimer;
    private bool _oxygenDepleting;

    private static GameManager instance;
    public static GameManager Instance {  get { return instance; } }

    private void Awake()
    {
        instance = this;
        PuzzleBlockSlot.CorrectSlotInserted += PuzzleBlockSlot_CorrectSlotInserted;
    }

    private void OnDestroy()
    {
        PuzzleBlockSlot.CorrectSlotInserted -= PuzzleBlockSlot_CorrectSlotInserted;
    }

    private void Start()
    {
        _oxygenDepleting = true;
        _oxygenTimer = oxygenTime;
    }

    private void Update()
    {
        if (gameEnd) return;
        if (_oxygenTimer > 0 && _oxygenDepleting)
        {
            _oxygenTimer -= Time.deltaTime;
            var percentage = _oxygenTimer / oxygenTime;

            oxygenImage.fillAmount = percentage;
            if ( _oxygenTimer <= 0 )
            {
                fadeGameObject.SetActive(true);
                gameEndText.gameObject.SetActive(true);
                gameEndText.SetText("Game Over!");
            }
        }
    }

    private void PuzzleBlockSlot_CorrectSlotInserted(string blockId, bool correct)
    {
        var state = puzzleStates.First(x => x.blockId == blockId);
        if (state != null)
        {
            state.correct = correct;

            if (puzzleStates.All(x=> x.correct))
            {
                gameEnd = true;
                puzzleGameObject.SetActive(false);
                gateAnimator.SetBool("IsOpen", true);
                StartCoroutine(FadeBlack());
            }
        }
    }

    private IEnumerator FadeBlack()
    {
        yield return new WaitForSeconds(2);
        fadeGameObject.SetActive(true);
        gameEndText.gameObject.SetActive(true);
        gameEndText.SetText("Success!");
    }

    public Mesh GetPuzzleMesh(string id)
    {
        var mesh = puzzleMeshes.First(x => x.blockId == id);
        if (mesh != null)
        {
            return mesh.mesh;
        }
        return null;
    }

}

[System.Serializable]
public class PuzzleState
{
    public string blockId;
    public bool correct;
}

[System.Serializable]
public class PuzzleMesh
{
    public string blockId;
    public Mesh mesh;
}
