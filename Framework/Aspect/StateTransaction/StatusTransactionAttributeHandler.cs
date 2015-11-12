using Framework.Aspect.BaseClasses;
using Microsoft.Practices.Unity.InterceptionExtension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Framework.Aspect.StateTransaction
{
    public class StatusTransactionAttributeHandler : BaseInterceptionAttributeHandler
    {

        PropertyInfo stateProperty;
        private object statusToOnSuccess;
        private object statusToOnFailure;

        public StatusTransactionAttributeHandler(object statusToOnSuccess, object statusToOnFailure)
        {
            this.statusToOnSuccess = statusToOnSuccess;
            this.statusToOnFailure = statusToOnFailure;
        }

        protected override void OnEntry(IMethodInvocation input)
        {
            var iStateTransactionctionableClosedGeneric = input.Target.GetType().GetInterfaces()
                                                                                .Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IStateTransactionctionable<>))
                                                                                .SingleOrDefault();

            if (iStateTransactionctionableClosedGeneric == null || iStateTransactionctionableClosedGeneric.GetGenericArguments().Count() > 1)
                throw new Exception("");

            var typeParameterIStateTransactionctionable = iStateTransactionctionableClosedGeneric.GetGenericArguments()[0];

            // Obtain current instance status
            stateProperty = iStateTransactionctionableClosedGeneric.GetProperty("State");
            // Obtain current status
            object currentState = stateProperty.GetValue(input.Target);
            // Construct array of to state
            var listClosedType = typeof(List<>).MakeGenericType(typeParameterIStateTransactionctionable);
            var listClosedTypeObj = Activator.CreateInstance(listClosedType);
            if (statusToOnSuccess != null)
            {
                listClosedTypeObj.GetType().GetMethod("Add").Invoke(listClosedTypeObj, new[] { statusToOnSuccess });
            }
            if (statusToOnFailure != null)
            {
                listClosedTypeObj.GetType().GetMethod("Add").Invoke(listClosedTypeObj, new[] { statusToOnFailure });
            }
            // Test in method in invocable
            bool methodCanBeinvoked = (bool)iStateTransactionctionableClosedGeneric.GetMethod("IsAllowedTransaction").Invoke(input.Target, new object[] { currentState, listClosedTypeObj });

            if (!methodCanBeinvoked)
                throw new Exception("");
        }

        protected override void OnSuccess(IMethodInvocation input)
        {
            if (statusToOnSuccess != null)
                stateProperty.SetValue(input.Target, statusToOnSuccess);
        }

        protected override void OnException(IMethodInvocation input)
        {
            if (statusToOnFailure != null)
                stateProperty.SetValue(input.Target, statusToOnFailure);
        }

    }
}
