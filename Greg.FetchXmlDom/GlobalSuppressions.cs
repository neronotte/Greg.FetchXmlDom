// This file is used by Code Analysis to maintain SuppressMessage
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given
// a specific target and scoped to a namespace, type, member, etc.

using System.Diagnostics.CodeAnalysis;

[assembly: SuppressMessage("Style", "IDE0300:Simplify collection initialization", Justification = "The package targets both net8.0 and net462. In net462 the feature is not available.", Scope = "module")]
[assembly: SuppressMessage("Style", "IDE0028:Simplify collection initialization", Justification = "The package targets both net8.0 and net462. In net462 the feature is not available.", Scope = "module")]
