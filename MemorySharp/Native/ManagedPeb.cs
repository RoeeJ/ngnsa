/*
 * MemorySharp Library
 * http://www.binarysharp.com/
 *
 * Copyright (C) 2012-2014 Jämes Ménétrey (a.k.a. ZenLulz).
 * This library is released under the MIT License.
 * See the file LICENSE for more information.
*/

using System;
using Binarysharp.MemoryManagement.Memory;

namespace Binarysharp.MemoryManagement.Native
{
    /// <summary>
    /// Class representing the Process Environment Block of a remote process.
    /// </summary>
    public class ManagedPeb : RemotePointer
    {
        #region Properties
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public byte InheritedAddressSpace
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        {
            get { return Read<byte>(PebStructure.InheritedAddressSpace); }
            set { Write(PebStructure.InheritedAddressSpace, value); }
        }
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public byte ReadImageFileExecOptions
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        {
            get { return Read<byte>(PebStructure.ReadImageFileExecOptions); }
            set { Write(PebStructure.ReadImageFileExecOptions, value); }
        }
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public bool BeingDebugged
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        {
            get { return Read<bool>(PebStructure.BeingDebugged); }
            set { Write(PebStructure.BeingDebugged, value); }
        }
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public byte SpareBool
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        {
            get { return Read<byte>(PebStructure.SpareBool); }
            set { Write(PebStructure.SpareBool, value); }
        }
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public IntPtr Mutant
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        {
            get { return Read<IntPtr>(PebStructure.Mutant); }
            set { Write(PebStructure.Mutant, value); }
        }
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public IntPtr Ldr
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        {
            get { return Read<IntPtr>(PebStructure.Ldr); }
            set { Write(PebStructure.Ldr, value); }
        }
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public IntPtr ProcessParameters
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        {
            get { return Read<IntPtr>(PebStructure.ProcessParameters); }
            set { Write(PebStructure.ProcessParameters, value); }
        }
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public IntPtr SubSystemData
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        {
            get { return Read<IntPtr>(PebStructure.SubSystemData); }
            set { Write(PebStructure.SubSystemData, value); }
        }
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public IntPtr ProcessHeap
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        {
            get { return Read<IntPtr>(PebStructure.ProcessHeap); }
            set { Write(PebStructure.ProcessHeap, value); }
        }
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public IntPtr FastPebLock
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        {
            get { return Read<IntPtr>(PebStructure.FastPebLock); }
            set { Write(PebStructure.FastPebLock, value); }
        }
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public IntPtr FastPebLockRoutine
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        {
            get { return Read<IntPtr>(PebStructure.FastPebLockRoutine); }
            set { Write(PebStructure.FastPebLockRoutine, value); }
        }
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public IntPtr FastPebUnlockRoutine
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        {
            get { return Read<IntPtr>(PebStructure.FastPebUnlockRoutine); }
            set { Write(PebStructure.FastPebUnlockRoutine, value); }
        }
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public IntPtr EnvironmentUpdateCount
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        {
            get { return Read<IntPtr>(PebStructure.EnvironmentUpdateCount); }
            set { Write(PebStructure.EnvironmentUpdateCount, value); }
        }
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public IntPtr KernelCallbackTable
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        {
            get { return Read<IntPtr>(PebStructure.KernelCallbackTable); }
            set { Write(PebStructure.KernelCallbackTable, value); }
        }
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public int SystemReserved
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        {
            get { return Read<int>(PebStructure.SystemReserved); }
            set { Write(PebStructure.SystemReserved, value); }
        }
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public int AtlThunkSListPtr32
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        {
            get { return Read<int>(PebStructure.AtlThunkSListPtr32); }
            set { Write(PebStructure.AtlThunkSListPtr32, value); }
        }
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public IntPtr FreeList
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        {
            get { return Read<IntPtr>(PebStructure.FreeList); }
            set { Write(PebStructure.FreeList, value); }
        }
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public IntPtr TlsExpansionCounter
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        {
            get { return Read<IntPtr>(PebStructure.TlsExpansionCounter); }
            set { Write(PebStructure.TlsExpansionCounter, value); }
        }
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public IntPtr TlsBitmap
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        {
            get { return Read<IntPtr>(PebStructure.TlsBitmap); }
            set { Write(PebStructure.TlsBitmap, value); }
        }
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public long TlsBitmapBits
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        {
            get { return Read<long>(PebStructure.TlsBitmapBits); }
            set { Write(PebStructure.TlsBitmapBits, value); }
        }
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public IntPtr ReadOnlySharedMemoryBase
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        {
            get { return Read<IntPtr>(PebStructure.ReadOnlySharedMemoryBase); }
            set { Write(PebStructure.ReadOnlySharedMemoryBase, value); }
        }
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public IntPtr ReadOnlySharedMemoryHeap
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        {
            get { return Read<IntPtr>(PebStructure.ReadOnlySharedMemoryHeap); }
            set { Write(PebStructure.ReadOnlySharedMemoryHeap, value); }
        }
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public IntPtr ReadOnlyStaticServerData
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        {
            get { return Read<IntPtr>(PebStructure.ReadOnlyStaticServerData); }
            set { Write(PebStructure.ReadOnlyStaticServerData, value); }
        }
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public IntPtr AnsiCodePageData
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        {
            get { return Read<IntPtr>(PebStructure.AnsiCodePageData); }
            set { Write(PebStructure.AnsiCodePageData, value); }
        }
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public IntPtr OemCodePageData
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        {
            get { return Read<IntPtr>(PebStructure.OemCodePageData); }
            set { Write(PebStructure.OemCodePageData, value); }
        }
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public IntPtr UnicodeCaseTableData
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        {
            get { return Read<IntPtr>(PebStructure.UnicodeCaseTableData); }
            set { Write(PebStructure.UnicodeCaseTableData, value); }
        }
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public int NumberOfProcessors
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        {
            get { return Read<int>(PebStructure.NumberOfProcessors); }
            set { Write(PebStructure.NumberOfProcessors, value); }
        }
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public long NtGlobalFlag
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        {
            get { return Read<long>(PebStructure.NtGlobalFlag); }
            set { Write(PebStructure.NtGlobalFlag, value); }
        }
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public long CriticalSectionTimeout
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        {
            get { return Read<long>(PebStructure.CriticalSectionTimeout); }
            set { Write(PebStructure.CriticalSectionTimeout, value); }
        }
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public IntPtr HeapSegmentReserve
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        {
            get { return Read<IntPtr>(PebStructure.HeapSegmentReserve); }
            set { Write(PebStructure.HeapSegmentReserve, value); }
        }
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public IntPtr HeapSegmentCommit
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        {
            get { return Read<IntPtr>(PebStructure.HeapSegmentCommit); }
            set { Write(PebStructure.HeapSegmentCommit, value); }
        }
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public IntPtr HeapDeCommitTotalFreeThreshold
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        {
            get { return Read<IntPtr>(PebStructure.HeapDeCommitTotalFreeThreshold); }
            set { Write(PebStructure.HeapDeCommitTotalFreeThreshold, value); }
        }
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public IntPtr HeapDeCommitFreeBlockThreshold
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        {
            get { return Read<IntPtr>(PebStructure.HeapDeCommitFreeBlockThreshold); }
            set { Write(PebStructure.HeapDeCommitFreeBlockThreshold, value); }
        }
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public int NumberOfHeaps
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        {
            get { return Read<int>(PebStructure.NumberOfHeaps); }
            set { Write(PebStructure.NumberOfHeaps, value); }
        }
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public int MaximumNumberOfHeaps
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        {
            get { return Read<int>(PebStructure.MaximumNumberOfHeaps); }
            set { Write(PebStructure.MaximumNumberOfHeaps, value); }
        }
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public IntPtr ProcessHeaps
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        {
            get { return Read<IntPtr>(PebStructure.ProcessHeaps); }
            set { Write(PebStructure.ProcessHeaps, value); }
        }
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public IntPtr GdiSharedHandleTable
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        {
            get { return Read<IntPtr>(PebStructure.GdiSharedHandleTable); }
            set { Write(PebStructure.GdiSharedHandleTable, value); }
        }
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public IntPtr ProcessStarterHelper
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        {
            get { return Read<IntPtr>(PebStructure.ProcessStarterHelper); }
            set { Write(PebStructure.ProcessStarterHelper, value); }
        }
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public IntPtr GdiDcAttributeList
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        {
            get { return Read<IntPtr>(PebStructure.GdiDcAttributeList); }
            set { Write(PebStructure.GdiDcAttributeList, value); }
        }
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public IntPtr LoaderLock
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        {
            get { return Read<IntPtr>(PebStructure.LoaderLock); }
            set { Write(PebStructure.LoaderLock, value); }
        }
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public int OsMajorVersion
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        {
            get { return Read<int>(PebStructure.OsMajorVersion); }
            set { Write(PebStructure.OsMajorVersion, value); }
        }
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public int OsMinorVersion
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        {
            get { return Read<int>(PebStructure.OsMinorVersion); }
            set { Write(PebStructure.OsMinorVersion, value); }
        }
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public ushort OsBuildNumber
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        {
            get { return Read<ushort>(PebStructure.OsBuildNumber); }
            set { Write(PebStructure.OsBuildNumber, value); }
        }
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public ushort OsCsdVersion
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        {
            get { return Read<ushort>(PebStructure.OsCsdVersion); }
            set { Write(PebStructure.OsCsdVersion, value); }
        }
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public int OsPlatformId
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        {
            get { return Read<int>(PebStructure.OsPlatformId); }
            set { Write(PebStructure.OsPlatformId, value); }
        }
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public int ImageSubsystem
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        {
            get { return Read<int>(PebStructure.ImageSubsystem); }
            set { Write(PebStructure.ImageSubsystem, value); }
        }
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public int ImageSubsystemMajorVersion
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        {
            get { return Read<int>(PebStructure.ImageSubsystemMajorVersion); }
            set { Write(PebStructure.ImageSubsystemMajorVersion, value); }
        }
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public IntPtr ImageSubsystemMinorVersion
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        {
            get { return Read<IntPtr>(PebStructure.ImageSubsystemMinorVersion); }
            set { Write(PebStructure.ImageSubsystemMinorVersion, value); }
        }
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public IntPtr ImageProcessAffinityMask
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        {
            get { return Read<IntPtr>(PebStructure.ImageProcessAffinityMask); }
            set { Write(PebStructure.ImageProcessAffinityMask, value); }
        }
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public IntPtr[] GdiHandleBuffer
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        {
            get { return Read<IntPtr>(PebStructure.GdiHandleBuffer, 0x22); }
            set { Write(PebStructure.GdiHandleBuffer, value); }
        }
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public IntPtr PostProcessInitRoutine
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        {
            get { return Read<IntPtr>(PebStructure.PostProcessInitRoutine); }
            set { Write(PebStructure.PostProcessInitRoutine, value); }
        }
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public IntPtr TlsExpansionBitmap
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        {
            get { return Read<IntPtr>(PebStructure.TlsExpansionBitmap); }
            set { Write(PebStructure.TlsExpansionBitmap, value); }
        }
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public IntPtr[] TlsExpansionBitmapBits
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        {
            get { return Read<IntPtr>(PebStructure.TlsExpansionBitmapBits, 0x20); }
            set { Write(PebStructure.TlsExpansionBitmapBits, value); }
        }
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public IntPtr SessionId
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        {
            get { return Read<IntPtr>(PebStructure.SessionId); }
            set { Write(PebStructure.SessionId, value); }
        }
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public long AppCompatFlags
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        {
            get { return Read<long>(PebStructure.AppCompatFlags); }
            set { Write(PebStructure.AppCompatFlags, value); }
        }
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public long AppCompatFlagsUser
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        {
            get { return Read<long>(PebStructure.AppCompatFlagsUser); }
            set { Write(PebStructure.AppCompatFlagsUser, value); }
        }
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public IntPtr ShimData
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        {
            get { return Read<IntPtr>(PebStructure.ShimData); }
            set { Write(PebStructure.ShimData, value); }
        }
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public IntPtr AppCompatInfo
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        {
            get { return Read<IntPtr>(PebStructure.AppCompatInfo); }
            set { Write(PebStructure.AppCompatInfo, value); }
        }
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public long CsdVersion
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        {
            get { return Read<long>(PebStructure.CsdVersion); }
            set { Write(PebStructure.CsdVersion, value); }
        }
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public IntPtr ActivationContextData
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        {
            get { return Read<IntPtr>(PebStructure.ActivationContextData); }
            set { Write(PebStructure.ActivationContextData, value); }
        }
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public IntPtr ProcessAssemblyStorageMap
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        {
            get { return Read<IntPtr>(PebStructure.ProcessAssemblyStorageMap); }
            set { Write(PebStructure.ProcessAssemblyStorageMap, value); }
        }
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public IntPtr SystemDefaultActivationContextData
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        {
            get { return Read<IntPtr>(PebStructure.SystemDefaultActivationContextData); }
            set { Write(PebStructure.SystemDefaultActivationContextData, value); }
        }
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public IntPtr SystemAssemblyStorageMap
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        {
            get { return Read<IntPtr>(PebStructure.SystemAssemblyStorageMap); }
            set { Write(PebStructure.SystemAssemblyStorageMap, value); }
        }
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public IntPtr MinimumStackCommit
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        {
            get { return Read<IntPtr>(PebStructure.MinimumStackCommit); }
            set { Write(PebStructure.MinimumStackCommit, value); }
        }
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="ManagedPeb"/> class.
        /// </summary>
        /// <param name="memorySharp">The reference of the <see cref="MemorySharp"/> object.</param>
        /// <param name="address">The location of the peb.</param>
        internal ManagedPeb(MemorySharp memorySharp, IntPtr address) : base(memorySharp, address)
        {}
        #endregion

        #region Methods
        /// <summary>
        /// Finds the Process Environment Block address of a specified process.
        /// </summary>
        /// <param name="processHandle">A handle of the process.</param>
        /// <returns>A <see cref="IntPtr"/> pointer of the PEB.</returns>
        public static IntPtr FindPeb(SafeMemoryHandle processHandle)
        {
            return MemoryCore.NtQueryInformationProcess(processHandle).PebBaseAddress;
        }
        #endregion
    }
}
