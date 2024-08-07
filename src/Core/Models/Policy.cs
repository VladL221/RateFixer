﻿using Newtonsoft.Json;
using System;

namespace MyNewProject.src.Core.Models
{

    public class Policy
    {
        [JsonProperty("type")]
        public PolicyType Type { get; set; }

        #region General Policy Prop
        public string FullName { get; set; }
        public DateTime DateOfBirth { get; set; }
        #endregion

        #region Life Insurance
        public bool IsSmoker { get; set; }
        public decimal Amount { get; set; }
        #endregion

        #region Travel
        public string Country { get; set; }
        public int Days { get; set; }
        #endregion

        #region Health
        public string Gender { get; set; }
        public decimal Deductible { get; set; }
        #endregion

    }
}
