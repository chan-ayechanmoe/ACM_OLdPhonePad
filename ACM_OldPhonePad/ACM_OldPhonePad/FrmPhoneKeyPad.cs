using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ACM_OldPhonePad
{
    public partial class FrmPhoneKeyPad : Form
    {
        public FrmPhoneKeyPad()
        {
            InitializeComponent();
        }
        #region "Event"
        private void btnShowAns_Click(object sender, EventArgs e)
        {
            string inputKey = "";
            string result = "";
            if (!String.IsNullOrEmpty(txtPhKey.Text))
            {
                //Assumes “#” keyword will always be included at the end of every input.
                //if not include, add "#" at the end of input.
                inputKey = txtPhKey.Text.Contains('#') ? txtPhKey.Text.ToString().Trim() : txtPhKey.Text.ToString().Trim() + "#";
                result = OldPhonePad(inputKey);
                if (!String.IsNullOrEmpty(result))
                {
                    txtAnswer.Text = result;
                }
            }
            else
            {
                MessageBox.Show("Please enter phone's key", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
        }
        #endregion

        #region "Function"
        public static string OldPhonePad(string userInput)
        {
            bool isErr = false;
            string result = "";
            char[] letterList;
            char currentLetter = '\0';
            int letterCount = 0;

            //Dictionary is for phone's key and value pairs 
            //This is data mapping for phone's digit and letter 
            Dictionary<int, string> phkey_Dict = new Dictionary<int, string>();
            phkey_Dict.Add('2', "ABC");
            phkey_Dict.Add('3', "DEF");
            phkey_Dict.Add('4', "GHI");
            phkey_Dict.Add('5', "JKL");
            phkey_Dict.Add('6', "MNO");
            phkey_Dict.Add('7', "PQRS");
            phkey_Dict.Add('8', "TUV");
            phkey_Dict.Add('9', "WXYZ");

            foreach (char key in userInput)
            {
                if (phkey_Dict.ContainsKey(key))
                {
                    //Check user input key is same or different
                    //If input is the same, increase letter's count
                    if (currentLetter == key)
                    {
                        letterCount++;
                    }
                    else
                    {
                        //if input is different,  ouput the result
                        if (currentLetter != '\0' && letterCount > 0)
                        {
                            if (CheckKeyCount(currentLetter, letterCount))
                            {
                                letterList = phkey_Dict[currentLetter].ToCharArray();
                                result = result + letterList[letterCount - 1].ToString();
                            }
                            else
                            {
                                isErr = true;
                                MessageBox.Show("Invalid key. Please enter space for more than three same character.", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Information);  
                            }  
                        }
                        if (isErr) break;
                        else 
                        //update the current letter and set the count to 1 
                        currentLetter = key;
                        letterCount = 1;
                       

                    }
                }
                else if (key == '#')
                {
                    //If key is '#', output the result
                    if (currentLetter != '\0' && letterCount > 0)
                    {
                        if (CheckKeyCount(currentLetter, letterCount))
                        {
                            letterList = phkey_Dict[currentLetter].ToCharArray();
                            result = result + letterList[letterCount - 1].ToString();
                        }
                        else
                        {
                            isErr = true;
                            MessageBox.Show("Invalid key. Please enter space for more than three same character.", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Information);
                          
                        }
                    }
                    if (isErr) break;
                    else
                    // set default character and count to 0
                    currentLetter = '\0';
                    letterCount = 0;
                  
                }
                else if (key == '*')
                {
                    // '*' keyword is not to take the current letter and delete the previous letter
                    // clear current letter and current count
                    // reset default character and count to 0

                    currentLetter = '\0';
                    letterCount = 0;

                }
                else if (key == ' ')
                {
                    // ' ' keyword is not to take the current letter and ouput the result
                    if (currentLetter != '\0' && letterCount > 0)
                    {
                        if (CheckKeyCount(currentLetter, letterCount))
                        {
                            letterList = phkey_Dict[currentLetter].ToCharArray();
                            result = result + letterList[letterCount - 1].ToString();  
                        }
                        else
                        {
                            isErr = true;
                            MessageBox.Show("Invalid key. Please enter space for more than three same character.", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }

                    }
                    if (isErr) break;
                    else
                    // set default character and count to 0
                    currentLetter = '\0';
                    letterCount = 0;
                }
            }

            return result.ToString();

        }
        public static bool CheckKeyCount(char key, int count)
        {
            if (key == '7' || key == '9')
            {
                if (count > 4)
                {
                    return false;
                }
            }
            else
            {
                if(count>3)
                {
                    return false;
                }
            }
               
            return true;
        }
        #endregion
    }
}
