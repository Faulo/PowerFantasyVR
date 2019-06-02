namespace PFVR.Gestures {
    public interface IGestureState {
        void OnExit(PlayerBehaviour player, PlayerHandBehaviour hand);
        void OnEnter(PlayerBehaviour player, PlayerHandBehaviour hand);
        void OnUpdate(PlayerBehaviour player, PlayerHandBehaviour hand);
    }
}