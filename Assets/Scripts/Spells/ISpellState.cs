using PFVR.Player;

namespace PFVR.Spells {
    public interface ISpellState {
        void OnExit(PlayerBehaviour player, PlayerHandBehaviour hand);
        void OnEnter(PlayerBehaviour player, PlayerHandBehaviour hand);
        void OnUpdate(PlayerBehaviour player, PlayerHandBehaviour hand);
    }
}