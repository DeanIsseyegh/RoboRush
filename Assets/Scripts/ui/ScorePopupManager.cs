using System.Collections;
using objectpooling;
using TMPro;
using UnityEngine;

public class ScorePopupManager : MonoBehaviour
{
    [SerializeField] private SimpleObjectPool scorePopupObjectPool;
    private float scorePopupLength = 3f;
    private WaitForSeconds _popupLengthWait;

    private void Awake()
    {
        _popupLengthWait = new WaitForSeconds(scorePopupLength);
    }

    internal void ShowScorePopup(Transform popupTransform, float scoreChange)
    {
        GameObject scoreIndicator = scorePopupObjectPool.Get();
        scoreIndicator.transform.position = popupTransform.position;
        TextMeshPro textMeshPro = scoreIndicator.GetComponentInChildren<TextMeshPro>();
        if (scoreChange < 0)
        {
            textMeshPro.color = Color.red;
            textMeshPro.SetText("" + scoreChange);
        } else
        {
            textMeshPro.color = Color.green;
            textMeshPro.SetText("+" + scoreChange);
        }
        StartCoroutine(scorePopupObjectPool.ReleaseAfterXSeconds(scoreIndicator, _popupLengthWait));
    }

}
