using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

/*
The purpose of this project is to create a color picker that will grab the color values from wherever
the user's mouse is hovering over. 
*/

namespace ColorPicker
{

    public partial class ColorPicker : Form
    {
        //Initialize
        public ColorPicker()
        {
            InitializeComponent();
            this.label5.AutoSize = true;        
        }       
        
        //Run code when mouse moves.
        protected override void OnMouseMove(MouseEventArgs e)
        {

            //Initialize list to hold RGB values
            List<int> ARGBValues = new List<int>();

            //Get positon of mouse
            int XPos = Cursor.Position.X;
            int YPos = Cursor.Position.Y;

            //Calls the picker class which will return the ARGB value of the area under wherever the mouse is sitting
            string cValue = picker.GetPixelColor(XPos, YPos).ToString();

            //Split into list and remove all empty values
            IList<string> values = cValue.Split(new String[] { ",", " ", "[", "]","=" }, StringSplitOptions.RemoveEmptyEntries);
            
            //Int used for loop
            int intVal = 0;

            //filter out all strings and pull just the four ARGB value codes
            foreach (string word in values)
            {
                if (int.TryParse(word, out intVal))
                {
                    ARGBValues.Add(intVal);
                }
            }
            
            //change background color to current RGB value
            this.BackColor = Color.FromArgb(ARGBValues[1], ARGBValues[2], ARGBValues[3]);

            //If the brightness of the background color is above 130, set text color to black
            if(Brightness(BackColor) > 130)
            {
                lblHEX.ForeColor = Color.Black;
                label2.ForeColor = Color.Black;
                label3.ForeColor = Color.Black;
                lblRGB.ForeColor = Color.Black;
                label5.ForeColor = Color.Black;
                label6.ForeColor = Color.Black;
                label7.ForeColor = Color.Black;
            }
            //If brightness is below 130 set text color to white
            else
            {
                lblHEX.ForeColor = Color.White;
                label2.ForeColor = Color.White;
                label3.ForeColor = Color.White;
                lblRGB.ForeColor = Color.White;
                label5.ForeColor = Color.White;
                label6.ForeColor = Color.White;
                label7.ForeColor = Color.White;
            }

            //Update label with Hex Value
            lblHEX.Text = RGB2Hex(ARGBValues[1], ARGBValues[2], ARGBValues[3]);
            //Update label with RGB values
            lblRGB.Text = ARGBValues[1].ToString() + ", " + ARGBValues[2].ToString() + ", " + ARGBValues[3].ToString();

            //Save Hex value to clipboard 
            Clipboard.SetText(RGB2Hex(ARGBValues[1], ARGBValues[2], ARGBValues[3]));

            base.OnMouseMove(e);
            
        }

        //Method to convert the RGB values into a HEX value
        public string RGB2Hex(int r, int g, int b) {

            string hex = string.Format("#{0}{1}{2}",
                ((int)(r)).ToString("X2"),
                ((int)(g)).ToString("X2"),
                ((int)(b)).ToString("X2")
                );
            return hex;
        }


        //Check the brightness of the color. 
        public int Brightness(Color c)
        {
            return (int)Math.Sqrt(
                c.R * c.R * .299 +
                c.G * c.G * .587 + 
                c.B * c.B * .114);
        }
    }
}





