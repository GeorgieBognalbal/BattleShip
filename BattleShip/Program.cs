using BattleShip;
using System;
using System.Collections.Generic;
using System.IO;
using System.Media;
using System.Net;
using System.Runtime.CompilerServices;
using System.Threading;
using WindowsInput;
using WindowsInput.Native;

namespace BattleShip
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Fullscreen
            var inputSimulator = new InputSimulator();
            inputSimulator.Keyboard.KeyPress(VirtualKeyCode.F11);

            Console.CursorVisible = false;
            Thread.Sleep(500);

            var GameStart = new GameStart();

            // Play background music
            string musicFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets", "BgMusic.wav");
            SoundPlayer player = new SoundPlayer(musicFilePath);
            player.Load();
            player.PlayLooping();

            //Show main menu
            GameStart.Start();

            

        }
    }
}
