namespace WiggleQuest
{
    public class ClearScene : FadeINOUT
    {
        protected override void Start()
        {
            FadeIN();
            base.Start();
            StartCoroutine(LoadWQScene(MainMenu, 4f));
        }
    }
}