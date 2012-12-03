using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Immersion
{
    // This class contains everything needed to call a Sprite's 
    // method using reflection.
    public class GameAction
    {
        Sprite myActor;
        MethodInfo myMethod;
        object[] myParameters;

        public GameAction(Sprite actor, MethodInfo method, object[] param)
        {
            myActor = actor;
            myMethod = method;
            myParameters = param;
        }

        public void Invoke()
        {
            myMethod.Invoke(myActor, myParameters);
        }

        // Sometimes I don't know what the parameters should be until
        // it's time to call the method.  When I go to invoke, this 
        // overload will re-set parameters.  This is especially important in 
        // dealing with the mouse.  I'd like to pass the CURRENT mouse location
        // to the method.  Maybe this method should ADD the given parameters rather than
        // resetting the parameters??  Hmmm - design decision!
        public void Invoke(object[] parameters)
        {
            myParameters = parameters;
            myMethod.Invoke(myActor, myParameters);
        }
    }
}