using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class AnimacaoBotao : MonoBehaviour
{

    [SerializeField] private Image _img;
    [SerializeField] private Sprite _defaut, _pressed;
   public void botaodown(PointerEventData eventData)
    {
        _img.sprite = _pressed;
    }

    public void botaoup(PointerEventData eventData)
    {
        _img.sprite = _defaut;
    }

}
