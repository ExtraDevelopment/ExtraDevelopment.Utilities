using System.Collections.Generic;

namespace ExtraDevelopment.Utilities {
    public class StateMachine<T> {
        private Dictionary<T, StateNode> _states = new();
        
        public T CurrentState { get; private set; }
        private StateNode CurrentStateNode
            => _states.TryGetValue(CurrentState, out var stateNode) ? stateNode : null;

        public void Execute() => CurrentState = CurrentStateNode != null ? CurrentStateNode.Execute() : CurrentState;

        public (StateAction Action, StateDirector Director) this[T state] {
            set {
                _states[state] = new(state, value.Action, value.Director);
            }
        }

        public StateTransition this[T fromState, T toState] {
            set {
                _states[fromState].AddTransition(value, toState);
            }
        }

        public delegate void StateAction();
        public delegate T StateDirector(T currentState);
        public delegate void StateTransition(T fromState, T toState);
        
        private class StateNode {
            private StateAction _action;
            private StateDirector _director;
            private Dictionary<T, StateTransition> _transitions = new();

            public StateNode(T state, StateAction action, StateDirector director)
            {
                State = state;
                _action = action;
                _director = director;
            }

            public StateNode AddTransition(StateTransition transition, T toState)
            {
                _transitions[toState] = transition;
                return this;
            }

            public T State { get; }

            public T Execute() {
                _action?.Invoke();
                T nextState = _director != null ? _director.Invoke(State) : State;
                if (_transitions.TryGetValue(nextState, out StateTransition transition)) {
                    transition?.Invoke(State, nextState);
                }
                return nextState;
            }
        }
    }
}