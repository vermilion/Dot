using System;
using System.Collections.Generic;

namespace PlatformFramework.EFCore.Entities
{
    /// <summary>
    /// Builder
    /// </summary>
    /// <typeparam name="TBuilder"></typeparam>
    public class FluentBuilder<TBuilder>
    {
        private readonly List<Action<TBuilder>> _actions = new List<Action<TBuilder>>();

        public void Apply(TBuilder builder)
        {
            _actions.ForEach(action => action(builder));
            Freeze();
        }

        protected void AddAction(Action<TBuilder> action)
        {
            if (Frozen)
                return;

            if (action != null)
                _actions.Add(action);
        }

        private void Freeze()
        {
            if (!Frozen)
                Frozen = true;
        }

        private bool Frozen { get; set; }
    }
}
