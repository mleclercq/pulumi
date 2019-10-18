﻿// Copyright 2016-2018, Pulumi Corporation

#nullable enable

using System.Collections.Immutable;
using Google.Protobuf.WellKnownTypes;

namespace Pulumi
{
    /// <summary>
    /// Base type for all resource argument classes.
    /// </summary>
    public abstract class ResourceArgs
    {
        public static readonly ResourceArgs Empty = new EmptyResourceArgs();

        protected ResourceArgs()
        {
        }

        internal ImmutableDictionary<string, IInput> ToDictionary()
        {
            var dictionaryBuilder = ImmutableDictionary.CreateBuilder<string, IInput>();
            AddProperties(new PropertyBuilder(dictionaryBuilder));
            return dictionaryBuilder.ToImmutable();
        }

        protected abstract void AddProperties(PropertyBuilder builder);

        protected struct PropertyBuilder
        {
            private readonly ImmutableDictionary<string, IInput>.Builder _builder;

            internal PropertyBuilder(ImmutableDictionary<string, IInput>.Builder builder)
            {
                _builder = builder;
            }

            public void Add<T>(string name, Input<T> input)
                => _builder.Add(name, input);

            public void Add<T>(string name, InputList<T> list)
                => _builder.Add(name, list);

            public void Add<T>(string name, InputMap<T> map)
                => _builder.Add(name, map);
        }

        private class EmptyResourceArgs : ResourceArgs
        {
            protected override void AddProperties(PropertyBuilder builder)
            {
            }
        }
    }
}
