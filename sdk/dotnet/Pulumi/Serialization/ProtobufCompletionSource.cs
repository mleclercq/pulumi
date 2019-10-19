﻿// Copyright 2016-2018, Pulumi Corporation

#nullable enable

using Google.Protobuf.WellKnownTypes;

namespace Pulumi.Rpc
{
    public abstract class ProtobufCompletionSource<T> : OutputCompletionSource<T>, IProtobufOutputCompletionSource
    {
        private readonly Deserializer<T> _deserialize;

        private protected ProtobufCompletionSource(
            Resource resource, Deserializer<T> deserialize)
            : base(resource)
        {
            _deserialize = deserialize;
        }

        void IProtobufOutputCompletionSource.SetResult(Value value)
        {
            // First, special case if this property points to null.  We need to handle that as that
            // is common in preview, and we want to map this to an unknown output if so.
            var (unwrapped, isSecret) = Deserializers.UnwrapSecret(value);

            if (unwrapped.KindCase == Value.KindOneofCase.NullValue)
            {
                // we're pointing at null.  During preview, we'll treat that as an unknown value.  
                // During non-preview though it will be converted the to the default value for this
                // output type.
                var isKnown = !Deployment.Instance.Options.DryRun;
                this.SetResult(new OutputData<T>(default!, isKnown, isSecret));
                return;
            }

            // non-null value.  this is always 'known' regardless of if we're in preview or not.
            // Defer to subclass to figure out what this value actually is. Note: we can just call
            // this on the top value again.  Deserialization functions will unwrap secrets as well.
            var (deserialized, innerIsSecret) = _deserialize(value);
            this.SetResult(new OutputData<T>(deserialized, isKnown: true, innerIsSecret));
        }
    }
}
