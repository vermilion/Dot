using System;
using System.Collections.Generic;

namespace PlatformFramework.EFCore.Entities.Customizers
{
    /// <summary>
    /// Builder
    /// </summary>
    /// <typeparam name="TBuilder"></typeparam>
    public class FluentBuilder<TBuilder>
    {
        protected List<Action<TBuilder>> Actions = new List<Action<TBuilder>>();

        public void Apply(TBuilder builder)
        {
            Actions.ForEach(action => action(builder));
            Freeze();
        }

        public void AddAction(Action<TBuilder> action)
        {
            if (Frozen)
                return;

            if (action != null)
                Actions.Add(action);
        }

        public void Freeze()
        {
            if (!Frozen)
                Frozen = true;
        }

        public bool Frozen { get; private set; }
    }
}
