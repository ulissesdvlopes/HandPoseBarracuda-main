public interface ITransitionable
{
    bool Started {
        get;
        set;
    }
    void Transition ();
}
