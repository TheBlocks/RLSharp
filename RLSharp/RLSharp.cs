/*Copyright 2017 Tangil Jahangir

Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at
http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.*/

using System;
using System.Diagnostics;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.Runtime.InteropServices;
using vJoyInterfaceWrap;

namespace RLSharp
{
    /// <summary>
    /// Class to derive from to create a bot agent.
    /// </summary>
    public class RLSharp
    {
        public SharedInputs inputs;
        private vJoy joystick;
        public Controller controller;
        private uint controllerID;
        private vJoy.JoystickState controllerState = new vJoy.JoystickState();

        /// <summary>
        /// Class used to derive from to create a bot agent.
        /// </summary>
        public RLSharp(uint id)
        {
            // Check if Rocket League is running, and if the DLL has been loaded.
            bool gameRunning = false;
            bool dllLoaded = false;

            Process[] allProcesses = Process.GetProcesses();
            foreach (Process process in allProcesses)
            {
                if (process.ProcessName == "RocketLeague")
                {
                    gameRunning = true;
                    foreach (ProcessModule module in process.Modules)
                    {
                        if (module.ToString().Contains("RLBot"))
                        {
                            dllLoaded = true;
                            break;
                        }
                    }
                    break;
                }
            }
            if (!gameRunning)
                throw new Exception("RocketLeague process could not be found.");
            
            if (!dllLoaded)
                throw new DllNotFoundException("RLBot.dll could not be found as a loaded module in a RocketLeague process.");
            

            // vJoy checks
            joystick = new vJoy();
            controllerID = id;
            if (!joystick.vJoyEnabled())
            {
                Console.WriteLine("WARNING: vJoy controller not enabled!");
            }
        }

        /// <summary>
        /// This method should contain all the logic for the bot agent. </summary>
        /// <remarks>
        /// Code inside this method runs every iteration of the update loop.
        /// This method should be overridden to implement bot logic. </remarks>
        virtual protected void Update() {}

        /// <summary>
        /// Updates the bot once. </summary>
        /// <remarks>
        /// <see cref="inputs"/> is updated once, then <see cref="Update"/> is called.
        /// Finally <see cref="controller"/> is fed to the game. </remarks>
        public void UpdateBot()
        {
            // Create objects to read from DLL through a memory mapped file
            MemoryMappedFile mmf = MemoryMappedFile.OpenExisting("Local\\RLBot");
            MemoryMappedViewStream stream = mmf.CreateViewStream(0, 2004);

            ushort dllLock = 1;
            stream.Position = 0;
            BinaryReader binReader = new BinaryReader(stream);
            dllLock = BitConverter.ToUInt16(binReader.ReadBytes(4), 0);

            // Read from DLL if a refresh is not in progress
            if (dllLock != 1)
            {
                stream.Position = 4;
                GCHandle handle = GCHandle.Alloc(binReader.ReadBytes(2000), GCHandleType.Pinned);
                inputs = (SharedInputs)Marshal.PtrToStructure(handle.AddrOfPinnedObject(), typeof(SharedInputs));
                handle.Free();
            }

            // Run bot logic once
            Update();

            // Update outputs fed to game from controller struct
            joystick.AcquireVJD(controllerID);
            controllerState.AxisX = controller.stickX;
            controllerState.AxisY = controller.stickY;
            controllerState.AxisZRot = controller.acceleration;
            controllerState.AxisZ = controller.deceleration;
            controllerState.Buttons = (uint)((controller.jump ? 1 : 0) + (controller.boost ? 1 : 0) * 2
                                             + (controller.powerslide ? 1 : 0) * 4);
            joystick.UpdateVJD(controllerID, ref controllerState);
        }

        /// <summary>
        /// Call this method to start the bot's update loop. </summary>
        /// <remarks>
        /// The update loop runs in a while (true) and will never stop.
        /// The loop pauses for 15 milliseconds so that it can run at around 60Hz.
        /// UpdateBot() is called every iteration, and Update() is called through UpdateBot(). </remarks>
        public void Run()
        {
            while (true)
            {
                UpdateBot();
                System.Threading.Thread.Sleep(15);
            }
        }
    }
}
