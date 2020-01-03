using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.IO;
using System.Security.Cryptography;
using System.Diagnostics;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            //random gen on open
            generate();
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //new random
            generate();
        }

        private void generate()
        {
            //Generate QR code

            string strrand = RandomString(16);
            string str = "https://chart.googleapis.com/chart?cht=qr&chs=370x370&chl=otpauth%3A%2F%2Ftotp%2FScreenConnect%3Fsecret%3D" + strrand + "&chld=H|0";
            var request = WebRequest.Create(str);

            using (var response = request.GetResponse())
            using (var stream = response.GetResponseStream())
            {
                pictureBox1.Image = Bitmap.FromStream(stream);
            }

            //if adaping for another purpose remove "goog:" and you will have raw key.
            textBox1.Text = "goog:" + strrand;

                }

        private static Random random = new Random();
        //Actually random string

        public static string RandomString(int length)
        {
            //char definitions
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijlmnopqurstuvwxyz";
            byte[] data = new byte[length];
            using (RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider())
            {
                crypto.GetBytes(data);
            }
            StringBuilder result = new StringBuilder(length);
            foreach (byte b in data)
            {
                result.Append(chars[b % (chars.Length)]);
            }
            return result.ToString();
            
            /*
             * Easy but not Cryptographically Random 
             * 
             * (return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
             
             */
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //<redacted>
        }

       
    }
}
