using System;
using System.Collections.Generic;
using System.Data.Entity;

namespace EFBase
{
    public class ModelCustomization
    {
        internal static readonly List<Action<DbModelBuilder>> Customizations 
            = new List<Action<DbModelBuilder>>();

        internal static readonly Dictionary<Type, Type> Substitutions
            = new Dictionary<Type, Type>();

        public static void Register(Action<DbModelBuilder> customization)
        {
            Customizations.Add(customization);
        }

        public static void RegisterTypeSubstitution<TSource, TDerived>()
        {
            Substitutions.Add(typeof(TSource), typeof(TDerived));
        }

    }
}