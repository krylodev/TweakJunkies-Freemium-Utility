using Guna.UI2.WinForms;
using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.ComponentModel;
using System.Reflection;
using System.Collections.Generic;
using Microsoft.Win32;
using System.IO.Compression;

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
            string tweaks = @"
(
reg add ""HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\Session Manager\Environment"" /v ""CONFIG_HZ"" /t REG_SZ /d ""FFFFFFFFFFFFFFFF"" /f >nul 2>&1
reg add ""HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\Session Manager\Environment"" /v ""HZ"" /t REG_SZ /d ""FFFFFFFFFFFFFFFF"" /f >nul 2>&1
reg add ""HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\Session Manager\Environment"" /v ""KERNEL_HZ"" /t REG_SZ /d ""FFFFFFFFFFFFFFFF"" /f >nul 2>&1
reg add ""HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\Session Manager\Environment"" /v ""CPU_MAX_PENDING_INTERRUPTS"" /t REG_SZ /d ""0"" /f >nul 2>&1
reg add ""HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\Session Manager\Environment"" /v ""CPU_MAX_PENDING_IO"" /t REG_SZ /d ""0"" /f >nul 2>&1
reg add ""HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\Session Manager\Environment"" /v ""DWM_MAX_BUFFER_AGE"" /t REG_SZ /d ""0"" /f >nul 2>&1
reg add ""HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\Session Manager\Environment"" /v ""DWM_MAXIMUM_BUFFER_AGE"" /t REG_SZ /d ""0"" /f >nul 2>&1
reg add ""HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\Session Manager\Environment"" /v ""MKL_ENABLE_INSTRUCTIONS"" /t REG_SZ /d ""AVX2"" /f >nul 2>&1
reg add ""HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\Session Manager\Environment"" /v ""DIRECT_GPU"" /t REG_SZ /d ""1"" /f >nul 2>&1
reg add ""HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\Session Manager\Environment"" /v ""DIRECT_CPU"" /t REG_SZ /d ""1"" /f >nul 2>&1
) >nul 2>&1
exit
";

            await Task.Run(() => TweaksFunc(tweaks));
        }

        private async void kernel_Click(object sender, EventArgs e)
        {
            string tweaks = @"
(
reg add ""HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\Session Manager\kernel"" /v ""SeTokenSingletonAttributesConfig"" /t REG_DWORD /d 0x00000003 /f >nul 2>&1
reg add ""HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\Session Manager\kernel"" /v ""obcaseinsensitive"" /t REG_DWORD /d 0x00000001 /f >nul 2>&1
reg add ""HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\Session Manager\kernel"" /v ""MitigationOptions"" /t REG_BINARY /d 00000000000000000000000000000000000000000000000000000000000000000000000000000000 /f >nul 2>&1
reg add ""HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\Session Manager\kernel"" /v ""MitigationAuditOptions"" /t REG_BINARY /d 000000000000000000000000000000000000000000000000 /f >nul 2>&1
reg add ""HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\Session Manager\kernel"" /v ""EAFModules"" /t REG_SZ /d """" /f >nul 2>&1
reg add ""HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\Session Manager\kernel"" /v ""GlobalTimerResolutionRequests"" /t REG_DWORD /d 0x00000001 /f >nul 2>&1
reg add ""HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\Session Manager\kernel"" /v ""DisableExceptionChainValidation"" /t REG_DWORD /d 0x00000000 /f >nul 2>&1
reg add ""HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\Session Manager\kernel"" /v ""SerializeTimerExpiration"" /t REG_DWORD /d 0x00000000 /f >nul 2>&1
reg add ""HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\Session Manager\kernel"" /v ""DisableIFEOCaching"" /t REG_DWORD /d 0x00000001 /f >nul 2>&1
reg add ""HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\Session Manager\kernel"" /v ""DpcWatchdogProfileOffset"" /t REG_DWORD /d 0x00000000 /f >nul 2>&1
reg add ""HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\Session Manager\kernel"" /v ""ForceForegroundBoostDecay"" /t REG_DWORD /d 0x00000000 /f >nul 2>&1
reg add ""HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\Session Manager\kernel"" /v ""DPCTimeout"" /t REG_DWORD /d 0x00000000 /f >nul 2>&1
reg add ""HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\Session Manager\kernel"" /v ""DpcSoftTimeout"" /t REG_DWORD /d 0x00000000 /f >nul 2>&1
reg add ""HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\Session Manager\kernel"" /v ""DpcCumulativeSoftTimeout"" /t REG_DWORD /d 0x00000000 /f >nul 2>&1
reg add ""HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\Session Manager\kernel"" /v ""DpcWatchdogPeriod"" /t REG_DWORD /d 0x00000000 /f >nul 2>&1
reg add ""HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\Session Manager\kernel"" /v ""VerifierDpcScalingFactor"" /t REG_DWORD /d 0x00000001 /f >nul 2>&1
reg add ""HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\Session Manager\kernel"" /v ""ThreadDpcEnable"" /t REG_DWORD /d 0x00000001 /f >nul 2>&1
reg add ""HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\Session Manager\kernel"" /v ""MinimumDpcRate"" /t REG_DWORD /d 0x99999999 /f >nul 2>&1
reg add ""HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\Session Manager\kernel"" /v ""MaximumKernelWorkerThreads"" /t REG_DWORD /d 0x00002000 /f >nul 2>&1
reg add ""HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\Session Manager\kernel"" /v ""DpcRequestRate"" /t REG_DWORD /d 0x99999999 /f >nul 2>&1
reg add ""HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\Session Manager\kernel"" /v ""DpcTimeLimit"" /t REG_DWORD /d 0x00000000 /f >nul 2>&1
reg add ""HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\Session Manager\kernel"" /v ""DpcTimeCount"" /t REG_DWORD /d 0x0000000a /f >nul 2>&1
reg add ""HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\Session Manager\kernel"" /v ""IdleHalt"" /t REG_DWORD /d 0x00000000 /f >nul 2>&1
reg add ""HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\Session Manager\kernel"" /v ""ClockOwner"" /t REG_DWORD /d 0x00000001 /f >nul 2>&1
reg add ""HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\Session Manager\kernel"" /v ""PendingTickFlags"" /t REG_DWORD /d 0x00000000 /f >nul 2>&1
reg add ""HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\Session Manager\kernel"" /v ""MaximumDpcQueueDepth"" /t REG_DWORD /d 0x000003e8 /f >nul 2>&1
reg add ""HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\Session Manager\kernel"" /v ""DpcWatchdogProfileCumulativeDpcThreshold"" /t REG_DWORD /d 0x00000000 /f >nul 2>&1
reg add ""HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\Session Manager\kernel"" /v ""DpcWatchdogProfileSingleDpcThreshold"" /t REG_DWORD /d 0x00000000 /f >nul 2>&1
reg add ""HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\Session Manager\kernel"" /v ""DpcLastCount"" /t REG_DWORD /d 0x000003e8 /f >nul 2>&1
reg add ""HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\Session Manager\kernel"" /v ""DpcRoutineActive"" /t REG_DWORD /d 0x00000001 /f >nul 2>&1
reg add ""HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\Session Manager\kernel"" /v ""QuantumEnd"" /t REG_DWORD /d 0x00000001 /f >nul 2>&1
reg add ""HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\Session Manager\kernel"" /v ""InterruptLastCount"" /t REG_DWORD /d 0xffffffff /f >nul 2>&1
reg add ""HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\Session Manager\kernel"" /v ""InterruptRate"" /t REG_DWORD /d 0x99999999 /f >nul 2>&1
reg add ""HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\Session Manager\kernel"" /v ""ReadyThreadCount"" /t REG_DWORD /d 0x000000ff /f >nul 2>&1
reg add ""HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\Session Manager\kernel"" /v ""KeSpinLockOrdering"" /t REG_DWORD /d 0x00000000 /f >nul 2>&1
reg add ""HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\Session Manager\kernel"" /v ""PriorityState"" /t REG_DWORD /d 0x00000001 /f >nul 2>&1
reg add ""HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\Session Manager\kernel"" /v ""DistributeTimers"" /t REG_DWORD /d 0x00000001 /f >nul 2>&1
reg add ""HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\Session Manager\kernel"" /v ""DisableDynamicTick"" /t REG_DWORD /d 0x00000001 /f >nul 2>&1
reg add ""HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\Session Manager\kernel"" /v ""TimerInterruptDelay"" /t REG_DWORD /d 0x00000000 /f >nul 2>&1
reg add ""HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\Session Manager\kernel"" /v ""MinimumIncrement"" /t REG_DWORD /d 0x00000001 /f >nul 2>&1
reg add ""HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\Session Manager\kernel"" /v ""MaximumIncrement"" /t REG_DWORD /d 0x00000001 /f >nul 2>&1
reg add ""HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\Session Manager\kernel"" /v ""PowerOffFrozenProcessors"" /t REG_DWORD /d 0x00000000 /f >nul 2>&1
reg add ""HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\Session Manager\kernel"" /v ""DisableLightWeightSuspend"" /t REG_DWORD /d 0x00000001 /f >nul 2>&1
reg add ""HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\Session Manager\kernel"" /v ""DpcQueueDepth"" /t REG_DWORD /d 0x00000001 /f >nul 2>&1
reg add ""HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\Session Manager\kernel"" /v ""AdjustDpcThreshold"" /t REG_DWORD /d 0x00000001 /f >nul 2>&1
reg add ""HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\Session Manager\kernel"" /v ""IdealDpcRate"" /t REG_DWORD /d 0x99999999 /f >nul 2>&1
reg add ""HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\Session Manager\kernel"" /v ""InterruptSteeringFlags"" /t REG_DWORD /d 0x00000003 /f >nul 2>&1
reg add ""HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\Session Manager\kernel"" /v ""SchedulerMaximumLatency"" /t REG_DWORD /d 0x00000000 /f >nul 2>&1
reg add ""HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\Session Manager\kernel"" /v ""DebugPollInterval"" /t REG_DWORD /d 0x00000001 /f >nul 2>&1
reg add ""HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\Session Manager\kernel"" /v ""HyperStartDisabled"" /t REG_DWORD /d 0x00000001 /f >nul 2>&1
reg add ""HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\Session Manager\kernel"" /v ""SeLpacEnableWatsonReporting"" /t REG_DWORD /d 0x00000000 /f >nul 2>&1
reg add ""HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\Session Manager\kernel"" /v ""SeLpacEnableWatsonThrottling"" /t REG_DWORD /d 0x00000000 /f >nul 2>&1
reg add ""HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\Session Manager\kernel"" /v ""AdminlessEnableWatsonReporting"" /t REG_DWORD /d 0x00000000 /f >nul 2>&1
reg add ""HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\Session Manager\kernel"" /v ""AdminlessEnableWatsonThrottling"" /t REG_DWORD /d 0x00000000 /f >nul 2>&1
reg add ""HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\Session Manager\kernel"" /v ""CyclesPerClockQuantum"" /t REG_DWORD /d 0x00000001 /f >nul 2>&1
reg add ""HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\Session Manager\kernel"" /v ""ForceDpcDmaCoalesce"" /t REG_DWORD /d 0x00000000 /f >nul 2>&1
reg add ""HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\Session Manager\kernel"" /v ""DisablePrefetcher"" /t REG_DWORD /d 0x00000001 /f >nul 2>&1
reg add ""HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\Session Manager\kernel"" /v ""LockPagesInMemory"" /t REG_DWORD /d 0x00000001 /f >nul 2>&1
reg add ""HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\Session Manager\kernel"" /v ""KdDisable"" /t REG_DWORD /d 0x00000001 /f >nul 2>&1
reg add ""HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\Session Manager\kernel"" /v ""ThreadPriorityBoost"" /t REG_DWORD /d 0x00000001 /f >nul 2>&1
reg add ""HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\Session Manager\kernel"" /v ""PerfBootPerformance"" /t REG_DWORD /d 0x00000001 /f >nul 2>&1
reg add ""HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\Session Manager\kernel"" /v ""PerfBoostPolicy"" /t REG_DWORD /d 0x00000001 /f >nul 2>&1
reg add ""HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\Session Manager\kernel"" /v ""CpuThrottle"" /t REG_DWORD /d 0x00000000 /f >nul 2>&1
reg add ""HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\Session Manager\kernel"" /v ""SchedulerAssistThreadFlagOverride"" /t REG_DWORD /d 0x00000001 /f >nul 2>&1
reg add ""HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\Session Manager\kernel"" /v ""DpcWatchdogProfileBufferSizeBytes"" /t REG_DWORD /d 0x00000000 /f >nul 2>&1
reg add ""HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\Session Manager\kernel"" /v ""DpcWatchdogProfileCumulativeDpcThresholdMs"" /t REG_DWORD /d 0x00000000 /f >nul 2>&1
reg add ""HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\Session Manager\kernel"" /v ""PassiveWatchdogTimeout"" /t REG_DWORD /d 0x00000000 /f >nul 2>&1
reg add ""HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\Session Manager\kernel"" /v ""DisableTsx"" /t REG_DWORD /d 0x00000001 /f >nul 2>&1
reg add ""HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\Session Manager\kernel"" /v ""MaximumSharedReadyQueueSize"" /t REG_DWORD /d 0x000000ff /f >nul 2>&1
reg add ""HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\Session Manager\kernel"" /v ""ClockRate"" /t REG_DWORD /d 0x00000001 /f >nul 2>&1
reg add ""HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\Session Manager\kernel"" /v ""TimerResolution"" /t REG_DWORD /d 0x00000001 /f >nul 2>&1
reg add ""HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\Session Manager\kernel"" /v ""TSCDeadline"" /t REG_DWORD /d 0x00000001 /f >nul 2>&1
reg add ""HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\Session Manager\kernel"" /v ""UsePlatformClock"" /t REG_DWORD /d 0x00000000 /f >nul 2>&1
reg add ""HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\Session Manager\kernel"" /v ""Clock Rate"" /t REG_DWORD /d 0x00000001 /f >nul 2>&1
reg add ""HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\Session Manager\kernel"" /v ""QuantumLength"" /t REG_DWORD /d 0x00000001 /f >nul 2>&1
reg add ""HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\Session Manager\kernel"" /v ""QuantumSize"" /t REG_DWORD /d 0x00000001 /f >nul 2>&1
reg add ""HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\Session Manager\kernel"" /v ""InterruptRequestRate"" /t REG_DWORD /d 0x99999999 /f >nul 2>&1
) >nul 2>&1

exit
";

            await Task.Run(() => TweaksFunc(tweaks));
        }

        private async void DWM_Click(object sender, EventArgs e)
        {
            string tweaks = @"
(
reg add ""HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\Dwm"" /v ""AnimationAttributionEnabled"" /t REG_DWORD /d 0x00000000 /f >nul 2>&1
reg add ""HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\Dwm"" /v ""AnimationAttributionHashingEnabled"" /t REG_DWORD /d 0x00000000 /f >nul 2>&1
reg add ""HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\Dwm"" /v ""OverlayMinFPS"" /t REG_DWORD /d 0x00000000 /f >nul 2>&1
reg add ""HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\Dwm"" /v ""BufferCount"" /t REG_DWORD /d 0x00000001 /f >nul 2>&1
reg add ""HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\Dwm"" /v ""MaxBufferCount"" /t REG_DWORD /d 0x00000001 /f >nul 2>&1
reg add ""HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\Dwm"" /v ""AnimationsShiftKey"" /t REG_DWORD /d 0x00000000 /f >nul 2>&1
reg add ""HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\Dwm"" /v ""BackdropBlurCachingThrottleMs"" /t REG_DWORD /d 0x00000000 /f >nul 2>&1
reg add ""HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\Dwm"" /v ""ChildWindowDpiIsolation"" /t REG_DWORD /d 0x00000001 /f >nul 2>&1
reg add ""HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\Dwm"" /v ""ColorizationColor"" /t REG_DWORD /d 0x00000000 /f >nul 2>&1
reg add ""HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\Dwm"" /v ""ColorPrevalence"" /t REG_DWORD /d 0x00000000 /f >nul 2>&1
reg add ""HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\Dwm"" /v ""ConfigureInput"" /t REG_DWORD /d 0x00000001 /f >nul 2>&1
reg add ""HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\Dwm"" /v ""DisableAdvancedDirectFlip"" /t REG_DWORD /d 0x00000001 /f >nul 2>&1
reg add ""HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\Dwm"" /v ""DisableDeviceBitmaps"" /t REG_DWORD /d 0x00000001 /f >nul 2>&1
reg add ""HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\Dwm"" /v ""DisableDeviceBitmapsForMultiAdapter"" /t REG_DWORD /d 0x00000001 /f >nul 2>&1
reg add ""HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\Dwm"" /v ""DisableDrawListCaching"" /t REG_DWORD /d 0x00000001 /f >nul 2>&1
reg add ""HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\Dwm"" /v ""DisableHologramCompositor"" /t REG_DWORD /d 0x00000001 /f >nul 2>&1
reg add ""HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\Dwm"" /v ""DisableLockingMemory"" /t REG_DWORD /d 0x00000001 /f >nul 2>&1
reg add ""HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\Dwm"" /v ""DisableProjectedShadows"" /t REG_DWORD /d 0x00000001 /f >nul 2>&1
reg add ""HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\Dwm"" /v ""DisableProjectedShadowsRendering"" /t REG_DWORD /d 0x00000001 /f >nul 2>&1
reg add ""HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\Dwm"" /v ""DisallowNonDrawListRendering"" /t REG_DWORD /d 0x00000001 /f >nul 2>&1
reg add ""HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\Dwm"" /v ""CompositionPolicy"" /t REG_DWORD /d 0x00000000 /f >nul 2>&1
reg add ""HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\Dwm"" /v ""Composition"" /t REG_DWORD /d 0x00000000 /f >nul 2>&1
reg add ""HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\Dwm"" /v ""MaxOutstandingFrames"" /t REG_DWORD /d 0x00000000 /f >nul 2>&1
reg add ""HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\Dwm"" /v ""FlipQueuePolicy"" /t REG_DWORD /d 0x00000004 /f >nul 2>&1
reg add ""HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\Dwm"" /v ""IdleTimeout"" /t REG_DWORD /d 0x00000000 /f >nul 2>&1
) >nul 2>&1
exit
";

            await Task.Run(() => TweaksFunc(tweaks));
        }

        private async void specialsauce_Click(object sender, EventArgs e)
        {
            string tweaks = @"
(
reg add ""HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\Power"" /v ""PdcLockWatchdog"" /t REG_DWORD /d 0x00000000 /f >nul 2>&1
reg add ""HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\Power"" /v ""PdcLockWatchdogTimeout"" /t REG_DWORD /d 0x00000000 /f >nul 2>&1
reg add ""HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\Power"" /v ""PdcLockStatsTelemetryPeriod"" /t REG_DWORD /d 0x00000000 /f >nul 2>&1
reg add ""HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\Power"" /v ""PdcCriticalTransitionTimeout"" /t REG_DWORD /d 0x00000000 /f >nul 2>&1
reg add ""HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\Power"" /v ""PdcCsEntryAction"" /t REG_DWORD /d 0x00000000 /f >nul 2>&1
reg add ""HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\Power"" /v ""PdcWcmTransitionTimeout"" /t REG_DWORD /d 0x00000000 /f >nul 2>&1
reg add ""HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\Power"" /v ""PdcCriticalActivatorTimeout"" /t REG_DWORD /d 0x00000000 /f >nul 2>&1
reg add ""HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\Power"" /v ""PdcActivatorClientResponseTimeout"" /t REG_DWORD /d 0x00000000 /f >nul 2>&1
reg add ""HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\Power"" /v ""PdcActivatorClientPolicyNotificationDebounce"" /t REG_DWORD /d 0x00000000 /f >nul 2>&1
reg add ""HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\Power"" /v ""PdcCriticalActivatorAction"" /t REG_DWORD /d 0x00000000 /f >nul 2>&1
reg add ""HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\Power"" /v ""PdcEnforceSystemIdleTimeoutOnConsoleLock"" /t REG_DWORD /d 0x00000000 /f >nul 2>&1

reg add ""HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\Wdf01000\Parameters"" /v ""WdfDefaultIdleInWorkingState"" /t REG_DWORD /d 0x00000001 /f >nul 2>&1
reg add ""HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\Wdf01000\Parameters"" /v ""WdfDirectedPowerTransitionEnable"" /t REG_DWORD /d 0x00000000 /f >nul 2>&1
reg add ""HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\Wdf01000\Parameters"" /v ""IdleInWorkingState"" /t REG_DWORD /d 0x00000001 /f >nul 2>&1
reg add ""HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\Wdf01000\Parameters"" /v ""WdfDirectedPowerTransitionChildrenOptional"" /t REG_DWORD /d 0x00000000 /f >nul 2>&1

reg add ""HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\GraphicsDrivers\Scheduler"" /v ""HwIndependentFlip"" /t REG_DWORD /d 0x00000002 /f >nul 2>&1
reg add ""HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\GraphicsDrivers\Scheduler"" /v ""ForegroundPriorityBoost"" /t REG_DWORD /d 0x00000001 /f >nul 2>&1
reg add ""HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\GraphicsDrivers\Scheduler"" /v ""QueuedPresentLimit"" /t REG_DWORD /d 0x00000001 /f >nul 2>&1
reg add ""HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\GraphicsDrivers\Scheduler"" /v ""EnablePreemption"" /t REG_DWORD /d 0x00000001 /f >nul 2>&1
reg add ""HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\GraphicsDrivers\Scheduler"" /v ""FlipOverrideMode"" /t REG_DWORD /d 0x00000003 /f >nul 2>&1

reg add ""HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\GraphicsDrivers"" /v ""PlatformSupportMiracast"" /t REG_DWORD /d 0x00000001 /f >nul 2>&1
reg add ""HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\GraphicsDrivers"" /v ""DxgKrnlVersion"" /t REG_DWORD /d 0x00010004 /f >nul 2>&1
reg add ""HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\GraphicsDrivers"" /v ""MinDxgKrnlVersion"" /t REG_DWORD /d 0x00005013 /f >nul 2>&1
reg add ""HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\GraphicsDrivers"" /v ""PMMEnable"" /t REG_DWORD /d 0x00000000 /f >nul 2>&1
reg add ""HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\GraphicsDrivers"" /v ""EnablePerformanceMode"" /t REG_DWORD /d 0x00000001 /f >nul 2>&1
reg add ""HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\GraphicsDrivers"" /v ""Capabilities"" /t REG_DWORD /d 0x00000005 /f >nul 2>&1
reg add ""HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\GraphicsDrivers"" /v ""HwFlipPolicy"" /t REG_DWORD /d 0x00000000 /f >nul 2>&1
reg add ""HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\GraphicsDrivers"" /v ""HwLegacyFlipPolicy"" /t REG_DWORD /d 0x00000000 /f >nul 2>&1
reg add ""HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\GraphicsDrivers"" /v ""TdrLevel"" /t REG_DWORD /d 0x00000000 /f >nul 2>&1
reg add ""HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\GraphicsDrivers"" /v ""TdrDelay"" /t REG_DWORD /d 0x00000005 /f >nul 2>&1
reg add ""HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\GraphicsDrivers"" /v ""TdrDdiDelay"" /t REG_DWORD /d 0x00000005 /f >nul 2>&1
reg add ""HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\GraphicsDrivers"" /v ""MaxFrameLatency"" /t REG_DWORD /d 0x00000001 /f >nul 2>&1
reg add ""HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\GraphicsDrivers"" /v ""HwSchMode"" /t REG_DWORD /d 0x00000002 /f >nul 2>&1
reg add ""HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\GraphicsDrivers"" /v ""FlipOverrideMode"" /t REG_DWORD /d 0x00000003 /f >nul 2>&1
reg add ""HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\GraphicsDrivers"" /v ""FlipQueuePolicy"" /t REG_DWORD /d 0x00000004 /f >nul 2>&1
reg add ""HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\GraphicsDrivers"" /v ""FlipModel"" /t REG_DWORD /d 0x00000001 /f >nul 2>&1
reg add ""HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\GraphicsDrivers"" /v ""EnableFlipDiscard"" /t REG_DWORD /d 0x00000001 /f >nul 2>&1
reg add ""HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\GraphicsDrivers"" /v ""DisableDwmVSync"" /t REG_DWORD /d 0x00000001 /f >nul 2>&1
reg add ""HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\GraphicsDrivers"" /v ""DWM_BUFFER_COUNT"" /t REG_DWORD /d 0x00000001 /f >nul 2>&1
reg add ""HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\GraphicsDrivers"" /v ""DwmFlipPolicy"" /t REG_DWORD /d 0x00000000 /f >nul 2>&1
reg add ""HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\GraphicsDrivers"" /v ""DwmQueuePolicy"" /t REG_DWORD /d 0x00000000 /f >nul 2>&1
reg add ""HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\GraphicsDrivers"" /v ""EnableOfferReclaimOnDriver"" /t REG_DWORD /d 0x00000001 /f >nul 2>&1
reg add ""HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\GraphicsDrivers"" /v ""ContextNoPatchMode"" /t REG_DWORD /d 0x00000001 /f >nul 2>&1
) >nul 2>&1

exit
";

            await Task.Run(() => TweaksFunc(tweaks));
        }
    }
}
