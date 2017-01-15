﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Uduino
{
    // We use a class Pin to optimize
    public class Pin
    {
        public UduinoManager manager = null;

        public string arduino = null;

        public PinMode pinMode;
        public PinMode prevPinMode;

        public int currentPin = -1;
        public int prevSendValue = 0;

        private string lastRead = null;

        public Pin(string arduinoParent, int pin, PinMode mode)
        {
            manager = UduinoManager.Instance;
            arduino = arduinoParent;
            currentPin = pin;
            pinMode = mode;
        }

        public void Init()
        {
            ChangePinMode(pinMode);
        }


        public virtual void WriteReadMessage(string message)
        {
            manager.Write(arduino, message);
            //TODO : ajouter ref a la carte arduino
        }

        public virtual void WriteMessage(string message)
        {
          manager.Write(arduino, message);
        }

        public bool PinTargetExists(string parentArduinoTarget, int currentPinTarget)
        {
            if ((arduino == null || arduino == "" || parentArduinoTarget == null || parentArduinoTarget == "" || parentArduinoTarget == arduino) && currentPinTarget == currentPin )
                return true;
            else
                return false;
        }

        /// <summary>
        /// Change Pin mode
        /// </summary>
        /// <param name="mode">Mode</param>
        public void ChangePinMode(PinMode mode)
        {
            pinMode = mode;
            WriteMessage("s " + currentPin + " " + (int)pinMode);
        }

        /// <summary>
        /// Send OptimizedValue
        /// </summary>
        /// <param name="sendValue">Value to send</param>
        public void SendRead()
        {
            manager.Read(arduino, "r " + currentPin);
        }

        /// <summary>
        /// Send OptimizedValue
        /// </summary>
        /// <param name="sendValue">Value to send</param>
        public void SendPinValue(int sendValue)
        {
            if(sendValue != prevSendValue)
            {
                WriteMessage("w " + currentPin + " " + sendValue);
                prevSendValue = sendValue;
            }
        }

        public void Destroy()
        {
            WriteMessage("w " + currentPin + " 0");
        }

        public virtual void Draw()
        {

        }

    }
}