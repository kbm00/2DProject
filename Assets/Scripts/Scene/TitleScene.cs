using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TitleScene : BaseScene, IPointerEnterHandler, IPointerExitHandler
{
    public Image buttonImage;
    public Sprite defaultSprite; 
    public Sprite dragSprite; 
    public override IEnumerator LoadingRoutine()
    {
        yield return null;
    }
  
    public void StartGame()
    {
        Manager.Scene.LoadScene("GameScene");
    }
   
    public void OnPointerEnter(PointerEventData eventData)
    {
        buttonImage.sprite = dragSprite;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        buttonImage.sprite = defaultSprite;
    }
}
