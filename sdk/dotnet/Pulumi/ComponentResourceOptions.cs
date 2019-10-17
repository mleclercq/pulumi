﻿// Copyright 2016-2018, Pulumi Corporation

#nullable enable

using System.Collections.Generic;

namespace Pulumi
{
    /// <summary>
    /// A bag of optional settings that control a <see cref="ComponentResource"/>'s behavior.
    /// </summary>
    public class ComponentResourceOptions : ResourceOptions
    {
        private List<ProviderResource>? _providers;

        /// <summary>
        /// An optional set of providers to use for child resources.
        ///
        /// Note: do not provide both <see cref="ResourceOptions.Provider"/> and <see
        /// cref="Providers"/>.
        /// </summary>
        public List<ProviderResource> Providers
        {
            get => _providers ?? (_providers = new List<ProviderResource>());
            set => _providers = value;
        }
    }
}