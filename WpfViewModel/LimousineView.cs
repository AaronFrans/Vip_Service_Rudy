using System;
using System.Collections.Generic;
using System.Text;

namespace WpfViewModel
{
    /// <summary>
    /// Represents the limousine info for the ViewModel.
    /// </summary>
    public class LimousineView
    {
        /// <summary>
        /// Name of the limousine
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Price of the first hour.
        /// </summary>
        public int FirstHourPrice { get; set; }
        /// <summary>
        /// Default price of the Nightlife arangement.
        /// </summar>
        public int? Nightlife { get; set; }
        /// <summary>
        /// Default price of the Wedding arangement.
        /// </summar>
        public int? Wedding { get; set; }
        /// <summary>
        /// Default price of the Wellness arangement.
        /// </summar>
        public int? Wellness { get; set; }
    }
}
