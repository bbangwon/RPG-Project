namespace RPG.SavingV0
{
    public interface ISaveable
    {
        object CaptureState();
        void RestoreState(object state);
    }
}