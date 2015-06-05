using System;
using ObjCRuntime;

[assembly: LinkWith ("libSignIn.a", LinkTarget.ArmV7 | LinkTarget.Simulator | LinkTarget.Simulator64 | LinkTarget.Arm64, SmartLink = true, ForceLoad = true)]
