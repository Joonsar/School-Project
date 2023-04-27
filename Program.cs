namespace School_Project
{
    public class Program
    {
        private static void Main(string[] args)
        {
            GameController gc = new GameController();
            GameController.Instance = gc;
            gc.CreateScreen();
            gc.Init();
            gc.Run();
        }
    }
}