﻿// Copyright 2016-2018, Pulumi Corporation

#nullable enable

using Google.Protobuf.WellKnownTypes;

namespace Pulumi.Rpc
{
    public delegate (T deserialized, bool isSecret) Deserializer<T>(Value value);
}