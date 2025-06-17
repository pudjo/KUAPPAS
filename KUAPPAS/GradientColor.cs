// Filename: GradientColor.cs
// Desc: GradientColor is a struct which describes a basic linear gradient.
// 2006-01-20 nschan Initial revision.

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;

namespace MenuControls
{
    /// <summary>
    /// GradientColor is a struct containing the
    /// minimum fields necessary to draw a basic
    /// linear gradient.
    /// </summary>
    [TypeConverter(typeof(GradientColorConverter))]
    public struct GradientColor
    {
        #region Private fields
        private Color startColor;
        private Color endColor;
        private LinearGradientMode gradientMode;
        #endregion Private fields

        #region Properties
        /// <summary>
        /// Get or set the start color for the gradient.
        /// </summary>
        [Description("The start color for the gradient.")]
        public Color StartColor
        {
            get { return this.startColor; }
            set { this.startColor = value; }
        }

        /// <summary>
        /// Get or set the end color for the gradient.
        /// </summary>
        [Description("The end color for the gradient.")]
        public Color EndColor
        {
            get { return this.endColor; }
            set { this.endColor = value; }
        }

        /// <summary>
        /// Get or set the linear gradient mode.
        /// </summary>
        [Description("The linear gradient mode.")]
        public LinearGradientMode GradientMode
        {
            get { return this.gradientMode; }
            set { this.gradientMode = value; }
        }

        /// <summary>
        /// Determine if this is a solid color.
        /// </summary>
        [BrowsableAttribute(false)]
        public bool IsSolidColor
        {
            get { return this.startColor == this.endColor; }
        }

        /// <summary>
        /// Determine if this gradient color has default values
        /// for the 3 fields.
        /// </summary>
        [BrowsableAttribute(false)]
        public bool IsDefault
        {
            get
            {
                if ( this.StartColor.ToArgb() == 0 &&
                     this.EndColor.ToArgb() == 0 &&
                     (int)this.GradientMode == 0 )
                    return true;

                return false;
            }
        }

        /// <summary>
        /// Return the midpoint color between the
        /// start and end colors.
        /// </summary>
        [BrowsableAttribute(false)]
        public Color MiddleColor
        {
            get
            {
                int red   = (this.StartColor.R + this.EndColor.R) / 2;
                int green = (this.StartColor.G + this.EndColor.G) / 2;
                int blue  = (this.StartColor.B + this.EndColor.B) / 2;
                return Color.FromArgb(red, green, blue);
            }
        }
        #endregion Properties

        #region Constructors
        /// <summary>
        /// Constructor to initialize the start and
        /// end colors to be the same.
        /// </summary>
        /// <param name="solidColor">Solid color.</param>
        public GradientColor(Color solidColor)
        {
            this.startColor = solidColor;
            this.endColor = solidColor;
            this.gradientMode = LinearGradientMode.Horizontal;
        }

        /// <summary>
        /// Constructor to initialize ourselves given the
        /// start and end colors.
        /// </summary>
        /// <param name="startColor">Start gradient color.</param>
        /// <param name="endColor">End gradient color.</param>
        public GradientColor(Color startColor, Color endColor)
        {
            this.startColor = startColor;
            this.endColor = endColor;
            this.gradientMode = LinearGradientMode.Horizontal;
        }

        /// <summary>
        /// Constructor to initialize ourselves given the
        /// start and end colors, and the gradient mode.
        /// </summary>
        /// <param name="startColor">Start gradient color.</param>
        /// <param name="endColor">End gradient color.</param>
        /// <param name="gradientMode">Gradient mode.</param>
        public GradientColor(Color startColor, Color endColor, LinearGradientMode gradientMode)
        {
            this.startColor = startColor;
            this.endColor = endColor;
            this.gradientMode = gradientMode;
        }

        /// <summary>
        /// Constructor to initialize ourselves given an input
        /// string with format, "StartColor;EndColor;GradientMode",
        /// where all 3 values are integers.
        /// </summary>
        /// <param name="str">Input string with format as specified in summary.</param>
        public GradientColor(string str)
            : this()
        {
            GradientColor.ParseHelper(str, ref this.startColor, ref this.endColor, ref this.gradientMode);
        }
        #endregion Constructors

