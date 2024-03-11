using System.Collections;

public class TitleScene : BaseScene
{
    public override IEnumerator LoadingRoutine()
    {
        yield return null;
    }

    public void StartGame()
    {
        Manager.Scene.LoadScene("GameScene");
    }
}
