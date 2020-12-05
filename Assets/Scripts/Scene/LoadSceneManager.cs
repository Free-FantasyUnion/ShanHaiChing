using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;
using DG.Tweening;

public class LoadSceneManager : MonoBehaviour
{

    [SerializeField] private Slider slider;
    private AsyncOperation mAsyncOperation;
    [SerializeField] private GameObject LBoard, RBoard;
    [SerializeField] private Camera mcamera;
    private bool loadFlag = false;
    // Start is called before the first frame update
    void Start()
    {
        init();
    }

    private void init()
    {
        Sequence sequence = DOTween.Sequence();
        sequence.Append(LBoard.transform.DOMove(new Vector3(-4.45f, 0, 0), 2.0f).SetEase(Ease.OutBounce));
        sequence.Join(RBoard.transform.DOMove(new Vector3(4.45f, 0, 0), 2.0f).SetEase(Ease.OutBounce));
        sequence.AppendCallback(() =>
        {
            StartCoroutine(LoadScene());
        });
    }

    // Update is called once per frame
    void Update()
    {
        UpdateSlider();
    }

    IEnumerator LoadScene()
    {
        mAsyncOperation = SceneManager.LoadSceneAsync("Part_1");
        mAsyncOperation.allowSceneActivation = false;
        yield return mAsyncOperation;
    }

    private void UpdateSlider()
    {
        if (mAsyncOperation == null) return;
        if (slider.value >= 1)
        {
            mAsyncOperation.allowSceneActivation = true;
            return;
        }
        else if (slider.value >= 0.1f)
        {
            slider.value += Time.deltaTime * 0.4f;
            return;
        }
        slider.value = Mathf.Min(mAsyncOperation.progress, 0.1f);
    }

}