        #region Reverse colors
        /// <summary>
        /// Swap the start and end colors.
        /// </summary>
        public void Reverse()
        {
            Color tmpColor = this.startColor;
            this.startColor = this.endColor;
            this.endColor = tmpColor;
        }
        #endregion Reverse colors

        #region Equality
        /// <summary>
        /// Static helper function to compare two gradient color objects.
        /// </summary>
        /// <param name="left">Left operand.</param>
        /// <param name="right">Right operand.</param>
        /// <returns>true if the two gradient colors are equal, false otherwise.</returns>
        private static bool CompareMembers(GradientColor left, GradientColor right)
        {
            if ( left.GradientMode != right.GradientMode )
                return false;
            if ( left.StartColor != right.StartColor )
                return false;
            if ( left.EndColor != right.EndColor )
                return false;

            return true;
        }

        /// <summary>
        /// Provide equality operator.
        /// </summary>
        /// <param name="left">Left operand.</param>
        /// <param name="right">Right operand.</param>
        /// <returns>true if colors are equal, false otherwise.</returns>
        public static bool operator ==(GradientColor left, GradientColor right)
        {
            return GradientColor.CompareMembers(left, right);
        }

        /// <summary>
        /// Provide inequality operator.
        /// </summary>
        /// <param name="left">Left operand.</param>
        /// <param name="right">Right operand.</param>
        /// <returns>true if colors are different, false otherwise.</returns>
        public static bool operator !=(GradientColor left, GradientColor right)
        {
            return !GradientColor.CompareMembers(left, right);
        }

        /// <summary>
        /// Override instance equals to provide more efficient
        /// comparison.
        /// </summary>
        /// <param name="obj">Right operand.</param>
        /// <returns>true if colors are equal, false otherwise.</returns>
        public override bool Equals(object obj)
        {
            if ( obj == null )
                return false;

            if ( this.GetType() != obj.GetType() )
                return false;

            return GradientColor.CompareMembers(this, (GradientColor)obj);
        }

        /// <summary>
        /// Override of GetHashCode() required since we override the
        /// instance Equals() method.
        /// </summary>
        /// <returns>Hash code.</returns>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        #endregion Equality

        #region ToString override
        /// <summary>
        /// We output two lines. The first line is just calling
        /// ToString() on each of our fields, separated by semicolons.
        /// The second line is for type conversion, where the values
        /// of the three fields are printed as integers separated
        /// by semicolons. The 2-line output looks something like this:
        ///
        /// Color [A=255, R=10, G=20, B=30];Color [A=255, R=10, G=20, B=40];Vertical
        /// -16116706;-16116696;1
        /// </summary>
        /// <returns>Formatted string representing the gradient color.</returns>
        public override string ToString()
        {
            const char sep = ';';

            StringBuilder sb = new StringBuilder();
            sb.Append(this.startColor.ToString());
            sb.Append(sep);
            sb.Append(this.endColor.ToString());
            sb.Append(sep);
            sb.Append(this.gradientMode.ToString());

            sb.Append('\n');

            sb.Append(this.startColor.ToArgb());
            sb.Append(sep);
            sb.Append(this.endColor.ToArgb());
            sb.Append(sep);
            sb.Append((int)this.gradientMode);

            return sb.ToString();
        }
        #endregion ToString override

        #region Parsing support
        /// <summary>
        /// Parse an input string with format,
        /// "StartColor;EndColor;GradientMode",
        /// and use the integer values to construct
        /// a new GradientColor object.
        /// </summary>
        /// <param name="str">Input string to parse.</param>
        /// <returns>New GradientColor object.</returns>
        public static GradientColor Parse(string input)
        {
            GradientColor color = new GradientColor(input);
            return color;
        }

