using System;

// This file is auto-generated. Do not modify or move this file.

namespace SuperUnityBuild.Generated
{
    public enum ReleaseType
    {
        None,
        Release,
    }

    public enum Platform
    {
        None,
        Windows,
        Linux,
        WebGL,
    }

    public enum ScriptingBackend
    {
        None,
        Mono,
        IL2CPP,
    }

    public enum Target
    {
        None,
        Player,
    }

    public enum Distribution
    {
        None,
    }

    public static class BuildConstants
    {
        public static readonly DateTime buildDate = new DateTime(638786888098847935);
        public const string version = "1.0.0.1";
        public const int buildCounter = 1;
        public const ReleaseType releaseType = ReleaseType.Release;
        public const Platform platform = Platform.WebGL;
        public const ScriptingBackend scriptingBackend = ScriptingBackend.IL2CPP;
        public const Target target = Target.Player;
        public const Distribution distribution = Distribution.None;
    }
}

