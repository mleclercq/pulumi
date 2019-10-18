﻿// Copyright 2016-2018, Pulumi Corporation

#nullable enable

using System.Collections.Generic;
using System.Linq;

namespace Pulumi
{
    /// <summary>
    /// <see cref="CustomResourceOptions"/> is a bag of optional settings that control a <see
    /// cref="CustomResource"/>'s behavior.
    /// </summary>
    public class CustomResourceOptions : ResourceOptions
    {
        /// <summary>
        /// When set to <c>true</c>, indicates that this resource should be deleted before its
        /// replacement is created when replacement is necessary.
        /// </summary>
        public bool? DeleteBeforeReplace { get; set; }

        private List<string>? _additionalSecretOutputs;

        /// <summary>
        /// The names of outputs for this resource that should be treated as secrets. This augments
        /// the list that the resource provider and pulumi engine already determine based on inputs
        /// to your resource. It can be used to mark certain outputs as a secrets on a per resource
        /// basis.
        /// </summary>
        public List<string> AdditionalSecretOutputs
        {
            get => _additionalSecretOutputs ?? (_additionalSecretOutputs = new List<string>());
            set => _additionalSecretOutputs = value;
        }

        /// <summary>
        /// When provided with a resource ID, import indicates that this resource's provider should
        /// import its state from the cloud resource with the given ID.The inputs to the resource's
        /// constructor must align with the resource's current state.Once a resource has been
        /// imported, the import property must be removed from the resource's options.
        /// </summary>
        public Id? Import { get; set; }

        internal override ResourceOptions Clone()
        {
            var cloned = base.Clone();
            return new CustomResourceOptions
            {
                // Base properties
                Aliases = cloned.Aliases,
                CustomTimeouts = cloned.CustomTimeouts,
                DependsOn = cloned.DependsOn,
                Id = cloned.Id,
                Parent = cloned.Parent,
                IgnoreChanges = cloned.IgnoreChanges,
                Protect = cloned.Protect,
                Provider = cloned.Provider,
                ResourceTransformations = cloned.ResourceTransformations,
                Version = cloned.Version,

                // Our properties
                AdditionalSecretOutputs = this.AdditionalSecretOutputs.ToList(),
                DeleteBeforeReplace = this.DeleteBeforeReplace,
                Import = this.Import,
            };
        }
    }
}