        /// <summary>
        /// Helper function to parse an input string into
        /// the 3 gradient color fields. The input string must
        /// have format, "StartColor;EndColor;GradientMode",
        /// where all 3 values are integers.
        /// </summary>
        /// <param name="input">Input string to parse.</param>
        /// <param name="startColor">The resulting start color value.</param>
        /// <param name="endColor">The resulting end color value.</param>
        /// <param name="gradientMode">The resulting gradient mode.</param>
        private static void ParseHelper(string input, ref Color startColor, ref Color endColor, ref LinearGradientMode gradientMode)
        {
            // Check for null input.
            if ( String.IsNullOrEmpty(input) )
                throw new FormatException("GradientColor_InvalidFormat");

            // Split input string into 3 tokens.
            string[] tokens = input.Split(';');
            if ( tokens.Length != 3 )
                throw new FormatException("GradientColor_InvalidFormat");

            // Read the 3 values.
            Color c1 = new Color();
            Color c2 = new Color();
            LinearGradientMode mode = new LinearGradientMode();
            try
            {
                c1 = Color.FromArgb(Int32.Parse(tokens[0]));
                c2 = Color.FromArgb(Int32.Parse(tokens[1]));
                mode = (LinearGradientMode) Int32.Parse(tokens[2]);
            }
            catch ( Exception e )
            {
                throw new FormatException("GradientColor_InvalidFormat", e);
            }

            // Assign the 3 values.
            startColor = c1;
            endColor = c2;
            gradientMode = mode;
        }
        #endregion Parsing support
    }

    /// <summary>
    /// Type converter class for GradientColor.
    /// </summary>
    public class GradientColorConverter : ExpandableObjectConverter
    {
        #region CanConvertFrom
        /// <summary>
        /// Allow conversion from string type.
        /// </summary>
        /// <param name="context">Type descriptor context.</param>
        /// <param name="sourceType">Source type to convert from.</param>
        /// <returns>true if we can convert from given source type, false otherwise.</returns>
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            if ( sourceType == typeof(String) )
                return true;

            return base.CanConvertFrom(context, sourceType);
        }
        #endregion CanConvertFrom

        #region ConvertFrom
        /// <summary>
        /// Convert from a string to a GradientColor instance.
        /// </summary>
        /// <param name="context">Type descriptor context.</param>
        /// <param name="culture">Culture.</param>
        /// <param name="value">String value to convert from.</param>
        /// <returns>GradientColor object.</returns>
        public override object ConvertFrom(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value)
        {
            if ( value is String )
            {
                try
                {
                    GradientColor color = new GradientColor((string)value);
                    return color;
                }
                catch ( Exception e )
                {
                    throw new ArgumentException("GradientColorConverter_ConvertFromError", e);
                }
            }

            return base.ConvertFrom(context, culture, value);
        }
        #endregion ConvertFrom

        #region CanConvertTo
        /// <summary>
        /// Allow conversion to InstanceDescriptor.
        /// </summary>
        /// <param name="context">Type descriptor context.</param>
        /// <param name="destinationType">Destintation type to convert to.</param>
        /// <returns>true if conversion allowed, false otherwise.</returns>
        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            if ( destinationType == typeof(InstanceDescriptor) )
                return true;

            return base.CanConvertTo(context, destinationType);
        }
        #endregion CanConvertTo

        #region ConvertTo
        /// <summary>
        /// Convert a GradientColor object to a string or InstanceDescriptor.
        /// </summary>
        /// <param name="context">Type descriptor context.</param>
        /// <param name="culture">Culture.</param>
        /// <param name="value">GradientColor object.</param>
        /// <param name="destinationType">Destination type to convert to.</param>
        /// <returns>String representation of GradientColor object.</returns>
        public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType)
        {
            if ( value is GradientColor )
            {
                GradientColor color = (GradientColor)value;
                if ( destinationType == typeof(InstanceDescriptor) )
                {
                    Type[] types = new Type[3];
                    object[] properties = new object[3];

                    // Start color.
                    types[0] = typeof(Color);
                    properties[0] = color.StartColor;

                    // End color.
                    types[1] = typeof(Color);
                    properties[1] = color.EndColor;

                    // Gradient mode.
                    types[2] = typeof(LinearGradientMode);
                    properties[2] = color.GradientMode;

                    // Build constructor.
                    ConstructorInfo ci = typeof(GradientColor).GetConstructor(types);
                    return new InstanceDescriptor(ci, properties);
                }
                else if ( destinationType == typeof(String) )
                {
                    string[] tokens = color.ToString().Split('\n');
                    Debug.Assert(tokens.Length == 2);
                    return tokens[1];
                }
            }

            return base.ConvertTo(context, culture, value, destinationType);
        }
        #endregion ConvertTo
    }
}

// END

