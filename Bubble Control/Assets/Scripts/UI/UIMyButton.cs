using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class UIMyButton : Button
{
    public bool IsBtnPressed => IsPressed();

    public Text myText;
    public Image myImage;
    public int index;
    public System.Action onMouseEnter;
    public System.Action onMouseExit;


    public bool isSelected = false;

    public bool mute = false;

    public override void OnPointerClick(PointerEventData eventData)
    {
        base.OnPointerClick(eventData);
        if (!mute)
        {
            //AudioManager.Instance.PlaySfxSound(SfxEnum.BtnClick);
        }
    }


    public override void OnPointerEnter(PointerEventData eventData)
    {
        base.OnPointerEnter(eventData);
        DoAnimationOnPointerEnter();
        onMouseEnter?.Invoke();
    }
    public override void OnPointerExit(PointerEventData eventData)
    {
        base.OnPointerExit(eventData);
        DoAnimationOnPointerExit();
        onMouseExit?.Invoke();
    }

    public void SetHightLight(bool isSelected)
    {
        DoStateTransition(SelectionState.Highlighted, false);
        interactable = !isSelected;
    }

    protected virtual void DoAnimationOnPointerEnter()
    {
        //LeanTween.scale(gameObject, Vector3.one * 1.2f, 0.2f);
        //if (myText)
        //{
        //    LeanTween.value(gameObject, myText.color, Color.yellow, 0.1f)
        //       .setOnUpdate((color) =>
        //       {
        //           myText.color = color;
        //       });
        //}
    }
    protected virtual void DoAnimationOnPointerExit()
    {
        //LeanTween.scale(gameObject, Vector3.one, 0.2f);
        //if (myText)
        //{
        //    LeanTween.value(gameObject, myText.color, Color.white, 0.1f)
        //        .setOnUpdate((color) =>
        //        {
        //            myText.color = color;
        //        });
        //}
    }
    public void RemoveAllListener()
    {
        onClick.RemoveAllListeners();
        onMouseExit = null;
        onMouseEnter = null;
    }
}
