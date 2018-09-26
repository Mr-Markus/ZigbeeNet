﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ZigbeeNet.TI.Models
{
    public class SubSystem
    {
        public string Name { get; set; }
        public List<Command> Commands { get; set; }

        public SubSystem(string name)
        {
            Name = name;
            Commands = new List<Command>();
        }
    }
}
