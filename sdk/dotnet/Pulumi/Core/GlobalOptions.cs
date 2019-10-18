﻿// Copyright 2016-2018, Pulumi Corporation

#nullable enable

namespace Pulumi
{
    internal class GlobalOptions
    {
        public static GlobalOptions Instance { get; internal set; }

        public readonly bool DryRun;
        public readonly bool QueryMode;
        public readonly int Parallel;
        public readonly string Project;
        public readonly string Stack;
        public readonly string Pwd;
        public readonly string Monitor;
        public readonly string Engine;
        public readonly string Tracing;

        public GlobalOptions(bool dryRun, bool queryMode, int parallel, string project, string stack, string pwd, string monitor, string engine, string tracing)
        {
            DryRun = dryRun;
            QueryMode = queryMode;
            Parallel = parallel;
            Project = project;
            Stack = stack;
            Pwd = pwd;
            Monitor = monitor;
            Engine = engine;
            Tracing = tracing;
        }
    }
}
