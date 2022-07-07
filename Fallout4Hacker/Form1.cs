using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Fallout4Hacker
{
   public partial class Form1 : Form
   {
      public Form1()
      {
         InitializeComponent();
         passwords.SelectionMode = SelectionMode.One;
         Application.DoEvents();

      }

      private void button1_Click(object sender, EventArgs e)
      {
         if (!string.IsNullOrWhiteSpace(password.Text))
         {
            passwords.Items.Add(new Password(password.Text));
         }

         password.Text = string.Empty;
      }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
           if (passwords.SelectedItem is Password pass && pass.Likeness != null)
           {
              likeness.Value = (decimal) pass.Likeness;
           }
           else
           {
              likeness.Value = 0;
           }
        }

        private void button2_Click(object sender, EventArgs e)
        {
           if (passwords.SelectedItem != null)
           {
              passwords.Items.Remove(passwords.SelectedItem);
           }
        }

        private void likeness_ValueChanged(object sender, EventArgs e)
        {
           if (!string.IsNullOrWhiteSpace(likeness.Text) && passwords.SelectedItem is Password pass)
           {
              pass.Likeness = (int)likeness.Value;
           }

           UpdateSolutions();
        }

        private void UpdateSolutions()
        {
           var possiblePasswords = passwords.Items.Cast<Password>().ToList();
           var excludedPasswords = new List<Password>();
           foreach (Password pass in possiblePasswords.Where(pass => pass.Likeness != null))
           {
              excludedPasswords.Add(pass);
              var max = pass.Likeness;
                 
              foreach (Password possiblePassword in possiblePasswords)
              {
                 if (possiblePassword == pass || excludedPasswords.Contains(possiblePassword))
                 {
                    continue;
                 }

                 var count = 0;
                 for (var i = 0; i < possiblePassword.ToString().Length; i++)
                 {
                    if (pass.ToString()[i] != possiblePassword.ToString()[i])
                    {
                       continue;
                    }

                    count++;
                    if (count > max)
                    {
                       i = pass.ToString().Length;
                    }
                 }

                 if (count != max)
                 {
                    excludedPasswords.Add(possiblePassword);
                 }
              }
           }

           IEnumerable<Password> newPasswords = possiblePasswords.Except(excludedPasswords);
           richTextBox1.Text = "";
           foreach (Password newPassword in newPasswords)
           {
              richTextBox1.Text += newPassword + ", ";
           }
        }

        private void password_KeyDown(object sender, KeyEventArgs e)
        {
           if (e.KeyCode == Keys.Enter)
           {
              button1_Click(this, new EventArgs());
           }
           
        }

        private void button3_Click(object sender, EventArgs e)
        {
           passwords.Items.Clear();
           password.Text = "";
           likeness.Value = 0;
        }
    }
}