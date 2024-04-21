namespace RPG.Saving
{
    public interface ISaveable
    {
        object CaptureState();
        void RestoreState(object state);
        System.Type GetStateType();
    }

}