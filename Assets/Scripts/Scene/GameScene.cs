using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : BaseScene
{
    public override IEnumerator LoadingRoutine()
    {
        yield return null;
    }

    public void Title()
    {
        Manager.Scene.LoadScene("TitleScene");
    }
}
