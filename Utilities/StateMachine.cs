using System.Collections.Generic;

namespace ExtraDevelopment.Utilities {
    public class StateMachine<T> {
        public delegate T StateAction();

        private readonly Dictionary<T, StateAction> _stateActions = new();

        public T CurrentState { get; private set; }

        public StateAction this[T state] {
            set => _stateActions[state] = value;
        }

        public T Execute() => CurrentState = _stateActions[CurrentState]();
    }
}