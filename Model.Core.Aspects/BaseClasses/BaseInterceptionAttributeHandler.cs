using Microsoft.Practices.Unity.InterceptionExtension;

namespace Model.Core.Aspects
{
    public abstract class BaseInterceptionAttributeHandler : ICallHandler
    {
        public IMethodReturn Invoke(IMethodInvocation input, GetNextHandlerDelegate getNext)
        {
            // Before invoking the method on the original target
            OnEntry(input);

            // Invoke the next handler in the chain
            var result = getNext().Invoke(input, getNext);

            // After invoking the method on the original target
            if (result.Exception != null)
            {
                OnException(input);
            }
            else
            {
                OnSuccess(input);
            }

            OnExit(input);

            return result;
        }

        protected virtual void OnEntry(IMethodInvocation input)
        {

        }

        protected virtual void OnSuccess(IMethodInvocation input)
        {

        }

        protected virtual void OnExit(IMethodInvocation input)
        {

        }

        protected virtual void OnException(IMethodInvocation input)
        {

        }


        public int Order
        {
            get
            {
                return 0;
            }
            set
            {

            }
        }
    }
}
