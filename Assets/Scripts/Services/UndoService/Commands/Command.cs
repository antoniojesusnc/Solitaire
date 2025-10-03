namespace Solitaire
{
    public abstract class Command : ICommand
    {
        public abstract void Undo();
    }
}
