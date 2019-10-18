// Copyright 2016-2018, Pulumi Corporation

#nullable enable

using System.Collections.Immutable;
using System.Threading.Tasks;
using Pulumi.Rpc;
using Pulumirpc;

namespace Pulumi
{
    /// <summary>
    /// CustomResource is a resource whose create, read, update, and delete(CRUD) operations are
    /// managed by performing external operations on some physical entity. The engine understands how
    /// to diff and perform partial updates of them, and these CRUD operations are implemented in a
    /// dynamically loaded plugin for the defining package.
    /// </summary>
    public class CustomResource : Resource
    {
        /// <summary>
        /// Id is the provider-assigned unique ID for this managed resource.  It is set during
        /// deployments and may be missing (unknown) during planning phases.
        /// </summary>
        public readonly Output<Id> Id;

        [ResourceField("id")]
        private readonly TaskCompletionSource<OutputData<Id>> _id = new TaskCompletionSource<OutputData<Id>>();

        /// <summary>
        /// Creates and registers a new managed resource.  t is the fully qualified type token and
        /// name is the "name" part to use in creating a stable and globally unique URN for the
        /// object. dependsOn is an optional list of other resources that this resource depends on,
        /// controlling the order in which we perform resource operations.Creating an instance does
        /// not necessarily perform a create on the physical entity which it represents, and
        /// instead, this is dependent upon the diffing of the new goal state compared to the
        /// current known resource state.
        /// </summary>
        public CustomResource(string type, string name, ImmutableDictionary<string, Input<object>> properties, ResourceOptions? opts = null)
            : base(type, name, custom: true, properties, opts ?? new ResourceOptions())
        {
            if (opts is ComponentResourceOptions componentOpts && componentOpts.Providers != null)
            {
                throw new ResourceException("Do not supply 'providers' option to a CustomResource. Did you mean 'provider' instead?", this);
            }

            this.Id = new Output<Id>(_id.Task);
        }
    }
}

//using Google.Protobuf.WellKnownTypes;
//using Pulumirpc;
//using System;
//using System.Collections.Generic;
//using System.Threading.Tasks;

//namespace Pulumi {

//    public abstract class CustomResource : Resource {

//        protected Task<Pulumirpc.RegisterResourceResponse> m_registrationResponse;
//        public Output<string> Id { get; private set;}
//        TaskCompletionSource<OutputState<string>> m_IdCompletionSoruce;
//        public CustomResource()  {
//            m_IdCompletionSoruce = new TaskCompletionSource<OutputState<string>>();
//            Id = new Output<string>(m_IdCompletionSoruce.Task);
//        }

//        protected override void OnResourceRegistrationComplete(Task<RegisterResourceResponse> resp) {
//            base.OnResourceRegistrationComplete(resp);
//            if (resp.IsCanceled) {
//                m_IdCompletionSoruce.SetCanceled();
//            } else if (resp.IsFaulted) {
//                m_IdCompletionSoruce.SetException(resp.Exception);
//            } else {
//                Serilog.Log.Debug("Setting id to {id} for {urn} with dependency {this}", resp.Result.Id, resp.Result.Urn, this);
//                m_IdCompletionSoruce.SetResult(new OutputState<string>(resp.Result.Id, !string.IsNullOrEmpty(resp.Result.Id), this));
//            }
//        }
//    }
//}