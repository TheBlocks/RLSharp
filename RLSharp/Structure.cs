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

using System.Runtime.InteropServices;

namespace RLSharp
{
    /// <summary>
    /// Struct that holds all the output information to be fed to the game.
    /// </summary>
    public struct Controller
    {
        public int stickX;
        public int stickY;
        public bool jump;
        public bool powerslide;
        public bool boost;
        public int acceleration;
        public int deceleration;
    }

    // Structs used for the DLL

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct Vector3
    {
        public float X;
        public float Y;
        public float Z;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct Rotator
    {
        public int Pitch;
        public int Yaw;
        public int Roll;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct ScoreInfo
    {
        public int Score;
        public int Goals;
        public int OwnGoals;
        public int Assists;
        public int Saves;
        public int Shots;
        public int Demolitions;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct PlayerInfo
    {
        public Vector3 Location;
        public Rotator Rotation;
        public Vector3 Velocity;
        public Vector3 AngularVelocity;
        public ScoreInfo Score;
        [MarshalAs(UnmanagedType.U1)]
        public bool bDemolished;
        [MarshalAs(UnmanagedType.U1)]
        public bool bOnGround;
        [MarshalAs(UnmanagedType.U1)]
        public bool bSuperSonic;
        [MarshalAs(UnmanagedType.U1)]
        public bool bBot;
        [MarshalAs(UnmanagedType.U1)]
        public bool bJumped;
        [MarshalAs(UnmanagedType.U1)]
        public bool bDoubleJumped;
        public int PlayerID;
        public byte Team;
        public int Boost;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct BallInfo
    {
        public Vector3 Location;
        public Rotator Rotation;
        public Vector3 Velocity;
        public Vector3 AngularVelocity;
        public Vector3 Acceleration;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct BoostInfo
    {
        public Vector3 Location;
        [MarshalAs(UnmanagedType.U1)]
        public bool bActive;
        public int Timer;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct GameInfo
    {
        public float TimeSeconds;
        public float GameTimeRemaining;
        [MarshalAs(UnmanagedType.U1)]
        public bool bOverTime;
        [MarshalAs(UnmanagedType.U1)]
        public bool bUnlimitedTime;
        [MarshalAs(UnmanagedType.U1)]
        public bool bRoundActive;
        [MarshalAs(UnmanagedType.U1)]
        public bool bBallHasBeenHit;
        [MarshalAs(UnmanagedType.U1)]
        public bool bMatchEnded;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct GameTickPacket
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
        public PlayerInfo[] gameCars;
        public int numCars;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 50)]
        public BoostInfo[] gameBoosts;
        public int numBoosts;
        public BallInfo gameBall;
        public GameInfo gameInfo;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct SharedInputs
    {
        public GameTickPacket GameTickPacket;
    }
}
