using DIKUArcade.State;


namespace Breakout.States {

    /// <summary>
    /// An interface for a state machine.
    /// </summary>
    public interface IStateMachine {
        IGameState GetGameState(GameStateType gameStateType);
        void SwitchGameState(GameStateType gameStateType);
    }
}