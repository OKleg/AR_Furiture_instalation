using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    [SerializeField] private RawImage buttonImage;

    private Button btn;
    private int _itemId;
    private Texture _buttonTexture;

    public int ItemId
    {
        set => _itemId = value;
    }
    public Texture ButtonTexture
    {
        set
        {
            _buttonTexture = value;
            if (_buttonTexture != null)
                buttonImage.texture = _buttonTexture;
        }
    }
    void Start()
    {
        btn = GetComponent<Button>();
        btn.onClick.AddListener(SelectObject);
    }

    void SelectObject()
    {
        DataHandler.Instance.SetFurniture(_itemId);
    }

    // Update is called once per frame
    void Update()
    {
        if (UIManager.Instance.OnEntered(gameObject))
        {
            //transform.localScale = Vector3.one * 2;
            transform.DOScale(Vector3.one * 1.3f, 0.3f);
        }
        else
        {
            //transform.localScale = Vector3.one;
            transform.DOScale(Vector3.one, 0.3f);
        }
    }
}