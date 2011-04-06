// <copyright file="Section.cs" company="University of Louisville Speed School of Engineering">
// GNU General Public License v3
// </copyright>
// <summary>A section number.</summary>
namespace SEATLibrary
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// The section number.
    /// </summary>
    public class Section
    {
        /// <summary>
        /// The section number.
        /// </summary>
        private string number;

        /// <summary>
        /// Initializes a new instance of the <see cref="Section"/> class.
        /// </summary>
        /// <param name="number">The number.</param>
        internal Section(string number)
        {
            this.number = number;
        }

        /// <summary>
        /// Gets the number.
        /// </summary>
        public string Number
        {
            get { return this.number; }
        }

        /// <summary>
        /// Determines whether the specified <see cref="System.Object"/> is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="System.Object"/> to compare with this instance.</param>
        /// <returns>
        ///   <c>true</c> if the specified <see cref="System.Object"/> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        /// <exception cref="T:System.NullReferenceException">
        /// The <paramref name="obj"/> parameter is null.
        /// </exception>
        public override bool Equals(object obj)
        {
            if (obj is Section)
            {
                Section s = obj as Section;
                return this.number.Equals(s.number);
            }

            return false;
        }

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String"/> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return this.number;
        }
    }
}
