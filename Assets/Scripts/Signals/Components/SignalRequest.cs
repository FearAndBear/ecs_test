namespace Signals.Components
{
    public struct SignalRequest
    {
        public SignalRequest(object signal)
        {
            _signal = signal;
        }

        private object _signal;
    }
}