using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Win32;

namespace TweakJunkies_Freemium_Utility
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
            MainPanel.Visible = false;
            this.DoubleBuffered = true;
        }
        #region Reg Keys
        private static RegistryKey EnvVariables = Registry.LocalMachine.OpenSubKey("HKEY_LOCAL_MACHINE\\SYSTEM\\CurrentControlSet\\Control\\Session Manager\\Environment");
        private static RegistryKey Kernel = Registry.LocalMachine.OpenSubKey(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\Session Manager\kernel");
        private static RegistryKey DWMKey = Registry.LocalMachine.OpenSubKey(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\Dwm");
        private static RegistryKey Power = Registry.LocalMachine.OpenSubKey(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\Power");
        private static RegistryKey WDF01000Params = Registry.LocalMachine.OpenSubKey(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\Wdf01000\Parameters");
        private static RegistryKey GraphicsDriverScheduler = Registry.LocalMachine.OpenSubKey(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\GraphicsDrivers\Scheduler");
        private static RegistryKey GraphicsDriver = Registry.LocalMachine.OpenSubKey(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\GraphicsDrivers");
        #endregion
        private async void Main_Load(object sender, EventArgs e)
        {
            MainPanel.Visible = false;
            this.Invalidate();
            guna2Transition1.ShowSync(MainPanel);
            MainPanel.Visible = true;
            this.Invalidate();
        }

        private async Task TweaksFunc(string commands)
        {
            var processInfo = new ProcessStartInfo("cmd.exe")
            {
                RedirectStandardInput = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            try {
                using (var process = Process.Start(processInfo)) {
                    if (process != null) {
                        using (var standardInput = process.StandardInput) {
                            if (standardInput.BaseStream.CanWrite) {
                                await standardInput.WriteLineAsync(commands);
                                await standardInput.FlushAsync();
                            }
                            standardInput.Close();

                            await Task.Run(() => process.WaitForExit());
                        }
                    }
                }
            }
            catch (Exception ex) {
                MessageBox.Show("error: " + ex.Message);
            }
        }


        private async void intelmsr_Click(object sender, EventArgs e)
        {
            string tweaks = @"
set rwePath=""C:\Program Files\RW Everything Portable\Rw.exe""
start /b """" %rwePath% /Min /NoLogo /Stdout /Command=""W32 0x610 0x00FFFFFF 0x00FFFFFF; W32 0x1A0 0x00000000 0x00050000; W32 0x770 0x00000000 0x00000001; W32 0x774 0x000003FF 0x00FFFFFF; W32 0x1FC 0x00000000 0x00000000; W32 0x620 0x00000000 0x00007F7F; W32 0x6E0 0x00000000 0x00000001; W32 0x618 0x00000000 0x00000000; W32 0x640 0x00000000 0x00000000; W32 0x638 0x00000000 0x00000000; W32 0x48 0x00000000 0x00000000; W16 0x1A0 0x00050009; W16 0x1A2 0x00000064; W16 0x3A 0x00000000; W16 0xE2 0x00000000; W16 0x19A 0x00000000; W16 0x610 0x00000000; W16 0x48C 0x00000000; W16 0x49C 0x00000000; W16 0x1A4 0x00000000; W16 0x10A 0x00000000; W16 0x1B0 0x00000000; W16 0x1A4 0x00000000; W16 0x17A 0x00000000; W16 0x618 0x00000000; W16 0x640 0x00000000; W16 0x638 0x00000000; wrmsr 0x610 0x00FFFFFF 0x00FFFFFF 0; wrmsr 0x1A0 0x00000000 0x00050000 0; wrmsr 0x1FC 0x00000000 0x00000000 0; wrmsr 0x620 0x00000000 0x00007F7F 0; wrmsr 0x6E0 0x00000000 0x00000001 0; wrmsr 0x618 0x00000000 0x00000000 0; wrmsr 0x640 0x00000000 0x00000000 0; wrmsr 0x638 0x00000000 0x00000000 0; wrmsr 0x48 0x00000000 0x00000000 0; wrmsr 0x17A 0x00000000 0x00000000 0; WRMSR 0xE4 0x00000000 0x00000000 0; WRMSR 0x2E7 0x00000000 0x00000000 0; WRMSR 0x63A 0x00000000 0x0000001F 0; WRMSR 0x642 0x00000000 0x0000001F 0; W32 0xFEE00380 0x00000001; W32 0xFEE003E0 0x0000000F"" >nul 2>&1
pause
";

            await Task.Run(() => TweaksFunc(tweaks));
        }

        private async void amdmsr_Click(object sender, EventArgs e)
        {
            string tweaks = @"
set rwePath=""C:\Program Files\RW Everything Portable\Rw.exe""
start /b """" %rwePath% /Stdout /Command=""wrmsr 0xC0010015 0x00000001 0xC9000011; WRMSR 0xC0011020 0x00000000 0x00000000 0; WRMSR 0xC0011021 0x00000000 0x00000040 0; WRMSR 0xC0011022 0x00000000 0x00510000 0; WRMSR 0xC001102B 0x00000018 0x08CC1600 0; WRMSR 0xC0010296 0x00000000 0x00000000 0; WRMSR 0xC001102D 0x00000000 0x00000000 0; WRMSR 0xC001102E 0x00000000 0x00000000 0; WRMSR 0xC0011015 0x00000000 0x00000001 0; WRMSR 0xC001102B 0x00000000 0x00000000 0; WRMSR 0xC0010292 0xFFFFFFFF 0xFFFFFFFF 0; WRMSR 0xC0010296 0x00000000 0x00000000 0; WRMSR 0xC0011023 0x00000000 0x00000000 0; WRMSR 0xC0011021 0x00000000 0x00000000 0; WRMSR 0xC0011022 0x00000000 0x00000000 0; WRMSR 0xC0011027 0x00000000 0x00000000 0; WRMSR 0xC001102C 0x00000000 0x00000000 0"" >nul 2>&1
pause
";

            await Task.Run(() => TweaksFunc(tweaks));
        }

        private async void w16andimod_Click(object sender, EventArgs e)
        {
            string tweaks = @"
set rwePath=""C:\Program Files\RW Everything Portable\Rw.exe""
start /b """" %rwePath% /Stdout /Command=""W32 0xFEE00380 0x00000001; W32 0xFEE003E0 0x0000000F; W16 0x54 0x0000; W32 0x42400024 0x00000000; W32 0x42400044 0x00000000; W32 0x42400064 0x00000000; W32 0x42400018 0x00000018; W16 0x3A0 0000; W16 0x20 0000; W16 0x3A8 0000; W16 0x30 0x0000; W16 0x3B0 0000; W16 0x3C0 0000; W16 0x40 0003; W16 0x50 0000; W16 0x58 0001; W16 0x6C 0000; W16 0x20 0001; W16 0x30 0001; W32 0x50 0x0000; W32 0x54 0x0000; W32 0x58 0x0000; W16 0x3A8 0001; W16 0x290 0000; W16 0x3F0 0000; W16 0x3E0 0000; W16 0x1F0 0000; W16 0x3C 0000; W16 0x1C 0001; W16 0x60 0005; W16 0xF8 0xFFFF; W16 0xA8 0000; W16 0xC8 0001; W16 0xD0 0000; W16 0x1B0 0000; W16 0x3D0 0000; W16 0x2A0 0000; W16 0x340 0000; W16 0x1C0 0001; W16 0x180 0001; W16 0x2F0 0000; W16 0x310 0001; W16 0x320 0000; W16 0x2D0 0000; W16 0x340 0001; W16 0x350 0001; W16 0x370 0000; W16 0x3A0 0001; W16 0x2C0 0000; W16 0x400 0001; W16 0x2D0 0001; W16 0x240 0000; W16 0x250 0001; W16 0x200 0001; W16 0x2B0 0000; W16 0x260 0001; W16 0x2F0 0001; W16 0x48 0000; W16 0x49 0000; W16 0xC0 0000; W16 0xC1 0000; W16 0x60 0000; W16 0x1D0 0000; W16 0x2C1 0000; W16 0x150 0000; W16 0x1F8 0000; W16 0x122 0000; W16 0x1B2 0000; W16 0x1B3 0000; W16 0x1C8 0000; W16 0x2E0 0000; W16 0x2B8 0000; W32 0x00000024 0x00000000; W32 0x00000044 0x00000000; W32 0x00000064 0x00000000; W32 0x00000084 0x00000000; W32 0x000000A4 0x00000000; W32 0x000000C4 0x00000000; W32 0x000000E4 0x00000000; W32 0x00000104 0x00000000; W32 0x42401024 0x00000000; W32 0x42401044 0x00000000; W32 0x42401064 0x00000000; W32 0x42401084 0x00000000; W32 0x424010A4 0x00000000; W32 0x424010C4 0x00000000; W32 0x424010E4 0x00000000; W32 0x42401104 0x00000000; W16 0x54 0x0000; W32 0x42400024 0x00000000; W32 0x42400044 0x00000000; W32 0x42400064 0x00000000; W32 0x42400018 0x00000018; W16 0x3A0 0000; W16 0x20 0000; W16 0x3A8 0000; W16 0x30 0x0000; W16 0x3B0 0000; W16 0x3C0 0000; W16 0x40 0003; W16 0x50 0000; W16 0x58 0001; W16 0x6C 0000; W16 0x3A0 0000; W16 0x20 0001; W16 0x30 0001; W32 0x50 0x0000; W32 0x54 0x0000; W32 0x58 0x0000; W16 0x3A8 0001; W16 0x3B0 0000; W16 0x3C0 0000; W16 0x40 0001; W16 0x6C 0000; W32 0x00000024 0x00000000; W32 0x44020000 0x00000000; W32 0x00000044 0x00000000; W32 0x44020000 0x00000000; W32 0x00000064 0x00000000; W32 0x44020000 0x00000000; W32 0x00000084 0x00000000; W32 0x44020000 0x00000000; W32 0x000000A4 0x00000000; W32 0x44020000 0x00000000; W32 0x000000C4 0x00000000; W32 0x44020000 0x00000000; W32 0x000000E4 0x00000000; W32 0x44020000 0x00000000; W32 0x00000104 0x00000000; W32 0x44020000 0x00000000; W32 0x42401024 0x00000000; W32 0x42401044 0x00000000; W32 0x42401064 0x00000000; W32 0x42401084 0x00000000; W32 0x424010A4 0x00000000; W32 0x424010C4 0x00000000; W32 0x424010E4 0x00000000; W32 0x42401104 0x00000000; W16 0xFEE00000 0x0000; W16 0xFED00000 0x0000; W16 0xFED08000 0x0000; W16 0x0070 0x0000; W16 0x0040 0x0000; W32 0x50000200 0x00000008; W32 0x50000204 0x00000008; W32 0x50000208 0x00000008; W32 0x5000020C 0x00000010; W32 0x50000300 0x00000001; W32 0x50000304 0x000000FF; W32 0x50000600 0x00000001; W32 0x50000604 0x00000001; W32 0x50000400 0x00000000; W32 0x50000404 0x00000000; W32 0x50000800 0x00000000; W32 0x50000804 0x00000000; W32 0x50000300 0x00000FFF; W32 0x50000200 0x0000000A; W32 0x50000204 0x00000004; W32 0x50000208 0x00000006; W32 0x5000020C 0x00000014; W32 0x50000700 0x00000001; W32 0x50000704 0x00000000; W32 0x50000520 0x00000003; W32 0x50000524 0x00000002; W32 0x50000540 0x00000001; W32 0x50000C00 0x00000001; W32 0x50000E00 0x00000000; W32 0x50000E20 0x00000010; W16 0xFED1F600 0x0000; W16 0xFED1F604 0x0000; W16 0xFED1F608 0x0000; W16 0xFED1F60C 0x0000; W16 0xFED1F610 0x0000; W16 0xFED1F614 0x0000; W16 0xFED1F618 0x0000; W16 0xFED1F61C 0x0000; W16 0xFED1F620 0x0000; W16 0xFED1F624 0x0000; W16 0xFED1F628 0x0000; W16 0xFED1F62C 0x0000; W16 0xFED1F630 0x0000; W16 0xFED1F634 0x0000; W16 0xFED1F638 0x0000; W16 0xFED1F63C 0x0000; W16 0xFED1F640 0x0000; W16 0xFED1F644 0x0000; W16 0xFED1F648 0x0000; W16 0xFED1F64C 0x0000; W16 0xFED1F650 0x0000; W16 0xFED1F654 0x0000; W16 0xFED1F658 0x0000; W16 0xFED1F65C 0x0000; W16 0xFE300000 0x0000; W16 0xFE300010 0x0000; W16 0xFE000004 0x0000; W16 0x1800 0x0000; W16 0xFE200000 0x0000; W16 0xFEA00050 0x0000; W16 0xFE300000 0x0000; W16 0xFEA00400 0x0000; W16 0xFE010010 0x0000; W16 0xFE012000 0x0000; W16 0xFE204000 0x0000; W16 0xFEA00500 0xFFFF; W16 0xFEA00504 0xFFFF; W16 0xFE000100 0x0000; W16 0x1814 0x0000; W16 0xFE004000 0x0000; W16 0xB2 0x0000; W16 0xFE302000 0x0000; W16 0xFE203000 0x0000; W16 0xFEA00010 0x0000; W16 0xFEA00100 0x0000; W16 0xFEA00200 0x0000; W16 0xFEA00204 0x0000; W16 0xFEA00300 0x0000; W16 0xFEA00304 0x0000; W16 0xFEA01000 0x0000; W32 0x60000024 0x00000000; W32 0x60000044 0x00000000; W32 0x60000064 0x00000000; W32 0x60000084 0x00000000; W32 0x60000200 0x00000000; W32 0x60000400 0x00000000; W32 0x60000600 0x00000000; W32 0x50000024 0x00000000; W32 0x50000044 0x00000000; W32 0x50000064 0x00000000; W32 0x50000084 0x00000000; W32 0x50000200 0x00000000; W32 0x50000400 0x00000000; W32 0x50000600 0x00000000; W32 0x70000024 0x00000000; W32 0x70000044 0x00000000; W32 0x70000064 0x00000000; W32 0x70000084 0x00000000; W32 0x70000200 0x00000000; W32 0x70000400 0x00000000; W32 0x70000600 0x00000000; W32 0x30000024 0x00000000; W32 0x30000044 0x00000000; W32 0x30000064 0x00000000; W32 0x30000084 0x00000000; W32 0x30000200 0x00000000; W32 0x30000400 0x00000000; W32 0x30000600 0x00000000; W32 0x50000024 0x00000000; W32 0x50000044 0x00000000; W32 0x50000064 0x00000000; W32 0x50000200 0x00000000; W32 0x50000220 0x00000000; W32 0x50000400 0x00000000; W32 0x50000420 0x00000000; W32 0x60000400 0x00000000; W32 0x60000404 0x00000000; W32 0x60000600 0x00000000; W16 0xFEE00000 0x0000; W16 0xFED00000 0x0000; W16 0xFE000000 0x0000; W16 0xF0000000 0x0000; W16 0xFE100000 0x0000; W16 0xFE200000 0x0000; W16 0xFEA00000 0x0000; W16 0xFE300000 0x0000; W16 0xFEC00000 0x0000; W16 0xB2 0x0000; W16 0x1800 0x0000; W16 0xFED08000 0x0000; W16 0xFED90000 0x0000; W16 0xFED1F404 0x0000; W16 0xFED1F408 0x0000; W16 0xFED1F410 0x0000; W16 0xFED1F414 0x0000; W16 0xFED1F418 0x0000; W16 0xFED1F420 0x0000; W16 0xFED1F424 0x0000; W16 0xFED1F428 0x0000; W16 0xFED1F42C 0x0000; W16 0xFED1F430 0x0000; W16 0xFED1F434 0x0000; W16 0xFED1F438 0x0000; W16 0xFED1F43C 0x0000; W16 0xFED1F440 0x0000; W16 0xFED1F444 0x0000; W16 0xFED1F448 0x0000; W16 0xFED1F44C 0x0000; W16 0xFED1F450 0x0000; W16 0xFED1F454 0x0000; W16 0xFED1F458 0x0000; W16 0xFED1F460 0x0000; W16 0xFED1F464 0x0000; W16 0xFED1F468 0x0000; W16 0xFED1F46C 0x0000; W16 0xFED1F470 0x0000; W16 0xFED1F474 0x0000; W16 0xFED1F478 0x0000; W16 0xFED1F47C 0x0000; W16 0xFED1F480 0x0000; W16 0xFED1F484 0x0000"" >nul 2>&1
pause
";

            await Task.Run(() => TweaksFunc(tweaks));
        }

        private async void variables_Click(object sender, EventArgs e)
        {
            EnvVariables.SetValue("CONFIG_HZ", "FFFFFFFFFFFFFFFF", RegistryValueKind.String);
            EnvVariables.SetValue("HZ", "FFFFFFFFFFFFFFFF", RegistryValueKind.String);
            EnvVariables.SetValue("KERNEL_HZ", "FFFFFFFFFFFFFFFF", RegistryValueKind.String);
            EnvVariables.SetValue("CPU_MAX_PENDING_INTERRUPTS", "0", RegistryValueKind.String);
            EnvVariables.SetValue("CPU_MAX_PENDING_IO", "0", RegistryValueKind.String);
            EnvVariables.SetValue("DWM_MAX_BUFFER_AGE", "0", RegistryValueKind.String);
            EnvVariables.SetValue("DWM_MAXIMUM_BUFFER_AGE", "0", RegistryValueKind.String);
            EnvVariables.SetValue("MKL_ENABLE_INSTRUCTIONS", "AVX2", RegistryValueKind.String);
            EnvVariables.SetValue("DIRECT_GPU", "1", RegistryValueKind.String);
            EnvVariables.SetValue("DIRECT_CPU", "1", RegistryValueKind.String);
        }

        private async void kernel_Click(object sender, EventArgs e)
        {
            Kernel.SetValue("SeTokenSingletonAttributesConfig",0x00000003);
            Kernel.SetValue("obcaseinsensitive",0x00000001);
            byte[] mitigationOptions = new byte[32];
            byte[] mitigationAuditOptions = new byte[24];
            Kernel.SetValue("MitigationOptions", mitigationOptions, RegistryValueKind.Binary);
            Kernel.SetValue("MitigationAuditOptions", mitigationAuditOptions, RegistryValueKind.Binary);
            Kernel.SetValue("EAFModules", "", RegistryValueKind.String);
            Kernel.SetValue("GlobalTimerResolutionRequests",0x00000001);
            Kernel.SetValue("DisableExceptionChainValidation",0x00000000);
            Kernel.SetValue("SerializeTimerExpiration",0x00000000);
            Kernel.SetValue("DisableIFEOCaching",0x00000001);
            Kernel.SetValue("DpcWatchdogProfileOffset",0x00000000);
            Kernel.SetValue("ForceForegroundBoostDecay",0x00000000);
            Kernel.SetValue("DPCTimeout",0x00000000);
            Kernel.SetValue("DpcSoftTimeout",0x00000000);
            Kernel.SetValue("DpcCumulativeSoftTimeout",0x00000000);
            Kernel.SetValue("DpcWatchdogPeriod",0x00000000);
            Kernel.SetValue("VerifierDpcScalingFactor",0x00000001);
            Kernel.SetValue("ThreadDpcEnable",0x00000001);
            Kernel.SetValue("MinimumDpcRate",0x99999999);
            Kernel.SetValue("MaximumKernelWorkerThreads",0x00002000);
            Kernel.SetValue("DpcRequestRate",0x99999999);
            Kernel.SetValue("DpcTimeLimit",0x00000000);
            Kernel.SetValue("DpcTimeCount",0x0000000a);
            Kernel.SetValue("IdleHalt",0x00000000);
            Kernel.SetValue("ClockOwner",0x00000001);
            Kernel.SetValue("PendingTickFlags",0x00000000);
            Kernel.SetValue("MaximumDpcQueueDepth",0x000003e8);
            Kernel.SetValue("DpcWatchdogProfileCumulativeDpcThreshold",0x00000000);
            Kernel.SetValue("DpcWatchdogProfileSingleDpcThreshold",0x00000000);
            Kernel.SetValue("DpcLastCount",0x000003e8);
            Kernel.SetValue("DpcRoutineActive",0x00000001);
            Kernel.SetValue("QuantumEnd",0x00000001);
            Kernel.SetValue("InterruptLastCount",0xffffffff);
            Kernel.SetValue("InterruptRate",0x99999999);
            Kernel.SetValue("ReadyThreadCount",0x000000ff);
            Kernel.SetValue("KeSpinLockOrdering",0x00000000);
            Kernel.SetValue("PriorityState",0x00000001);
            Kernel.SetValue("DistributeTimers",0x00000001);
            Kernel.SetValue("DisableDynamicTick",0x00000001);
            Kernel.SetValue("TimerInterruptDelay",0x00000000);
            Kernel.SetValue("MinimumIncrement",0x00000001);
            Kernel.SetValue("MaximumIncrement",0x00000001);
            Kernel.SetValue("PowerOffFrozenProcessors",0x00000000);
            Kernel.SetValue("DisableLightWeightSuspend",0x00000001);
            Kernel.SetValue("DpcQueueDepth",0x00000001);
            Kernel.SetValue("AdjustDpcThreshold",0x00000001);
            Kernel.SetValue("IdealDpcRate",0x99999999);
            Kernel.SetValue("InterruptSteeringFlags",0x00000003);
            Kernel.SetValue("SchedulerMaximumLatency",0x00000000);
            Kernel.SetValue("DebugPollInterval",0x00000001);
            Kernel.SetValue("HyperStartDisabled",0x00000001);
            Kernel.SetValue("SeLpacEnableWatsonReporting",0x00000000);
            Kernel.SetValue("SeLpacEnableWatsonThrottling",0x00000000);
            Kernel.SetValue("AdminlessEnableWatsonReporting",0x00000000);
            Kernel.SetValue("AdminlessEnableWatsonThrottling",0x00000000);
            Kernel.SetValue("CyclesPerClockQuantum",0x00000001);
            Kernel.SetValue("ForceDpcDmaCoalesce",0x00000000);
            Kernel.SetValue("DisablePrefetcher",0x00000001);
            Kernel.SetValue("LockPagesInMemory",0x00000001);
            Kernel.SetValue("KdDisable",0x00000001);
            Kernel.SetValue("ThreadPriorityBoost",0x00000001);
            Kernel.SetValue("PerfBootPerformance",0x00000001);
            Kernel.SetValue("PerfBoostPolicy",0x00000001);
            Kernel.SetValue("CpuThrottle",0x00000000);
            Kernel.SetValue("SchedulerAssistThreadFlagOverride",0x00000001);
            Kernel.SetValue("DpcWatchdogProfileBufferSizeBytes",0x00000000);
            Kernel.SetValue("DpcWatchdogProfileCumulativeDpcThresholdMs",0x00000000);
            Kernel.SetValue("PassiveWatchdogTimeout",0x00000000);
            Kernel.SetValue("DisableTsx",0x00000001);
            Kernel.SetValue("MaximumSharedReadyQueueSize",0x000000ff);
            Kernel.SetValue("ClockRate",0x00000001);
            Kernel.SetValue("TimerResolution",0x00000001);
            Kernel.SetValue("TSCDeadline",0x00000001);
            Kernel.SetValue("UsePlatformClock",0x00000000);
            Kernel.SetValue("Clock Rate",0x00000001);
            Kernel.SetValue("QuantumLength",0x00000001);
            Kernel.SetValue("QuantumSize",0x00000001);
            Kernel.SetValue("InterruptRequestRate",0x99999999);
        }

        private async void DWM_Click(object sender, EventArgs e)
        {
            DWMKey.SetValue("AnimationAttributionEnabled",0x00000000);
            DWMKey.SetValue("AnimationAttributionHashingEnabled",0x00000000);
            DWMKey.SetValue("OverlayMinFPS",0x00000000);
            DWMKey.SetValue("BufferCount",0x00000001);
            DWMKey.SetValue("MaxBufferCount",0x00000001);
            DWMKey.SetValue("AnimationsShiftKey",0x00000000);
            DWMKey.SetValue("BackdropBlurCachingThrottleMs",0x00000000);
            DWMKey.SetValue("ChildWindowDpiIsolation",0x00000001);
            DWMKey.SetValue("ColorizationColor",0x00000000);
            DWMKey.SetValue("ColorPrevalence",0x00000000);
            DWMKey.SetValue("ConfigureInput",0x00000001);
            DWMKey.SetValue("DisableAdvancedDirectFlip",0x00000001);
            DWMKey.SetValue("DisableDeviceBitmaps",0x00000001);
            DWMKey.SetValue("DisableDeviceBitmapsForMultiAdapter",0x00000001);
            DWMKey.SetValue("DisableDrawListCaching",0x00000001);
            DWMKey.SetValue("DisableHologramCompositor",0x00000001);
            DWMKey.SetValue("DisableLockingMemory",0x00000001);
            DWMKey.SetValue("DisableProjectedShadows",0x00000001);
            DWMKey.SetValue("DisableProjectedShadowsRendering",0x00000001);
            DWMKey.SetValue("DisallowNonDrawListRendering",0x00000001);
            DWMKey.SetValue("CompositionPolicy",0x00000000);
            DWMKey.SetValue("Composition",0x00000000);
            DWMKey.SetValue("MaxOutstandingFrames",0x00000000);
            DWMKey.SetValue("FlipQueuePolicy",0x00000004);
            DWMKey.SetValue("IdleTimeout",0x00000000);
        }

        private async void specialsauce_Click(object sender, EventArgs e)
        {
            Power.SetValue("PdcLockWatchdog",0x00000000);
            Power.SetValue("PdcLockWatchdogTimeout",0x00000000);
            Power.SetValue("PdcLockStatsTelemetryPeriod",0x00000000);
            Power.SetValue("PdcCriticalTransitionTimeout",0x00000000);
            Power.SetValue("PdcCsEntryAction",0x00000000);
            Power.SetValue("PdcWcmTransitionTimeout",0x00000000);
            Power.SetValue("PdcCriticalActivatorTimeout",0x00000000);
            Power.SetValue("PdcActivatorClientResponseTimeout",0x00000000);
            Power.SetValue("PdcActivatorClientPolicyNotificationDebounce",0x00000000);
            Power.SetValue("PdcCriticalActivatorAction",0x00000000);
            Power.SetValue("PdcEnforceSystemIdleTimeoutOnConsoleLock",0x00000000);
            WDF01000Params.SetValue("WdfDefaultIdleInWorkingState",0x00000001);
            WDF01000Params.SetValue("WdfDirectedPowerTransitionEnable",0x00000000);
            WDF01000Params.SetValue("IdleInWorkingState",0x00000001);
            WDF01000Params.SetValue("WdfDirectedPowerTransitionChildrenOptional",0x00000000);
            GraphicsDriverScheduler.SetValue("HwIndependentFlip",0x00000002);
            GraphicsDriverScheduler.SetValue("ForegroundPriorityBoost",0x00000001);
            GraphicsDriverScheduler.SetValue("QueuedPresentLimit",0x00000001);
            GraphicsDriverScheduler.SetValue("EnablePreemption",0x00000001);
            GraphicsDriverScheduler.SetValue("FlipOverrideMode",0x00000003);
            GraphicsDriver.SetValue("PlatformSupportMiracast",0x00000001);
            GraphicsDriver.SetValue("DxgKrnlVersion",0x00010004);
            GraphicsDriver.SetValue("MinDxgKrnlVersion",0x00005013);
            GraphicsDriver.SetValue("PMMEnable",0x00000000);
            GraphicsDriver.SetValue("EnablePerformanceMode",0x00000001);
            GraphicsDriver.SetValue("Capabilities",0x00000005);
            GraphicsDriver.SetValue("HwFlipPolicy",0x00000000);
            GraphicsDriver.SetValue("HwLegacyFlipPolicy",0x00000000);
            GraphicsDriver.SetValue("TdrLevel",0x00000000);
            GraphicsDriver.SetValue("TdrDelay",0x00000005);
            GraphicsDriver.SetValue("TdrDdiDelay",0x00000005);
            GraphicsDriver.SetValue("MaxFrameLatency",0x00000001);
            GraphicsDriver.SetValue("HwSchMode",0x00000002);
            GraphicsDriver.SetValue("FlipOverrideMode",0x00000003);
            GraphicsDriver.SetValue("FlipQueuePolicy",0x00000004);
            GraphicsDriver.SetValue("FlipModel",0x00000001);
            GraphicsDriver.SetValue("EnableFlipDiscard",0x00000001);
            GraphicsDriver.SetValue("DisableDwmVSync",0x00000001);
            GraphicsDriver.SetValue("DWM_BUFFER_COUNT",0x00000001);
            GraphicsDriver.SetValue("DwmFlipPolicy",0x00000000);
            GraphicsDriver.SetValue("DwmQueuePolicy",0x00000000);
            GraphicsDriver.SetValue("EnableOfferReclaimOnDriver",0x00000001);
            GraphicsDriver.SetValue("ContextNoPatchMode",0x00000001);
        }
    }
}
